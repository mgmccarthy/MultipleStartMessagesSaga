using System;
using System.Threading.Tasks;
using NServiceBus;

namespace MultipleStartMessagesSaga.Billing.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "MultipleStartMessagesSaga.Billing.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("MultipleStartMessagesSaga.Billing.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
