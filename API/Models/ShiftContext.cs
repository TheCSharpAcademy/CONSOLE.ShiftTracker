using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings.Get("connectionString"));
        }

        public DbSet<Shift> ShiftsList { get; set; } = null!;
    }
}
