using System;
using MediatR;
using SQS.Consumer.Messages;

namespace SQS.Consumer.Handlers
{
    public class OrderDeletedHandler : IRequestHandler<OrderDeleted>
    {
        private readonly ILogger<OrderDeletedHandler> _logger;

        public OrderDeletedHandler(ILogger<OrderDeletedHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(OrderDeleted request, CancellationToken cancellationToken)
        {
            //Send Sns topic that order was created to the order processing team? Just thinking
            _logger.LogInformation($"Order with Id-{request.Id} processed at {request.publishedDate} is deleted.💀");

            return Unit.Task;
        }
    }
}
