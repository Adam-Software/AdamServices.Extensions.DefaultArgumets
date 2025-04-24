using System;
using System.Reflection;

namespace DefaultArguments.Utilties
{
    internal class AssemblyUtilties
    {
        internal static string FullVersion => Version.ToString();
        internal static string ShortVersion => $"{Version.Major}.{Version.Minor}.{Version.Build}";
        internal static string Name => Assembly.GetEntryAssembly().GetName().Name;
        private static Version Version => Assembly.GetEntryAssembly().GetName().Version;
    }
}
