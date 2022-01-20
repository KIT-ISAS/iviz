
using System;
using System.Linq;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
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

namespace Iviz.Controllers
{
    public sealed class OctomapListener : ListenerController
    {
        static readonly Vector2 WhiteBounds = new Vector2(-10, 1);

        readonly MeshListDisplay resource;
        readonly FrameNode node;
        [CanBeNull] OctreeHelper helper;
        [CanBeNull] Octomap lastMsg;
        readonly Action<NativeList<float4>> setterFunction;

        [CanBeNull] public override TfFrame Frame => node.Parent;

        readonly OctomapConfiguration config = new();

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

        IListener listener;
        public override IListener Listener => listener;

        public OctomapListener()
        {
            node = FrameNode.Instantiate("Octomap Node");

            resource = ResourcePool.RentDisplay<MeshListDisplay>(node.Transform);
            resource.UseIntensityForAllScales = true;
            resource.MeshResource = Resource.Displays.Cube;
            resource.UseColormap = false;
            resource.Colormap = ColormapId.gray;

            Config = new OctomapConfiguration();

            setterFunction = SetterFunction;
        }

        public void StartListening()
        {
            listener = new Listener<Octomap>(config.Topic, Handler);
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
                    RosLogger.Debug($"{this}: Unknown or unimplemented binary octomap id '{lastMsg.Id}'");
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
                        RosLogger.Debug($"{this}: Unknown or unimplemented octomap id '{lastMsg.Id}'");
                        return;
                }

                var enumerator = helper.EnumerateLeaves(lastMsg.Data, 0, valueStride, MaxDepth);
                buffer.EnsureCapacity(enumerator.NumberOfNodes / 2);
                foreach (ref readonly var leaf in enumerator)
                {
                    buffer.Add(new float4(leaf.x, leaf.y, leaf.z, leaf.w));
                }
            }

            RosLogger.Debug($"{this}: Construction of octomap finished with {buffer.Length.ToString()} values");
        }

        public override void Dispose()
        {
            base.Dispose();
            resource.ReturnToPool();
            node.Dispose();
        }
    }
}