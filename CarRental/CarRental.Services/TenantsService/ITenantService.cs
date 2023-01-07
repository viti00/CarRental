using CarRental.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CarRental.Services.TenantsService
{
    public interface ITenantService
    {
        public List<DrivingLicensePhoto> CreatePhotos(IFormFileCollection files, string userId);

        public bool Approve(int id);

        public bool Reject(int id);

    }
}
