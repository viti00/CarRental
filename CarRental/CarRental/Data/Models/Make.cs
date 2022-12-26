using System.ComponentModel.DataAnnotations;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    public class Make
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MakeConstants.MakeNameMaxLegnth, MinimumLength = MakeConstants.MakeNameMinLegnth)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; } =  new HashSet<Model>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
