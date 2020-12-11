using System;
using System.IO;
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
        public bool IsEnabled { get; }

#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
        ModelServer modelServer;
#endif

        public ModelService()
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
            Logger.Internal("Trying to start embedded <b>Iviz.Model.Service</b>...");

            string homeFolder;
            bool isRosInstalled;
            switch (UnityEngine.Application.platform)
            {
                case UnityEngine.RuntimePlatform.OSXEditor:
                case UnityEngine.RuntimePlatform.OSXPlayer:
                case UnityEngine.RuntimePlatform.LinuxEditor:
                case UnityEngine.RuntimePlatform.LinuxPlayer:
                    homeFolder = Environment.GetEnvironmentVariable("HOME");
                    isRosInstalled = Directory.Exists("/opt/ros");
                    break;
                case UnityEngine.RuntimePlatform.WindowsEditor:
                case UnityEngine.RuntimePlatform.WindowsPlayer:
                    homeFolder = Environment.GetEnvironmentVariable("%HOMEDRIVE%%HOMEPATH%");
                    isRosInstalled = false;
                    break;
                default:
                    Logger.Internal("Embedded Iviz.Model.Service will not start, mobile platform detected. " +
                                    "You will need to start it as an external node.");
                    return;
            }

            string rosPackagePathExtras = null;
            string extrasPath = homeFolder + "/.iviz/ros_package_path";
            if (File.Exists(extrasPath))
            {
                try
                {
                    rosPackagePathExtras = File.ReadAllText(extrasPath);
                }
                catch (IOException e)
                {
                    Logger.Warn($"Extras file '{extrasPath}' could not be read: {e.Message}");
                }
            }

            modelServer = new ModelServer(rosPackagePathExtras, true);
            if (modelServer.NumPackages == 0)
            {
                IsEnabled = false;
                Logger.External(
                    "Cannot start embedded Iviz.Model.Service instance. ROS_PACKAGE_PATH not found, or it resolved to an empty value. You will need to start it as an external node.");

                if (isRosInstalled && rosPackagePathExtras == null)
                {
                    Logger.External(LogLevel.Warn,
                        "Iviz.Model.Service could not be started because ROS_PACKAGE_PATH could not be read.\n" +
                        "However, a ROS distro has been detected. You can try creating the folder $HOME/.iviz/ " +
                        "and then calling \"echo $ROS_PACKAGE_PATH > ros_package_path\" in there.");
                }

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
            Logger.Internal($"Embedded Iviz.Model.Service started with {modelServer.NumPackages} paths.");
#endif
        }
    }
}