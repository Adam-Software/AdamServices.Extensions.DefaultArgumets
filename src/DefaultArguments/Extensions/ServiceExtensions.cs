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
        
        public static IServiceCollection AddArgumentsParserService(this IServiceCollection services, string[] args)
        {
            ArgumentsParserService parserService = new();
            Parser parser = parserService.Parser;

            var parserResult = parser.ParseArguments<object>(args);
            parserResult.WithNotParsed(HandleErrors);

            return services;
        }

        public static IServiceCollection AddArgumentsParserService(this IServiceCollection services, object instance, string[] args)
        {
            ArgumentsParserService parserService = new();
            Parser parser = parserService.Parser;

            var type = instance.GetType();
            Type[] types = [type];

            ParserResult<object> parserResult = parser.ParseArguments(args, types);

            object s = instance;

            parserResult.WithParsed(result => 
            {
                s = result;
            });

            parserResult.WithNotParsed(HandleErrors);

            services.AddSingleton(type, s);

            return services;
        }


        public static void UseAdamDefaultArguments(this IHost host, string[] args) 
        {
            IServiceProvider serviceProvider = host.Services;
            IArgumentsParserService argumentParser = serviceProvider.GetRequiredService<IArgumentsParserService>();

            var parserResult = argumentParser.Parser.ParseArguments<object>(args);
            
            parserResult.WithNotParsed(HandleErrors);
        }

        public static void UseAdamDefaultArguments(this IHost host, Type instance, string[] args)
        {
            IServiceProvider serviceProvider = host.Services;
            IArgumentsParserService argumentParser = serviceProvider.GetRequiredService<IArgumentsParserService>();
            
            //var type = instance.GetType();
            Type[] types = [instance];
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


        /*Parser.Default.ParseArguments<DefaultArgumentsService>(args)
            .WithParsed(appArgs =>
            {
                services.AddTransient<IDefaultArgumentsService>(app => appArgs);
            });
        */
    }
}
