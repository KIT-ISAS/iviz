using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Iviz.App.Displays;
using System;

namespace Iviz.App.Listeners
{
    public sealed class TFFrame : ClickableNode, IRecyclable
    {
        const int MaxPoseMagnitude = 1000;

        public const int Layer = 9;

        readonly Timeline timeline = new Timeline();

        [SerializeField] string id_;
        public string Id
        {
            get => id_;
            set
            {
                id_ = value;
                labelObjectText.text = id_;
            }
        }

        [SerializeField] bool forceInvisible_;
        public bool ForceInvisible
        {
            get => forceInvisible_;
            set
            {
                forceInvisible_ = value;
                LabelVisible = LabelVisible; // update
            }
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                selected_ = value;
                labelObject.SetActive(value || LabelVisible);
                if (value)
                {
                    TFListener.GuiManager.ShowBoundary(null);
                }
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
            children.Add(frame.Id, frame);
        }

        public void RemoveChild(TFFrame frame)
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
                TFListener.Instance.MarkAsDead(this);
            }
        }

        public bool AxisVisible
        {
            get => resource.Visible;
            set { resource.Visible = value; }
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
            set
            {
                labelObject.transform.localScale = value * Vector3.one;
            }
        }

        public bool ConnectorVisible
        {
            get => parentConnector.gameObject.activeSelf;
            set
            {
                parentConnector.gameObject.SetActive(value);
            }
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


        public override TFFrame Parent
        {
            get => base.Parent;
            set
            {
                SetParent(value);
            }
        }


        public bool SetParent(TFFrame newParent)
        {
            if (newParent == Parent)
            {
                return true;
            }
            if (newParent == this)
            {
                Logger.Error($"TFFrame: Cannot set '{newParent.Id}' as a parent to itself!");
                return false;
            }
            if (newParent != null && newParent.IsChildOf(this))
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
            if (Parent != null)
            {
                Parent.RemoveChild(this);
            }
            base.Parent = newParent;
            if (Parent != null)
            {
                Parent.AddChild(this);
            }

            parentConnector.B = transform.parent != null ?
                transform.parent :
                TFListener.BaseFrame.transform;

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
        public Pose Pose => pose;

        public Pose WorldPose => transform.AsPose();

        [SerializeField] Vector3 rosPosition_;

        public void SetPose(in TimeSpan time, in Pose newPose)
        {
            if (!IgnoreUpdates)
            {
                if (newPose.position.sqrMagnitude > MaxPoseMagnitude * MaxPoseMagnitude)
                {
                    return; // lel
                }

                pose = newPose;
                rosPosition_ = pose.position.Unity2Ros();
                transform.SetLocalPose(newPose);
                LogPose(time);
            }
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
                TFListener.BaseFrame?.transform; // TFListener.BaseFrame may not exist yet
            parentConnector.name = "[Connector]";

            resource = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrameResource, transform);
            resource.Layer = Layer;
            resource.name = "[Axis]";

            AxisLength = 0.25f;
            //OrbitColorEnabled = false;

            parentConnector.gameObject.SetActive(false);
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.AxisFrameResource, resource.gameObject);
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

        /*
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (GetClickCount(eventData) == 3)
            {
                TFListener.GuiManager.StartOrbitingAround(OrbitColorEnabled ? null : this);
            }
        }
        */

    }
}
