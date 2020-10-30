using UnityEngine;
using System;
using System.Runtime.Serialization;
using Iviz.Roslib;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class DepthCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Resource.Module Module => Resource.Module.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string ColorName { get; set; } = "";
        [DataMember] public string DepthName { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 1f;
        [DataMember] public float FovAngle { get; set; } = 1.0f * Mathf.Rad2Deg;
    }

    public sealed class DepthCloudController : IController, IHasFrame
    {
        readonly DepthCloudResource resource;
        readonly SimpleDisplayNode node;

        public IModuleData ModuleData { get; }

        public TfFrame Frame => depthImage?.Frame;

        readonly DepthCloudConfiguration config = new DepthCloudConfiguration();
        public DepthCloudConfiguration Config
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
            set => config.ColorName = value;
        }

        public string DepthName
        {
            get => config.DepthName;
            set => config.DepthName = value;
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                resource.ElementScale = value;
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
                if (!(depthImage is null))
                {
                    node.transform.SetParentLocal(null);
                }
                depthImage = value;
                if (!(depthImage is null))
                {
                    node.transform.SetParentLocal(depthImage.Node.transform);
                }
                resource.DepthImage = value?.ImageTexture;
            }
        }

        public DepthCloudController([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = SimpleDisplayNode.Instantiate("DepthImage");
            resource = ResourcePool.GetOrCreate<DepthCloudResource>(Resource.Displays.DepthImageResource, node.transform);
            Config = new DepthCloudConfiguration();
        }

        public void StopController()
        {
            resource.Suspend();
            node.Stop();
            ResourcePool.Dispose(Resource.Displays.DepthImageResource, resource.gameObject);
            UnityEngine.Object.Destroy(node.gameObject);
        }

        public void ResetController()
        {
        }
    }
}