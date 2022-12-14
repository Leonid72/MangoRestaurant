
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private string serviceBusConnectionString = "Endpoint=sb://restmango.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=RcDwdGrd8N+ecA6QO2mOJFWkFtxEhwYPSMDK4U8MyT8=";
        //private readonly string serviceBusConnectionString;
        //private readonly IConfiguration _configuration;
        //public AzureServiceBusMessageBus(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    serviceBusConnectionString = _configuration.GetSection("ServiceBusConnectionString").Value;

        //}
         //.GetSection("AzureServiceBus").GetSection("ConnectionStrings").Value;

        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(serviceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
