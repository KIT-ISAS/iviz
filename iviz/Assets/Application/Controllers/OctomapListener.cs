using System;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.OctomapMsgs;
using Iviz.Octree;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class OctomapListener : ListenerController
    {
        static readonly Vector2 WhiteBounds = new Vector2(-10, 1);

        readonly MeshListResource resource;
        readonly FrameNode node;
        OctreeHelper helper;
        Octomap lastMsg;
        readonly Action<NativeList<float4>> setterFunction;

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
                Tint = value.Tint.ToUnityColor();
            }
        }

        public override bool Visible
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
            get => config.Tint.ToUnityColor();
            set
            {
                config.Tint = value.ToRos();
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
            resource.Colormap = ColormapId.gray;

            Config = new OctomapConfiguration();

            setterFunction = SetterFunction;
        }

        public override void StartListening()
        {
            Listener = new Listener<Octomap>(config.Topic, Handler);
        }

        void Handler([NotNull] Octomap msg)
        {
            node.AttachTo(msg.Header);
            lastMsg = msg;
            resource.SetDirect(setterFunction);
            resource.IntensityBounds = WhiteBounds;
        }

        void SetterFunction(NativeList<float4> buffer)
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
                buffer.EnsureCapacity(enumerator.NumberOfNodes / 2);
                foreach (ref readonly var leaf in enumerator)
                {
                    buffer.Add(new float4(leaf.x, leaf.y, leaf.z, leaf.w));
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
                buffer.EnsureCapacity(enumerator.NumberOfNodes / 2);
                foreach (ref readonly var leaf in enumerator)
                {
                    buffer.Add(new float4(leaf.x, leaf.y, leaf.z, leaf.w));
                }
            }

            Logger.Debug($"{this}: Construction of octomap finished with {buffer.Length.ToString()} values");
        }

        public override void StopController()
        {
            base.StopController();
            resource.ReturnToPool();
            node.DestroySelf();
        }
    }
}