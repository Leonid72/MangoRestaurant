using Mango.Web.Services;
using Mango.Web.Services.IServices;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddHttpClient<IBaseService, BaseService>("MangoAPI");
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IWebApiCaller, WebApiCaller>();

            SD.ProuctAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];

            //builder.Services.AddAuthentication(option =>
            //{
            //    option.DefaultScheme = "Cookies";
            //    option.DefaultChallengeScheme = "oidc";

            //})
            //.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            //.AddOpenIdConnect("oidc",);
            
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

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}