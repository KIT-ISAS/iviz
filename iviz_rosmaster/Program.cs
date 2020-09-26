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
            Uri masterUri = RosClient.TryGetCallerUri(RosMasterServer.DefaultPort);
            using RosMasterServer rosMasterServer = new RosMasterServer(masterUri);
            Logger.Log("** Welcome to iviz_rosmaster!");
            rosMasterServer.AddKey("/rosdistro", "noetic");
            rosMasterServer.AddKey("/rosversion", "1.15.8");
            await rosMasterServer.Start();
        }
    }
}