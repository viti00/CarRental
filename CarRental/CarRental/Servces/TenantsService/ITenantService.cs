using CarRental.Data.Models;

namespace CarRental.Servces.TenantsService
{
    public interface ITenantService
    {
        public List<DrivingLicensePhoto> CreatePhotos(IFormFileCollection files, string userId);

        public bool Approve(int id);

        public bool Reject(int id);

    }
}
