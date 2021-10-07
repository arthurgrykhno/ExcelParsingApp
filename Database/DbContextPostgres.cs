using ExcelParsingApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelParsingApp.Database
{
    public class DbContextPostgres : DbContext
    {
        public DbContextPostgres()
        {
            Database.EnsureCreated();
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagValues> TagValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Excel;Username=postgres;Password=secret;TrustServerCertificate=False;");
        }
    }
}
