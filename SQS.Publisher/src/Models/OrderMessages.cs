using System;
using Event = SQS.Publisher.Constants.EventTypes;
namespace SQS.Publisher.Messaging
{

    //SQS messages published to the Queue
	public record OrderCreated(Guid Id = default, string EventType = Event.OrderCreated, string publishedDate = default!);
	public record OrderUpdated(Guid Id = default, string EventType = Event.OrderUpdated, string publishedDate = default!);
	public record OrderDeleted(Guid Id = default, string EventType = Event.OrderDelete, string publishedDate = default!);
}

