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

namespace Iviz.Ros
{
    public sealed class RosModelService
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

        public async ValueTask RestartAsync(bool enableFileScheme, CancellationToken token = default)
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL)
            string? rosPackagePathExtras = await GetPathExtras(token);

            modelServer?.Dispose();
            modelServer = new ModelServer(rosPackagePathExtras, enableFileScheme);
            if (enableFileScheme)
            {
                RosLogger.Warn(
                    $"{nameof(RosModelService)} started. Uris starting with 'package://' and 'file://' are now enabled." +
                    " This grants access to all files from the outside");
            }
            else if (modelServer.NumPackages == 0)
            {
                RosLogger.Info(
                    $"{nameof(RosModelService)} started. However, no packages were found. Try creating a ros_package_path file with a list of ROS root paths.");
                return;
            }
            else
            {
                RosLogger.Info(
                    $"{nameof(RosModelService)} started. Uris starting with 'package://' are now enabled." +
                    " This grants access to all files within the ROS packages.");
            }

            RosManager.Connection.AdvertiseService<GetModelResource>(
                ModelServer.ModelServiceName, ModelCallback);

            RosManager.Connection.AdvertiseService<GetModelTexture>(
                ModelServer.TextureServiceName, modelServer.TextureCallback);

            RosManager.Connection.AdvertiseService<GetFile>(
                ModelServer.FileServiceName, modelServer.FileCallback);

            RosManager.Connection.AdvertiseService<GetSdf>(
                ModelServer.SdfServiceName, modelServer.SdfCallback);

            RosLogger.Info($"{nameof(RosModelService)} started with {modelServer.NumPackages} paths.");
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
                    RosLogger.Info($"{nameof(RosModelService)} will not start, mobile platform detected. " +
                                "You will need to start it as an external node.");
                    return null;
            }

            if (homeFolder == null)
            {
                return null;
            }

            string extrasPath = homeFolder + "/.iviz/ros_package_path";
            RosLogger.Info($"{nameof(RosModelService)}: Searching for ROS package file in '{extrasPath}'");
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
                RosLogger.Error($"{nameof(RosModelService)}: ROS package file '{extrasPath}' exists but could not be read", e);
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

            var model = await Resources.Resource.External.GetModelFromFileAsync(msg.Request.Uri);
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
            RosManager.Connection.UnadvertiseService(ModelServer.ModelServiceName);
            RosManager.Connection.UnadvertiseService(ModelServer.TextureServiceName);
            RosManager.Connection.UnadvertiseService(ModelServer.FileServiceName);
            RosManager.Connection.UnadvertiseService(ModelServer.SdfServiceName);

            modelServer?.Dispose();
            modelServer = null;
#endif
        }
    }
}