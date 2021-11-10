#nullable enable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
using Iviz.ModelService;
#endif
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;

namespace Iviz.Controllers
{
    public sealed class ModelService : IDisposable
    {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
        public bool IsEnabled => modelServer != null;
        public int NumPackages => modelServer?.NumPackages ?? 0;
        public bool IsFileSchemeEnabled => modelServer?.IsFileSchemaEnabled ?? false;

        ModelServer? modelServer;
#else
        public bool IsEnabled => false;
        public int NumPackages => 0;
        public bool IsFileSchemeEnabled => false;

#endif


        public async ValueTask Restart(bool enableFileScheme, CancellationToken token = default)
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
            string? rosPackagePathExtras = await GetPathExtras(token);

            modelServer?.Dispose();
            modelServer = new ModelServer(rosPackagePathExtras, enableFileScheme);
            if (enableFileScheme)
            {
                Logger.Warn(
                    "Iviz.ModelService started. Uris starting with 'package://' and 'file://' are now enabled." +
                    " This grants access to all files from the outside");
            }
            else if (modelServer.NumPackages == 0)
            {
                Logger.Info(
                    "Iviz.Model.Service started. However, no packages were found. Try creating a ros_package_path file with a list of ROS root paths.");
                return;
            }
            else
            {
                Logger.Info(
                    "Iviz.ModelService started. Uris starting with 'package://' are now enabled." +
                    " This grants access to all files within the ROS packages.");
            }

            ConnectionManager.Connection.AdvertiseService<GetModelResource>(
                ModelServer.ModelServiceName, ModelCallback);

            ConnectionManager.Connection.AdvertiseService<GetModelTexture>(
                ModelServer.TextureServiceName, modelServer.TextureCallback);

            ConnectionManager.Connection.AdvertiseService<GetFile>(
                ModelServer.FileServiceName, modelServer.FileCallback);

            ConnectionManager.Connection.AdvertiseService<GetSdf>(
                ModelServer.SdfServiceName, modelServer.SdfCallback);

            Logger.Info($"Iviz.Model.Service started with {modelServer.NumPackages} paths.");
#endif
        }

        static async ValueTask<string?> GetPathExtras(CancellationToken token)
        {
            string? homeFolder;
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
            Logger.Info($"Iviz.ModelService: Searching for ROS package file in '{extrasPath}'");
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
                Logger.Error($"Iviz.ModelService: ROS package file '{extrasPath}' exists but could not be read", e);
                return null;
            }
        }

#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
        async ValueTask ModelCallback(GetModelResource msg)
        {
            if (modelServer == null)
            {
                return;
            }
            
            modelServer.ModelCallback(msg);
            if (msg.Response.Success)
            {
                return;
            }

            var model = await Resources.Resource.External.TryGetModelFromFileAsync(msg.Request.Uri);
            if (model != null)
            {
                msg.Response.Success = true;
                msg.Response.Model = model;
            }
        }
#endif

        public void Dispose()
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
            ConnectionManager.Connection.UnadvertiseService(ModelServer.ModelServiceName);
            ConnectionManager.Connection.UnadvertiseService(ModelServer.TextureServiceName);
            ConnectionManager.Connection.UnadvertiseService(ModelServer.FileServiceName);
            ConnectionManager.Connection.UnadvertiseService(ModelServer.SdfServiceName);

            modelServer?.Dispose();
            modelServer = null;
#endif
        }
    }
}