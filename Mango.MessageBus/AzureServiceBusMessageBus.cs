using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
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
        private string connectionString = "Endpoint=sb://restmango.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=RcDwdGrd8N+ecA6QO2mOJFWkFtxEhwYPSMDK4U8MyT8=";
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
           ISenderClient client = new TopicClient(connectionString, topicName);
            var jsonMessage = JsonConvert.SerializeObject(message);
            var finalMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await client.SendAsync(finalMessage);
            await client.CloseAsync();
        }
    }
}
