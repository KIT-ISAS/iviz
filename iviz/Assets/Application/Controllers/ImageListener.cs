#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Displays.Helpers;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using UnityEngine;

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
        InterlockedBoolean isProcessing;

        Texture2D? Texture => imageTexture.Texture;

        public Material Material => imageTexture.Material;

        public Vector2? MeasuredIntensityBounds => imageTexture.MeasuredIntensityBounds;

        public string Description
        {
            get
            {
                if (descriptionOverride != null)
                {
                    return descriptionOverride;
                }

                if (MeasuredIntensityBounds is not { } bounds)
                {
                    return imageTexture.Description;
                }

                (float x, float y) = bounds;
                string minIntensityStr = UnityUtils.FormatFloat(x);
                string maxIntensityStr = UnityUtils.FormatFloat(y);
                return $"{imageTexture.Description}\n[{minIntensityStr} .. {maxIntensityStr}]";
            }
        }

        public bool IsMono => imageTexture.IsMono;
        public string Topic => config.Topic;
        public override TfFrame? Frame => node.Parent;

        public Vector2Int ImageSize =>
            Texture != null ? new Vector2Int(Texture.width, Texture.height) : Vector2Int.zero;

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
                BillboardOffset = value.BillboardOffset.ToUnity();
                UseIntrinsicScale = value.UseIntrinsicScale;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                billboard.Visible = value && EnableBillboard;
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
                billboard.Visible = value && Visible;
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
            get => config.BillboardOffset.ToUnity();
            set
            {
                config.BillboardOffset = value.ToRos();
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

        public override Listener Listener { get; }

        public ImageListener(ImageConfiguration? config, string topic, string type)
        {
            imageTexture = new ImageTexture(this);
            node = new FrameNode("ImageNode");
            billboard = ResourcePool.RentDisplay<ImageResource>();
            billboard.Texture = imageTexture;
            billboard.Transform.SetParentLocal(node.Transform);

            Config = config ?? new ImageConfiguration
            {
                Topic = topic,
                Type = type,
                Id = topic
            };

            Listener = Config.Type switch
            {
                Image.MessageType => new Listener<Image>(Config.Topic, 
                    Handler, RosSubscriptionProfile.SensorKeepLast),
                CompressedImage.MessageType => new Listener<CompressedImage>(Config.Topic, 
                    HandlerCompressed, RosSubscriptionProfile.SensorKeepLast),
                _ => Listener.ThrowUnsupportedMessageType(Config.Type)
            };

            billboard.Title = Listener.Topic;

            string infoTopic = RosUtils.GetCameraInfoTopic(Config.Topic);
            infoListener = new Listener<CameraInfo>(infoTopic, InfoHandler);

            isProcessing.Changed = Listener.SetPause;
        }

        bool HandlerCompressed(CompressedImage msg, IRosConnection _)
        {
            if (msg.Format.Length == 0)
            {
                RosLogger.Error($"{ToString()}: Image format field is not set!");
                descriptionOverride = "[Format field empty]";
                return true;
            }

            if (msg.Data.Length == 0)
            {
                RosLogger.Error($"{ToString()}: Data field is not set!");
                descriptionOverride = "[Data field empty]";
                return true;
            }

            if (!isProcessing.TrySet())
            {
                return false;
            }

            var shared = msg.Data.Share();

            void PostProcess()
            {
                if (node.IsAlive)
                {
                    node.AttachTo(msg.Header);
                }

                shared.TryReturn();
                isProcessing.Reset();
            }

            string format = msg.Format.ToUpperInvariant();
            switch (format)
            {
                case "PNG":
                    descriptionOverride = null;
                    imageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "JPEG" or "JPG":
                    descriptionOverride = null;
                    imageTexture.ProcessJpeg(msg.Data, PostProcess);
                    break;
                default:
                {
                    if (format.Contains("PNG"))
                    {
                        descriptionOverride = null;
                        imageTexture.ProcessPng(msg.Data, PostProcess);
                    }
                    else if (format.Contains("JPEG"))
                    {
                        descriptionOverride = null;
                        imageTexture.ProcessJpeg(msg.Data, PostProcess);
                    }
                    else
                    {
                        RosLogger.Error($"{ToString()}: Unknown format '{msg.Format}'");
                        descriptionOverride = $"[Unknown Format '{msg.Format}']";
                        GameThread.PostInListenerQueue(PostProcess);
                    }

                    break;
                }
            }

            return true;
        }

        bool Handler(Image msg, IRosConnection _)
        {
            checked
            {
                // basic checks
                if (msg.Data.Length < msg.Width * msg.Height)
                {
                    RosLogger.Error($"{ToString()}: Image data is too small!");
                    return true;
                }

                if (msg.Step < msg.Width || msg.Data.Length < msg.Step * msg.Height)
                {
                    RosLogger.Error($"{ToString()}: Image step does not correspond to image size!");
                    return true;
                }
            }

            if (msg.Encoding.Length == 0)
            {
                RosLogger.Error($"{ToString()}: Image encoding field is not set!");
                return true;
            }

            if (!isProcessing.TrySet())
            {
                return false;
            }

            var shared = msg.Data.Share();
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
                    isProcessing.Reset();
                    shared.TryReturn();
                }
            });

            return true;
        }

        public bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format, out Vector4 color) =>
            imageTexture.TrySampleColor(rawUV, out uv, out format, out color);

        void InfoHandler(CameraInfo info)
        {
            if (!UseIntrinsicScale)
            {
                return;
            }

            var intrinsic = new Intrinsic(info.K);
            if (!intrinsic.IsValid)
            {
                RosLogger.Error($"{ToString()}: Ignoring invalid intrinsic {intrinsic.ToString()}.");
                return;
            }

            billboard.Intrinsic = intrinsic;
        }

        public override void Dispose()
        {
            base.Dispose();
            billboard.ReturnToPool();
            imageTexture.Dispose();
            infoListener.Dispose();
            node.Dispose();
        }
    }
}