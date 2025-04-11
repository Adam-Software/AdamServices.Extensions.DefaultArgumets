
using Microsoft.Extensions.Hosting;
using DefaultArguments.Extensions;

namespace DefaultArguments.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                 {
                     services.AddAdamServiceDefaultArguments(args);

                 })

                 .Build();

            host.Run();
        }
    }
}
