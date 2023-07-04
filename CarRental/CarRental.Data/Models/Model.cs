using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Models", Schema = "19118076")]
    public class Model
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.ModelNameMaxLegnth, MinimumLength = ModelConstants.ModelNameMinLegnth)]
        public string Name { get; set; }

        [ForeignKey(nameof(Make))]
        public int MakeId { get; set; }

        public virtual Make Make { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
