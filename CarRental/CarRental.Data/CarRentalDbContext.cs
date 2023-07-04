using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class CarRentalDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ReservedCar> ReservedCars { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CarPhoto> Photos { get; set; }
        public DbSet<DrivingLicensePhoto> DrivingLicensePhotos { get; set; }
        public DbSet<DealerRequest> DealerRequests { get; set; }
        public DbSet<RentalApproveRequest> RentalApproveRequests { get; set; }
        public DbSet<log_19118076> log_19118076 { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>().ToTable("Carss", "19118076");
            builder.Entity<CarPhoto>().ToTable("CarPhotos", "19118076");
            builder.Entity<Category>().ToTable("Category", "19118076");
            builder.Entity<City>().ToTable("City", "19118076");
            builder.Entity<DealerRequest>().ToTable("DealerRequests", "19118076");
            builder.Entity<DrivingLicensePhoto>().ToTable("DrivingLicensePhotos", "19118076");
            builder.Entity<Engine>().ToTable("Engines", "19118076");
            builder.Entity<log_19118076>().ToTable("log_19118976", "19118076");
            builder.Entity<Make>().ToTable("Make", "19118076");
            builder.Entity<Model>().ToTable("Model", "19118076");
            builder.Entity<RentalApproveRequest>().ToTable("RentalApproveRequests", "19118076");
            builder.Entity<ReservedCar>().ToTable("ReservedCar", "19118076");
            builder.Entity<Transmission>().ToTable("Transsmisions", "19118076");

            base.OnModelCreating(builder);
        }
    }

    
}