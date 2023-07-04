using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Cars", Schema = "19118076")]
    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(Make))]
        public int MakeId { get; set; }

        public virtual Make? Make { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [Range(CarConstants.HorsePowerMinRange, CarConstants.HorsePowerMaxRange)]
        public double HorsePower { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [Range
            (CarConstants.FuelConsumptionMinRange,
            CarConstants.FuelConsumptionMaxRange,
            ErrorMessage ="Консумацията на гориво трябва да бъде в интервала от 0 до 60 литра на 100 километра!")]
        public double FuelConsumption { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [Range
            (CarConstants.PricePerDayMinRange,
            CarConstants.PricePerDayMaxRange,
            ErrorMessage ="Цената за ден трябва да бъде в интервала 1 лев и 500 лева!")]
        public double PricePerDay { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City? City { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public int Year { get; set; }

        public string? Description { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }

        public virtual Engine? Engine { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }

        public virtual Transmission? Transmission { get; set; }

        public virtual ICollection<CarPhoto> Photos { get; set; } = new HashSet<CarPhoto>();

        public virtual ICollection<ReservedCar> CarRents { get; set; } = new HashSet<ReservedCar>();

        [ForeignKey(nameof(Creator))]
        public string? CreatorId { get; set; }

        public virtual ApplicationUser? Creator { get; set; }

        [NotMapped]
        public List<string>? PhotosCollection { get; set; } = new List<string>();

        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
