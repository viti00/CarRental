using CarRental.Data.Models;

namespace CarRental.Servces.TenantsService
{
    public class TenantService : ITenantService
    {
        public List<DrivingLicensePhoto> CreatePhotos(IFormFileCollection files)
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
    }
}
