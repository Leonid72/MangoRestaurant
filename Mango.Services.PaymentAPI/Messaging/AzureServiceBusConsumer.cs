using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Mango.MessageBus;
using Mango.Services.PaymentAPI.Messages;
using PaymentProcessor;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionPayment;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderupdatepaymentresulttopic;
        private readonly IProcessPayment _processPayment;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        private ServiceBusProcessor _orderPaymentprocessor;
        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IProcessPayment processPayment)
        {
            _configuration = configuration;
            _processPayment = processPayment;
            _messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionPayment = _configuration.GetValue<string>("OrderPaymentProcessSubscription");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderupdatepaymentresulttopic = _configuration.GetValue <string>("OrderUpdatePaymentResultTopic");

            _messageBus = messageBus;
            var client = new ServiceBusClient(serviceBusConnectionString);
            _orderPaymentprocessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);
        }

        public async Task Start()
        {
            _orderPaymentprocessor.ProcessMessageAsync += ProcessPayments;
            _orderPaymentprocessor.ProcessErrorAsync += ErrorHandler;
            await _orderPaymentprocessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _orderPaymentprocessor.StopProcessingAsync();
            await _orderPaymentprocessor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage requestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body.ToString());

            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResult = new()
            {
                 Status = result,
                 OrderId = requestMessage.OrderId
            };

            
            try
            {
                await _messageBus.PublishMessage(updatePaymentResult, orderupdatepaymentresulttopic);
                args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
