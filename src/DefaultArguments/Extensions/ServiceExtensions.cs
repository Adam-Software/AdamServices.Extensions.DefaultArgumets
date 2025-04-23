using CommandLine;
using CommandLine.Text;
using DefaultArguments.Model;
using DefaultArguments.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DefaultArguments.Extensions
{
    public static class ServiceExtensions
    {
        
        public static IServiceCollection AddArgumentsParserService(this IServiceCollection services)
        {
            services.AddSingleton<IArgumentsParserService, ArgumentsParserService>();

            /*Parser.Default.ParseArguments<DefaultArgumentsService>(args)
                .WithParsed(appArgs =>
                {
                    services.AddTransient<IDefaultArgumentsService>(app => appArgs);
                });
            */
            return services;
        }

        public static void UseAdamDefaultArguments(this IHost host, string[] args) 
        {
            IServiceProvider serviceProvider = host.Services;
            IArgumentsParserService argumentParser = serviceProvider.GetRequiredService<IArgumentsParserService>();

            ParserResult<Arguments> parserResult = argumentParser.Parser.ParseArguments<Arguments>(args);
            
            parserResult.WithNotParsed(HandleErrors);
        }

        public static void UseAdamDefaultArguments(this IHost host, object instance, string[] args)
        {
            IServiceProvider serviceProvider = host.Services;
            IArgumentsParserService argumentParser = serviceProvider.GetRequiredService<IArgumentsParserService>();
            
            
            var type = instance.GetType();
            Type[] types = [type];
            var parserResult = argumentParser.Parser.ParseArguments(args, types);

            parserResult.WithNotParsed(HandleErrors);
        }

        private static void HandleErrors(IEnumerable<Error> errs)
        {
            Console.WriteLine("Parser Fail");
        }

        static string GetHelp<T>(ParserResult<T> parserResult)
        {
            return HelpText.AutoBuild(parserResult, h => h, e => e);
        }
    }
}
