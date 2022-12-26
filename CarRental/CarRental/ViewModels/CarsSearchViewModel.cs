using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class CarsSearchViewModel
    {
        public string? SearchByMake { get; set;}

        public string? SearchByModel { get; set; }

        public string? SearchByCity { get; set; }

        public string? SearchByEngine { get; set; }

        public string? SearchByCategory { get; set; }

        public string? SearchByTransmission { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateToTake { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateToReturn { get; set; }

        public decimal? PricePerDayMin { get; set; }

        public decimal? PricePerDayMax { get; set; }

        public decimal? HorsePowerMin { get; set; }

        public decimal? HorsePowerMax { get; set; }

        public decimal? FuelConsumptionMin { get; set; }

        public decimal? FuelConsumptionMax { get; set; }
    }
}
