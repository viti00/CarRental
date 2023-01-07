using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Infrastructure;
using CarRental.Services.CarService;
using CarRental.Services.DealerService;
using CarRental.Services.TenantsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarRentalDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IDealerService, DealerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
   
    app.UseHsts();
}

app.PrepareDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "Areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    endpoint.MapDefaultControllerRoute();
});
app.MapRazorPages();

app.Run();
