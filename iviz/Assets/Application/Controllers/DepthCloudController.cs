using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class DepthCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember, NotNull] public string ColorTopic { get; set; } = "";

        [DataMember, NotNull] public string DepthTopic { get; set; } = "";

        [DataMember, NotNull] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class DepthCloudController : IController, IHasFrame
    {
        readonly DepthCloudConfiguration config = new DepthCloudConfiguration();
        readonly FrameNode node;
        readonly DepthCloudResource projector;
        readonly ImageTexture depthImageTexture;
        readonly ImageTexture colorImageTexture;
        bool depthIsProcessing;
        bool colorIsProcessing;
        TextureFormat? lastDepthFormat;

        public IModuleData ModuleData { get; }
        public TfFrame Frame => node.Parent;

        [CanBeNull] public IListener DepthListener { get; private set; }
        [CanBeNull] public Listener<CameraInfo> DepthInfoListener { get; private set; }
        [CanBeNull] public IListener ColorListener { get; private set; }

        [NotNull] public Material ColorMaterial => colorImageTexture.Material;
        [NotNull] public Material DepthMaterial => depthImageTexture.Material;

        public Vector2Int ColorImageSize => colorImageTexture.Texture != null
            ? new Vector2Int(colorImageTexture.Texture.width, colorImageTexture.Texture.height)
            : Vector2Int.zero;

        public Vector2Int DepthImageSize => depthImageTexture.Texture != null
            ? new Vector2Int(depthImageTexture.Texture.width, depthImageTexture.Texture.height)
            : Vector2Int.zero;

        public DepthCloudController([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            depthImageTexture = new ImageTexture();
            colorImageTexture = new ImageTexture();
            node = FrameNode.Instantiate("DepthCloud");
            node.Transform.localRotation = new Quaternion(0, 0.7071f, 0.7071f, 0);

            projector = ResourcePool.RentDisplay<DepthCloudResource>(node.Transform);
            projector.DepthImage = depthImageTexture;
            projector.ColorImage = colorImageTexture;
            Config = new DepthCloudConfiguration();
        }

        bool DepthIsProcessing
        {
            get => depthIsProcessing;
            set
            {
                depthIsProcessing = value;
                DepthListener?.SetPause(value);
            }
        }

        bool ColorIsProcessing
        {
            get => colorIsProcessing;
            set
            {
                colorIsProcessing = value;
                ColorListener?.SetPause(value);
            }
        }

        [NotNull]
        public DepthCloudConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                ColorTopic = value.ColorTopic;
                DepthTopic = value.DepthTopic;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                projector.Visible = value;
            }
        }

        [NotNull]
        public string ColorTopic
        {
            get => config.ColorTopic;
            set
            {
                if (config.ColorTopic == value)
                {
                    return;
                }

                if (ColorListener != null)
                {
                    ColorListener.Stop();
                    ColorListener = null;
                }

                string colorTopic = value.Trim();
                config.ColorTopic = colorTopic;
                if (string.IsNullOrWhiteSpace(colorTopic))
                {
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == colorTopic)?.Type;
                switch (type)
                {
                    case Image.RosMessageType:
                        ColorListener = new Listener<Image>(colorTopic, ColorHandler);
                        break;
                    case CompressedImage.RosMessageType:
                        ColorListener = new Listener<CompressedImage>(colorTopic, ColorHandlerCompressed);
                        break;
                    default:
                        config.ColorTopic = "";
                        break;
                }
            }
        }

        [NotNull]
        public string DepthTopic
        {
            get => config.DepthTopic;
            set
            {
                if (config.DepthTopic == value)
                {
                    return;
                }

                DepthListener?.Stop();
                DepthListener = null;

                DepthInfoListener?.Stop();
                DepthInfoListener = null;

                string depthTopic = value.Trim();
                config.DepthTopic = value;
                if (string.IsNullOrWhiteSpace(depthTopic))
                {
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == depthTopic)?.Type;
                switch (type)
                {
                    case Image.RosMessageType:
                        DepthListener = new Listener<Image>(depthTopic, DepthHandler);
                        break;
                    case CompressedImage.RosMessageType:
                        DepthListener = new Listener<CompressedImage>(depthTopic, DepthHandlerCompressed);
                        break;
                    default:
                        config.DepthTopic = "";
                        break;
                }

                int lastSlash = depthTopic.LastIndexOf('/');
                string root = lastSlash != -1 ? depthTopic.Substring(0, lastSlash) : "";
                string infoTopic = $"{root}/camera_info";

                DepthInfoListener = new Listener<CameraInfo>(infoTopic, InfoHandler);
            }
        }
        
        [NotNull]
        public string Description
        {
            get
            {
                var depthTexture = depthImageTexture.Texture;
                var colorTexture = colorImageTexture.Texture;

                if (depthTexture == null && colorTexture == null)
                {
                    return "[No Depth or Color Image]";
                }

                string depthDescription = depthTexture != null
                    ? $"<b>Depth {depthImageTexture.Description}</b>\n"
                    : "[No Depth Image]\n";
                
                string colorDescription = colorTexture != null
                    ? $"<b>Color {depthImageTexture.Description}</b>"
                    : "[No Color Image]";

                return depthDescription + colorDescription;
                
                /*
                float horizontalFov = projector.Intrinsic.GetHorizontalFovInRad(depthImageTexture.Width);
                float verticalFov = projector.Intrinsic.GetVerticalFovInRad(depthImageTexture.Height);
                string intrinsicDescription = horizontalFov != 0
                    ? "hFOV=" + ((int) (horizontalFov * Mathf.Rad2Deg)).ToString() + " deg " +
                      "vFOV=" + ((int) (verticalFov * Mathf.Rad2Deg)).ToString() + " deg"
                    : "[No Intrinsic Data]";

                return depthDescription + colorDescription + intrinsicDescription;
                */
            }
        }

        void DepthHandler([NotNull] Image msg)
        {
            node.AttachTo(msg.Header);
            
            int width = (int) msg.Width;
            int height = (int) msg.Height;
            depthImageTexture.Set(width, height, msg.Encoding, msg.Data);
            UpdateIntensityBounds();
        }

        bool DepthHandlerCompressed([NotNull] CompressedImage msg)
        {
            if (DepthIsProcessing)
            {
                return false;
            }

            DepthIsProcessing = true;

            void PostProcess()
            {
                node.AttachTo(msg.Header);
                DepthIsProcessing = false;
                UpdateIntensityBounds();
            }

            switch (msg.Format.ToUpperInvariant())
            {
                case "PNG":
                    depthImageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "JPEG":
                case "JPG":
                    depthImageTexture.ProcessJpg(msg.Data, PostProcess);
                    break;
            }

            return true;
        }

        void UpdateIntensityBounds()
        {
            TextureFormat? format = depthImageTexture.Texture != null
                ? depthImageTexture.Texture.format
                : (TextureFormat?) null;
            
            if (format == lastDepthFormat)
            {
                return;
            }

            lastDepthFormat = format;
            depthImageTexture.Colormap = ColormapId.gray;
            switch (format)
            {
                case TextureFormat.RFloat:
                    depthImageTexture.IntensityBounds = new Vector2(0, 5);
                    break;
                case TextureFormat.R16:
                    depthImageTexture.IntensityBounds = new Vector2(0, 5000 / 65535f);
                    break;
            }
        }

        void ColorHandler([NotNull] Image msg)
        {
            int width = (int) msg.Width;
            int height = (int) msg.Height;
            colorImageTexture.Set(width, height, msg.Encoding, msg.Data);
        }

        bool ColorHandlerCompressed([NotNull] CompressedImage msg)
        {
            if (ColorIsProcessing)
            {
                return false;
            }

            ColorIsProcessing = true;

            void PostProcess()
            {
                ColorIsProcessing = false;
            }

            switch (msg.Format.ToUpperInvariant())
            {
                case "PNG":
                    colorImageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "JPEG":
                case "JPG":
                    colorImageTexture.ProcessJpg(msg.Data, PostProcess);
                    break;
            }

            return true;
        }

        void InfoHandler([NotNull] CameraInfo info)
        {
            projector.Intrinsic = new Intrinsic(info.K);
        }

        public void StopController()
        {
            ColorListener?.Stop();
            DepthListener?.Stop();

            projector.ReturnToPool();
            node.DestroySelf();
        }

        public void ResetController()
        {
        }
    }
}