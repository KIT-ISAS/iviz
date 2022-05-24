using System;
using System.IO;
using System.Text;
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

            var masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri. " +
                                        "Try setting ROS_MASTER_URI to the address of the master.");
                return;
            }

            bool enableFileSchema = false;
            bool verbose = false;

            var additionalPaths = new StringBuilder();
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
                else
                {
                    additionalPaths.Append(arg);
                }
            }

            if (await GetPathExtras() is { } paths)
            {
                additionalPaths.Append(additionalPaths.Length == 0 ? paths : (":" + paths));
            }

            using var modelServer = new ModelServer(additionalPaths.ToString(), enableFileSchema, verbose);

            if (modelServer.NumPackages == 0)
            {
                return;
            }

            Console.WriteLine($"** Found {modelServer.NumPackages} ROS packages. Trying to connect...");

            var myUri = RosClient.TryGetCallerUriFor(masterUri) ?? RosClient.TryGetCallerUri();
            await using RosClient client = await RosClient.CreateAsync(masterUri, "iviz_model_service", myUri);

            Console.WriteLine($"** Started node at URI {client.CallerUri}");

            //Console.WriteLine("** Starting service {0} [{1}].", ModelServer.ModelServiceName,
            //    GetModelResource.ServiceType);

            //Console.WriteLine("** Starting service {0} [{1}].", ModelServer.ModelServiceName,
            //    GetModelResource.ServiceType);
            await client.AdvertiseServiceAsync<GetModelResource>(ModelServer.ModelServiceName,
                modelServer.ModelCallback);

            //Console.WriteLine("** Starting service {0} [{1}].", ModelServer.TextureServiceName,
            //    GetModelTexture.ServiceType);
            await client.AdvertiseServiceAsync<GetModelTexture>(ModelServer.TextureServiceName,
                modelServer.TextureCallback);

            //Console.WriteLine("** Starting service {0} [{1}].", ModelServer.FileServiceName, GetFile.ServiceType);
            await client.AdvertiseServiceAsync<GetFile>(ModelServer.FileServiceName, modelServer.FileCallback);

            //Console.WriteLine("** Starting service {0} [{1}].", ModelServer.SdfServiceName, GetSdf.ServiceType);
            await client.AdvertiseServiceAsync<GetSdf>(ModelServer.SdfServiceName, modelServer.SdfCallback);

            Console.WriteLine("** Iviz.ModelService started. Standing by for requests...");

            await WaitForCancel();

            Console.WriteLine();
        }

        static Task WaitForCancel()
        {
            var tc = new TaskCompletionSource();
            Console.CancelKeyPress += (_, _) => tc.TrySetResult();
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
                Console.WriteLine($"** Checking file '{extrasPath}' for additional ROS paths...");
                return (await File.ReadAllTextAsync(extrasPath)).Trim();
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