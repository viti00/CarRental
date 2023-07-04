using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("RentalApproveRequests", Schema = "19118076")]
    public class RentalApproveRequest
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string DrivingLicenseNumber { get; set; }

        public virtual ICollection<DrivingLicensePhoto> Photos { get; set; } = new HashSet<DrivingLicensePhoto>();

        [NotMapped]
        public List<string>? PhotosCollection { get; set; } = new List<string>();

        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }

        public DateTime LastModified_19118076 { get; set; }

        public string Status { get; set; } = "Обработва се";
    }
}
