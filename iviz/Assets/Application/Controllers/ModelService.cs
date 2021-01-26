using System;
using System.IO;
using System.Threading;
using Iviz.Core;
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
using Iviz.ModelService;
#endif
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;

namespace Iviz.Controllers
{
    public sealed class ModelService
    {
        public bool IsEnabled { get; private set; }

#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
        ModelServer modelServer;
#endif

        public ModelService()
        {
            Restart();
        }

        public async void Restart(CancellationToken token = default)
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
            //Logger.Internal("Trying to start embedded <b>Iviz.Model.Service</b>...");

            IsEnabled = false;

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
                    return;
            }

            if (homeFolder == null)
            {
                return;
            }

            string rosPackagePathExtras;
            string extrasPath = homeFolder + "/.iviz/ros_package_path";
            if (!File.Exists(extrasPath))
            {
                Logger.Info(
                    "Iviz.ModelService could not be started because ROS_PACKAGE_PATH could not be read.\n" +
                    "If you have a ROS install, you can create the folder $HOME/.iviz/ and then call \"echo $ROS_PACKAGE_PATH > ros_package_path\" in there.");
                return;
            }

            try
            {
                rosPackagePathExtras = await FileUtils.ReadAllTextAsync(extrasPath, token);
            }
            catch (IOException e)
            {
                Logger.Warn(
                    $"Iviz.ModelService: ROS package file '{extrasPath}' exists but could not be read: {e.Message}");
                return;
            }

            modelServer = new ModelServer(rosPackagePathExtras, true);
            if (modelServer.NumPackages == 0)
            {
                IsEnabled = false;

                Logger.Info("Iviz.Model.Service tried to start, but no packages were found in the given paths.");
                return;
            }

            ConnectionManager.Connection.AdvertiseService<GetModelResource>(
                ModelServer.ModelServiceName, modelServer.ModelCallback);

            ConnectionManager.Connection.AdvertiseService<GetModelTexture>(
                ModelServer.TextureServiceName, modelServer.TextureCallback);

            ConnectionManager.Connection.AdvertiseService<GetFile>(
                ModelServer.FileServiceName, modelServer.FileCallback);

            ConnectionManager.Connection.AdvertiseService<GetSdf>(
                ModelServer.SdfServiceName, modelServer.SdfCallback);

            IsEnabled = true;
            Logger.Info($"Iviz.Model.Service started with {modelServer.NumPackages} paths.");
#endif
        }
    }
}