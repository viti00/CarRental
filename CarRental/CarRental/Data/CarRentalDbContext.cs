using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class CarRentalDbContext : IdentityDbContext
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
    }
}