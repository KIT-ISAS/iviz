using UnityEngine;
using System;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Roslib;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class ImageConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Image;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.gray;
        [DataMember] public float MinIntensity { get; set; } = 0.0f;
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool FlipMinMax { get; set; } = false;
        [DataMember] public bool EnableBillboard { get; set; } = false;
        [DataMember] public float BillboardSize { get; set; } = 1.0f;
        [DataMember] public bool BillboardFollowCamera { get; set; } = false;
        [DataMember] public SerializableVector3 BillboardOffset { get; set; }
    }

    public sealed class ImageListener : ListenerController
    {
        readonly FrameNode node;
        readonly ImageResource billboard;
        readonly ImageTexture imageTexture;

        public override TfFrame Frame => node.Parent;

        [CanBeNull] Texture2D Texture => imageTexture.Texture;
        [NotNull] public Material Material => imageTexture.Material;

        public Vector2Int ImageSize =>
            Texture != null ? new Vector2Int(Texture.width, Texture.height) : Vector2Int.zero;

        string descriptionOverride;
        [NotNull] public string Description => descriptionOverride ?? imageTexture.Description;

        public bool IsMono => imageTexture.IsMono;
        bool isProcessing;
        
        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener?.SetPause(value);
            }
        }        

        readonly ImageModuleData moduleData;
        public override IModuleData ModuleData => moduleData;

        readonly ImageConfiguration config = new ImageConfiguration();

        public ImageConfiguration Config
        {
            get => config;
            set
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
        
        [NotNull]
        public string Topic => config.Topic;        

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

        public ImageListener([NotNull] IModuleData moduleData)
        {
            imageTexture = new ImageTexture();
            node = FrameNode.Instantiate("[ImageNode]");
            this.moduleData = (ImageModuleData) (moduleData ?? throw new ArgumentNullException(nameof(moduleData)));
            billboard = ResourcePool.RentDisplay<ImageResource>();
            billboard.Texture = imageTexture;
            billboard.Parent = node.transform;

            Config = new ImageConfiguration();
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Image.RosMessageType:
                    Listener = new Listener<Image>(config.Topic, Handler);
                    break;
                case CompressedImage.RosMessageType:
                    Listener = new Listener<CompressedImage>(config.Topic, HandlerCompressed);
                    break;
            }
        }

        bool HandlerCompressed([NotNull] CompressedImage msg)
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

        bool Handler([NotNull] Image msg)
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

        public override void StopController()
        {
            base.StopController();
            billboard.Texture = null;
            billboard.ReturnToPool();

            imageTexture.Stop();
            imageTexture.Destroy();

            node.DestroySelf();
        }
    }
}