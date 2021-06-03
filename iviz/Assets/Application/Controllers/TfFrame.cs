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

namespace Iviz.Controllers
{
    public sealed class TfFrame : FrameNode
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
        bool forceVisible;
        bool forceInvisible;

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
                    trail = ResourcePool.RentDisplay<TrailResource>(TfListener.UnityFrame.Transform);
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
                    labelObjectText = ResourcePool.RentDisplay<TextMarkerResource>(Transform);
                    labelObjectText.Name = "[Label]";
                    labelObjectText.Text = Id;
                    labelObjectText.transform.localScale = 0.5f * LabelSize * FrameSize * Vector3.one;
                    labelObjectText.Visible = !ForceInvisible && LabelVisible && Visible;
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
        bool HasLabelObjectText => labelObjectText != null;
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
                    LabelObjectText.Text = id;
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

                LabelObjectText.Visible = !ForceInvisible && Visible && value;
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
                ParentConnector.B = Transform.parent.CheckedNull() ?? TfListener.OriginFrame.Transform;
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
            axis.ReturnToPool();
            trail.ReturnToPool();
            labelObjectText.ReturnToPool();

            trail = null;
            axis = null;
        }
    }
}