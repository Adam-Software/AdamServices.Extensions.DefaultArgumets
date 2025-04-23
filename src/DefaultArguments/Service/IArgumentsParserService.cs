using CommandLine;

namespace DefaultArguments.Service
{
    public interface IArgumentsParserService
    {
        public Parser Parser { get; }
    }
}
