using CarRental.Data;
using CarRental.Data.Models;

namespace CarRental.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(CarRentalDbContext context)
        {
            SeedCategory(context);
            SeedEngines(context);
            SeedTransmissions(context);
            SeedMakes(context);
            SeedModels(context);
        }

        private static void SeedCategory(CarRentalDbContext context)
        {
            if(!context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category {Name = "SUV"},
                    new Category {Name = "Хечбег"},
                    new Category {Name = "Кросовер"},
                    new Category {Name = "Седан"},
                    new Category {Name = "Спортни"},
                    new Category {Name = "Купе"},
                    new Category {Name = "Миниван"}
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void SeedEngines(CarRentalDbContext context)
        {
            if (!context.Engines.Any())
            {
                var engines = new List<Engine>()
                {
                    new Engine{Type = "Бензин"},
                    new Engine{Type = "Дизел"},
                    new Engine{Type = "Метан"},
                    new Engine{Type = "Бензин-Газ"},
                    new Engine{Type = "Хибрид"},
                    new Engine{Type = "Електрически"}
                };
                context.Engines.AddRange(engines);
                context.SaveChanges();
            }
        }

        private static void SeedTransmissions(CarRentalDbContext context)
        {
            if (!context.Transmissions.Any())
            {
                var transmissions = new List<Transmission>()
                {
                    new Transmission{Type = "Автоматична"},
                    new Transmission{Type = "Ръчна"}
                };

                context.Transmissions.AddRange(transmissions);
                context.SaveChanges();
            }
        }

        private static void SeedMakes(CarRentalDbContext context)
        {
            if (!context.Makes.Any())
            {
                var makes = new List<Make>()
                {
                    new Make{Name = "Audi"},
                    new Make{Name = "BMW"},
                    new Make{Name = "Mercedes"},
                    new Make{Name = "Volkswagen"},
                    new Make{Name = "Peugeot"},
                    new Make{Name = "Citroen"},
                    new Make{Name = "Skoda"},
                    new Make{Name = "Toyota"},
                    new Make{Name = "Ford"},
                    new Make{Name = "Nissan"}
                };

                context.Makes.AddRange(makes);
                context.SaveChanges();
            }
        }

        private static void SeedModels(CarRentalDbContext context)
        {
            var audiId = context.Makes.First(x => x.Name == "Audi").Id;
            var bmwId = context.Makes.First(x => x.Name == "BMW").Id;
            var mercedesId = context.Makes.First(x => x.Name == "Mercedes").Id;
            var volkswagenId = context.Makes.First(x => x.Name == "Volkswagen").Id;
            var peugeotId = context.Makes.First(x => x.Name == "Peugeot").Id;
            var citroenId = context.Makes.First(x => x.Name == "Citroen").Id;
            var skodaId = context.Makes.First(x => x.Name == "Skoda").Id;
            var toyotaId = context.Makes.First(x => x.Name == "Toyota").Id;
            var fordId = context.Makes.First(x => x.Name == "Ford").Id;
            var nissanId = context.Makes.First(x => x.Name == "Nissan").Id;

            var models = new List<Model>()
            {
                new Model{Name="A3", MakeId = audiId},
                new Model{Name="A5", MakeId = audiId},
                new Model{Name="S5", MakeId = audiId},
                new Model{Name="S8", MakeId = audiId},
                new Model{Name="RS6", MakeId = audiId},
                new Model{Name="RS7", MakeId = audiId},
                new Model{Name="SQ8", MakeId = audiId},
                new Model{Name="A4 ALLROAD", MakeId = audiId},
                new Model{Name="A6 ALLROAD", MakeId = audiId},
                new Model{Name="E-TRON", MakeId = audiId},
                new Model{Name="335I", MakeId = bmwId},
                new Model{Name="530d", MakeId = bmwId},
                new Model{Name="M3", MakeId = bmwId},
                new Model{Name="M5", MakeId = bmwId},
                new Model{Name="730d", MakeId = bmwId},
                new Model{Name="750i", MakeId = bmwId},
                new Model{Name="X5", MakeId = bmwId},
                new Model{Name="X6", MakeId = bmwId},
                new Model{Name="X7", MakeId = bmwId},
                new Model{Name="i8", MakeId = bmwId},
                new Model{Name="C220", MakeId = mercedesId},
                new Model{Name="C320", MakeId = mercedesId},
                new Model{Name="E350", MakeId = mercedesId},
                new Model{Name="E500", MakeId = mercedesId},
                new Model{Name="S500", MakeId = mercedesId},
                new Model{Name="S63", MakeId = mercedesId},
                new Model{Name="ML500", MakeId = mercedesId},
                new Model{Name="GLS63", MakeId = mercedesId},
                new Model{Name="G500", MakeId = mercedesId},
                new Model{Name="SL65AMG", MakeId = mercedesId},
                new Model{Name="Golf ", MakeId = volkswagenId},
                new Model{Name="Arteon", MakeId = volkswagenId},
                new Model{Name="Passat", MakeId = volkswagenId},
                new Model{Name="Touran", MakeId = volkswagenId},
                new Model{Name="Bora", MakeId = volkswagenId},
                new Model{Name="Phaeton", MakeId = volkswagenId},
                new Model{Name="Scirrocco", MakeId = volkswagenId},
                new Model{Name="Touareg", MakeId = volkswagenId},
                new Model{Name="Polo", MakeId = volkswagenId},
                new Model{Name="Jetta", MakeId = volkswagenId},
                new Model{Name="306", MakeId = peugeotId},
                new Model{Name="4007", MakeId = peugeotId},
                new Model{Name="406", MakeId = peugeotId},
                new Model{Name="508", MakeId = peugeotId},
                new Model{Name="607", MakeId = peugeotId},
                new Model{Name="Partner", MakeId = peugeotId},
                new Model{Name="3008", MakeId = peugeotId},
                new Model{Name="402", MakeId = peugeotId},
                new Model{Name="807", MakeId = peugeotId},
                new Model{Name="206", MakeId = peugeotId},
                new Model{Name="AX", MakeId = citroenId},
                new Model{Name="C-Elysee", MakeId = citroenId},
                new Model{Name="C3", MakeId = citroenId},
                new Model{Name="C4 Pikasso", MakeId = citroenId},
                new Model{Name="C5X", MakeId = citroenId},
                new Model{Name="Jumpy", MakeId = citroenId},
                new Model{Name="Saxo", MakeId = citroenId},
                new Model{Name="DS5", MakeId = citroenId},
                new Model{Name="DS7", MakeId = citroenId},
                new Model{Name="C4 Cactus", MakeId = citroenId},
                new Model{Name="Fabia", MakeId = skodaId},
                new Model{Name="Kamiq", MakeId = skodaId},
                new Model{Name="Kodiaq", MakeId = skodaId},
                new Model{Name="Octavia", MakeId = skodaId},
                new Model{Name="Superb", MakeId = skodaId},
                new Model{Name="Favorit", MakeId = skodaId},
                new Model{Name="Formann", MakeId = skodaId},
                new Model{Name="Roomster", MakeId = skodaId},
                new Model{Name="Karoq", MakeId = skodaId},
                new Model{Name="Citigo", MakeId = skodaId},
                new Model{Name="Auris", MakeId = toyotaId},
                new Model{Name="Avensis", MakeId = toyotaId},
                new Model{Name="Corolla", MakeId = toyotaId},
                new Model{Name="Hilux", MakeId = toyotaId},
                new Model{Name="Land Cruiser", MakeId = toyotaId},
                new Model{Name="Prius", MakeId = toyotaId},
                new Model{Name="Rav4", MakeId = toyotaId},
                new Model{Name="Supra", MakeId = toyotaId},
                new Model{Name="Tundra", MakeId = toyotaId},
                new Model{Name="Yaris", MakeId = toyotaId},
                new Model{Name="Bronco", MakeId = fordId},
                new Model{Name="Escort", MakeId = fordId},
                new Model{Name="Raptor F350", MakeId = fordId},
                new Model{Name="Fiesta", MakeId = fordId},
                new Model{Name="Focus", MakeId = fordId},
                new Model{Name="Galaxy", MakeId = fordId},
                new Model{Name="Ka", MakeId = fordId},
                new Model{Name="Mondeo", MakeId = fordId},
                new Model{Name="Mustang", MakeId = fordId},
                new Model{Name="Sierra", MakeId = fordId},
                new Model{Name="350z", MakeId = nissanId},
                new Model{Name="Almera", MakeId = nissanId},
                new Model{Name="GT-R", MakeId = nissanId},
                new Model{Name="Juke", MakeId = nissanId},
                new Model{Name="Micra", MakeId = nissanId},
                new Model{Name="Pathfinder", MakeId = nissanId},
                new Model{Name="Patrol", MakeId = nissanId},
                new Model{Name="Quashkai", MakeId = nissanId},
                new Model{Name="Silvia", MakeId = nissanId},
                new Model{Name="Skyline", MakeId = nissanId},
            };

            context.Models.AddRange(models);
            context.SaveChanges();
        }
    }
}
