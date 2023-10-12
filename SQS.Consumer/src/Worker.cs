using System.Text.Json;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Microsoft.Extensions.Options;
using SQS.Consumer.Messages;

namespace SQS.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IAmazonSQS _sqsClient;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly IMediator _mediator;
    private string _queueUrlCache = string.Empty;

    public Worker(ILogger<Worker> logger, IAmazonSQS sqsClient, IOptions<QueueSettings> queueSettings, IMediator mediator)
    {
        _logger = logger;
        _sqsClient = sqsClient;
        _queueSettings = queueSettings;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var url = await GetSQSQueueUrl();

        var messgageRequest = GetMessageRequest(url);


        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("fetching messages from sqs...");
            var response = await _sqsClient.ReceiveMessageAsync(messgageRequest, stoppingToken);
         
            foreach (var message in response.Messages)
            {
                var messageHandler = GetMessageHandlers(message);

                if (messageHandler is not null)
                {
                    var messageContent = (IOrderMessage)JsonSerializer.Deserialize(message.Body, messageHandler)!;
                    try
                    {
                        //send message to the appropriate handler
                        _logger.LogInformation("Send message to handler");
                        await _mediator.Send(messageContent, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "message failed during processing");
                        continue;
                    }

                }

                _logger.LogInformation("Deleting SQS messageing...");
                //delete SQS Message
                await _sqsClient.DeleteMessageAsync(url, message.ReceiptHandle, stoppingToken);
            }


            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
        }
    }




    // fetch SQS url
    private async Task<string> GetSQSQueueUrl()
    {
        if (string.IsNullOrEmpty(_queueUrlCache))
        {
            var queueUrl = await _sqsClient.GetQueueUrlAsync(_queueSettings.Value.Name);
            _queueUrlCache = queueUrl.QueueUrl;
            return queueUrl.QueueUrl;
        }

        return _queueUrlCache;
    }


    //get message request
    private ReceiveMessageRequest GetMessageRequest(string url)
    {
        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = url,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string> { "All" },
            MaxNumberOfMessages = 1

        };

        return receiveMessageRequest;
    }

    //get message handler
    private Type GetMessageHandlers(Message sqsMessage)
    {
        var typeOfMessage = sqsMessage.MessageAttributes["TypeOfMessage"].StringValue;

        var handler = Type.GetType($"SQS.Consumer.Messages.{typeOfMessage}");

        return handler!;
    }
}
