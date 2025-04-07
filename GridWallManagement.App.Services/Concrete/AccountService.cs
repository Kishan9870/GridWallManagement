using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Common.Configuration;
using GridWallManagement.App.Models.Request.Account;
using GridWallManagement.App.Models.Response.Account;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace GridWallManagement.App.Services.Concrete
{
    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IGenericRepository<Users> _usersRepository;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _config;
        AppSettingsConfiguration AppSettingsConfiguration = new AppSettingsConfiguration();

        public AccountService(IGenericRepository<Users> usersRepository,
                              IGenericRepository<RefreshToken> refreshTokenRepository,
                              IConfiguration config,
                              IMapper mapper, 
                              IUserContext userContext) : base(mapper,userContext)
        {
            _usersRepository = usersRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _config = config;
            AppSettingsConfiguration.Secret = _config.GetSection("AppSettings:Secret").Value;
            AppSettingsConfiguration.RefreshTokenTTL = Convert.ToInt32(_config.GetSection("AppSettings:RefreshTokenTTL").Value);
        }

        public async Task<ResponseBase<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest,
                                                                           string ipAddress,
                                                                           CancellationToken cancellationToken = default)
        {
            try
            {
                var result = new AuthenticateResponse();
                var user = await _usersRepository.GetQueryable()
                                                 .Where(x => x.Email == authenticateRequest.Email && x.IsActive)
                                                 .Include(x => x.Roles)
                                                 .FirstOrDefaultAsync();

                if (user == null || !BC.Verify(authenticateRequest.Password, user.PasswordHash))
                    throw new InvalidOperationException("Email Id / Password is wrong.");
                else
                {
                    // authentication successful so generate jwt and refresh tokens
                    var jwtToken = GenerateJwtToken(user);
                    var refreshToken = GenerateRefreshToken(ipAddress, user.Id);

                    // remove old refresh tokens from account
                    await RemoveOldRefreshTokens(user);

                    // save changes to db
                    await _refreshTokenRepository.AddAsync(refreshToken);

                    //get users role
                    var role = _mapper.Map<RolesResponse>(user.Roles);

                    await _usersRepository.UpdateAsync(user);

                    var authenticateResponse = _mapper.Map<AuthenticateResponse>(user);
                    authenticateResponse.JwtToken = jwtToken;
                    authenticateResponse.RefreshToken = refreshToken.Token;
                    authenticateResponse.Roles = role;

                    result = authenticateResponse;
                }

                return new ResponseBase<AuthenticateResponse>(result);
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<AuthenticateResponse>(null) { ResponseStatusCode = System.Net.HttpStatusCode.Unauthorized };
                result.AddExceptionLog(ex);
                return result;
            }
        }


        #region Private Functions
        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettingsConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()),
                                                     new Claim("Role",  user.Roles.Name) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task RemoveOldRefreshTokens(Users user)
        {
            var tokens = await _refreshTokenRepository.GetQueryable().Where(x => x.UserId == user.Id).ToListAsync();

            tokens.RemoveAll(x => x.Expires <= DateTime.UtcNow || x.Revoked != null);

            for (int i = 0; i < tokens.Count; i++)
                await _refreshTokenRepository.DeleteAsync(tokens[i]);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, string userId)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(AppSettingsConfiguration.RefreshTokenTTL),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                UserId = userId,
                CreatedBy = _userContext.UserId,
                CreatedDate = DateTime.UtcNow
            };
        }

        private async Task<(RefreshToken, Users)> GetRefreshToken(string token, CancellationToken cancellationToken = default)
        {
            RefreshToken refreshToken = null;

            var user = await _usersRepository.GetQueryable()
                                             .Where(x => x.IsActive
                                                      && x.RefreshTokens.Any(t => t.Token == token && t.IsActive))
                                             .Include(x => x.RefreshTokens)
                                             .Include(x => x.Roles)
                                             .FirstOrDefaultAsync(cancellationToken);

            if (user != null)
                refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            return (refreshToken, user);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private static string GeneratePassword()
        {
            int length = 8;
            bool nonAlphanumeric = true;
            bool digit = true;
            bool lowercase = true;
            bool uppercase = true;
            int uniqueChars = 1;

            string[] randomChars = new[] {
                                           "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                                           "abcdefghijkmnopqrstuvwxyz",    // lowercase
                                           "0123456789",                   // digits
                                           "!@$?_"                        // non-alphanumeric
                                           };

            Random rand = new Random(Environment.TickCount);
            List<char> password = new List<char>();

            if (uppercase)
                password.Insert(rand.Next(0, password.Count), randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (lowercase)
                password.Insert(rand.Next(0, password.Count), randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (digit)
                password.Insert(rand.Next(0, password.Count), randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (nonAlphanumeric)
                password.Insert(rand.Next(0, password.Count), randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = password.Count; i < length || password.Distinct().Count() < uniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                password.Insert(rand.Next(0, password.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(password.ToArray());
        }

        #endregion
    }
}
