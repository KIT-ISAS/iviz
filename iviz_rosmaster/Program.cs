using System;
using System.Net;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.RosMaster
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            Uri masterUri = null;
            
            if (args.Length >= 1
                && args[1] == "--uri")
            {
                if (args.Length == 1)
                {
                    Console.Error.WriteLine("EE Expected an uri as parameter in position 2");
                    return;
                }

                if (!Uri.TryCreate(args[2], UriKind.Absolute, out Uri uri))
                {
                    Console.Error.WriteLine("EE Could not parse position 2 as an uri (example: http://XXX:1234)");
                    return;
                }

                masterUri = uri;
            }
            
            //Logger.Log = Console.WriteLine;
            //Logger.LogError = Console.Error.WriteLine;
            
            masterUri ??= new Uri($"http://{Dns.GetHostName()}:{RosMasterServer.DefaultPort}/");
            using RosMasterServer rosMasterServer = new RosMasterServer(masterUri);    
            Console.WriteLine($"** Iviz.RosMaster: Starting at uri {rosMasterServer.MasterUri} ...");
            rosMasterServer.AddKey("/rosdistro", "noetic");
            rosMasterServer.AddKey("/rosversion", "1.15.8");
            await rosMasterServer.StartAsync();
        }
    }
}