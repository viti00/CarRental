using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("DealerRequests", Schema = "19118076")]
    public class DealerRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public DateTime LastModified_19118076 { get; set; }

        public string Status { get; set; } = "Обработва се";
    }
}
