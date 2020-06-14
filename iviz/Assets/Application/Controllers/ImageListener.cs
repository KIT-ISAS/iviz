using UnityEngine;
using System;
using Iviz.App.Displays;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Displays;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class ImageConfiguration : JsonToString, IConfiguration
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

    public class ImageListener : TopicListener
    {
        public DisplayClickableNode Node { get; private set; }
        ImageResource marker;

        public override TFFrame Frame => Node.Parent;

        public ImageTexture ImageTexture { get; private set; }
        public Texture2D Texture => ImageTexture.Texture;
        public Material Material => ImageTexture.Material;

        public int ImageWidth => Texture?.width ?? 0;
        public int ImageHeight => Texture?.height ?? 0;

        string descriptionOverride;
        public string Description => descriptionOverride ?? ImageTexture.Description;

        public bool IsMono => ImageTexture.IsMono;

        public override DisplayData DisplayData
        {
            get => Node.DisplayData;
            set => Node.DisplayData = value;
        }

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

        public Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

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
                marker.EnableBillboard = value;
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

        void Awake()
        {
            ImageTexture = new ImageTexture();
            Node = DisplayClickableNode.Instantiate("ImageNode");
            marker = ResourcePool.GetOrCreate<ImageResource>(Resource.Markers.Image);
            marker.Texture = ImageTexture;
            Node.Target = marker;

            Config = new ImageConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();
            if (config.Type == Image.RosMessageType)
            {
                Listener = new RosListener<Image>(config.Topic, Handler);
            }
            else if (config.Type == CompressedImage.RosMessageType)
            {
                Listener = new RosListener<CompressedImage>(config.Topic, HandlerCompressed);
            }
            Listener.MaxQueueSize = (int)MaxQueueSize;
            name = "Node:" + config.Topic;
            Node.SetName($"[{config.Topic}]");
        }

        void HandlerCompressed(CompressedImage msg)
        {
            Node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            switch(msg.Format)
            {
                case "png":
                    descriptionOverride = null;
                    ImageTexture.SetPng(msg.Data);
                    break;
                case "jpeg":
                    descriptionOverride = null;
                    ImageTexture.SetJpg(msg.Data);
                    break;
                default:
                    descriptionOverride = msg.Format.Length == 0 ?
                        $"<b>Desc:</b> <color=red>[empty format](?)</color>" :
                        $"<b>Desc:</b> <color=red>[{msg.Format}](?)</color>";
                    break;
            }
        }

        void Handler(Image msg)
        {
            Node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            int width = (int)msg.Width;
            int height = (int)msg.Height;
            ImageTexture.Set(width, height, msg.Encoding, msg.Data);
        }

        public override void Stop()
        {
            base.Stop();
            marker.Texture = null;
            ResourcePool.Dispose(Resource.Markers.Image, marker.gameObject);
            marker = null;

            ImageTexture.Stop();
            ImageTexture.Destroy();

            Node.Stop();
            Destroy(Node);
        }

    }
}