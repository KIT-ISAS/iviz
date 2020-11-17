using Iviz.Core;
using Iviz.ModelService;
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;

namespace Iviz.Controllers
{
    public sealed class ModelService
    {
        public bool IsEnabled { get; }

        public ModelService()
        {
            Logger.Internal("Trying to start embedded <b>Iviz.Model.Service</b>...");
            if (Settings.IsMobile)
            {
                IsEnabled = false;
                Logger.Internal("Embedded Iviz.Model.Service will not start, mobile platform detected. You will need to start it as an external node.");
                return;
            }

            ModelServer modelServer = new ModelServer();
            if (modelServer.NumPackages == 0)
            {
                IsEnabled = false;
                Logger.Internal("Cannot start embedded Iviz.Model.Service instance. ROS_PACKAGE_PATH not found, or it resolved to an empty value. You will need to start it as an external node.");
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
        }
    }
}