using UnityEngine;
using System.Threading.Tasks;
using System;
using Unity.Collections;
using Iviz.Msgs.sensor_msgs;

namespace Iviz.App
{
    public class ImageListener : TopicListener
    {
        ImageTexture texture;
        DisplayNode node;
        ImageResource marker;

        public ImageTexture ImageTexture => texture;
        public Texture2D Texture => texture.Texture;
        public Material Material => texture.Material;
        public string Description => texture.Description;
        public bool IsMono => texture.IsMono;

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.Image;
            public string topic = "";
            public string type = "";
            public Resource.ColormapId colormap = Resource.ColormapId.gray;
            public AnchorCanvas.AnchorType anchor = AnchorCanvas.AnchorType.None;
            public float minIntensity = 0.0f;
            public float maxIntensity = 1.0f;
            public bool enableMarker;
            public float markerScale;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                config.type = value.type;
                Colormap = value.colormap;
                //Anchor = value.anchor;
                MinIntensity = value.minIntensity;
                MaxIntensity = value.maxIntensity;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.colormap;
            set
            {
                config.colormap = value;
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
            get => config.minIntensity;
            set
            {
                config.minIntensity = value;
                texture.MinIntensity = value;
            }
        }

        public float MaxIntensity
        {
            get => config.maxIntensity;
            set
            {
                config.maxIntensity = value;
                texture.MaxIntensity = value;
            }
        }

        public bool EnableMarker
        {
            get => config.enableMarker;
            set
            {
                config.enableMarker = value;
                marker.Active = value;
            }
        }

        public float MarkerScale
        {
            get => config.markerScale;
            set
            {
                config.markerScale = value;
                marker.Scale = value;
            }
        }


        void Awake()
        {
            texture = new ImageTexture();
            node = SimpleDisplayNode.Instantiate(transform);
            marker = ResourcePool.GetOrCreate(Resource.Markers.Image, node.transform).GetComponent<ImageResource>();
            marker.Texture = texture;

            Config = new Configuration();
        }

        public override void StartListening()
        {
            base.StartListening();
            if (config.type == Image.RosMessageType)
            {
                Listener = new RosListener<Image>(config.topic, Handler);
            }
            else if (config.type == CompressedImage.RosMessageType)
            {
                Listener = new RosListener<CompressedImage>(config.topic, HandlerCompressed);
            }
        }

        void HandlerCompressed(CompressedImage msg)
        {
            node.SetParent(msg.header.frame_id);

            if (msg.format != "png")
            {
                Logger.Error("ImageListener: Can only handle png compression");
                return;
            }
            texture.SetPng(msg.data);
        }

        void Handler(Image msg)
        {
            node.SetParent(msg.header.frame_id);

            int width = (int)msg.width;
            int height = (int)msg.height;
            texture.Set(width, height, msg.encoding, msg.data);
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