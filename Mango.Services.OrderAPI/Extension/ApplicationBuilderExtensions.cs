using Mango.Services.OrderAPI.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Mango.Services.OrderAPI.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static IAzureServiceBusConsumer? ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife?.ApplicationStarted.Register(OnStart);
            hostApplicationLife?.ApplicationStopped.Register(OnStop);
            return app;         
        }
        private static void OnStart()
        {
            try
            {
                ServiceBusConsumer?.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }

        private static void OnStop()
        {
            ServiceBusConsumer?.Stop();
        }
    }
}
