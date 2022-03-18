using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.ModelService
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await MainImpl(args);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("EE Fatal error: " + Logger.ExceptionToString(e));
            }
        }

        static async Task MainImpl(string[] args)
        {
            Console.WriteLine("** Starting Iviz.ModelService...");

            Uri? masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri. " +
                                        "Try setting ROS_MASTER_URI to the address of the master.");
                return;
            }

            bool enableFileSchema = false;
            bool verbose = false;

            foreach (string arg in args)
            {
                if (arg == "--enable-file-schema" && !enableFileSchema)
                {
                    enableFileSchema = true;
                    Console.Error.WriteLine("WW Uris starting with 'file://' are now accepted. " +
                                            "This makes all your files available to the outside.");
                }
                else if (arg is "--verbose" or "-v")
                {
                    verbose = true;
                }
            }

            string? rosPackagePathExtras;
            string? extrasPath = await GetPathExtras();
            if (extrasPath != null && File.Exists(extrasPath))
            {
                try
                {
                    rosPackagePathExtras = await File.ReadAllTextAsync(extrasPath);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"EE Extras file '{extrasPath}' could not be read: {e.Message}");
                    rosPackagePathExtras = null;
                }
            }
            else
            {
                rosPackagePathExtras = null;
            }

            using var modelServer = new ModelServer(rosPackagePathExtras, enableFileSchema, verbose);

            if (modelServer.NumPackages == 0)
            {
                return;
            }

            Uri myUri = RosClient.TryGetCallerUriFor(masterUri) ?? RosClient.TryGetCallerUri();
            await using RosClient client = await RosClient.CreateAsync(masterUri, "iviz_model_service", myUri);

            Console.WriteLine($"** Starting node at URI {client.CallerUri}...");

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.ModelServiceName,
                GetModelResource.RosServiceType);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.ModelServiceName,
                GetModelResource.RosServiceType);
            await client.AdvertiseServiceAsync<GetModelResource>(ModelServer.ModelServiceName,
                modelServer.ModelCallback);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.TextureServiceName,
                GetModelTexture.RosServiceType);
            await client.AdvertiseServiceAsync<GetModelTexture>(ModelServer.TextureServiceName,
                modelServer.TextureCallback);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.FileServiceName, GetFile.RosServiceType);
            await client.AdvertiseServiceAsync<GetFile>(ModelServer.FileServiceName, modelServer.FileCallback);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.SdfServiceName, GetSdf.RosServiceType);
            await client.AdvertiseServiceAsync<GetSdf>(ModelServer.SdfServiceName, modelServer.SdfCallback);


            Console.WriteLine("** Done.");
            Console.WriteLine("** Iviz.ModelService started with " + modelServer.NumPackages + " ROS package path(s).");
            Console.WriteLine("** Standing by for requests.");

            await WaitForCancel();

            Console.WriteLine();
        }

        static Task WaitForCancel()
        {
            var tc = new TaskCompletionSource();
            Console.CancelKeyPress += (_, __) => tc.SetResult();
            return tc.Task;
        }

        static async Task<string?> GetPathExtras()
        {
            string? homeFolder;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    homeFolder = Environment.GetEnvironmentVariable("HOME");
                    break;
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                    homeFolder = Environment.GetEnvironmentVariable("%HOMEDRIVE%%HOMEPATH%");
                    break;
                default:
                    return null;
            }

            if (homeFolder == null)
            {
                return null;
            }

            string extrasPath = homeFolder + "/.iviz/ros_package_path";
            if (!File.Exists(extrasPath))
            {
                return null;
            }

            try
            {
                Console.WriteLine($"** Reading extra ROS package paths from file '{extrasPath}'");
                return await File.ReadAllTextAsync(extrasPath);
            }
            catch (IOException e)
            {
                Logger.LogErrorFormat("EE ROS package file '{0}' exists but could not be read. Reason: {1}", 
                    extrasPath, e.Message);
                return null;
            }
        }
    }
}