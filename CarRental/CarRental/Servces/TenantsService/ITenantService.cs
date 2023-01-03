using CarRental.Data.Models;

namespace CarRental.Servces.TenantsService
{
    public interface ITenantService
    {
        public List<DrivingLicensePhoto> CreatePhotos(IFormFileCollection files);
    }
}
