using GridWallManagement.App.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GridWallManagement.App.Database.DBContexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        { }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        { }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserLicense> UserLicenses { get; set; }
        public DbSet<UserLicenseRenewal> UserLicenseRenewals { get; set; }
    }
}
