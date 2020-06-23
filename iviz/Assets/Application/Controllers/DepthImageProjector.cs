using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Iviz.App.Displays;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class DepthImageProjectorConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Resource.Module Module => Resource.Module.DepthImageProjector;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string ColorName { get; set; } = "";
        [DataMember] public string DepthName { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 1f;
        [DataMember] public float FovAngle { get; set; } = 1.0f * Mathf.Rad2Deg;
    }

    public sealed class DepthImageProjector : MonoBehaviour, IController
    {
        DepthImageResource resource;
        SimpleDisplayNode node;

        public ModuleData ModuleData { get; set; }

        public TFFrame Frame => depthImage?.Frame;

        readonly DepthImageProjectorConfiguration config = new DepthImageProjectorConfiguration();
        public DepthImageProjectorConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                ColorName = value.ColorName;
                DepthName = value.DepthName;
                PointSize = value.PointSize;
                FovAngle = value.FovAngle;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value;
            }
        }

        public string ColorName
        {
            get => config.ColorName;
            set
            {
                config.ColorName = value;
            }
        }

        public string DepthName
        {
            get => config.DepthName;
            set
            {
                config.DepthName = value;
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                resource.PointSize = value;
            }
        }

        public float FovAngle
        {
            get => config.FovAngle;
            set
            {
                config.FovAngle = value;
                resource.FovAngle = value;
            }
        }

        ImageListener colorImage;
        public ImageListener ColorImage
        {
            get => colorImage;
            set
            {
                colorImage = value;
                resource.ColorImage = value?.ImageTexture;
            }
        }

        ImageListener depthImage;
        public ImageListener DepthImage
        {
            get => depthImage;
            set
            {
                if (depthImage != null)
                {
                    node.transform.parent = null;
                }
                depthImage = value;
                if (depthImage != null)
                {
                    node.transform.parent = depthImage.Node.transform;
                }
                resource.DepthImage = value?.ImageTexture;
            }
        }

        void Awake()
        {
            node = SimpleDisplayNode.Instantiate("DepthImage");
            resource = ResourcePool.GetOrCreate<DepthImageResource>(Resource.Displays.DepthImageResource, node.transform);
            Config = new DepthImageProjectorConfiguration();
        }

        public void Stop()
        {
            resource.Stop();
            Destroy(node.gameObject);
            ResourcePool.Dispose(Resource.Displays.DepthImageResource, resource.gameObject);
        }
    }
}