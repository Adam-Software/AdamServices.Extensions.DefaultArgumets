using CommandLine;
using DefaultArguments.Service;
using Microsoft.Extensions.DependencyInjection;

namespace DefaultArguments.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAdamServiceDefaultArguments(this IServiceCollection services, string[] args)
        {
            _ = Parser.Default.ParseArguments<DefaultArgumentsService>(args)
                         .WithParsed(appArgs =>
                         {
                             services.AddSingleton<IDefaultArgumentsService>(appArgs);
                         });
            return services;
        }
    }
}
