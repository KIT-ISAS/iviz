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
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.gray;
        [DataMember] public AnchorCanvas.AnchorType Anchor { get; set; } = AnchorCanvas.AnchorType.None;
        [DataMember] public float MinIntensity { get; set; } = 0.0f;
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool EnableMarker { get; set; } = false;
        [DataMember] public float MarkerScale { get; set; } = 1.0f;
    }

    public class ImageListener : TopicListener
    {
        ImageTexture texture;
        SimpleClickableDisplayNode node;
        ImageResource marker;

        public ImageTexture ImageTexture => texture;
        public Texture2D Texture => texture.Texture;
        public Material Material => texture.Material;
        public string Description => texture.Description;
        public bool IsMono => texture.IsMono;

        readonly ImageConfiguration config = new ImageConfiguration();
        public ImageConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Colormap = value.Colormap;
                //Anchor = value.anchor;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
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

        public bool EnableMarker
        {
            get => config.EnableMarker;
            set
            {
                config.EnableMarker = value;
                marker.Active = value;
            }
        }

        public float MarkerScale
        {
            get => config.MarkerScale;
            set
            {
                config.MarkerScale = value;
                marker.Scale = value;
            }
        }


        void Awake()
        {
            transform.parent = TFListener.ListenersFrame.transform;

            texture = new ImageTexture();
            node = SimpleClickableDisplayNode.Instantiate("ImageNode", transform);
            marker = ResourcePool.GetOrCreate(Resource.Markers.Image, node.transform).GetComponent<ImageResource>();
            marker.Texture = texture;
            node.Target = marker;

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
            name = "Image:" + config.Topic;
            node.name = "ImageNode:" + config.Topic;
        }

        void HandlerCompressed(CompressedImage msg)
        {
            node.SetParent(msg.Header.FrameId);

            if (msg.Format != "png")
            {
                Logger.Error("ImageListener: Can only handle png compression");
                return;
            }
            texture.SetPng(msg.Data);
        }

        void Handler(Image msg)
        {
            node.SetParent(msg.Header.FrameId);

            int width = (int)msg.Width;
            int height = (int)msg.Height;
            texture.Set(width, height, msg.Encoding, msg.Data);
        }

        public override void Stop()
        {
            texture.Stop();
        }

        void OnDestroy()
        {
            texture.Destroy();
        }

    }
}