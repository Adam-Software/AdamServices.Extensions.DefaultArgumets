using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DefaultArguments.Extensions
{
    public static class ServiceExtensions
    {
        
        public static IServiceCollection AddArgumentsParserService(this IServiceCollection services, string[] args)
        {
            Parser parser = new(config => config.HelpWriter = Console.Out);
            _ = parser.ParseArguments<object>(args);

            return services;
        }

        public static IServiceCollection AddArgumentsParserService(this IServiceCollection services, object instance, string[] args)
        {
            Parser parser = new(config => config.HelpWriter = Console.Out);
            Type type = instance.GetType();
            Type[] types = [type];

            ParserResult<object> parserResult = parser.ParseArguments(args, types);

            object resultInstance = instance;

            parserResult.WithParsed(result => 
            {
                resultInstance = result;
            });

            services.AddSingleton(type, resultInstance);

            return services;
        }
    }
}
