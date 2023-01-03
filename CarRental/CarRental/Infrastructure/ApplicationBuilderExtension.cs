using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CarRental.WebConstants;

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
            SeedAdministrator(services);
            CreateDealerRole(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarRentalDbContext>();
            DbInitializer.Initialize(data);
            data.Database.Migrate();
        }

        public static void CreateDealerRole(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(DealerRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = DealerRoleName };

                    await roleManager.CreateAsync(role);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@admin.com";
                    const string adminUsername = "admin";
                    const string adminPassword = "123admin123";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminUsername,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
