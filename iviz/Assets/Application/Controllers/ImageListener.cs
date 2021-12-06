#nullable enable

using UnityEngine;
using System;
using Iviz.Msgs.SensorMsgs;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Ros;

namespace Iviz.Controllers
{
    public sealed class ImageListener : ListenerController
    {
        readonly FrameNode node;
        readonly ImageResource billboard;
        readonly ImageTexture imageTexture;
        readonly ImageConfiguration config = new();
        
        string? descriptionOverride;
        bool isProcessing;

        Texture2D? Texture => imageTexture.Texture;

        public Material Material => imageTexture.Material;
        public string Description => descriptionOverride ?? imageTexture.Description;
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
                Listener?.SetPause(value);
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
                imageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                imageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
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
        }

        bool HandlerCompressed(CompressedImage msg)
        {
            if (IsProcessing)
            {
                return false;
            }

            IsProcessing = true;

            void PostProcess()
            {
                if (node.IsAlive)
                {
                    node.AttachTo(msg.Header);
                }

                IsProcessing = false;
            }

            switch (msg.Format)
            {
                case "png":
                    descriptionOverride = null;
                    imageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "jpeg":
                case "jpg":
                    descriptionOverride = null;
                    imageTexture.ProcessJpg(msg.Data, PostProcess);
                    break;
                default:
                    descriptionOverride = msg.Format.Length == 0
                        ? "[Unknown Format]"
                        : $"[Unknown Format '{msg.Format}']";
                    break;
            }

            return true;
        }

        bool Handler(Image msg)
        {
            if (IsProcessing)
            {
                return false;
            }

            IsProcessing = true;

            GameThread.PostInListenerQueue(() =>
            {
                if (!node.IsAlive)
                {
                    IsProcessing = false;
                    return; // stopped!
                }

                node.AttachTo(msg.Header);

                int width = (int) msg.Width;
                int height = (int) msg.Height;
                imageTexture.Set(width, height, msg.Encoding, msg.Data);
                IsProcessing = false;
            });

            return true;
        }

        public override void Dispose()
        {
            base.Dispose();
            billboard.Texture = null;
            billboard.ReturnToPool();

            imageTexture.Stop();
            imageTexture.Destroy();

            node.DestroySelf();
        }
    }
}