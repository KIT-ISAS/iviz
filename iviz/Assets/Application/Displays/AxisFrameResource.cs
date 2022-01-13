#nullable enable

using Iviz.Common;
using Iviz.Core;
using UnityEngine;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.Displays
{
    public sealed class AxisFrameResource : MeshMarkerHolderResource, IRecyclable, IHighlightable
    {
        static readonly string[] Names = {"Axis-X", "Axis-Y", "Axis-Z"};

        float axisLength;

        MeshMarkerResource[] Frames => children.Length != 0
            ? children
            : children = new[]
            {
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, Transform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, Transform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, Transform),
            };

        public float AxisLength
        {
            get => axisLength;
            set
            {
                axisLength = value;
                UpdateFrameMesh(axisLength, axisLength / 10);
            }
        }

        public Color ColorX
        {
            set => Frames[0].Color = value;
        }

        public Color ColorY
        {
            set => Frames[1].Color = value;
        }

        public Color ColorZ
        {
            set => Frames[2].Color = value;
        }

        public float Emissive
        {
            set
            {
                Frames[0].EmissiveColor = value * Frames[0].Color;
                Frames[1].EmissiveColor = value * Frames[1].Color;
                Frames[2].EmissiveColor = value * Frames[2].Color;
            }
        }
        
        public IHighlightable? Highlightable { get; set; }

        void Awake()
        {
            foreach (var (frame, frameName) in Frames.Zip(Names))
            {
                frame.gameObject.name = frameName;
                frame.EnableCollider = false;
                frame.Layer = Layer;
            }

            AxisLength = 0.25f;

            ColorX = Resource.Colors.AxisX;
            ColorY = Resource.Colors.AxisY;
            ColorZ = Resource.Colors.AxisZ;
        }

        void UpdateFrameMesh(float newFrameAxisLength, float newFrameAxisWidth)
        {
            var frames = Frames;
            frames[0].Transform.localScale = new Vector3(newFrameAxisLength, newFrameAxisWidth, newFrameAxisWidth);
            frames[0].Transform.localPosition = -0.5f * newFrameAxisLength * Vector3.right;
            frames[1].Transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisWidth, newFrameAxisLength);
            frames[1].Transform.localPosition = newFrameAxisLength * new Vector3(0, 0.001f, -0.5f);
            frames[2].Transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisLength, newFrameAxisWidth);
            frames[2].Transform.localPosition = newFrameAxisLength * new Vector3(0.001f, 0.5f, 0.001f);

            Collider.center = 0.5f * (newFrameAxisLength - newFrameAxisWidth / 2) * new Vector3(-1, 1, -1);
            Collider.size = (newFrameAxisLength + newFrameAxisWidth / 2) * Vector3.one;
        }

        public void OverrideMaterial(Material? material)
        {
            foreach (var frame in Frames)
            {
                frame.OverrideMaterial(material);
            }
        }

        public void SplitForRecycle()
        {
            foreach (var child in children)
            {
                child.ReturnToPool(Resource.Displays.Cube);
            }
        }

        public override void Suspend()
        {
            base.Suspend();

            OverrideMaterial(null);
            AxisLength = 0.25f;
            ColorX = Resource.Colors.AxisX;
            ColorY = Resource.Colors.AxisY;
            ColorZ = Resource.Colors.AxisZ;
            OcclusionOnly = false;
            Tint = Color.white;
            Emissive = 0;
        }
        
        public bool IsAlive => Highlightable?.IsAlive ?? false;
        
        public void Highlight(in Vector3 hitPoint) => Highlightable?.Highlight(hitPoint);
    }
}