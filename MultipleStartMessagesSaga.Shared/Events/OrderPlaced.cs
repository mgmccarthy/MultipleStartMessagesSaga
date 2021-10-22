using NServiceBus;

namespace MultipleStartMessagesSaga.Shared.Events
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
    }
}
