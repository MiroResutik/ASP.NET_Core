using DevSpot.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevSpot.Constants;
using DevSpot.Models;
using DevSpot.Repositories;
namespace DevSpot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
            
            // dont need a confirmed account to log in (options.SignIn.RequireConfirmedAccount = false)
            // To create users
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IRepository<JobPosting>, JobPostingRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Seeding to database on startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Call role seeder class
                RoleSeeder.SeedRolesAsync(services).Wait();

                // Call user seeder class
                UserSeeder.SeedUsersAsync(services).Wait();

            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            // Enable razor page mapping
            // Adding endpoints for razor pages
            app.MapRazorPages();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
