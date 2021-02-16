using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
using Iviz.ModelService;
#endif
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    public sealed class ModelService : IDisposable
    {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
        public bool IsEnabled => modelServer != null;
        public int NumPackages => modelServer?.NumPackages ?? 0;
        public bool IsFileSchemaEnabled => modelServer?.IsFileSchemaEnabled ?? false;

        ModelServer modelServer;
#else
        public bool IsEnabled => false;
        public int NumPackages => 0;
        public bool IsFileSchemaEnabled => false;

#endif


        public async Task Restart(bool enableFileSchema, CancellationToken token = default)
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
            string rosPackagePathExtras = await GetPathExtras(token);

            modelServer?.Dispose();
            modelServer = new ModelServer(rosPackagePathExtras, enableFileSchema);
            if (enableFileSchema)
            {
                Logger.Warn(
                    "Iviz.ModelService started. Uris starting with 'package://' and 'file://' are now enabled." +
                    " This grants access to all files from the outside");
            }
            else if (modelServer.NumPackages == 0)
            {
                Logger.Info(
                    "Iviz.Model.Service started. However, no packages were found. Try creating a ros_package_path file.");
                return;
            }
            else
            {
                Logger.Info(
                    "Iviz.ModelService started. Uris starting with 'package://' are now enabled." +
                    " This grants access to all files within the ROS packages.");
            }

            ConnectionManager.Connection.AdvertiseService<GetModelResource>(
                ModelServer.ModelServiceName, modelServer.ModelCallback);

            ConnectionManager.Connection.AdvertiseService<GetModelTexture>(
                ModelServer.TextureServiceName, modelServer.TextureCallback);

            ConnectionManager.Connection.AdvertiseService<GetFile>(
                ModelServer.FileServiceName, modelServer.FileCallback);

            ConnectionManager.Connection.AdvertiseService<GetSdf>(
                ModelServer.SdfServiceName, modelServer.SdfCallback);

            Logger.Info($"Iviz.Model.Service started with {modelServer.NumPackages} paths.");
#endif
        }

        [ItemCanBeNull]
        static async Task<string> GetPathExtras(CancellationToken token)
        {
            string homeFolder;
            switch (UnityEngine.Application.platform)
            {
                case UnityEngine.RuntimePlatform.OSXEditor:
                case UnityEngine.RuntimePlatform.OSXPlayer:
                case UnityEngine.RuntimePlatform.LinuxEditor:
                case UnityEngine.RuntimePlatform.LinuxPlayer:
                    homeFolder = Environment.GetEnvironmentVariable("HOME");
                    break;
                case UnityEngine.RuntimePlatform.WindowsEditor:
                case UnityEngine.RuntimePlatform.WindowsPlayer:
                    homeFolder = Environment.GetEnvironmentVariable("%HOMEDRIVE%%HOMEPATH%");
                    break;
                default:
                    Logger.Info("Iviz.Model.Service will not start, mobile platform detected. " +
                                "You will need to start it as an external node.");
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
                return await FileUtils.ReadAllTextAsync(extrasPath, token);
            }
            catch (IOException e)
            {
                Logger.Warn(
                    $"Iviz.ModelService: ROS package file '{extrasPath}' exists but could not be read: {e.Message}");
                return null;
            }
        }

        public void Dispose()
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
            modelServer?.Dispose();
            modelServer = null;
#endif
        }
    }
}