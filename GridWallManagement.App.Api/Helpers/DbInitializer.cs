using GridWallManagement.App.Database.DBContexts;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Common;
using BC = BCrypt.Net.BCrypt;

namespace GridWallManagement.App.Api.Helpers
{
    public class DbInitializer
    {
        public static void InitializeDatabase(DataBaseContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            context.Database.EnsureCreated();
            SeedDataForRoles(context);
        }

        private static async Task SeedDataForRoles(DataBaseContext _context)
        {
            var masterUserId = Guid.NewGuid().ToString();

            if (!_context.Roles.Any(m => m.Name == UserRoles.SUPER_ADMIN))
                _context.Roles.Add(new Roles { Name = UserRoles.SUPER_ADMIN, Code = "SUPER_ADMIN", DisplayName = "Super Administrator", CreatedBy = masterUserId, CreatedDate = DateTime.UtcNow });
            if (!_context.Roles.Any(m => m.Name == UserRoles.ADMIN))
                _context.Roles.Add(new Roles { Name = UserRoles.ADMIN, Code = "ADMIN", DisplayName = "Administrator", CreatedBy = masterUserId, CreatedDate = DateTime.UtcNow });
            if (!_context.Roles.Any(m => m.Name == UserRoles.USER))
                _context.Roles.Add(new Roles { Name = UserRoles.USER, Code = "USER", DisplayName = "Standard User", CreatedBy = masterUserId, CreatedDate = DateTime.UtcNow });

            _context.SaveChanges();

            if (!_context.Users.Any(m => m.Username == "SuperAdmin"))
                _context.Users.Add(new Users
                {
                    Id = masterUserId,
                    Username = UserRoles.SUPER_ADMIN,
                    Email = "superadmin@gridwallmanagement.com",
                    MobileNumber = "+919999999999",
                    PasswordHash = BC.HashPassword("Adm!n123"),
                    RoleId = _context.Roles.FirstOrDefault(x => x.Name == UserRoles.SUPER_ADMIN).Id,
                    IsActive = true,
                    CreatedBy = masterUserId,
                    CreatedDate = DateTime.UtcNow
                });

            _context.SaveChanges();
        }
    }
}
