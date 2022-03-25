#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class BoundaryWidget : MonoBehaviour, IWidgetWithCaption, IWidgetWithColor, 
        IWidgetWithBoundary, IWidgetWithSecondaryScale, IRecyclable
    {
        SelectionFrame? frame;
        Tooltip? tooltip;
        Transform? mTransform;
        Bounds bounds;

        SelectionFrame Frame => ResourcePool.RentChecked(ref frame, Transform);
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public bool Interactable
        {
            set => Frame.Collider.enabled = value;
        }

        public string Caption
        {
            set
            {
                if (value.Length == 0)
                {
                    tooltip.ReturnToPool();
                    tooltip = null;
                }
                else
                {
                    if (tooltip == null)
                    {
                        tooltip = ResourcePool.RentDisplay<Tooltip>(Transform);
                        tooltip.CaptionColor = Color.white;
                        tooltip.Color = Resource.Colors.TooltipBackground;
                        tooltip.Scale = 0.25f;
                        Update();
                    }

                    tooltip.Caption = value;
                }
            }
        }

        public Color Color
        {
            set => Frame.Color = value;
        }

        public Color SecondaryColor
        {
            set => Frame.EmissiveColor = value;
        }

        public float SecondaryScale
        {
            set => Frame.ColumnWidth = value;
        }

        public BoundingBox Boundary
        {
            set
            {
                Transform.SetLocalPose(value.Center.Ros2Unity());
                
                var size = value.Size.Ros2Unity().Abs();
                Frame.Size = size;
                bounds.size = size;
            }
        }

        void Update()
        {
            if (tooltip == null)
            {
                return;
            }

            var worldBounds = bounds.TransformBound(Transform.AsPose(), Transform.lossyScale);
            float labelSize = Tooltip.GetRecommendedSize(Transform.position);
            tooltip.Scale = labelSize;
            tooltip.Transform.position = worldBounds.center +
                                         2f * (worldBounds.size.y * 0.3f + labelSize) * Vector3.up;
        }        
        
        public void Suspend()
        {
            Caption = "";
        }

        public void SplitForRecycle()
        {
            frame.ReturnToPool();
            tooltip.ReturnToPool();
        }
    }
}