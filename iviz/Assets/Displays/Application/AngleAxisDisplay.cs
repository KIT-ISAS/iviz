#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class AngleAxisDisplay : DisplayWrapper, ISupportsTint, IRecyclable
    {
        readonly List<float4x2> lines = new();

        Color color;
        LineDisplay? resource;

        LineDisplay Resource => ResourcePool.RentChecked(ref resource, Transform);

        protected override IDisplay Display => Resource;

        public Color Color
        {
            get => color;
            set
            {
                color = value;

                float colorAsFloat = UnityUtils.AsFloat(color);
                var linesAsSpan = lines.AsSpan();
                foreach (ref var line in linesAsSpan)
                {
                    line.c0.w = colorAsFloat;
                    line.c1.w = colorAsFloat;
                }

                Resource.Set(linesAsSpan, color.a < 1);
            }
        }

        void Awake()
        {
            Resource.ElementScale = 0.005f;
            Resource.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;
            Color = Color.yellow.WithSaturation(0.75f);
            Layer = LayerType.IgnoreRaycast;
        }

        public void Reset()
        {
            Resource.Visible = false;
        }

        public Color Tint
        {
            set => Resource.Tint = value;
        }

        public void Set(in Quaternion q, float scale = 0.3f)
        {
            var axis = new Vector3(q.x, q.y, q.z);

            float sin = axis.Magnitude();
            float cos = q.w;

            if (sin.ApproximatelyZero())
            {
                Resource.Visible = false;
                return;
            }

            float angle = Mathf.Atan2(sin, cos) * 2;
            Set(angle, axis / sin, scale);
        }

        public void Set(in Vector3 rod, float scale = 0.3f)
        {
            float angle = rod.Magnitude();
            if (angle.ApproximatelyZero())
            {
                Resource.Visible = false;
                return;
            }

            Vector3 axis = rod / angle;
            Set(angle, axis, scale);
        }

        void Set(float angle, in Vector3 axis, float scale)
        {
            ValidatedSet(-angle, axis, scale);
        }

        void ValidatedSet(float angle, in Vector3 axis, float scale)
        {
            Resource.Visible = true;
            lines.Clear();

            var notAxis = (Math.Abs(axis.z) - 1).ApproximatelyZero()
                ? Vector3.right
                : Vector3.forward;

            var dirY = notAxis.Cross(axis).Normalized() * scale;
            var dirX = axis.Cross(dirY);

            int n = (int)(Math.Abs(angle) / (2 * Mathf.PI) * 32 + 1);

            float4x2 v = new();
            ref float4 v0 = ref v.c0;
            ref float4 v1 = ref v.c1;

            float colorAsFloat = UnityUtils.AsFloat(color);
            v0.w = colorAsFloat;
            v1.w = colorAsFloat;

            (v1.x, v1.y, v1.z) = 1.2f * dirX;
            
            lines.Add(v);

            const float scaleFromAngle = 0.02f;

            (v0.x, v0.y, v0.z) = dirX;
            foreach (int i in 1..(n + 1))
            {
                float a = i / (float)n * angle;
                float segmentScale = 1 + a * scaleFromAngle;
                float ax = Mathf.Cos(a) * segmentScale;
                float ay = Mathf.Sin(a) * segmentScale;
                (v1.x, v1.y, v1.z) = ax * dirX + ay * dirY;
                lines.Add(v);

                v0.x = v1.x;
                v0.y = v1.y;
                v0.z = v1.z;
            }

            v0.x = 0;
            v0.y = 0;
            v0.z = 0;
            v1.x *= 1.2f;
            v1.y *= 1.2f;
            v1.z *= 1.2f;
            lines.Add(v);

            Resource.Set(lines.AsReadOnlySpan(), Color.a < 1);
        }

        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }
    }
}