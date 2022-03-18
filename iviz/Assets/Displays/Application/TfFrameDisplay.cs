#nullable enable

using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Implements the visualization of a <see cref="TfFrame"/>.
    /// </summary>
    public sealed class TfFrameDisplay : TfFrame, IHighlightable, ITrailDataSource
    {
        const int TrailTimeWindowInMs = 5000;

        AxisFrameDisplay? axis;
        TextMarkerDisplay? label;
        LineConnector? parentConnector;
        TrailDisplay? trail;
        
        bool visible = true;
        float labelSize = 1.0f;
        float axisLength = 0.125f;
        bool forceInvisible;
        bool enableCollider = true;

        TrailDisplay Trail
        {
            get
            {
                if (trail != null)
                {
                    return trail;
                }

                trail = ResourcePool.RentDisplay<TrailDisplay>(TfModule.UnityFrameTransform);
                trail.TimeWindowInMs = TrailTimeWindowInMs;
                trail.Color = Color.yellow;
                trail.name = $"[Trail:{Id}]";
                return trail;
            }
        }

        TextMarkerDisplay Label
        {
            get
            {
                if (label != null)
                {
                    return label;
                }

                label = ResourcePool.RentDisplay<TextMarkerDisplay>(Transform);
                label.gameObject.name = "[Label]";
                label.Text = Id;
                label.ElementSize = 0.5f * LabelSize * FrameSize;
                label.Visible = !forceInvisible && LabelVisible && Visible;
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
                parentConnector.B = Transform.parent.CheckedNull() ?? TfModule.RootFrame.Transform;

                parentConnector.name = "[Connector]";
                parentConnector.LineWidth = FrameSize / 20;
                parentConnector.gameObject.SetActive(false);
                return parentConnector;
            }
        }
        
        AxisFrameDisplay Axis
        {
            get
            {
                if (axis != null)
                {
                    return axis;
                }

                axis = ResourcePool.RentDisplay<AxisFrameDisplay>(Transform);
                axis.EnableCollider = enableCollider;
                axis.Layer = LayerType.IgnoreRaycast;
                axis.gameObject.layer = LayerType.TfAxis;
                axis.name = "[Axis]";
                axis.Highlightable = this;
                FrameSize = FrameSize;
                return axis;
            }
        }

        public override bool Visible
        {
            get => visible;
            set
            {
                // visible in FrameNode hides children too, here we only hide the frame display 
                visible = value;
                AxisVisible = value && !forceInvisible;
                LabelVisible = LabelVisible;
                TrailVisible = TrailVisible;
            }
        }

        bool AxisVisible
        {
            set
            {
                if (axis != null || value)
                {
                    Axis.Visible = value;
                }
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
                    Label.Visible = !forceInvisible && Visible && value;
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
                    ParentConnector.Visible = !forceInvisible && Visible && value;
                }
            }
        }
        
        public override bool EnableCollider
        {
            set
            {
                enableCollider = value;
                if (axis != null)
                {
                    axis.EnableCollider = value;
                }
            }
        }

        public override float FrameSize
        {
            get => axisLength;
            set
            {
                axisLength = value;
                if (axis == null)
                {
                    return;
                }
                
                axis.AxisLength = value;
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
                Trail.DataSource = this;
            }
        }

        public Vector3 TrailPosition => Transform.position;

        public TfFrameDisplay(string id) : base(id)
        {
        }

        public override bool TrySetParent(TfFrame? parent)
        {
            if (!base.TrySetParent(parent))
            {
                return false;
            }

            if (parent is null)
            {
                if (parentConnector != null)
                {
                    ParentConnector.B = TfModule.OriginFrame.Transform;
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
            axis.ReturnToPool();
            trail.ReturnToPool();
            label.ReturnToPool();

            base.Stop();
        }
        
        public override void ForceInvisible()
        {
            forceInvisible = true;
            Visible = Visible;
            LabelVisible = LabelVisible;
            ConnectorVisible = ConnectorVisible;
        }

        public void Highlight(in Vector3 _) => Highlight();

        public override void Highlight()
        {
            if (IsAlive)
            {
                FAnimator.Start(new TfFrameHighlighter(this));
            }
        }
    }
}