using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace DefaultArguments.Utilties
{
    internal static class ParserUtilties
    {
        internal static Parser Parser { get; } = new Parser(config =>
        {
            config.HelpWriter = null;
            config.IgnoreUnknownArguments = false;
            config.AutoHelp = false;
            config.AutoVersion = false;
        });

        internal static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {
                string version = Assembly.GetEntryAssembly().GetName().Version.ToString();
                Console.WriteLine(AssemblyUtilties.ShortVersion);

                return;
            }

            var helpText = HelpText.AutoBuild(result, helpText =>
            {
                var version = Assembly.GetEntryAssembly().GetName().Version.ToString();

                helpText.Heading = $"{AssemblyUtilties.Name} v.{AssemblyUtilties.ShortVersion}";

                helpText.AddDashesToOption = true;
                helpText.AddNewLineBetweenHelpSections = false;
                helpText.AdditionalNewLineAfterOption = false;

                return HelpText.DefaultParsingErrorsHandler(result, helpText);

            }, e => e);

            Console.WriteLine(helpText);
        }
    }
}
