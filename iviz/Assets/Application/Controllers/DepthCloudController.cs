﻿#nullable enable

using System;
using System.Linq;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using UnityEngine;

namespace Iviz.Controllers
{
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
        Listener<CameraInfo>? depthInfoListener;

        public IModuleData ModuleData { get; }
        public TfFrame? Frame => node.Parent;
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
            private set
            {
                Visible = value.Visible;
                ColorTopic = value.ColorTopic;
                DepthTopic = value.DepthTopic;
                Colormap = value.Colormap;
                Visible = value.Visible;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;                
            }
        }
        
        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                depthImageTexture.Colormap = value;
                projector.Colormap = value;
            }
        }

        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                if (OverrideMinMax)
                {
                    var intensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    depthImageTexture.IntensityBounds = intensityBounds;
                    projector.IntensityBounds = intensityBounds;
                }
            }
        }      
        
        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                if (OverrideMinMax)
                {
                    var intensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    depthImageTexture.IntensityBounds = intensityBounds;
                    projector.IntensityBounds = intensityBounds;
                }
            }
        }

        public bool OverrideMinMax
        {
            get => config.OverrideMinMax;
            set
            {
                config.OverrideMinMax = value;
                depthImageTexture.OverrideIntensityBounds = value;
                if (value)
                {
                    var intensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    depthImageTexture.IntensityBounds = intensityBounds;
                    projector.IntensityBounds = intensityBounds;
                }
            }
        }
        
        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                depthImageTexture.FlipMinMax = value;
                projector.FlipMinMax = value;
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
                if (string.IsNullOrWhiteSpace(colorTopic))
                {
                    config.ColorTopic = "";
                    colorImageTexture.Reset();
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string? type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == colorTopic)?.Type;
                ColorListener = type switch
                {
                    Image.RosMessageType => new Listener<Image>(colorTopic, ColorHandler),
                    CompressedImage.RosMessageType => new Listener<CompressedImage>(colorTopic, ColorHandlerCompressed),
                    _ => null
                };

                if (ColorListener == null)
                {
                    config.ColorTopic = "";
                    colorImageTexture.Reset();
                    return;
                }

                config.ColorTopic = colorTopic;
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
                if (string.IsNullOrWhiteSpace(depthTopic))
                {
                    config.DepthTopic = "";
                    depthImageTexture.Reset();
                    return;
                }

                var topicInfos = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
                string? type = topicInfos.FirstOrDefault(topicInfo => topicInfo.Topic == depthTopic)?.Type;
                DepthListener = type switch
                {
                    Image.RosMessageType => new Listener<Image>(depthTopic, DepthHandler),
                    CompressedImage.RosMessageType => new Listener<CompressedImage>(depthTopic, DepthHandlerCompressed),
                    _ => null
                };

                if (DepthListener == null)
                {
                    config.DepthTopic = "";
                    depthImageTexture.Reset();
                    return;
                }

                config.DepthTopic = depthTopic;

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

        public DepthCloudController(IModuleData moduleData, DepthCloudConfiguration? config)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            depthImageTexture = new ImageTexture();
            colorImageTexture = new ImageTexture();

            node = FrameNode.Instantiate("DepthCloud");
            node.Transform.localRotation = new Quaternion(0, 0.7071f, 0.7071f, 0);

            projector = ResourcePool.RentDisplay<DepthCloudResource>(node.Transform);
            projector.DepthImage = depthImageTexture;
            projector.ColorImage = colorImageTexture;
            Config = config ?? new DepthCloudConfiguration();
            colorImageTexture.Colormap = ColormapId.bone;
        }

        bool DepthHandler(Image msg)
        {
            if (DepthIsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            DepthIsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    if (!node.IsAlive)
                    {
                        return; // stopped!
                    }

                    node.AttachTo(msg.Header);

                    depthImageTexture.Set((int)msg.Width, (int)msg.Height, msg.Encoding, msg.Data.AsSpan());
                    UpdateIntensityBounds();
                }
                finally
                {
                    msg.Data.TryReturn();
                    DepthIsProcessing = false;
                }
            });

            return true;
        }

        bool DepthHandlerCompressed(CompressedImage msg)
        {
            if (DepthIsProcessing)
            {
                msg.Data.TryReturn();
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
                msg.Data.TryReturn();
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
                default:
                    PostProcess();
                    break;
            }

            return true;
        }

        void UpdateIntensityBounds()
        {
            if (!OverrideMinMax)
            {
                projector.IntensityBounds = depthImageTexture.NormalizedIntensityBounds;
            }
        }

        bool ColorHandler(Image msg)
        {
            if (ColorIsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            ColorIsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    colorImageTexture.Set((int)msg.Width, (int)msg.Height, msg.Encoding, msg.Data.AsSpan());
                }
                finally
                {
                    msg.Data.TryReturn();
                    ColorIsProcessing = false;
                }
            });

            return true;
        }

        bool ColorHandlerCompressed(CompressedImage msg)
        {
            if (ColorIsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            ColorIsProcessing = true;

            void PostProcess()
            {
                msg.Data.TryReturn();
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
                default:
                    PostProcess();
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