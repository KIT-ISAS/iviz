using System;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.RosMaster
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            Logger.Log = Console.WriteLine;
            Logger.LogError = Console.Error.WriteLine;
            
            Uri masterUri = RosClient.TryGetCallerUri(RosMasterServer.DefaultPort);
            using RosMasterServer rosMasterServer = new RosMasterServer(masterUri);
            Console.WriteLine("** Iviz.RosMaster: Starting...");
            rosMasterServer.AddKey("/rosdistro", "noetic");
            rosMasterServer.AddKey("/rosversion", "1.15.8");
            await rosMasterServer.Start();
        }
    }
}