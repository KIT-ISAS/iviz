using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.App;
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
        //const int Layer = 9;

        [SerializeField] string id;

        readonly Dictionary<string, TfFrame> children = new Dictionary<string, TfFrame>();
        readonly List<FrameNode> listeners = new List<FrameNode>();
        readonly Timeline timeline = new Timeline();

        Pose pose;

        bool parentCanChange = true;
        float alpha = 1;
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
                    trail = ResourcePool.GetOrCreateDisplay<TrailResource>(Transform);
                    trail.TimeWindowInMs = 5000;
                    trail.Color = Color.yellow;
                    trail.Name = "[Trail:" + id + "]";
                }

                return trail;
            }
        }

        bool HasTrail => trail != null;

        [NotNull] public IEnumerable<TfFrame> Children => children.Values;

        [NotNull]
        public string Id
        {
            get => id;
            set
            {
                id = value ?? throw new ArgumentNullException(nameof(value));
                labelObjectText.Text = id;
                if (HasTrail)
                {
                    Trail.Name = "[Trail:" + id + "]";
                }
            }
        }

        /*
        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                labelObjectText.Visible = value || LabelVisible;
            }
        }
        */
        const bool Selected = false;

        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                axis.ColorX = new Color(axis.ColorX.r, axis.ColorX.g, axis.ColorX.b, alpha);
                axis.ColorY = new Color(axis.ColorY.r, axis.ColorY.g, axis.ColorY.b, alpha);
                axis.ColorZ = new Color(axis.ColorZ.r, axis.ColorZ.g, axis.ColorZ.b, alpha);
            }
        }

        /*
        public override Bounds Bounds => axis.Bounds;
        public override Bounds WorldBounds => axis.WorldBounds;
        public override Vector3 BoundsScale => axis.WorldScale;
        */

        public bool ColliderEnabled
        {
            get => axis.ColliderEnabled;
            set => axis.ColliderEnabled = value;
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
                labelObjectText.Visible = !ForceInvisible && (value || Selected) && (ForceVisible || Visible);
            }
        }

        public float LabelSize
        {
            get => labelSize;
            set
            {
                labelSize = value;
                labelObjectText.transform.localScale = 0.5f * value * FrameSize * Vector3.one;
            }
        }

        public bool ConnectorVisible
        {
            get => parentConnector.Visible;
            set => parentConnector.Visible = value;
        }

        public float FrameSize
        {
            get => axis.AxisLength;
            set
            {
                axis.AxisLength = value;
                parentConnector.LineWidth = FrameSize / 20;
                labelObjectText.BillboardOffset = 1.5f * FrameSize * Vector3.up;
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

        public bool ParentCanChange
        {
            get => parentCanChange;
            set
            {
                parentCanChange = value;
                /*
                if (!parentCanChange)
                {
                    Parent = TfListener.RootFrame;
                }
                */
            }
        }

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

        public Pose WorldPose => TfListener.RelativePoseToOrigin(Transform.AsPose());

        public Pose AbsolutePose => Transform.AsPose();

        bool HasNoListeners => listeners.Count == 0;

        bool IsChildless => children.Count == 0;

        /*
        public override string Name => Id;

        public override Pose BoundsPose => transform.AsPose();
        */

        void Awake()
        {
            labelObjectText = ResourcePool.GetOrCreateDisplay<TextMarkerResource>(Transform);
            labelObjectText.Visible = false;
            labelObjectText.Name = "[Label]";
            labelObjectText.transform.localScale = 0.5f * Vector3.one;

            parentConnector = ResourcePool.GetOrCreateDisplay<LineConnector>(Transform);
            parentConnector.A = Transform;


            // TFListener.BaseFrame may not exist yet
            var parent = Transform.parent;
            parentConnector.B = parent != null
                ? parent
                : (TfListener.RootFrame != null ? TfListener.RootFrame.Transform : null);

            parentConnector.name = "[Connector]";

            axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(Transform);

            if (Settings.IsHololens)
            {
                axis.ColliderEnabled = false;
            }

            axis.ColliderEnabled = true;
            axis.Layer = LayerType.IgnoreRaycast;

            axis.name = "[Axis]";

            FrameSize = 0.125f;

            parentConnector.gameObject.SetActive(false);

            //UsesBoundaryBox = false;

            /*
            trail = ResourcePool.GetOrCreateDisplay<TrailResource>(mTransform);
            trail.TimeWindowInMs = 5000;
            trail.Color = Color.yellow;
            */
            TrailVisible = false;
        }

        public void AddListener([NotNull] FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            listeners.Add(frame);
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
            //Debug.Log(Id + " has new child " + frame);
            children.Add(frame.Id, frame);
        }

        void RemoveChild([NotNull] TfFrame frame)
        {
            if (IsChildless)
            {
                return;
            }

            //Debug.Log(Id + " loses child " + frame);
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

            var parent = Transform.parent;
            parentConnector.B = parent != null ? parent : TfListener.OriginFrame.Transform;

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
            SetPose(default(TimeSpan), newPose);
        }

        public void SetPose(in Msgs.time time, in Pose newPose)
        {
            /*
            var timestamp = time == default 
                ? TimeSpan.MaxValue 
                : time.ToTimeSpan();
            SetPose(timestamp, newPose);
                */

            SetPose(default(TimeSpan), newPose);
        }

        void SetPose(in TimeSpan time, in Pose newPose)
        {
            if (pose == newPose)
            {
                return;
            }

            pose = newPose;
            Transform.SetLocalPose(newPose);
            /*
            LogPose(time);
            */
        }

        void LogPose(in TimeSpan time)
        {
            if (listeners.Count != 0)
            {
                timeline.Add(time, WorldPose);
            }

            foreach (var child in children.Values)
            {
                child.LogPose(time);
            }
        }

        public Pose LookupPose(in TimeSpan time)
        {
            return timeline.Count == 0 ? pose : timeline.Lookup(time);
        }

        public override void Stop()
        {
            base.Stop();
            Id = "";
            timeline.Clear();
            axis.DisposeDisplay();

            if (trail != null)
            {
                trail.Name = "[Trail:In Trash]";
                trail.DisposeDisplay();
                trail = null;
            }

            axis = null;
        }
    }
}