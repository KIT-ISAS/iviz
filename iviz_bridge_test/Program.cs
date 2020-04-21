
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using RosSharp.RosBridgeClient.Protocols;
using System.Threading;

namespace Iviz.Bridge.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string masterUrl = args.Length > 0 ? args[0] : "ws://141.3.59.5:9090";

            RosSocket socket = new RosSocket(new WebSocketSharpProtocol(masterUrl));

            socket.Advertise<Int32>("/bridge_test/int_topic");
            socket.Subscribe<Int32>("/bridge_test/int_topic", Handle);
            int k = 0;
            while (true)
            {
                k++;
                socket.Publish("/bridge_test/int_topic", new Int32(k));
                Thread.Sleep(10);
            }
            //socket.Unadvertise("/bridge_test/int_topic");
            
            /*
            //socket.Subscribe<CompressedImage>("/armar6/image_compressed", Handle);
            while (true)
            {
                Thread.Sleep(1000);
            }
            */
        }

        static void Handle(CompressedImage img)
        {
            System.Console.WriteLine(img.data.Length);
        }

        static void Handle(Int32 img)
        {
            System.Console.WriteLine(img.data);
        }
    }
}
