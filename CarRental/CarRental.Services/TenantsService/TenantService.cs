using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services.TenantsService
{
    public class TenantService : ITenantService
    {
        private readonly CarRentalDbContext context;

        public TenantService(CarRentalDbContext context)
            => this.context = context;

        public bool Approve(int id)
        {
            var rentalRequest = context
                                .RentalApproveRequests
                                .Include(x=> x.Photos)
                                .FirstOrDefault(x=> x.Id == id);
            var user = context.Users.FirstOrDefault(x => x.Id == rentalRequest.UserId);

            if(user == null)
            {
                return false;
            }
            if(rentalRequest == null)
            {
                return false;
            }

            try
            {
                user.FirstName = rentalRequest.FirstName;
                user.LastName = rentalRequest.LastName;
                user.PhoneNumber = rentalRequest.PhoneNumber;
                user.CanRent = true;
                user.DrivingLicensePhotos.AddRange(rentalRequest.Photos);

                context.DrivingLicensePhotos.RemoveRange(rentalRequest.Photos);
                context.RentalApproveRequests.Remove(rentalRequest);

                context.Users.Update(user);
                context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            

            return true;
        }

        public List<DrivingLicensePhoto> CreatePhotos(IFormFileCollection files, string userId)
        {
            List<DrivingLicensePhoto> photos = new List<DrivingLicensePhoto>();
            Task.Run(async () =>
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            if (memoryStream.Length < 2097152)
                            {
                                var newphoto = new DrivingLicensePhoto()
                                {
                                    DriverId = userId,
                                    Bytes = memoryStream.ToArray(),
                                    Description = file.FileName,
                                    FileExtension = Path.GetExtension(file.FileName),
                                    Size = file.Length,
                                };
                                photos.Add(newphoto);
                            }
                        }
                    }
                }
            }).GetAwaiter()
               .GetResult();

            return photos;
        }

        public bool Reject(int id)
        {
            var rentalRequest = context
                                 .RentalApproveRequests
                                 .Include(x => x.Photos)
                                 .FirstOrDefault(x => x.Id == id);

            if(rentalRequest == null)
            {
                return false;
            }

            try
            {
                context.DrivingLicensePhotos.RemoveRange(rentalRequest.Photos);
                context.RentalApproveRequests.Remove(rentalRequest);

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
