
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
                     services.AddArgumentsParserService();
                     //services.AddSingleton<IUserArgumentService>(serviceProvider =>
                     //{
                     //    CommandLine.Parser parser = serviceProvider.GetRequiredService<IArgumentsParserService>().Parser;
                     //    parser.ParseArguments<UserArgumentService>(args);

                     //    return new UserArgumentService(serviceProvider);
                     //});
                 })
                
                 .Build();

            UserArgumentService userArgumentService = new();
            //host.UseAdamDefaultArguments(args);
            host.UseAdamDefaultArguments(userArgumentService, args);
            host.RunAsync();

            //var userArguments = host.Services.GetService<IUserArgumentService>();
            //var logger = host.Services.GetService<ILogger<Program>>();

            //logger.LogInformation("User param is {test} ", userArguments.Test2);
        }
    }
}
