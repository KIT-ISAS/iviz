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
            
            using ModelServer modelServer = new ModelServer();
            
            if (modelServer.NumPackages == 0)
            {
                Console.WriteLine("EE Empty list of package paths. Nothing to do.");
                return;
            }

            client.AdvertiseService<GetModelResource>(ModelServer.ModelServiceName, modelServer.ModelCallback);
            client.AdvertiseService<GetModelTexture>(ModelServer.TextureServiceName, modelServer.TextureCallback);
            client.AdvertiseService<GetFile>(ModelServer.FileServiceName, modelServer.FileCallback);
            client.AdvertiseService<GetSdf>(ModelServer.SdfServiceName, modelServer.SdfCallback);

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
