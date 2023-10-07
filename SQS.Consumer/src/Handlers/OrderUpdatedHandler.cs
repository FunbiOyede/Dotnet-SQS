using System;
using MediatR;
using SQS.Consumer.Messages;

namespace SQS.Consumer.Handlers
{
	public class OrderUpdatedHandler : IRequestHandler<OrderUpdated>
	{

        private readonly ILogger<OrderUpdatedHandler> _logger;

        public OrderUpdatedHandler(ILogger<OrderUpdatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderUpdated request, CancellationToken cancellationToken)
        {
            //Send Sns topic that order was created to the order processing team? Just thinking
            _logger.LogInformation($"Order with Id-{request.Id} processed at {request.publishedDate} is updated...😊");
            return Unit.Task;
        }
    }
}

