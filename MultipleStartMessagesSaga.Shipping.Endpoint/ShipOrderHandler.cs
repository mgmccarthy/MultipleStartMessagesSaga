using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace MultipleStartMessagesSaga.Shipping.Endpoint
{
    public class ShipOrderHandler : IHandleMessages<ShipOrder>
    {
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();

        public Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Successfully shipped.");
            return Task.CompletedTask;
        }
    }
}
