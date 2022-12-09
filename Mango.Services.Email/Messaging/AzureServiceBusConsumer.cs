using Azure.Messaging.ServiceBus;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Repository;
using Newtonsoft.Json;
using System.Text;


namespace Mango.Services.Email.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionEmail;

        private readonly string orderUpdatePaymentResultTopic;

        private readonly EmailRepository _emailRepo;
        private readonly IConfiguration _configuration;

        private ServiceBusProcessor _orderUpdatePaymentProcessor;

        public AzureServiceBusConsumer(EmailRepository emailRepository,
                      IConfiguration configuration)
        {
            _emailRepo = emailRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionEmail = _configuration.GetValue<string>("SubscriptionName");

            orderUpdatePaymentResultTopic = _configuration.GetValue<string>("OrderUpdatePaymentResulttopic");
    
            var client = new ServiceBusClient(serviceBusConnectionString);
 
            _orderUpdatePaymentProcessor = client.CreateProcessor(orderUpdatePaymentResultTopic, subscriptionEmail);
        }

        public async Task Start()
        {
            _orderUpdatePaymentProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            _orderUpdatePaymentProcessor.ProcessErrorAsync += ErrorHandler;
            await _orderUpdatePaymentProcessor.StartProcessingAsync();
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage objMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            try
            {
                await _emailRepo.SendAndLogEmail(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task Stop()
        {


            await _orderUpdatePaymentProcessor.StopProcessingAsync();
            await _orderUpdatePaymentProcessor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        
    }
}
