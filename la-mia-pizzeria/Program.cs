using la_mia_pizzeria.CustomLoggers;
using la_mia_pizzeria.Database;
using la_mia_pizzeria.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("PizzeriaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaContextConnection' not found.");

            builder.Services.AddDbContext<PizzeriaContext>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PizzeriaContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICustomLogger, CustomFileLogger>();
            builder.Services.AddScoped<PizzeriaContext, PizzeriaContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Pizza";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // MANUAL add authentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{slug?}");

            // MANUAL insert
            app.MapRazorPages();

            app.Run();
        }
    }
}