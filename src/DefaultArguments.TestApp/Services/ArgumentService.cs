using CommandLine;

namespace DefaultArguments.TestApp.Services
{
    [Verb("ArgumentService", true)]
    public class ArgumentService
    {

        [Option(shortName: 's', longName: "test2", Required = false, HelpText = "Test2")]
        public bool Test2 { get; set; }

        [Option(shortName: 'q', longName: "test3", Required = false, HelpText = "Test3")]
        public bool Test3 { get; set; }
    }
}
