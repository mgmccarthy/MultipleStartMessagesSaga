using NServiceBus;

namespace MultipleStartMessagesSaga.Shared.Commands
{
    public class ShipOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
