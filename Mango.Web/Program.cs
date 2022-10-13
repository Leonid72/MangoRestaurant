using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

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

            builder.Services.AddHttpClient<ICartService, CartService>();
            builder.Services.AddScoped<ICartService,CartService>();

            builder.Services.AddHttpClient<ICouponService, CouponService>();
            builder.Services.AddScoped<ICouponService,CouponService>();

            builder.Services.AddScoped<IWebApiCaller, WebApiCaller>();

            SD.ProuctAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
            SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShopingCartAPI"];
            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultScheme = "Cookies";
                option.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            .AddOpenIdConnect("oidc",options =>
            {
                options.Authority = builder.Configuration["ServiceUrls:IdentityApi"];
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = "mango";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.Scope.Add("mango");
                options.SaveTokens = true;

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}