using Event = SQS.Consumer.EventTypes;

namespace SQS.Consumer.Messages
{
    public record OrderCreated(Guid Id = default, string EventType = Event.OrderCreated, string publishedDate = default!) : IOrderMessage;
    public record OrderUpdated(Guid Id = default, string EventType = Event.OrderUpdated, string publishedDate = default!) : IOrderMessage;
    public record OrderDeleted(Guid Id = default, string EventType = Event.OrderDelete, string publishedDate = default!) : IOrderMessage;
}

