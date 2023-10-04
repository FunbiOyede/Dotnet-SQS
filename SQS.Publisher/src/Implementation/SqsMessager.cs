using System;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using SQS.Publisher.Messaging;
using SQS.Publisher.Services;

namespace SQS.Publisher.Implementation
{
	public class SqsMessager : ISqsMessage
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly IOptions<QueueSettings> _queueSettings;
        private readonly ILogger<SqsMessager> _logger;
        private string _queueUrlCache = string.Empty;



        public SqsMessager(IAmazonSQS sqsClient, IOptions<QueueSettings> queueSettings, ILogger<SqsMessager> logger)
		{
            _sqsClient = sqsClient;
            _queueSettings = queueSettings;
            _logger = logger;
		}

        public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
        {
            var queueUrl = await GetSQSQueueUrl();

            //send message request to sqs
            _logger.LogInformation("Generating SQS message request");

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "TypeOfMessage", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = typeof(T).Name
                        }
                    }
                },
                MessageGroupId = "OrderMessagesGroup",
                

            };

            _logger.LogInformation("Send SQS message request");

            return await _sqsClient.SendMessageAsync(sendMessageRequest);

        }



        //Get SQS url

        private async Task<string> GetSQSQueueUrl()
        {
            if(string.IsNullOrEmpty(_queueUrlCache))
            {
                var queueUrl = await _sqsClient.GetQueueUrlAsync(_queueSettings.Value.Name);
                _queueUrlCache = queueUrl.QueueUrl;
                return queueUrl.QueueUrl;
            }

            return _queueUrlCache;
        }
    }
}

