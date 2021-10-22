using NServiceBus;

namespace MultipleStartMessagesSaga.Shared.Commands
{
    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
