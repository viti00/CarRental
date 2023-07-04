using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Transmissions", Schema = "19118076")]
    public class Transmission
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TransmissionConstants.TransmissionTypeMaxLength,MinimumLength =TransmissionConstants.TransmissionTypeMinLength)]
        public string Type { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
