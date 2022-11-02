using Mango.MessageBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Mango.Services.ShoppingCartAPI.RabbitMQSender
{
    public class RabbitMQCartMessageSender : IRabbitMQCartMessageSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private  IConnection _connection;

        public RabbitMQCartMessageSender()
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
        }
        public void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                 HostName = _hostname,
                 UserName = _username,
                 Password =_password
            };
            _connection = factory.CreateConnection();

            using var chanel = _connection.CreateModel();
            chanel.QueueDeclare(queue : queueName,false,false,false,arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            chanel.BasicPublish(exchange: "", routingKey: queueName,basicProperties : null,body: body);
        }
    }
}
