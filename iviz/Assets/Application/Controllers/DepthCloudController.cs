using System;
using System.Runtime.Serialization;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class DepthCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string ColorName { get; set; } = "";
        [DataMember] public string DepthName { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 1f;
        [DataMember] public float FovAngle { get; set; } = 1.0f * Mathf.Rad2Deg;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class DepthCloudController : IController, IHasFrame
    {
        readonly DepthCloudConfiguration config = new DepthCloudConfiguration();
        readonly FrameNode node;
        readonly DepthCloudResource projector;

        ImageListener colorImage;
        ImageListener depthImage;

        public DepthCloudController([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = FrameNode.Instantiate("DepthCloud");
            projector = ResourcePool.RentDisplay<DepthCloudResource>(node.transform);
            Config = new DepthCloudConfiguration();
        }

        [NotNull]
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
                projector.Visible = value;
            }
        }

        [NotNull]
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
                projector.ElementScale = value;
            }
        }

        public float FovAngle
        {
            get => config.FovAngle;
            set
            {
                config.FovAngle = value;
                projector.FovAngle = value;
            }
        }

        [CanBeNull]
        public ImageListener ColorImage
        {
            get => colorImage;
            set
            {
                colorImage = value;
                projector.ColorImage = value?.ImageTexture;
            }
        }

        [CanBeNull]
        public ImageListener DepthImage
        {
            get => depthImage;
            set
            {
                if (depthImage != null)
                {
                    node.transform.SetParentLocal(null);
                }

                depthImage = value;
                if (depthImage != null)
                {
                    node.transform.SetParentLocal(depthImage.Node.transform);
                }

                projector.DepthImage = value?.ImageTexture;
            }
        }

        public IModuleData ModuleData { get; }

        public void StopController()
        {
            node.Stop();
            projector.ReturnToPool();
            Object.Destroy(node.gameObject);
        }

        public void ResetController()
        {
        }

        public TfFrame Frame => depthImage?.Frame;
    }
}