using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Make))]
        public int MakeId { get; set; }

        public virtual Make Make { get; set; }
    }
}
