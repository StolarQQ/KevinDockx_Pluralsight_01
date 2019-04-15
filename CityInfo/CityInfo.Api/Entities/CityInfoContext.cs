using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Entities
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
            //atabase.Migrate();
        }

        // We used base ctor, and configuring connection string is startup.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("CityDb");

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var cities = modelBuilder.Entity<City>();
            //cities.HasKey(x => x.Id);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}
