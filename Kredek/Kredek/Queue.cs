using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Kredek
{
    public class Queue
    {
        private readonly ILogger<Queue> _logger;

        public Queue(ILogger<Queue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Queue))]
        public async Task Run([QueueTrigger("queue", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            string queueName = "queue";
            string storageCS = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            byte[] bytes1 = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] bytes2 = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

            // Instantiate a QueueClient to create and interact with the queue
            QueueClient queueClient = new QueueClient(storageCS, queueName);

            queueClient.SendMessage(Convert.ToBase64String(bytes1));
            queueClient.SendMessage(Convert.ToBase64String(bytes2));
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
