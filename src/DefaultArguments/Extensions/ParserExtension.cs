using CommandLine;
using DefaultArguments.Utilties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace DefaultArguments.Extensions
{
    public static class ParserExtension
    {
        private readonly static Parser mParser = ParserUtilties.Parser;
        private static bool mErrorHappened = false;

        public static IServiceCollection AddAdamDefaultArgumentsParser(this IServiceCollection services, string[] args)
        {
            ParserResult<object> parserResult = mParser.ParseArguments<object>(args);

            parserResult.WithNotParsed(errs =>
            {
                mErrorHappened = true;
                ParserUtilties.DisplayHelp(parserResult, errs);
            });

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

            parserResult.WithNotParsed(errs => 
            {
                mErrorHappened = true;
                ParserUtilties.DisplayHelp(parserResult, errs); 
            });

            services.AddTransient(instanceType, factory => resultInstance);
            
            return services;
        }

        public static void ParseAndRun(this IHost host)
        {
            var lifetime = host.Services.GetService<IHostApplicationLifetime>();

            if (mErrorHappened)
            {
                lifetime.StopApplication();
                return;
            }

            host.Run();
        }

        public static Task ParseAndRunAsync(this IHost host, CancellationToken token = default)
        {
            var lifetime = host.Services.GetService<IHostApplicationLifetime>();

            if (mErrorHappened)
            {
                lifetime.StopApplication();
                return Task.CompletedTask;
            }

            return host.RunAsync(token);
        }

    }
}
