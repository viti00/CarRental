using System.ComponentModel.DataAnnotations;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    public class Transmission
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TransmissionConstants.TransmissionTypeMaxLength,MinimumLength =TransmissionConstants.TransmissionTypeMinLength)]
        public string Type { get; set; }
    }
}
