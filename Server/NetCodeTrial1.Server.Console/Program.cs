using Microsoft.Extensions.Hosting;
using NetCodeTrial1.Server.Realtime.Application;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.Title = "NetCodeTrial1: Realtime";

            var executingAssembly = Assembly.GetExecutingAssembly();

            Task realtimeServer = RealtimeServer
                .Create(Path.GetDirectoryName(executingAssembly.Location), args)
                .RunConsoleAsync();

            await Task.WhenAll(realtimeServer);
        }
    }
}
