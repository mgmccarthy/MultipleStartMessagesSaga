using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Commands;
using MultipleStartMessagesSaga.Shared.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace MultipleStartMessagesSaga.Shipping.Endpoint
{
    public class ShippingSaga : Saga<ShippingSaga.SagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<ShippingSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<OrderBilled>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info("OrderPlaced message received.");
            Data.IsOrderPlaced = true;
            return ProcessOrder(context);
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info("OrderBilled message received.");
            Data.IsOrderBilled = true;
            return ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsOrderPlaced && Data.IsOrderBilled)
            {
                await context.SendLocal(new ShipOrder { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }

        public class SagaData : ContainSagaData
        {
            public string OrderId { get; set; }
            public bool IsOrderPlaced { get; set; }
            public bool IsOrderBilled { get; set; }
        }
    }
}
