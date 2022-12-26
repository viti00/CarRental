namespace CarRental.Data
{
    public static class ModelsConstnants
    {
        public static class MakeConstants
        {
            public const int MakeNameMinLegnth = 2;
            public const int MakeNameMaxLegnth = 15;
        }
        public static class ModelConstants
        {
            public const int ModelNameMinLegnth = 2;
            public const int ModelNameMaxLegnth = 20;
        }
        public static class EngineConstants
        {
            public const int EngineTypeMinLength = 2;
            public const int EngineTypeMaxLength = 20;
        }
        public static class TransmissionConstants
        {
            public const int TransmissionTypeMinLength = 2;
            public const int TransmissionTypeMaxLength = 20;
        }
        public static class CategoryConstants
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 20;
        }
        public static class CarConstants
        {
            public const double HorsePowerMinRange = 60;
            public const double HorsePowerMaxRange = 1000;

            public const double FuelConsumptionMinRange = 0;
            public const double FuelConsumptionMaxRange = 60;

            public const double PricePerDayMinRange = 1;
            public const double PricePerDayMaxRange = 500;

            public const int CityMinLength = 2;
            public const int CityMaxLength = 20;
        }

    }
}
