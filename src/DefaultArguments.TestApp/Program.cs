
using Microsoft.Extensions.Hosting;
using DefaultArguments.Extensions;
using Microsoft.Extensions.DependencyInjection;
using DefaultArguments.TestApp.Services;
using Microsoft.Extensions.Logging;
using DefaultArguments.Service;

namespace DefaultArguments.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                 {
                     //services.AddArgumentsParserService(args);

                     UserArgumentService userArgumentService = new();
                     services.AddArgumentsParserService(userArgumentService, args);
                     //services.AddSingleton<IUserArgumentService>(userArgumentService);
                
                 })
                
                 .Build();

            //UserArgumentService userArgumentService = new();
            //host.UseAdamDefaultArguments(args);
            //host.UseAdamDefaultArguments(typeof(UserArgumentService), args);
            host.RunAsync();

            var userArguments = host.Services.GetService<UserArgumentService>();
            var logger = host.Services.GetService<ILogger<Program>>();

            logger.LogInformation("User param is {test} ", userArguments.Test2);
        }
    }
}
