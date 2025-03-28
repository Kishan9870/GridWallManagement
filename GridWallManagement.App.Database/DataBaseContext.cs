using GridWallManagement.App.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GridWall.App.Database.DBContexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        { }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        { }
        public DbSet<ApplicationUserLog> ApplicationUserLogs { get; set; }
    }
}
