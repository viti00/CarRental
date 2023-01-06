using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class SearchTerm
    {
        public int? SearchByMake { get; set;}

        public int? SearchByModel { get; set; }

        public int? SearchByCity { get; set; }

        public int? SearchByEngine { get; set; }

        public int? SearchByCategory { get; set; }

        public int? SearchByTransmission { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateToTake { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateToReturn { get; set; }

        public double? PricePerDayMin { get; set; }

        public double? PricePerDayMax { get; set; }

        public double? HorsePowerMin { get; set; }

        public double? HorsePowerMax { get; set; }

        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }
    }
}
