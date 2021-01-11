using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class TfFrame : FrameNode
    {
        const int TrailTimeWindowInMs = 5000;

        [SerializeField] string id;

        readonly Dictionary<string, TfFrame> children = new Dictionary<string, TfFrame>();
        readonly HashSet<FrameNode> nodes = new HashSet<FrameNode>();

        Pose pose;

        float labelSize = 1.0f;
        bool labelVisible;
        bool trailVisible;
        bool visible;
        bool forceVisible;

        AxisFrameResource axis;
        TextMarkerResource labelObjectText;
        LineConnector parentConnector;

        [CanBeNull] TrailResource trail;

        [NotNull]
        TrailResource Trail
        {
            get
            {
                if (trail == null)
                {
                    trail = ResourcePool.GetOrCreateDisplay<TrailResource>(TfListener.UnityFrame.Transform);
                    trail.TimeWindowInMs = TrailTimeWindowInMs;
                    trail.Color = Color.yellow;
                    trail.Name = $"[Trail:{id}]";
                }

                return trail;
            }
        }

        [NotNull]
        TextMarkerResource LabelObjectText
        {
            get
            {
                if (labelObjectText == null)
                {
                    labelObjectText = ResourcePool.GetOrCreateDisplay<TextMarkerResource>(Transform);
                    labelObjectText.Name = "[Label]";
                    labelObjectText.Text = Id;
                    labelObjectText.transform.localScale = 0.5f * LabelSize * FrameSize * Vector3.one;
                    labelObjectText.Visible = !ForceInvisible && LabelVisible && (ForceVisible || Visible);
                    labelObjectText.BillboardOffset = 1.5f * FrameSize * Vector3.up;
                    labelObjectText.Layer = LayerType.IgnoreRaycast;
                }

                return labelObjectText;
            }
        }
        
        [NotNull]
        LineConnector ParentConnector
        {
            get
            {
                if (parentConnector == null)
                {
                    parentConnector = ResourcePool.GetOrCreateDisplay<LineConnector>(Transform);
                    parentConnector.A = Transform;

                    parentConnector.B = Transform.parent.SafeNull() ?? TfListener.RootFrame.Transform;

                    parentConnector.name = "[Connector]";
                    parentConnector.LineWidth = FrameSize / 20;
                    parentConnector.Layer = LayerType.IgnoreRaycast;
                    parentConnector.gameObject.SetActive(false);
                }

                return parentConnector;
            }
        }

        bool HasTrail => trail != null;
        bool HasLabelObjectText => labelObjectText != null;
        bool HasParentConnector => parentConnector != null;

        [NotNull] public IEnumerable<TfFrame> Children => children.Values;

        [NotNull]
        public string Id
        {
            get => id;
            set
            {
                id = value ?? throw new ArgumentNullException(nameof(value));
                if (HasLabelObjectText)
                {
                    LabelObjectText.Text = id;
                }

                if (HasTrail)
                {
                    Trail.Name = $"[Trail:{id}]";
                }
            }
        }

        bool ForceVisible
        {
            get => forceVisible;
            set
            {
                forceVisible = value;
                Visible = Visible;
                TrailVisible = TrailVisible;
            }
        }

        public bool ForceInvisible { get; set; }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                axis.Visible = (value || ForceVisible) && !ForceInvisible;
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

                LabelObjectText.Visible = !ForceInvisible && (ForceVisible || Visible);
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
                    LabelObjectText.transform.localScale = 0.5f * value * FrameSize * Vector3.one;
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
                
                ParentConnector.Visible = value;   
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
                    LabelObjectText.BillboardOffset = 1.5f * FrameSize * Vector3.up;
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

                Trail.Visible = value && (Visible || ForceVisible);
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
                    Logger.Error($"TFFrame: Failed to set '{(value != null ? value.Id : "null")}' as a parent to {Id}");
                }
            }
        }

        /// <summary>
        /// Pose in relation to the ROS origin in Unity coordinates
        /// </summary>
        public Pose WorldPose => TfListener.RelativePoseToOrigin(UnityWorldPose);

        /// <summary>
        /// Pose in relation to the Unity origin in Unity coordinates
        /// </summary>
        public Pose UnityWorldPose => Transform.AsPose();

        bool HasNoListeners => nodes.Count == 0;

        bool IsChildless => children.Count == 0;

        void Awake()
        {
            axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(Transform);

            axis.ColliderEnabled = true;
            axis.Layer = LayerType.IgnoreRaycast;
            axis.name = "[Axis]";
            FrameSize = 0.125f;
        }

        public void AddListener([NotNull] FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            nodes.Add(frame);
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

            nodes.Remove(frame);
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
            if (HasNoListeners && IsChildless)
            {
                TfListener.Instance.MarkAsDead(this);
            }
        }

        public bool SetParent([CanBeNull] TfFrame newParent)
        {
            if (newParent == Parent)
            {
                return true;
            }

            if (!ParentCanChange &&
                newParent != TfListener.RootFrame &&
                newParent != TfListener.UnityFrame &&
                newParent != null)
            {
                return false;
            }

            if (newParent == this)
            {
                return false;
            }

            if (newParent != null && newParent.IsChildOf(this))
            {
                newParent.CheckIfDead();
                return false;
            }

            if (Parent != null)
            {
                Parent.RemoveChild(this);
            }

            base.Parent = newParent;
            if (Parent != null)
            {
                Parent.AddChild(this);
            }

            if (HasParentConnector)
            {
                ParentConnector.B = Transform.parent.SafeNull() ?? TfListener.OriginFrame.Transform;
            }

            return true;
        }

        bool IsChildOf([NotNull] TfFrame frame)
        {
            if (Parent == null)
            {
                return false;
            }

            return Parent == frame || Parent.IsChildOf(frame);
        }

        public void SetPose(in Pose newPose)
        {
            if (pose == newPose)
            {
                return;
            }

            pose = newPose;
            Transform.SetLocalPose(newPose);
        }

        public override void Stop()
        {
            base.Stop();
            axis.DisposeDisplay();
            trail.DisposeDisplay();
            labelObjectText.DisposeDisplay();

            trail = null;
            axis = null;
        }
    }
}