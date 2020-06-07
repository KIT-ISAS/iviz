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
        //[DataMember] public AnchorCanvas.AnchorType Anchor { get; set; } = AnchorCanvas.AnchorType.None;
        [DataMember] public float MinIntensity { get; set; } = 0.0f;
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool EnableBillboard { get; set; } = false;
        [DataMember] public float BillboardSize { get; set; } = 1.0f;
    }

    public class ImageListener : TopicListener
    {
        ImageTexture texture;
        public DisplayClickableNode Node { get; private set; }
        ImageResource marker;

        public ImageTexture ImageTexture => texture;
        public Texture2D Texture => texture.Texture;
        public Material Material => texture.Material;

        string descriptionOverride;
        public string Description => descriptionOverride ?? texture.Description;

        public bool IsMono => texture.IsMono;

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
                //Anchor = value.anchor;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
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
                texture.Colormap = value;
            }
        }

        public Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

        /*
        public AnchorCanvas.AnchorType Anchor
        {
            get => config.anchor;
            set
            {
                config.anchor = value;
            }
        }
        */

        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                texture.MinIntensity = value;
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                texture.MaxIntensity = value;
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

        void Awake()
        {
            texture = new ImageTexture();
            Node = DisplayClickableNode.Instantiate("ImageNode");
            marker = ResourcePool.GetOrCreate<ImageResource>(Resource.Markers.Image);
            marker.Texture = texture;
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
            name = "Node:" + config.Topic;
            Node.SetName($"[{config.Topic}]");
        }

        void HandlerCompressed(CompressedImage msg)
        {
            Node.AttachTo(msg.Header.FrameId);

            if (msg.Format != "png")
            {
                //Logger.Error("ImageListener: Can only handle png compression");
                descriptionOverride = $"[Format '{msg.Format}]";
                return;
            }
            descriptionOverride = null;
            texture.SetPng(msg.Data);
        }

        void Handler(Image msg)
        {
            Node.AttachTo(msg.Header.FrameId);

            int width = (int)msg.Width;
            int height = (int)msg.Height;
            texture.Set(width, height, msg.Encoding, msg.Data);
        }

        public override void Stop()
        {
            base.Stop();
            marker.Texture = null;
            ResourcePool.Dispose(Resource.Markers.Image, marker.gameObject);
            marker = null;

            texture.Stop();
            texture.Destroy();

            Node.Stop();
            Destroy(Node);
        }

    }
}