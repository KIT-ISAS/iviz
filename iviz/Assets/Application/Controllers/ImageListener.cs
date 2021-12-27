#nullable enable

using UnityEngine;
using System;
using Iviz.Msgs.SensorMsgs;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.StdMsgs;
using Iviz.Ros;
using Iviz.Roslib;

namespace Iviz.Controllers
{
    public sealed class ImageListener : ListenerController
    {
        readonly FrameNode node;
        readonly ImageResource billboard;
        readonly ImageTexture imageTexture;
        readonly ImageConfiguration config = new();
        readonly Listener<CameraInfo> infoListener;

        string? descriptionOverride;
        bool isProcessing;

        Texture2D? Texture => imageTexture.Texture;

        public Material Material => imageTexture.Material;

        public string Description
        {
            get
            {
                if (descriptionOverride != null)
                {
                    return descriptionOverride;
                }

                if (imageTexture.MeasuredIntensityBounds is not { } bounds)
                {
                    return imageTexture.Description;
                }

                (float x, float y) = bounds;
                string minIntensityStr = x.ToString("#,0.##", UnityUtils.Culture);
                string maxIntensityStr = y.ToString("#,0.##", UnityUtils.Culture);
                return $"{imageTexture.Description}\n[{minIntensityStr} .. {maxIntensityStr}]";
            }
        }

        public bool IsMono => imageTexture.IsMono;
        public string Topic => config.Topic;
        public override TfFrame? Frame => node.Parent;
        public override IModuleData ModuleData { get; }

        public Vector2Int ImageSize =>
            Texture != null ? new Vector2Int(Texture.width, Texture.height) : Vector2Int.zero;

        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener.SetPause(value);
            }
        }

        public ImageConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Colormap = value.Colormap;
                Visible = value.Visible;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                EnableBillboard = value.EnableBillboard;
                BillboardSize = value.BillboardSize;
                BillboardFollowsCamera = value.BillboardFollowCamera;
                BillboardOffset = value.BillboardOffset;
                UseIntrinsicScale = value.UseIntrinsicScale;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                billboard.Visible = value && config.EnableBillboard;
            }
        }

        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                imageTexture.Colormap = value;
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
                    imageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
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
                    imageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool OverrideMinMax
        {
            get => config.OverrideMinMax;
            set
            {
                config.OverrideMinMax = value;
                imageTexture.OverrideIntensityBounds = value;
                if (value)
                {
                    imageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                imageTexture.FlipMinMax = value;
            }
        }

        public bool EnableBillboard
        {
            get => config.EnableBillboard;
            set
            {
                config.EnableBillboard = value;
                billboard.Visible = value && config.Visible;
            }
        }

        public float BillboardSize
        {
            get => config.BillboardSize;
            set
            {
                config.BillboardSize = value;
                billboard.Scale = value;
            }
        }

        public bool BillboardFollowsCamera
        {
            get => config.BillboardFollowCamera;
            set
            {
                config.BillboardFollowCamera = value;
                billboard.BillboardEnabled = value;
            }
        }

        public Vector3 BillboardOffset
        {
            get => config.BillboardOffset;
            set
            {
                config.BillboardOffset = value;
                billboard.Offset = value.Ros2Unity();
            }
        }

        public bool UseIntrinsicScale
        {
            get => config.UseIntrinsicScale;
            set
            {
                config.UseIntrinsicScale = value;
                if (!value)
                {
                    billboard.Intrinsic = null;
                }
            }
        }

        public override IListener Listener { get; }

        public ImageListener(IModuleData moduleData, ImageConfiguration? config, string topic, string type)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            imageTexture = new ImageTexture();
            node = FrameNode.Instantiate("[ImageNode]");
            billboard = ResourcePool.RentDisplay<ImageResource>();
            billboard.Texture = imageTexture;
            billboard.Transform.SetParentLocal(node.transform);

            Config = config ?? new ImageConfiguration
            {
                Topic = topic,
                Type = type
            };

            Listener = Config.Type switch
            {
                Image.RosMessageType => new Listener<Image>(Config.Topic, Handler),
                CompressedImage.RosMessageType => new Listener<CompressedImage>(Config.Topic, HandlerCompressed),
                _ => throw new InvalidOperationException("Invalid message type")
            };

            billboard.Title = Listener.Topic;

            string infoTopic = RosUtils.GetCameraInfoTopic(Config.Topic);
            infoListener = new Listener<CameraInfo>(infoTopic, InfoHandler, RosTransportHint.PreferUdp);
        }

        bool HandlerCompressed(CompressedImage msg)
        {
            if (IsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            IsProcessing = true;

            void PostProcess()
            {
                if (node.IsAlive)
                {
                    node.AttachTo(msg.Header);
                }

                msg.Data.TryReturn();
                IsProcessing = false;
            }

            switch (msg.Format.ToUpperInvariant())
            {
                case "PNG":
                    descriptionOverride = null;
                    imageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "JPEG":
                case "JPG":
                    descriptionOverride = null;
                    imageTexture.ProcessJpg(msg.Data, PostProcess);
                    break;
                default:
                    descriptionOverride = msg.Format.Length == 0
                        ? "[Unknown Format]"
                        : $"[Unknown Format '{msg.Format}']";
                    PostProcess();
                    break;
            }

            return true;
        }

        bool Handler(Image msg)
        {
            if (IsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            IsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    if (!node.IsAlive)
                    {
                        return; // stopped!
                    }

                    node.AttachTo(msg.Header);
                    imageTexture.Set((int)msg.Width, (int)msg.Height, msg.Encoding, msg.Data.AsSpan());
                }
                finally
                {
                    IsProcessing = false;
                    msg.Data.TryReturn();
                }
            });

            return true;
        }

        public bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format, out Vector4 color) =>
            imageTexture.TrySampleColor(rawUV, out uv, out format, out color);

        void InfoHandler(CameraInfo info)
        {
            if (UseIntrinsicScale)
            {
                billboard.Intrinsic = new Intrinsic(info.K);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            billboard.Texture = null;
            billboard.ReturnToPool();

            imageTexture.Stop();
            imageTexture.Dispose();
            infoListener.Dispose();

            node.DestroySelf();
        }
    }
}