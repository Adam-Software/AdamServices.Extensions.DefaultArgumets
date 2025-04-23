
using DefaultArguments.Extensions;
using DefaultArguments.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DefaultArguments.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                 {
                     UserArgumentService userArgumentService = new();
                     services.AddArgumentsParserService(userArgumentService, args);   
                 })
                
                 .Build();

            host.RunAsync();

            var userArguments = host.Services.GetService<UserArgumentService>();
            var logger = host.Services.GetService<ILogger<Program>>();

            logger.LogInformation("User param is {test} ", userArguments.Test2);
        }
    }
}
