using System;
using System.Net;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.RosMaster
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            Uri masterUri = null;
            
            if (args.Length >= 1) 
            {
                if (args[0] != "--uri")
                {
                    Console.Error.WriteLine("Usage: Iviz.Rosmaster [--uri masterUri]");
                    return;
                }

                if (args.Length == 1)
                {
                    Console.Error.WriteLine("EE Expected an uri as parameter in position 2");
                    return;
                }

                if (!Uri.TryCreate(args[2], UriKind.Absolute, out Uri uri))
                {
                    Console.Error.WriteLine("EE Could not parse position 2 as an uri (example: http://localhost:11311)");
                    return;
                }

                masterUri = uri;
            }
            
            Logger.LogCallback = Console.WriteLine;
            Logger.LogErrorCallback = Console.Error.WriteLine;

            try
            {
                masterUri ??= new Uri($"http://{Dns.GetHostName()}:{RosMasterServer.DefaultPort}/");
                await using var rosMasterServer = new RosMasterServer(masterUri);
                rosMasterServer.AddKey("/rosdistro", "noetic");
                rosMasterServer.AddKey("/rosversion", "1.15.8");
                await rosMasterServer.StartAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"EE Fatal error: {Logger.ExceptionToString(e)}");
            }
        }
    }
}