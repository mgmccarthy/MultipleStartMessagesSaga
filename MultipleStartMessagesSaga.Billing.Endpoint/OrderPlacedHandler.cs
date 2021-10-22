using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace MultipleStartMessagesSaga.Billing.Endpoint
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderPlaced, OrderId = {message.OrderId} - Charging credit card...");

            var orderBilled = new OrderBilled { OrderId = message.OrderId };
            return context.Publish(orderBilled);
        }
    }
}