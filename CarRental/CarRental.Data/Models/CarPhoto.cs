using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("CarPhotos", Schema = "19118076")]
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

        public DateTime LastModified_19118076 { get; set; }
    }
}
