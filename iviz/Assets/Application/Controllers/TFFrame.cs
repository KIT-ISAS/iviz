using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using System;
using System.Collections.ObjectModel;
using Iviz.App;

namespace Iviz.Controllers
{
    public sealed class TFFrame : ClickableNode, IRecyclable
    {
        const int MaxPoseMagnitude = 1000;
        const int Layer = 9;

        readonly Timeline timeline = new Timeline();
        TrailResource trail;
        AnchorLineResource anchor;

        GameObject labelObject;
        TextMesh labelObjectText;
        LineConnector parentConnector;
        AxisFrameResource axis;

        readonly HashSet<DisplayNode> listeners = new HashSet<DisplayNode>();
        readonly Dictionary<string, TFFrame> children = new Dictionary<string, TFFrame>();

        public ReadOnlyDictionary<string, TFFrame> Children =>
            new ReadOnlyDictionary<string, TFFrame>(children);

        [SerializeField] string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                labelObjectText.text = id;
                trail.Name = "[Trail:" + id + "]";
                //anchor.Name = "[Anchor:" + id + "]";
            }
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                labelObject.SetActive(value || LabelVisible);
            }
        }

        float alpha = 1;

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

        public void AddListener(DisplayNode display)
        {
            listeners.Add(display);
        }

        public bool ColliderEnabled
        {
            get => axis.ColliderEnabled;
            set => axis.ColliderEnabled = value;
        }

        public void RemoveListener(DisplayNode display)
        {
            if (HasNoListeners)
            {
                return;
            }

            listeners.Remove(display);
            CheckIfDead();
        }

        void AddChild(TFFrame frame)
        {
            //Debug.Log(Id + " has new child " + frame);
            children.Add(frame.Id, frame);
        }

        void RemoveChild(TFFrame frame)
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
                TFListener.Instance.MarkAsDead(this);
            }
        }

        bool forceVisible;

        public bool ForceVisible
        {
            get => forceVisible;
            set
            {
                forceVisible = value;
                Visible = Visible;
                AnchorVisible = AnchorVisible;
                TrailVisible = TrailVisible;
            }
        }

        public bool ForceInvisible { get; set; }

        bool visible;

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                axis.Visible = (value || ForceVisible) && !ForceInvisible;
                TrailVisible = TrailVisible;
                AnchorVisible = AnchorVisible;
            }
        }

        bool anchorVisible;

        bool AnchorVisible
        {
            get => anchorVisible;
            set
            {
                anchorVisible = value;
                if (value && anchor == null)
                {
                    anchor = ResourcePool.GetOrCreate<AnchorLineResource>(
                        Resource.Displays.AnchorLine,
                        TFListener.UnityFrame?.transform);
                }

                if (anchor != null)
                {
                    anchor.Visible = value && (Visible || ForceVisible);
                }
            }
        }

        bool labelVisible;

        public bool LabelVisible
        {
            get => labelVisible;
            set
            {
                labelVisible = value;
                labelObject.SetActive(!ForceInvisible && (value || Selected) && (ForceVisible || Visible));
            }
        }

        public float LabelSize
        {
            get => labelObject.transform.localScale.x;
            set => labelObject.transform.localScale = value * Vector3.one;
        }

        public bool ConnectorVisible
        {
            get => parentConnector.gameObject.activeSelf;
            set => parentConnector.gameObject.SetActive(value);
        }

        public float AxisLength
        {
            get => axis.AxisLength;
            set
            {
                axis.AxisLength = value;
                parentConnector.LineWidth = AxisLength / 20;
            }
        }

        bool trailVisible;

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

        bool acceptsParents = true;

        public bool AcceptsParents
        {
            get => acceptsParents;
            set
            {
                acceptsParents = value;
                if (!acceptsParents)
                {
                    Parent = TFListener.RootFrame;
                }
            }
        }

        public override TFFrame Parent
        {
            get => base.Parent;
            set
            {
                if (!SetParent(value))
                {
                    Logger.Error($"TFFrame: Failed to set '{value.Id}' as a parent to {Id}");
                }
            }
        }

        public bool SetParent(TFFrame newParent)
        {
            if (!AcceptsParents &&
                newParent != TFListener.RootFrame &&
                newParent != TFListener.UnityFrame &&
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

            Transform parent = transform.parent;            
            parentConnector.B = parent != null ? parent : TFListener.RootFrame.transform;

            return true;
        }

        bool IsChildOf(TFFrame frame)
        {
            if (Parent == null)
            {
                return false;
            }

            return Parent == frame || Parent.IsChildOf(frame);
        }


        Pose pose;

        public Pose WorldPose => TFListener.RelativePose(transform.AsPose());

        public Pose AbsolutePose => transform.AsPose();

        [SerializeField] Vector3 debugRosPosition;

        public void SetPose(in TimeSpan time, in Pose newPose)
        {
            pose = newPose;
            debugRosPosition = pose.position.Unity2Ros();

            if (newPose.position.sqrMagnitude > MaxPoseMagnitude * MaxPoseMagnitude)
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

            foreach (TFFrame child in children.Values)
            {
                child.LogPose(time);
            }
        }

        public Pose LookupPose(in TimeSpan time)
        {
            return timeline.Count == 0 ? pose : timeline.Lookup(time);
        }

        bool HasNoListeners => !listeners.Any();

        bool IsChildless => !children.Any();

        public override string Name => Id;

        public override Pose BoundsPose => transform.AsPose();

        void Awake()
        {
            labelObject = ResourcePool.GetOrCreate(Resource.Displays.Text, transform);
            labelObject.gameObject.SetActive(false);
            labelObjectText = labelObject.GetComponent<TextMesh>();
            labelObject.name = "[Label]";

            parentConnector = ResourcePool.GetOrCreate(Resource.Displays.LineConnector, transform)
                .GetComponent<LineConnector>();
            parentConnector.A = transform;


            // TFListener.BaseFrame may not exist yet
            Transform parent = transform.parent;
            parentConnector.B = parent != null ? parent : TFListener.RootFrame?.transform; 

            parentConnector.name = "[Connector]";

            axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrame, transform);
            
            if (Settings.IsHololens)
            {
                axis.ColliderEnabled = false;
            }
            else
            {
                axis.ColliderEnabled = true;
                axis.Layer = Layer;
            }

            axis.name = "[Axis]";

            AxisLength = 0.125f;

            parentConnector.gameObject.SetActive(false);

            UsesBoundaryBox = false;

            trail = ResourcePool.GetOrCreate<TrailResource>(Resource.Displays.Trail);
            trail.TimeWindowInMs = 5000;
            trail.Color = Color.yellow;
            TrailVisible = false;

            //anchor = ResourcePool.GetOrCreate<AnchorLine>(Resource.Displays.AnchorLine, TFListener.UnityFrame?.transform);
            //anchor.Visible = false;
        }

        public void SplitForRecycle()
        {
            ResourcePool.Dispose(Resource.Displays.AxisFrame, axis.gameObject);
            ResourcePool.Dispose(Resource.Displays.Text, labelObject);
            ResourcePool.Dispose(Resource.Displays.LineConnector, parentConnector.gameObject);
            ResourcePool.Dispose(Resource.Displays.Trail, trail.gameObject);
            axis = null;
            labelObject = null;
            parentConnector = null;
            trail = null;
        }

        public override void Stop()
        {
            base.Stop();
            Id = "";
            trail.Name = "[Trail:In Trash]";
            timeline.Clear();
            axis.Suspend();
            trail.Suspend();
            TrailVisible = false;

            if (anchor == null)
            {
                return;
            }

            anchor.Visible = false;
            anchor.Name = "[Anchor:In Trash]";
        }

        public Vector3? UpdateAnchor(IAnchorProvider anchorProvider, bool forceRebuild = false)
        {
            if (anchorProvider is null)
            {
                AnchorVisible = forceRebuild;
                return null;
            }

            AnchorVisible = true;
            anchor.AnchorProvider = anchorProvider;

            return anchor.SetPosition(transform.position, forceRebuild);
        }

        protected override void OnDoubleClick()
        {
            TFListener.GuiManager.Select(this);
            ModuleListPanel.Instance.ShowFrame(this);
        }
    }
}