using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    interface IHighlightable
    {
        void Highlight();
    }
    
    public sealed class TfFrame : FrameNode, IHighlightable
    {
        const int TrailTimeWindowInMs = 5000;

        [SerializeField] string id;

        readonly Dictionary<string, TfFrame> children = new Dictionary<string, TfFrame>();
        readonly List<FrameNode> listeners = new List<FrameNode>();

        Pose pose;

        float labelSize = 1.0f;
        bool labelVisible;
        bool trailVisible;
        bool visible;
        bool forceInvisible;

        AxisFrameResource axis;
        TextMarkerResource label;
        LineConnector parentConnector;

        [CanBeNull] TrailResource trail;

        [NotNull]
        TrailResource Trail
        {
            get
            {
                if (trail == null)
                {
                    trail = ResourcePool.RentDisplay<TrailResource>(TfListener.UnityFrame.Transform);
                    trail.TimeWindowInMs = TrailTimeWindowInMs;
                    trail.Color = Color.yellow;
                    trail.Name = $"[Trail:{id}]";
                }

                return trail;
            }
        }

        [NotNull]
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

        [NotNull]
        LineConnector ParentConnector
        {
            get
            {
                if (parentConnector == null)
                {
                    parentConnector = ResourcePool.RentDisplay<LineConnector>(Transform);
                    parentConnector.A = Transform;

                    parentConnector.B = Transform.parent.CheckedNull() ?? TfListener.RootFrame.Transform;

                    parentConnector.name = "[Connector]";
                    parentConnector.LineWidth = FrameSize / 20;
                    parentConnector.Layer = LayerType.IgnoreRaycast;
                    parentConnector.gameObject.SetActive(false);
                }

                return parentConnector;
            }
        }

        bool HasTrail => trail != null;
        bool HasLabelObjectText => label != null;
        bool HasParentConnector => parentConnector != null;

        [NotNull] public Dictionary<string, TfFrame>.ValueCollection Children => children.Values;

        [NotNull]
        public string Id
        {
            get => id;
            private set
            {
                id = value ?? throw new ArgumentNullException(nameof(value));
                
                if (HasLabelObjectText)
                {
                    Label.Text = id;
                }

                if (HasTrail)
                {
                    Trail.Name = $"[Trail:{id}]";
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
                axis.Visible = value && !ForceInvisible;
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
                if (!HasLabelObjectText && !value)
                {
                    return;
                }

                Label.Visible = !ForceInvisible && Visible && value;
            }
        }

        float LabelSize
        {
            get => labelSize;
            set
            {
                labelSize = value;
                if (HasLabelObjectText)
                {
                    Label.ElementSize = 0.5f * value * FrameSize;
                }
            }
        }

        public bool ConnectorVisible
        {
            get => HasParentConnector && ParentConnector.Visible;
            set
            {
                if (!HasParentConnector && !value)
                {
                    return;
                }

                ParentConnector.Visible = !ForceInvisible && Visible && value;
            }
        }

        public float FrameSize
        {
            get => axis.AxisLength;
            set
            {
                axis.AxisLength = value;
                if (HasParentConnector)
                {
                    parentConnector.LineWidth = FrameSize / 20;
                }

                if (HasLabelObjectText)
                {
                    Label.BillboardOffset = 1.5f * FrameSize * Vector3.up;
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
                if (!HasTrail && !value)
                {
                    return;
                }

                Trail.Visible = value && Visible;
                if (value)
                {
                    Trail.DataSource = () => Transform.position;
                }
                else
                {
                    Trail.DataSource = null;
                }
            }
        }

        public bool ParentCanChange { get; set; } = true;

        public override TfFrame Parent
        {
            get => base.Parent;
            set
            {
                if (!SetParent(value))
                {
                    Logger.Error($"{this}: Failed to set '{(value != null ? value.Id : "null")}' as a parent to {Id}");
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
            axis = ResourcePool.RentDisplay<AxisFrameResource>(Transform);

            axis.ColliderEnabled = true;
            axis.Layer = LayerType.IgnoreRaycast;
            axis.gameObject.layer = Settings.IsHololens ? LayerType.IgnoreRaycast : LayerType.TfAxis;
            axis.name = "[Axis]";
            FrameSize = 0.125f;
        }

        public void Setup([NotNull] string newId)
        {
            Id = newId;
            Name = "{" + Id + "}";
        }

        public void AddListener([NotNull] FrameNode frame)
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

        public void RemoveListener([NotNull] FrameNode frame)
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

        void AddChild([NotNull] TfFrame frame)
        {
            children.Add(frame.Id, frame);
        }

        void RemoveChild([NotNull] TfFrame frame)
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

        public bool SetParent([CanBeNull] TfFrame newParent)
        {
            if (!ParentCanChange)
            {
                return false;
            }
            
            bool newParentIsNull = (newParent is null);
            TfFrame oldParent = Parent;

            if (newParentIsNull)
            {
                if (!(oldParent is null))
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = null;
                if (HasParentConnector)
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

                if (!(oldParent is null))
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = newParent;
                newParent.AddChild(this);

                if (HasParentConnector)
                {
                    ParentConnector.B = Transform.parent;
                }
            }

            return true;
        }

        static bool IsChildOf([CanBeNull] TfFrame maybeChild, [NotNull] Object frame)
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
            //if (pose == newPose)
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