using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Iviz.App.Displays;
using System;
using System.Collections.ObjectModel;

namespace Iviz.App.Listeners
{
    public sealed class TFFrame : ClickableNode, IRecyclable
    {
        const int MaxPoseMagnitude = 1000;

        public const int Layer = 9;

        readonly Timeline timeline = new Timeline();
        TrailResource trail;

        [SerializeField] string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                labelObjectText.text = id;
            }
        }

        [SerializeField] bool forceInvisible;
        public bool ForceInvisible
        {
            get => forceInvisible;
            set
            {
                forceInvisible = value;
                if (value)
                {
                    resource.Visible = false;
                }
                LabelVisible = LabelVisible; // update
            }
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                //selected_ = value;
                base.Selected = value;
                labelObject.SetActive(value || LabelVisible);
                /*
                if (value)
                {
                    TFListener.GuiManager.ShowBoundary(null);
                }
                */
            }
        }

        public bool IgnoreUpdates { get; set; }

        GameObject labelObject;
        TextMesh labelObjectText;
        LineConnector parentConnector;
        BoxCollider boxCollider;
        AxisFrameResource resource;

        readonly HashSet<DisplayNode> listeners = new HashSet<DisplayNode>();
        readonly Dictionary<string, TFFrame> children = new Dictionary<string, TFFrame>();

        public ReadOnlyDictionary<string, TFFrame> Children =>
            new ReadOnlyDictionary<string, TFFrame>(children);

        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;
        public override Vector3 BoundsScale => Vector3.one;

        public void AddListener(DisplayNode display)
        {
            listeners.Add(display);
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

        public void AddChild(TFFrame frame)
        {
            //Debug.Log(Id + " has new child " + frame);
            children.Add(frame.Id, frame);
        }

        public void RemoveChild(TFFrame frame)
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

        public bool AxisVisible
        {
            get => resource.Visible;
            set => resource.Visible = value && !ForceInvisible;
        }

        bool labelVisible;
        public bool LabelVisible
        {
            get => labelVisible;
            set
            {
                labelVisible = value;
                labelObject.SetActive(!ForceInvisible && (value || Selected));
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
            get => resource.AxisLength;
            set
            {
                resource.AxisLength = value;
                parentConnector.LineWidth = AxisLength / 20;
            }
        }

        public bool TrailVisible
        {
            get => trail.enabled;
            set
            {
                trail.enabled = value;
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

        public override TFFrame Parent
        {
            get => base.Parent;
            set => SetParent(value);
        }


        public bool SetParent(TFFrame newParent)
        {
            //Debug.Log("a child " + Id + " parent " + newParent?.Id);
            if (newParent == Parent)
            {
                return true;
            }
            //Debug.Log("b child " + Id + " parent " + newParent?.Id);
            if (newParent == this)
            {
                Logger.Error($"TFFrame: Cannot set '{newParent.Id}' as a parent to itself!");
                return false;
            }
            //Debug.Log("c child " + Id + " parent " + newParent?.Id);
            if (!(newParent is null) && newParent.IsChildOf(this))
            {
                Logger.Error($"TFFrame: Cannot set '{newParent.Id}' as parent to '{Id}' because it causes a cycle!");
                newParent.CheckIfDead();
                return false;
            }
            /*
            if (!timeline.Empty && time < timeline.LastTime)
            {
                return true; //??
            }
            */
            //Debug.Log("d child " + Id + " parent " + newParent?.Id);
            Parent?.RemoveChild(this);
            //Debug.Log("3 child " + Id + " parent " + newParent?.Id);
            base.Parent = newParent;
            //Debug.Log("2 child " + Id + " parent " + newParent?.Id);
            Parent?.AddChild(this);
            //Debug.Log("1 child " + Id + " parent " + newParent?.Id);

            parentConnector.B = transform.parent ?? TFListener.MapFrame.transform;

            return true;
        }

        bool IsChildOf(TFFrame frame)
        {
            if (Parent is null)
            {
                return false;
            }
            return Parent == frame || Parent.IsChildOf(frame);
        }


        Pose pose;
        public Pose Pose => pose;

        public Pose WorldPose => TFListener.RelativePose(transform.AsPose());

        public Pose AbsolutePose => transform.AsPose();

        [SerializeField] Vector3 rosPosition_;

        public void SetPose(in TimeSpan time, in Pose newPose)
        {
            if (IgnoreUpdates)
            {
                return;
            }

            pose = newPose;
            rosPosition_ = pose.position.Unity2Ros();

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
            //Debug.Log(timeline.Count + " " + (timeline.LastTime - timeline.FirstTime).Milliseconds);
        }

        public Pose GetPose(in TimeSpan time)
        {
            if (timeline.Count == 0)
            {
                return Pose;
            }
            return timeline.Get(time);
        }

        public bool HasNoListeners => !listeners.Any();

        public bool IsChildless => !children.Any();

        public override string Name => Id;

        public override Pose BoundsPose => transform.AsPose();

        void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();

            labelObject = ResourcePool.GetOrCreate(Resource.Displays.Text, transform);
            labelObject.gameObject.SetActive(false);
            labelObject.name = "Frame Axis Label";
            labelObjectText = labelObject.GetComponent<TextMesh>();
            labelObject.name = "[Label]";

            parentConnector = ResourcePool.
                GetOrCreate(Resource.Displays.LineConnector, transform).
                GetComponent<LineConnector>();
            parentConnector.name = "Parent Connector";
            parentConnector.A = transform;
            parentConnector.B = transform.parent != null ?
                transform.parent :
                TFListener.MapFrame?.transform; // TFListener.BaseFrame may not exist yet
            parentConnector.name = "[Connector]";

            resource = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrame, transform);
            resource.Layer = Layer;
            resource.name = "[Axis]";

            AxisLength = 0.25f;
            //OrbitColorEnabled = false;

            parentConnector.gameObject.SetActive(false);

            UsesBoundaryBox = false;

            trail = GetComponent<TrailResource>();
            trail.TimeWindowInMs = 5000;
            trail.Color = Color.yellow;
            TrailVisible = false;
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.AxisFrame, resource.gameObject);
            ResourcePool.Dispose(Resource.Displays.Text, labelObject);
            ResourcePool.Dispose(Resource.Displays.LineConnector, parentConnector.gameObject);
            resource = null;
            labelObject = null;
            parentConnector = null;
        }

        public override void Stop()
        {
            base.Stop();
            Id = "";
            timeline.Clear();
        }

    }
}
