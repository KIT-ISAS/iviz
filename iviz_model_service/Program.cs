using System;
using System.Threading;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.ModelService
{
    public static class Program
    {
        static void Main()
        {
            Uri masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri. Is ROS_MASTER_URI set?");
                return;
            }

            using RosClient client = new RosClient(masterUri, "/iviz_model_loader");
            
            Server server = new Server();
            
            if (server.NumPackages == 0)
            {
                Console.WriteLine("EE Empty list of package paths. Nothing to do.");
                return;
            }

            client.AdvertiseService<GetModelResource>(Server.ModelServiceName, server.ModelCallback);
            client.AdvertiseService<GetModelTexture>(Server.TextureServiceName, server.TextureCallback);
            client.AdvertiseService<GetFile>(Server.FileServiceName, server.FileCallback);
            client.AdvertiseService<GetSdf>(Server.SdfServiceName, server.SdfCallback);

            Console.WriteLine("** Done. Waiting for requests...");

            WaitForCancel();
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
