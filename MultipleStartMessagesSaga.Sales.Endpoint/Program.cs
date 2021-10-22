using System;
using System.Threading.Tasks;
using NServiceBus;

namespace MultipleStartMessagesSaga.Sales.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "MultipleStartMessagesSaga.Sales.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("MultipleStartMessagesSaga.Sales.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
