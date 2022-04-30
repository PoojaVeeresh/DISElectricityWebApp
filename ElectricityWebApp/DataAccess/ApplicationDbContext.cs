using Microsoft.EntityFrameworkCore;
using ElectricityWebApp.Models;
namespace ElectricityWebApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }

        public DbSet<Series> series { get; set; }
        public DbSet<CountryData> countryData { get; set; }

        public DbSet<ElectricityDetails> electricityDetails { get; set; }

    }
}
