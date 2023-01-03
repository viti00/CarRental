using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    public class RentalApproveRequest
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<DrivingLicensePhoto> Photos { get; set; } = new HashSet<DrivingLicensePhoto>();

        [FromForm]
        [NotMapped]
        public IFormFileCollection Files { get; set; }
    }
}
