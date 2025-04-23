using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DefaultArguments.Extensions
{
    public static class ServiceExtensions
    {
        private readonly static Parser mParser = new(config => config.HelpWriter = Console.Out);

        public static IServiceCollection AddAdamDefaultArgumentsParser(this IServiceCollection services, string[] args)
        {
            _ = mParser.ParseArguments<object>(args);

            return services;
        }

        public static IServiceCollection AddAdamArgumentsParserTransient<T>(this IServiceCollection services, string[] args) where T : class
        {
            Type instanceType = typeof(T);
            object instance = Activator.CreateInstance(instanceType);
            ParserResult<object> parserResult = mParser.ParseArguments(args, [instanceType]);

            object resultInstance = instance;
            parserResult.WithParsed(result =>
            {
                resultInstance = result;
            });

            services.AddTransient(instanceType, factory => resultInstance);

            return services;
        }
    }
}
