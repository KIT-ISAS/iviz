using System;
using System.IO;
using System.Threading;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.ModelService
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** Iviz.ModelService: Starting...");

            Uri? masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri. Is ROS_MASTER_URI set?");
                return;
            }

            using RosClient client = new RosClient(masterUri, "/iviz_model_loader");

            bool enableFileSchema;
            if (args.Length != 0 && args[0] == "--enable-file-schema")
            {
                enableFileSchema = true;
                Console.Error.WriteLine(
                    "WW Uris starting with 'file://' are now accepted. This makes all your files available to the outside");
            }
            else
            {
                enableFileSchema = false;
            }

            string? rosPackagePathExtras = null;
            string extrasPath = "/Users/akzeac/.iviz/ros_package_path";
            if (File.Exists(extrasPath))
            {
                try
                {
                    rosPackagePathExtras = File.ReadAllText(extrasPath);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Extras file '{extrasPath}' could not be read: {e.Message}");
                }
            }

            Console.WriteLine(rosPackagePathExtras);

            using var modelServer = new ModelServer(rosPackagePathExtras, enableFileSchema);


            //using ModelServer modelServer = new ModelServer();

            if (modelServer.NumPackages == 0)
            {
                Console.WriteLine("EE Empty list of package paths. Nothing to do.");
                return;
            }

            client.AdvertiseService<GetModelResource>(ModelServer.ModelServiceName, modelServer.ModelCallback);
            client.AdvertiseService<GetModelTexture>(ModelServer.TextureServiceName, modelServer.TextureCallback);
            client.AdvertiseService<GetFile>(ModelServer.FileServiceName, modelServer.FileCallback);
            client.AdvertiseService<GetSdf>(ModelServer.SdfServiceName, modelServer.SdfCallback);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.ModelServiceName,
                GetModelResource.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.TextureServiceName,
                GetModelTexture.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.FileServiceName, GetFile.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.SdfServiceName, GetSdf.RosServiceType);

            Console.WriteLine("** Done.");
            Console.WriteLine("** Waiting for requests...");

            WaitForCancel();

            Console.WriteLine();
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