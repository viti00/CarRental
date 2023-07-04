using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Engines", Schema = "19118076")]
    public class Engine
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EngineConstants.EngineTypeMaxLength, MinimumLength = EngineConstants.EngineTypeMinLength)]
        public string Type { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
