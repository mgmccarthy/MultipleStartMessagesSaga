using System;
using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace MultipleStartMessagesSaga.Client.ConsoleApp
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            Console.Title = "MultipleStartMessagesSaga.Client.ConsoleApp";

            var endpointConfiguration = new EndpointConfiguration("MultipleStartMessagesSaga.Client.ConsoleApp");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(messageType: typeof(PlaceOrder), destination: "MultipleStartMessagesSaga.Sales.Endpoint");

            endpointConfiguration.SendOnly();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await RunLoop(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        var command = new PlaceOrder { OrderId = Guid.NewGuid().ToString() };
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command).ConfigureAwait(false);
                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
