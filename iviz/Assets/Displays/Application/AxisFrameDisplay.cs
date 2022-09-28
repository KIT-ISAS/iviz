#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;
using Iviz.Resources;
using Iviz.Tools;

namespace Iviz.Displays
{
    /// <summary>
    /// Display that uses three cubes to represent an axis frame.
    /// Used mostly by <see cref="TfFrameDisplay"/> and <see cref="CameraOverlayDisplay"/>.
    /// </summary>
    public sealed class AxisFrameDisplay : MeshMarkerHolderDisplay, IRecyclable, IHighlightable
    {
        static readonly string[] Names = {"Axis-X", "Axis-Y", "Axis-Z"};

        float axisLength;

        MeshMarkerDisplay[] Frames => Children.Length != 0
            ? Children
            : Children = new[]
            {
                ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, Transform),
                ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, Transform),
                ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, Transform),
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

        public void SplitForRecycle()
        {
            foreach (var child in Children)
            {
                child.ReturnToPool(Resource.Displays.Cube);
            }

            Children = Array.Empty<MeshMarkerDisplay>();
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