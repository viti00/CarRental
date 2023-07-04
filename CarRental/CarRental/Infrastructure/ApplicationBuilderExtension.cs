using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CarRental.Global.WebConstants;

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
            SeedDealerRoleAndDealer(services);
            SeedTenant(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarRentalDbContext>();
            DbInitializer.Initialize(data);
            
        }

        private static void SeedDealerRoleAndDealer(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(DealerRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = DealerRoleName };

                    await roleManager.CreateAsync(role);

                    if (userManager.Users.FirstOrDefault(x => x.UserName == "dealer") == null)
                    {
                        const string Email = "dealer@dealer.bg";
                        const string Username = "dealer";
                        const string Password = "dealer123456";

                        var user = new ApplicationUser
                        {
                            Email = Email,
                            UserName = Username,
                        };

                        await userManager.CreateAsync(user, Password);

                        await userManager.AddToRoleAsync(user, role.Name);
                    }
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
                    IdentityRole role;
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        role = roleManager.Roles.FirstOrDefault(x => x.Name == AdministratorRoleName);
                    }
                    else
                    {
                        role = new IdentityRole { Name = AdministratorRoleName };

                        await roleManager.CreateAsync(role);
                    }
                    
                    if(userManager.Users.FirstOrDefault(x=> x.UserName == "admin") == null)
                    {
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
                    }
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedTenant(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            Task
                .Run(async () =>
                {
                    
                    if (userManager.Users.FirstOrDefault(x => x.UserName == "tenant") == null)
                    {
                        const string Email = "tenant@tenant.bg";
                        const string Username = "tenant";
                        const string Password = "tenant123456";

                        var user = new ApplicationUser
                        {
                            Email = Email,
                            UserName = Username,
                            CanRent = true
                        };

                        await userManager.CreateAsync(user, Password);

                    }
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
