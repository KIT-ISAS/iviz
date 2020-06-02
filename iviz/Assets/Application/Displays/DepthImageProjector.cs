using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;

namespace Iviz.App.Displays
{
    [DataContract]
    public class DepthImageProjectorConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Resource.Module Module => Resource.Module.DepthImageProjector;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string ColorName { get; set; } = "";
        [DataMember] public string DepthName { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 1f;
        [DataMember] public float FovAngle { get; set; } = 1.0f * Mathf.Rad2Deg;
    }

    public class DepthImageProjector : MonoBehaviour, IController
    {
        DepthImageResource resource;
        DisplayClickableNode node;

        public DisplayData DisplayData { get; set; }

        readonly DepthImageProjectorConfiguration config = new DepthImageProjectorConfiguration();
        public DepthImageProjectorConfiguration Config
        {
            get => config;
            set
            {
                ColorName = value.ColorName;
                DepthName = value.DepthName;
                PointSize = value.PointSize;
                FovAngle = value.FovAngle;
            }
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(this);
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

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
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
                depthImage = value;
                resource.DepthImage = value?.ImageTexture;
            }
        }

        void Awake()
        {
            //resource = Resource.
            Config = new DepthImageProjectorConfiguration();
        }

        public void Stop()
        {
            resource.Stop();
        }
    }
}