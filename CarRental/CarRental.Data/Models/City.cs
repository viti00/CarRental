using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("Cities", Schema = "19118076")]
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
