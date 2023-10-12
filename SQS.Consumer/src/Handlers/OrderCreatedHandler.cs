using System;
using MediatR;
using SQS.Consumer.Messages;

namespace SQS.Consumer.Handlers
{

    public class OrderCreatedHandler : IRequestHandler<OrderCreated>
    {
        private readonly ILogger<OrderCreatedHandler> _logger;

        public OrderCreatedHandler(ILogger<OrderCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreated request, CancellationToken cancellationToken)
        {
            //Send Sns topic that order was created to the order processing team? Just thinking
            //_logger.LogInformation($"Order with Id-{request.Id} processed at {request.publishedDate} is created.🔥");
            throw new Exception("Unable to process queue messages 👿");
           // return Unit.Task;
        }
    }
}

