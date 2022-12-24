using CarRental.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(
                this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);


            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarRentalDbContext>();
            DbInitializer.Initialize(data);
            data.Database.Migrate();
        }
    }
}
