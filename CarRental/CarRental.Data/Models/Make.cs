using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Makes", Schema = "19118076")]
    public class Make
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MakeConstants.MakeNameMaxLegnth, MinimumLength = MakeConstants.MakeNameMinLegnth)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; } =  new HashSet<Model>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public DateTime LastModified_19118076 { get; set; }
    }
}
