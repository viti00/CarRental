using CarRental.Data.Models;
using static CarRental.ViewModels.ModelConstants.ModelConstants;

namespace CarRental.ViewModels
{
    public class AllQueryCarsModel
    {
        public IList<Car> Cars { get; set; }

        public int CarsPerPage { get; set; } = carsPerPage;

        public int CurrentPage { get; set; } = initialCurrPage;

        public int MaxPage { get; init; }

        public SearchTerm SearchTerm { get; set; }
    }
}
