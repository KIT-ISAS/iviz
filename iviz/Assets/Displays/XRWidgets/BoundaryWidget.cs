#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class BoundaryWidget : MonoBehaviour, IWidgetWithCaption, IWidgetWithColor, IWidgetWithBoundary, IRecyclable
    {
        FrameNode? node;
        SelectionFrame? frame;
        Tooltip? tooltip;
        Transform? mTransform;

        FrameNode Node => node ??= new FrameNode("FrameNode");
        SelectionFrame Frame  => frame != null ? frame : (frame = ResourcePool.RentDisplay<SelectionFrame>());
        
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        
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
                }
                else
                {
                    if (tooltip == null)
                    {
                        tooltip = ResourcePool.RentDisplay<Tooltip>(Node.Transform);
                        tooltip.CaptionColor = Color.white;
                        tooltip.Color = Resource.Colors.TooltipBackground;
                        tooltip.Scale = 0.25f;
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

        public BoundingBoxStamped Boundary
        {
            set
            {
                Node.AttachTo(value.Header);
                Node.Transform.SetLocalPose(value.Boundary.Center.Ros2Unity());
                Frame.Size = value.Boundary.Size.Ros2Unity().Abs();
            }
        }

        void Awake()
        {
            Transform.SetParentLocal(Node.Transform);
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

        void OnDestroy()
        {
            Transform.SetParentLocal(null);
            node?.Dispose();
        }
    }
}