#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class TfFrameDisplay : TfFrame, IHighlightable
    {
        const int TrailTimeWindowInMs = 5000;

        float labelSize = 1.0f;
        
        AxisFrameResource? axis;
        TextMarkerResource? label;
        LineConnector? parentConnector;
        TrailResource? trail;
        bool visible = true;

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
                trail.Name = $"[Trail:{Id}]";
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
                label.gameObject.name = "[Label]";
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

        public override string Id
        {
            get => base.Id;
            protected set
            {
                base.Id = value ?? throw new ArgumentNullException(nameof(value));

                if (label != null)
                {
                    label.Text = base.Id;
                }

                if (trail != null)
                {
                    trail.Name = $"[Trail:{base.Id}]";
                }
            }
        }

        public override bool ForceInvisible
        {
            get => base.ForceInvisible;
            set
            {
                base.ForceInvisible = value;
                Visible = Visible;
                LabelVisible = LabelVisible;
                ConnectorVisible = ConnectorVisible;
            }
        }

        public override bool Visible
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

        public override bool LabelVisible
        {
            get => base.LabelVisible;
            set
            {
                base.LabelVisible = value;
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

        public override bool ConnectorVisible
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

        public override float FrameSize
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

        public override bool TrailVisible
        {
            get => base.TrailVisible;
            set
            {
                base.TrailVisible = value;
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

        void Awake()
        {
            FrameSize = 0.125f;
        }

        public override bool SetParent(TfFrame? newParent)
        {
            if (!ParentCanChange)
            {
                return false;
            }
            
            if (!IsAlive)
            {
                return false; // destroying!
            }

            base.SetParent(newParent);

            if (newParent is null)
            {
                if (parentConnector != null)
                {
                    ParentConnector.B = TfListener.OriginFrame.Transform;
                }
            }
            else
            {
                if (parentConnector != null)
                {
                    ParentConnector.B = Transform.parent;
                }
            }

            return true;
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

        public override void Highlight()
        {
            if (IsAlive)
            {
                FAnimator.Start(new TfFrameHighlighter(this));
            }
        }
    }
}