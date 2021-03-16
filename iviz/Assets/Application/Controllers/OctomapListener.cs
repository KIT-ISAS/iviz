using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.OctomapMsgs;
using Iviz.Octree;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class OctomapConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public SerializableColor Tint { get; set; } = new Color(0.5f, 0.5f, 1);
        [DataMember] public int MaxDepth { get; set; } = 16;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
    }

    public sealed class OctomapListener : ListenerController
    {
        static readonly Vector2 WhiteBounds = new Vector2(-10, 1);

        readonly MeshListResource resource;
        readonly FrameNode node;
        OctreeHelper helper;
        Octomap lastMsg;
        readonly PointListResource.DirectPointSetter setterFunction;

        public override IModuleData ModuleData { get; }
        public override TfFrame Frame => node.Parent;

        readonly OctomapConfiguration config = new OctomapConfiguration();

        public OctomapConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                Tint = value.Tint;
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

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                resource.Tint = value;
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                resource.OcclusionOnly = value;
            }
        }

        public int MaxDepth
        {
            get => config.MaxDepth;
            set
            {
                if (config.MaxDepth == value)
                {
                    return;
                }

                config.MaxDepth = value;
                if (lastMsg != null)
                {
                    resource.SetDirect(setterFunction);
                    resource.IntensityBounds = WhiteBounds;
                }
            }
        }

        public OctomapListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = FrameNode.Instantiate("Octomap Node");

            resource = ResourcePool.RentDisplay<MeshListResource>(node.Transform);
            resource.UseIntensityForAllScales = true;
            resource.MeshResource = Resource.Displays.Cube;
            resource.UseColormap = false;
            resource.Colormap = Resource.ColormapId.gray;

            Config = new OctomapConfiguration();

            setterFunction = SetterFunction;
        }

        public override void StartListening()
        {
            Listener = new Listener<Octomap>(config.Topic, Handler);
        }

        void Handler(Octomap msg)
        {
            node.AttachTo(msg.Header);
            lastMsg = msg;
            resource.SetDirect(setterFunction);
            resource.IntensityBounds = WhiteBounds;
        }

        void SetterFunction(ref NativeList<float4> buffer)
        {
            if (helper == null || !Mathf.Approximately(helper.Resolution, (float) lastMsg.Resolution))
            {
                helper = new OctreeHelper((float) lastMsg.Resolution);
            }

            if (lastMsg.Binary)
            {
                if (lastMsg.Id != "OcTree")
                {
                    Logger.Debug($"{this}: Unknown or unimplemented binary octomap id '{lastMsg.Id}'");
                    return;
                }

                var enumerator = helper.EnumerateLeavesBinary(lastMsg.Data, 0, MaxDepth);
                buffer.Capacity = Math.Max(buffer.Capacity, enumerator.NumberOfNodes / 2);
                foreach (var leaf in enumerator)
                {
                    buffer.Add(leaf);
                }
            }
            else
            {
                uint valueStride;
                switch (lastMsg.Id)
                {
                    case "OcTree":
                        valueStride = 4; // float value
                        break;
                    case "ColorOcTree":
                        valueStride = 7; // float value + rgb
                        break;
                    default:
                        Logger.Debug($"{this}: Unknown or unimplemented octomap id '{lastMsg.Id}'");
                        return;
                }

                var enumerator = helper.EnumerateLeaves(lastMsg.Data, 0, valueStride, MaxDepth);
                buffer.Capacity = Math.Max(buffer.Capacity, enumerator.NumberOfNodes / 2);
                foreach (var leaf in enumerator)
                {
                    buffer.Add(leaf);
                }
            }

            Logger.Debug($"{this}: Construction of octomap finished with {buffer.Length.ToString()} values");
        }

        public override void StopController()
        {
            base.StopController();
            resource.ReturnToPool();
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}