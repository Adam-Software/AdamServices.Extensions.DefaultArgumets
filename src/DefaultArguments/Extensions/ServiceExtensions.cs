using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DefaultArguments.Extensions
{
    public static class ServiceExtensions
    {
        private readonly static Parser mParser = new(config => 
        { 
            config.HelpWriter = null; 
        });

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

            parserResult.WithNotParsed(errs => 
            { 
                DisplayHelp(parserResult, errs); 
            });

            services.AddTransient(instanceType, factory => resultInstance);
            
            return services;
        }

        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            //HelpText helpText = null;


            //if (errs.IsVersion())  //check if error is version request
            //    helpText = HelpText.AutoBuild(result);

            //else
            //{
            HelpText helpText = HelpText.AutoBuild(result, helpText =>
                {
                    helpText.AutoHelp = true;
                    helpText.AutoVersion = true;
                    helpText.AdditionalNewLineAfterOption = true;
                    
                    helpText.AddOptions(result);
                    helpText.AdditionalNewLineAfterOption = false;
                    helpText.AddDashesToOption = true;
                    

                    //h.Heading = "Myapp 2.0.0-beta"; //change header
                    //h.Copyright = "Copyright (c) 2019 Global.com"; //change copyright text

                    return HelpText.DefaultParsingErrorsHandler(result, helpText);
                }, e => e, true, 100);
            //}

            Console.WriteLine(helpText);
        }

    }
}
