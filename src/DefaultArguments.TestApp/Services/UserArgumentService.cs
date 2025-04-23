using CommandLine;
using DefaultArguments.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DefaultArguments.TestApp.Services
{
    [Verb("add", HelpText = "Add file contents to the index.")]
    public class UserArgumentService //:  IUserArgumentService
    {
        public UserArgumentService() 
        {
            //this.Test2 = Test2;
            //IDefaultArgumentsService defaultArgumentService = serviceProvider.GetService<IDefaultArgumentsService>();
            //ILogger<UserArgumentService> logger = serviceProvider.GetService<ILogger<UserArgumentService>>();
            //IArgumentsParserService argumentsParserService = serviceProvider.GetService<IArgumentsParserService>();
            //var parser = argumentsParserService.Parser;


            //logger.LogInformation("Default test param is {test}", defaultArgumentService.Test1);
        }

        [Option(shortName: 's', longName: "test2", Required = true, HelpText = "Test2")]
        public bool Test2 { get; set; }
    }
}
