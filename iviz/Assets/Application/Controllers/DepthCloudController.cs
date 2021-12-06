#nullable enable

using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using UnityEngine;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class DepthCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string ColorTopic { get; set; } = "";

        [DataMember] public string DepthTopic { get; set; } = "";

        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class DepthCloudController : IController, IHasFrame
    {
        readonly DepthCloudConfiguration config = new();
        readonly FrameNode node;
        readonly DepthCloudResource projector;
        readonly ImageTexture depthImageTexture;
        readonly ImageTexture colorImageTexture;
        bool depthIsProcessing;
        bool colorIsProcessing;
        TextureFormat? lastDepthFormat;

        public IModuleData ModuleData { get; }
        public TfFrame? Frame => node.Parent;

        Listener<CameraInfo>? depthInfoListener;
        public IListener? DepthListener { get; private set; }
        public IListener? ColorListener { get; private set; }

        public Material ColorMaterial => colorImageTexture.Material;
        public Material DepthMaterial => depthImageTexture.Material;

        public Vector2Int ColorImageSize => colorImageTexture.Texture != null
            ? new Vector2Int(colorImageTexture.Texture.width, colorImageTexture.Texture.height)
            : Vector2Int.zero;

        public Vector2Int DepthImageSize => depthImageTexture.Texture != null
            ? new Vector2Int(depthImageTexture.Texture.width, depthImageTexture.Texture.height)
            : Vector2Int.zero;

        public DepthCloudController(IModuleData moduleData)
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

            depthImageTexture.Colormap = ColormapId.gray;
            colorImageTexture.Colormap = ColormapId.gray;
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
                    colorImageTexture.Reset();
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string? type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == colorTopic)?.Type;
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

                depthInfoListener?.Stop();
                depthInfoListener = null;

                string depthTopic = value.Trim();
                config.DepthTopic = value;
                if (string.IsNullOrWhiteSpace(depthTopic))
                {
                    depthImageTexture.Reset();
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string? type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == depthTopic)?.Type;
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
                string root = lastSlash != -1 ? depthTopic[..lastSlash] : "";
                string infoTopic = $"{root}/camera_info";

                depthInfoListener = new Listener<CameraInfo>(infoTopic, InfoHandler, RosTransportHint.PreferUdp);
            }
        }

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
                    ? $"<b>Color {colorImageTexture.Description}</b>"
                    : "[No Color Image]";

                return depthDescription + colorDescription;
            }
        }

        bool DepthHandler(Image msg)
        {
            if (DepthIsProcessing)
            {
                return false;
            }

            DepthIsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                if (!node.IsAlive)
                {
                    return; // stopped!
                }
                
                node.AttachTo(msg.Header);
                int width = (int)msg.Width;
                int height = (int)msg.Height;
                depthImageTexture.Set(width, height, msg.Encoding, msg.Data);
                UpdateIntensityBounds();
                DepthIsProcessing = false;
            });

            return true;
        }

        bool DepthHandlerCompressed(CompressedImage msg)
        {
            if (DepthIsProcessing)
            {
                return false;
            }

            DepthIsProcessing = true;

            void PostProcess()
            {
                if (!node.IsAlive)
                {
                    return; // stopped!
                }

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
                : null;

            if (format == lastDepthFormat)
            {
                return;
            }

            lastDepthFormat = format;
            depthImageTexture.IntensityBounds = format switch
            {
                TextureFormat.RFloat => new Vector2(0, 5),
                TextureFormat.R16 => new Vector2(0, 5000 / 65535f),
                _ => depthImageTexture.IntensityBounds
            };
        }

        bool ColorHandler(Image msg)
        {
            if (ColorIsProcessing)
            {
                return false;
            }

            ColorIsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                int width = (int)msg.Width;
                int height = (int)msg.Height;
                colorImageTexture.Set(width, height, msg.Encoding, msg.Data);
                ColorIsProcessing = false;
            });

            return true;
        }

        bool ColorHandlerCompressed(CompressedImage msg)
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

        void InfoHandler(CameraInfo info)
        {
            projector.Intrinsic = new Intrinsic(info.K);
        }

        public void Dispose()
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