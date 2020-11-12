using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class TfFrame : ClickableNode
    {
        const int MaxPoseMagnitude = 1000;
        const int Layer = 9;

        [SerializeField] string id;
        [SerializeField] Vector3 rosPosition;

        readonly Dictionary<string, TfFrame> children = new Dictionary<string, TfFrame>();
        readonly HashSet<DisplayNode> listeners = new HashSet<DisplayNode>();
        readonly Timeline timeline = new Timeline();

        Pose pose;

        bool acceptsParents = true;
        float alpha = 1;
        float labelSize = 1.0f;
        bool labelVisible;
        bool trailVisible;

        bool visible;
        bool forceVisible;

        AxisFrameResource axis;
        TextMarkerResource labelObjectText;
        LineConnector parentConnector;
        TrailResource trail;

        [NotNull] public ReadOnlyDictionary<string, TfFrame> Children { get; }

        [NotNull]
        public string Id
        {
            get => id;
            set
            {
                id = value ?? throw new ArgumentNullException(nameof(value));
                labelObjectText.Text = id;
                trail.Name = "[Trail:" + id + "]";
            }
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                labelObjectText.Visible = value || LabelVisible;
            }
        }

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

        public override Bounds Bounds => axis.Bounds;
        public override Bounds WorldBounds => axis.WorldBounds;
        public override Vector3 BoundsScale => axis.WorldScale;

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
                trail.Visible = value && (Visible || ForceVisible);
                if (value)
                {
                    trail.DataSource = () => transform.position;
                }
                else
                {
                    trail.DataSource = null;
                }
            }
        }

        public bool AcceptsParents
        {
            get => acceptsParents;
            set
            {
                acceptsParents = value;
                if (!acceptsParents)
                {
                    Parent = TfListener.RootFrame;
                }
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

        public Pose WorldPose => TfListener.RelativePoseToRoot(transform.AsPose());

        public Pose AbsolutePose => transform.AsPose();

        bool HasNoListeners => listeners.Count == 0;

        bool IsChildless => children.Count == 0;

        public override string Name => Id;

        public override Pose BoundsPose => transform.AsPose();

        public TfFrame()
        {
            Children = new ReadOnlyDictionary<string, TfFrame>(children);
        }

        void Awake()
        {
            Transform mTransform = transform;
            
            labelObjectText = ResourcePool.GetOrCreateDisplay<TextMarkerResource>(mTransform);
            labelObjectText.Visible = false;
            labelObjectText.Name = "[Label]";
            labelObjectText.transform.localScale = 0.5f * Vector3.one;

            parentConnector = ResourcePool.GetOrCreateDisplay<LineConnector>(mTransform);
            parentConnector.A = mTransform;


            // TFListener.BaseFrame may not exist yet
            var parent = mTransform.parent;
            parentConnector.B = parent != null ? parent : TfListener.RootFrame?.transform;

            parentConnector.name = "[Connector]";

            axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(mTransform);

            if (Settings.IsHololens)
            {
                axis.ColliderEnabled = false;
            }

            axis.ColliderEnabled = true;
            axis.Layer = Layer;

            axis.name = "[Axis]";

            FrameSize = 0.125f;

            parentConnector.gameObject.SetActive(false);

            UsesBoundaryBox = false;

            trail = ResourcePool.GetOrCreateDisplay<TrailResource>(mTransform);
            trail.TimeWindowInMs = 5000;
            trail.Color = Color.yellow;
            TrailVisible = false;
        }
        
        public void AddListener([NotNull] DisplayNode display)
        {
            if (display == null)
            {
                throw new ArgumentNullException(nameof(display));
            }

            listeners.Add(display);
        }

        public void RemoveListener([NotNull] DisplayNode display)
        {
            if (display == null)
            {
                throw new ArgumentNullException(nameof(display));
            }

            if (HasNoListeners)
            {
                return;
            }

            listeners.Remove(display);
            CheckIfDead();
        }

        void AddChild(TfFrame frame)
        {
            //Debug.Log(Id + " has new child " + frame);
            children.Add(frame.Id, frame);
        }

        void RemoveChild(TfFrame frame)
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
            if (!AcceptsParents &&
                newParent != TfListener.RootFrame &&
                newParent != TfListener.UnityFrame &&
                newParent != null)
            {
                return false;
            }

            if (newParent == Parent)
            {
                return true;
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

            var parent = transform.parent;
            parentConnector.B = parent != null ? parent : TfListener.RootFrame.transform;

            return true;
        }

        bool IsChildOf(TfFrame frame)
        {
            if (Parent == null)
            {
                return false;
            }

            return Parent == frame || Parent.IsChildOf(frame);
        }

        public void SetPose(in TimeSpan time, in Pose newPose)
        {
            pose = newPose;
            rosPosition = pose.position.Unity2Ros();

            if (newPose.position.MagnitudeSq() > MaxPoseMagnitude * MaxPoseMagnitude)
            {
                return; // lel
            }

            transform.SetLocalPose(newPose);
            LogPose(time);
        }

        void LogPose(in TimeSpan time)
        {
            if (listeners.Count != 0)
            {
                timeline.Add(time, transform.AsPose());
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
            trail.Name = "[Trail:In Trash]";
            timeline.Clear();
            axis.DisposeDisplay();
            trail.DisposeDisplay();
            axis = null;
            trail = null;
        }

        protected override void OnDoubleClick()
        {
            TfListener.GuiCamera.Select(this);
            ModuleListPanel.Instance.ShowFrame(this);
        }
    }
}