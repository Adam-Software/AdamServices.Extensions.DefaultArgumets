using DefaultArguments.Extensions;
using DefaultArguments.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DefaultArguments.TestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                 {
                     //added default parser without service
                     services.AddAdamDefaultArgumentsParser(args);

                     //added T as transient service
                     //services.AddAdamArgumentsParserTransient<ArgumentService>(args);   
                 })
                 .Build();

            await host.ParseAndRunAsync();

            //ExampleReadArguments(host);
        }

        private static void ExampleReadArguments(IHost host)
        {
            var userArguments = host.Services.GetService<ArgumentService>();
            var logger = host.Services.GetService<ILogger<Program>>();

            logger.LogInformation("User argument test2 is {result} ", userArguments.Test2);
            logger.LogInformation("User argument test3 is {result} ", userArguments.Test3);
        }
    }
}
