using System.ComponentModel.DataAnnotations;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    public class Engine
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EngineConstants.EngineTypeMaxLength, MinimumLength = EngineConstants.EngineTypeMinLength)]
        public string Type { get; set; }
    }
}
