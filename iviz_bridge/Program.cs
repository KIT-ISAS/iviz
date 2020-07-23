using Iviz.Roslib;

namespace Iviz.Bridge
{

    class Program
    {
        static void Main(string[] args)
        {
            /*
            RosClient rosClient = new RosClient(
                "http://141.3.59.5:11311",
                "/Iviz_Rosbridge",
                $"http://141.3.59.11:7614");

            string myWebsocketUrl = "ws://141.3.59.11:8080";
            */

            using RosClient rosClient = new RosClient(
                "http://192.168.0.73:11311", 
                "/Iviz_Rosbridge", 
                $"http://192.168.0.157:7614");

            string myWebsocketUrl = "ws://192.168.0.157:8080";

            RosBridge rosBridge = new RosBridge(rosClient, myWebsocketUrl);
            rosBridge.Run();
        }
    }
}
