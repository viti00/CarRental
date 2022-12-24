using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    public class ReservedCar
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
