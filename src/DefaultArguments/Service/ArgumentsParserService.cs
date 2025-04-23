using CommandLine;
using System;

namespace DefaultArguments.Service
{
    public class ArgumentsParserService : IArgumentsParserService
    {
        public ArgumentsParserService() 
        {
            Parser = new Parser(config => config.HelpWriter = Console.Out);
        }

        public Parser Parser { get; private set; }

    }
}
