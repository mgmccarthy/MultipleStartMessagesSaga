using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Commands;
using MultipleStartMessagesSaga.Shared.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace MultipleStartMessagesSaga.Sales.Endpoint
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            var orderPlaced = new OrderPlaced { OrderId = message.OrderId };
            return context.Publish(orderPlaced);
        }
    }
}
