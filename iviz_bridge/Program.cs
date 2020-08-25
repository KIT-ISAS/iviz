using System;
using System.Threading;
using Iviz.Roslib;

namespace Iviz.Bridge
{
    static class Program
    {
        static void Main()
        {
            Uri masterUri = RosClient.TryGetCallerUri();
            //Uri masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri");
                return;
            }

            RosClient client = null;
            RosBridge rosBridge = null;
            try
            {
                client = new RosClient(masterUri, "/iviz_bridge");
                rosBridge = new RosBridge(client, 8080);
                WaitForCancel();
            }
            catch (ConnectionException)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to connect to the ROS master");
            }
            finally
            {
                client?.Close();
                rosBridge?.Close();
            }
        }

        static void WaitForCancel()
        {
            object o = new object();
            Console.CancelKeyPress += delegate
            {
                lock (o) Monitor.Pulse(o);
            };
            lock (o) Monitor.Wait(o);
        }
    }
}