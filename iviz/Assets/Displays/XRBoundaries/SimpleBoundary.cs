#nullable enable

using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class SimpleBoundary : MonoBehaviour, IBoundary, IRecyclable
    {
        SelectionFrame? frame;
        Tooltip? tooltip;
        Transform? mTransform;
        MeshMarkerDisplay? cube;
        Bounds bounds;

        SelectionFrame Frame => ResourcePool.RentChecked(ref frame, Transform);
        Transform Transform => this.EnsureHasTransform(ref mTransform);

        public string Id { get; set; } = "";

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
                        tooltip = CreateTooltip(Transform);
                        Update();
                    }

                    tooltip.Caption = value;
                }
            }
        }

        static Tooltip CreateTooltip(Transform transform)
        {
            var tooltip = ResourcePool.RentDisplay<Tooltip>(transform);
            tooltip.CaptionColor = Color.white;
            tooltip.Color = Resource.Colors.TooltipBackground;
            tooltip.Scale = 0.25f;
            return tooltip;
        }

        public Color Color
        {
            set
            {
                Frame.Color = value;
                Frame.EmissiveColor = value;
            }
        }

        public Color SecondColor
        {
            set
            {
                if (value.a == 0)
                {
                    if (cube != null)
                    {
                        cube.Visible = false;
                    }

                    return;
                }

                if (cube == null)
                {
                    cube = CreateCube(Transform, bounds.size);
                }

                cube.Visible = true;
                cube.Color = value;
                cube.EmissiveColor = value;
            }
        }

        static MeshMarkerDisplay CreateCube(Transform transform, in Vector3 scale)
        {
            var cube = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, transform);
            cube.Smoothness = 0;
            cube.Metallic = 0;
            cube.EnableShadows = true;
            cube.Transform.localScale = scale;
            return cube;
        }

        public float SecondaryScale
        {
            set => Frame.ColumnWidth = value;
        }

        public Vector3 Scale
        {
            set
            {
                Frame.Size = value;
                bounds.size = value;
                if (cube != null)
                {
                    cube.Transform.localScale = value;
                }
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

        public virtual void Suspend()
        {
            Caption = "";
            
            cube.ReturnToPool(Resource.Displays.Cube);
            cube = null;
        }

        public void SplitForRecycle()
        {
            frame.ReturnToPool();
            tooltip.ReturnToPool();
            cube.ReturnToPool(Resource.Displays.Cube);
        }
    }
}