using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
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
                user.LastModified_19118076 = DateTime.Now;

                var log_user = new log_19118076
                {
                    Table = "Users",
                    Action = "Update",
                    ActionDate = DateTime.Now
                };
                context.log_19118076.Add(log_user);

                dealerRequest.Status = "Одобрена";

                var log_requests = new log_19118076
                {
                    Table = "DealerRequests",
                    Action = "Update",
                    ActionDate = DateTime.Now
                };
                context.log_19118076.Add(log_requests);

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
                var log_requests = new log_19118076
                {
                    Table = "DealerRequests",
                    Action = "Update",
                    ActionDate = DateTime.Now
                };
                context.log_19118076.Add(log_requests);

                dealerRequest.Status = "Отказана";


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
