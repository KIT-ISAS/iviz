using UnityEngine;
using System;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.Roslib;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class ImageConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Image;
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
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class ImageListener : ListenerController
    {
        public DisplayClickableNode Node { get; }
        ImageResource marker;

        public override TfFrame Frame => Node.Parent;

        [NotNull] public ImageTexture ImageTexture { get; }
        [CanBeNull] Texture2D Texture => ImageTexture.Texture;
        [NotNull] public Material Material => ImageTexture.Material;

        public int ImageWidth => Texture?.width ?? 0;
        public int ImageHeight => Texture?.height ?? 0;

        string descriptionOverride;
        public string Description => descriptionOverride ?? ImageTexture.Description;

        public bool IsMono => ImageTexture.IsMono;
        bool isProcessing;

        public override IModuleData ModuleData => Node.ModuleData;

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
                MaxQueueSize = value.MaxQueueSize;
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

        public uint MaxQueueSize
        {
            get => config.MaxQueueSize;
            set
            {
                config.MaxQueueSize = value;
                if (Listener != null)
                {
                    Listener.MaxQueueSize = (int)value;
                }
            }
        }

        public ImageListener([NotNull] IModuleData moduleData)
        {
            ImageTexture = new ImageTexture();
            Node = DisplayClickableNode.Instantiate("[ImageNode]");
            Node.ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData)); 
            marker = ResourcePool.GetOrCreate<ImageResource>(Resource.Displays.Image);
            marker.Texture = ImageTexture;
            Node.Target = marker;

            Config = new ImageConfiguration();
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Image.RosMessageType:
                    Listener = new RosListener<Image>(config.Topic, Handler);
                    break;
                case CompressedImage.RosMessageType:
                    Listener = new RosListener<CompressedImage>(config.Topic, HandlerCompressed);
                    break;
            }
            Listener.MaxQueueSize = (int)MaxQueueSize;
            //name = "Node:" + config.Topic;
            Node.SetName($"[{config.Topic}]");
        }

        bool HandlerCompressed(CompressedImage msg)
        {
            if (isProcessing)
            {
                return false;
            }
            
            isProcessing = true;

            void PostProcess()
            {
                Node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);
                isProcessing = false;
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
                    descriptionOverride = msg.Format.Length == 0 ?
                        $"<b>Desc:</b> <color=red>[empty format](?)</color>" :
                        $"<b>Desc:</b> <color=red>[{msg.Format}](?)</color>";
                    break;
            }

            return true;
        }

        void Handler(Image msg)
        {
            Node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            int width = (int)msg.Width;
            int height = (int)msg.Height;
            ImageTexture.Set(width, height, msg.Encoding, msg.Data);
        }

        public override void StopController()
        {
            base.StopController();
            marker.Texture = null;
            ResourcePool.Dispose(Resource.Displays.Image, marker.gameObject);
            marker = null;

            ImageTexture.Stop();
            ImageTexture.Destroy();

            Node.Stop();
            UnityEngine.Object.Destroy(Node.gameObject);
        }

    }
}