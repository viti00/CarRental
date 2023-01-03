using Microsoft.AspNetCore.Identity;

namespace CarRental.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Car>? Cars { get; set; }

        public List<ReservedCar>? ReservedCars { get; set; }

        public List<DrivingLicensePhoto>? DrivingLicensePhotos { get; set; }

        public bool CanRent { get; set; } = false;
    }
}
