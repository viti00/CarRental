using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using static CarRental.Global.WebConstants;

namespace CarRental.Services.DealerService
{
    public class DealerService : IDealerService
    {
        private readonly CarRentalDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public DealerService(CarRentalDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<bool> Approve(int id)
        {
            var dealerRequest = context
                                .DealerRequests
                                .FirstOrDefault(x => x.Id == id);

            var user = context.Users.FirstOrDefault(x => x.Id == dealerRequest.UserId);

            if(dealerRequest == null)
            {
                return false;
            }
            if(user == null)
            {
                return false;
            }

            try
            {
                user.FirstName = dealerRequest.FirstName;
                user.LastName = dealerRequest.LastName;
                user.PhoneNumber = dealerRequest.PhoneNumber;

                context.DealerRequests.Remove(dealerRequest);

                await context.SaveChangesAsync();

                await userManager.AddToRoleAsync(user, DealerRoleName);
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool Reject(int id)
        {
            var dealerRequest = context
                                .DealerRequests
                                .FirstOrDefault(x => x.Id == id);

            if (dealerRequest == null)
            {
                return false;
            }

            try
            {
                context.DealerRequests.Remove(dealerRequest);

                context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
    }
}
