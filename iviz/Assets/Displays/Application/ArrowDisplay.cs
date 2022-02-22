#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ArrowDisplay : MarkerDisplay, ISupportsColor, ISupportsPbr, ISupportsTint,
        ISupportsAROcclusion, ISupportsShadows
    {
        const float MaxArrowWidth = 5.0f;

        [SerializeField] MeshMarkerDisplay? arrow;

        MeshMarkerDisplay Arrow => arrow.AssertNotNull(nameof(arrow));

        public Color Color
        {
            set => Arrow.Color = value;
        }

        public Color EmissiveColor
        {
            set => Arrow.EmissiveColor = value;
        }

        void Awake()
        {
            Reset();
        }

        public void Set(in Vector3 a, in Vector3 b, float? overrideScaleXY = null)
        {
            var direction = b - a;
            float scaleZ = direction.Magnitude();

            if (scaleZ.ApproximatelyZero())
            {
                Reset();
                return;
            }

            float scaleXY = overrideScaleXY ?? Math.Min(scaleZ * 0.15f, MaxArrowWidth);

            Arrow.Transform.localScale = new Vector3(scaleXY, scaleXY, scaleZ);
            Arrow.Transform.localPosition = a;
            Arrow.Transform.localRotation = Quaternion.LookRotation(direction);
            UpdateBounds();
        }

        public void Set(in Vector3 scale)
        {
            Arrow.Transform.localScale = new Vector3(scale.z, scale.y, scale.x);
            UpdateBounds();
        }

        public void Reset()
        {
            Arrow.Transform.localScale = Vector3.zero;
            UpdateBounds();
        }

        void UpdateBounds()
        {
            Collider.SetLocalBounds(Arrow.Bounds is { } bounds 
                ? bounds.TransformBound(Arrow.Transform) 
                : default);
        }

        public override void Suspend()
        {
            Arrow.Suspend();
            Reset();
        }

        public float Metallic
        {
            set => Arrow.Metallic = value;
        }

        public float Smoothness
        {
            set => Arrow.Smoothness = value;
        }

        public Color Tint
        {
            set => Arrow.Tint = value;
        }

        public bool OcclusionOnly
        {
            set => Arrow.OcclusionOnly = value;
        }

        public bool EnableShadows
        {
            set => Arrow.EnableShadows = value;
        }
    }
}