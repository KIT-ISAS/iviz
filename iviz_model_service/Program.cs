﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;

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
            Console.WriteLine("** Iviz.ModelService: Starting...");

            Uri? masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri. Is ROS_MASTER_URI set?");
                return;
            }

            Uri myUri = RosClient.TryGetCallerUriFor(masterUri) ?? RosClient.TryGetCallerUri();
            await using RosClient client = await RosClient.CreateAsync(masterUri, "iviz_model_service", myUri);
                        
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
            string? extrasPath = await GetPathExtras();
            if (extrasPath != null && File.Exists(extrasPath))
            {
                try
                {
                    rosPackagePathExtras = await File.ReadAllTextAsync(extrasPath);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Extras file '{extrasPath}' could not be read: {e.Message}");
                }
            }

            using var modelServer = new ModelServer(rosPackagePathExtras, enableFileSchema);

            if (modelServer.NumPackages == 0)
            {
                Console.WriteLine("EE Empty list of package paths. Nothing to do.");
                return;
            }

            await client.AdvertiseServiceAsync<GetModelResource>(ModelServer.ModelServiceName,
                modelServer.ModelCallback);
            await client.AdvertiseServiceAsync<GetModelTexture>(ModelServer.TextureServiceName,
                modelServer.TextureCallback);
            await client.AdvertiseServiceAsync<GetFile>(ModelServer.FileServiceName, modelServer.FileCallback);
            await client.AdvertiseServiceAsync<GetSdf>(ModelServer.SdfServiceName, modelServer.SdfCallback);

            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.ModelServiceName,
                GetModelResource.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.TextureServiceName,
                GetModelTexture.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.FileServiceName, GetFile.RosServiceType);
            Console.WriteLine("** Starting service {0} [{1}]...", ModelServer.SdfServiceName, GetSdf.RosServiceType);

            Console.WriteLine("** Done.");
            Console.WriteLine("** Waiting for requests...");

            await WaitForCancel();

            Console.WriteLine();
        }

        static Task WaitForCancel()
        {
            TaskCompletionSource tc = new TaskCompletionSource();
            Console.CancelKeyPress += (_, __) => { tc.SetResult(); };
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
                return await File.ReadAllTextAsync(extrasPath);
            }
            catch (IOException e)
            {
                Logger.LogError($"EE ROS package file '{extrasPath}' exists but could not be read: {e.Message}");
                return null;
            }
        }
    }
}
