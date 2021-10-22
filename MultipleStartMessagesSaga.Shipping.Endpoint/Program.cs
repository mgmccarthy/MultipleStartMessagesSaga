using System;
using System.Threading.Tasks;
using MultipleStartMessagesSaga.Shared.Commands;
using NServiceBus;

namespace MultipleStartMessagesSaga.Shipping.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "MultipleStartMessagesSaga.Shipping.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("MultipleStartMessagesSaga.Shipping.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
