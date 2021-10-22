using NServiceBus;

namespace MultipleStartMessagesSaga.Shared.Events
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}
