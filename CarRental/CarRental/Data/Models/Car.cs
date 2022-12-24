using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(Make))]
        public int MakeId { get; set; }

        public virtual Make Make { get; set; }

        public string Model { get; set; }

        public double HorsePower { get; set; }

        public double FuelConsumption { get; set; }

        public double PricePerDay { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }
    }
}
