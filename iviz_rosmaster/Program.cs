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
            Uri masterUri = RosClient.TryGetCallerUri(Server.DefaultPort);
            using Server server = new Server(masterUri);
            Logger.Log("** Welcome to iviz_rosmaster!");
            server.AddKey("/rosdistro", "noetic");
            server.AddKey("/rosversion", "1.15.8");
            await server.Start();
        }
    }
}