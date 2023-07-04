using CarRental.Data.Models;

namespace CarRental.Services.DealerService
{
    public interface IDealerService
    {
        public Task<bool> Approve(int id);

        public bool Reject(int id);
    }
}
