#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    internal interface IHighlightable
    {
        void Highlight();
    }

    public sealed class TfFrame : FrameNode, IHighlightable
    {
        const int TrailTimeWindowInMs = 5000;

        [SerializeField] string id = "";

        readonly Dictionary<string, TfFrame> children = new();
        readonly List<FrameNode> listeners = new();

        Pose pose;

        float labelSize = 1.0f;
        bool labelVisible;
        bool trailVisible;
        bool visible;
        bool forceInvisible;

        AxisFrameResource? axis;
        TextMarkerResource? label;
        LineConnector? parentConnector;
        TrailResource? trail;

        AxisFrameResource Axis
        {
            get
            {
                if (axis != null)
                {
                    return axis;
                }

                axis = ResourcePool.RentDisplay<AxisFrameResource>(Transform);
                axis.ColliderEnabled = true;
                axis.Layer = LayerType.IgnoreRaycast;
                axis.gameObject.layer = LayerType.TfAxis;
                axis.name = "[Axis]";
                return axis;
            }
        }

        TrailResource Trail
        {
            get
            {
                if (trail != null)
                {
                    return trail;
                }

                trail = ResourcePool.RentDisplay<TrailResource>(TfListener.UnityFrame.Transform);
                trail.TimeWindowInMs = TrailTimeWindowInMs;
                trail.Color = Color.yellow;
                trail.Name = $"[Trail:{id}]";
                return trail;
            }
        }

        TextMarkerResource Label
        {
            get
            {
                if (label != null)
                {
                    return label;
                }

                label = ResourcePool.RentDisplay<TextMarkerResource>(Transform);
                label.Name = "[Label]";
                label.Text = Id;
                label.ElementSize = 0.5f * LabelSize * FrameSize;
                label.Visible = !ForceInvisible && LabelVisible && Visible;
                label.BillboardOffset = 1.5f * FrameSize * Vector3.up;
                label.Layer = LayerType.IgnoreRaycast;
                return label;
            }
        }

        LineConnector ParentConnector
        {
            get
            {
                if (parentConnector != null)
                {
                    return parentConnector;
                }

                parentConnector = ResourcePool.RentDisplay<LineConnector>(Transform);
                parentConnector.A = Transform;
                parentConnector.B = Transform.parent.CheckedNull() ?? TfListener.RootFrame.Transform;

                parentConnector.name = "[Connector]";
                parentConnector.LineWidth = FrameSize / 20;
                parentConnector.Layer = LayerType.IgnoreRaycast;
                parentConnector.gameObject.SetActive(false);
                return parentConnector;
            }
        }

        public Dictionary<string, TfFrame>.ValueCollection Children => children.Values;

        public string Id
        {
            get => id;
            private set
            {
                id = value ?? throw new ArgumentNullException(nameof(value));

                if (label != null)
                {
                    label.Text = id;
                }

                if (trail != null)
                {
                    trail.Name = $"[Trail:{id}]";
                }
            }
        }

        public bool ForceInvisible
        {
            get => forceInvisible;
            set
            {
                forceInvisible = value;
                Visible = Visible;
                LabelVisible = LabelVisible;
                ConnectorVisible = ConnectorVisible;
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                Axis.Visible = value && !ForceInvisible;
                LabelVisible = LabelVisible;
                TrailVisible = TrailVisible;
            }
        }

        public bool LabelVisible
        {
            get => labelVisible;
            set
            {
                labelVisible = value;
                if (label != null || value)
                {
                    Label.Visible = !ForceInvisible && Visible && value;
                }
            }
        }

        float LabelSize
        {
            get => labelSize;
            set
            {
                labelSize = value;
                if (label != null)
                {
                    label.ElementSize = 0.5f * value * FrameSize;
                }
            }
        }

        public bool ConnectorVisible
        {
            get => parentConnector != null && parentConnector.Visible;
            set
            {
                if (parentConnector != null || value)
                {
                    ParentConnector.Visible = !ForceInvisible && Visible && value;
                }
            }
        }

        public float FrameSize
        {
            get => Axis.AxisLength;
            set
            {
                Axis.AxisLength = value;
                if (parentConnector != null)
                {
                    parentConnector.LineWidth = FrameSize / 20;
                }

                if (label != null)
                {
                    label.BillboardOffset = 1.5f * FrameSize * Vector3.up;
                }

                LabelSize = LabelSize;
            }
        }

        public bool TrailVisible
        {
            get => trailVisible;
            set
            {
                trailVisible = value;
                if (trail == null && !value)
                {
                    return;
                }

                Trail.Visible = value && Visible;
                Trail.DataSource = value 
                    ? () => Transform.position 
                    : null;
            }
        }

        public bool ParentCanChange { get; set; } = true;

        public override TfFrame? Parent
        {
            get => base.Parent;
            set
            {
                if (!SetParent(value))
                {
                    RosLogger.Error($"{this}: Failed to set '{(value != null ? value.Id : "null")}' as a parent to {Id}");
                }
            }
        }

        /// <summary>
        /// Pose in relation to the ROS origin in Unity coordinates
        /// </summary>
        public Pose OriginWorldPose => TfListener.RelativePoseToOrigin(AbsoluteUnityPose);

        /// <summary>
        /// Pose in relation to the Unity origin in Unity coordinates
        /// </summary>
        public Pose AbsoluteUnityPose => Transform.AsPose();

        bool HasNoListeners => listeners.Count == 0;

        bool IsChildless => children.Count == 0;

        void Awake()
        {
            FrameSize = 0.125f;
        }

        public void Setup(string newId)
        {
            Id = newId;
            Name = "{" + Id + "}";
        }

        public void AddListener(FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            if (!listeners.Contains(frame))
            {
                listeners.Add(frame);
            }
        }

        public void RemoveListener(FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            if (HasNoListeners)
            {
                return;
            }

            listeners.Remove(frame);
            CheckIfDead();
        }

        void AddChild(TfFrame frame)
        {
            children.Add(frame.Id, frame);
        }

        void RemoveChild(TfFrame frame)
        {
            if (IsChildless)
            {
                return;
            }

            children.Remove(frame.Id);
        }

        void CheckIfDead()
        {
            if (listeners.RemoveAll(listener => listener == null) != 0)
            {
                Debug.LogWarning($"Frame '{id}' had a listener that was previously destroyed.");
            }

            if (HasNoListeners && IsChildless)
            {
                TfListener.Instance.MarkAsDead(this);
            }
        }

        public bool SetParent(TfFrame? newParent)
        {
            if (!ParentCanChange)
            {
                return false;
            }

            TfFrame? oldParent = Parent;

            if (newParent is null)
            {
                if (oldParent is not null)
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = null;
                if (parentConnector != null)
                {
                    ParentConnector.B = TfListener.OriginFrame.Transform;
                }
            }
            else
            {
                if (IsChildOf(newParent, this))
                {
                    newParent.CheckIfDead();
                    return false;
                }

                if (newParent == oldParent)
                {
                    return true;
                }

                if (oldParent is not null)
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = newParent;
                newParent.AddChild(this);

                if (parentConnector != null)
                {
                    ParentConnector.B = Transform.parent;
                }
            }

            return true;
        }

        static bool IsChildOf(TfFrame? maybeChild, Object frame)
        {
            int frameId = frame.GetInstanceID();

            while (true)
            {
                if (maybeChild is null)
                {
                    return false;
                }

                if (maybeChild.GetInstanceID() == frameId)
                {
                    return true;
                }

                maybeChild = maybeChild.Parent;
            }
        }

        public void SetPose(in Pose newPose)
        {
            if (pose.EqualsApprox(newPose))
            {
                return;
            }

            pose = newPose;
            Transform.SetLocalPose(newPose);
        }

        protected override void Stop()
        {
            base.Stop();
            axis.ReturnToPool();
            trail.ReturnToPool();
            label.ReturnToPool();

            trail = null;
            axis = null;
        }

        public void Highlight()
        {
            ResourcePool.RentDisplay<TfFrameHighlighter>().HighlightFrame(this);
        }
    }
}