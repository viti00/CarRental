using Microsoft.AspNetCore.Identity;

namespace CarRental.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<Car>? Cars { get; set; }

        public List<ReservedCar>? ReservedCars { get; set; }

        public List<DrivingLicensePhoto>? DrivingLicensePhotos { get; set; }

        public bool CanRent { get; set; } = false;

        public DateTime LastModified_19118076 { get; set; }
    }
}
