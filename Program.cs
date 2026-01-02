using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace TM_MULTIHEAD_PHISHING_DETECTOR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Force content root to project folder and web root to wwwroot
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName,
                WebRootPath = "wwwroot"
            });

            // Add MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Serve static files
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
