using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    public class CarPhoto
    {
        public int Id { get; set; }

        public byte[] Bytes { get; set; }

        public string Description { get; set; }

        public string FileExtension { get; set; }

        public double Size { get; set; }

        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
