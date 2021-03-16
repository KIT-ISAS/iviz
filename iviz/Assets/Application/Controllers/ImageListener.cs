using UnityEngine;
using System;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.App;
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
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Image;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.gray;
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
        public FrameNode Node { get; }
        ImageResource marker;

        public override TfFrame Frame => Node.Parent;

        [NotNull] public ImageTexture ImageTexture { get; }
        [CanBeNull] Texture2D Texture => ImageTexture.Texture;
        [NotNull] public Material Material => ImageTexture.Material;

        public int ImageWidth => Texture != null ? Texture.width : 0;
        public int ImageHeight => Texture != null ? Texture.height : 0;

        string descriptionOverride;
        [NotNull] public string Description => descriptionOverride ?? ImageTexture.Description;

        public bool IsMono => ImageTexture.IsMono;
        bool isProcessing;
        
        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener.SetPause(value);
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

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                marker.Visible = value && config.EnableBillboard;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                ImageTexture.Colormap = value;
            }
        }

        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                ImageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                ImageTexture.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                ImageTexture.FlipMinMax = value;
            }
        }

        public bool EnableBillboard
        {
            get => config.EnableBillboard;
            set
            {
                config.EnableBillboard = value;
                marker.Visible = value && config.Visible;
            }
        }

        public float BillboardSize
        {
            get => config.BillboardSize;
            set
            {
                config.BillboardSize = value;
                marker.Scale = value;
            }
        }

        public bool BillboardFollowsCamera
        {
            get => config.BillboardFollowCamera;
            set
            {
                config.BillboardFollowCamera = value;
                marker.BillboardEnabled = value;
            }
        }

        public Vector3 BillboardOffset
        {
            get => config.BillboardOffset;
            set
            {
                config.BillboardOffset = value;
                marker.Offset = value.Ros2Unity();
            }
        }

        public ImageListener([NotNull] IModuleData moduleData)
        {
            ImageTexture = new ImageTexture();
            Node = FrameNode.Instantiate("[ImageNode]");
            this.moduleData = (ImageModuleData) (moduleData ?? throw new ArgumentNullException(nameof(moduleData)));
            marker = ResourcePool.RentDisplay<ImageResource>();
            marker.Texture = ImageTexture;
            marker.Parent = Node.transform;

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
                Node.AttachTo(msg.Header);
                IsProcessing = false;
            }

            switch (msg.Format)
            {
                case "png":
                    descriptionOverride = null;
                    ImageTexture.ProcessPng(msg.Data, PostProcess);
                    break;
                case "jpeg":
                    descriptionOverride = null;
                    ImageTexture.ProcessJpg(msg.Data, PostProcess);
                    break;
                default:
                    descriptionOverride = msg.Format.Length == 0
                        ? $"<b>Desc:</b> <color=red>[empty format](?)</color>"
                        : $"<b>Desc:</b> <color=red>[{msg.Format}](?)</color>";
                    break;
            }

            return true;
        }

        void Handler([NotNull] Image msg)
        {
            Node.AttachTo(msg.Header);

            int width = (int) msg.Width;
            int height = (int) msg.Height;
            ImageTexture.Set(width, height, msg.Encoding, msg.Data);
        }

        public override void StopController()
        {
            base.StopController();
            marker.Texture = null;
            marker.ReturnToPool();
            marker = null;

            ImageTexture.Stop();
            ImageTexture.Destroy();

            Node.Stop();
            UnityEngine.Object.Destroy(Node.gameObject);
        }
    }
}