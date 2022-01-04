#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class AngleAxisResource : DisplayWrapperResource, ISupportsTint, IRecyclable
    {
        readonly List<LineWithColor> lines = new();

        Color color;
        LineResource? resource;

        LineResource Resource =>
            resource != null ? resource : (resource = ResourcePool.RentDisplay<LineResource>(transform));

        protected override IDisplay Display => Resource;

        public Color Color
        {
            get => color;
            set
            {
                color = value;

                float colorAsFloat = UnityUtils.AsFloat(color);

                var linesAsSpan = lines.AsSpan();
                for (int i = 0; i < linesAsSpan.Length; i++)
                {
                    ref var line = ref linesAsSpan[i];
                    line.f.c0.w = colorAsFloat;
                    line.f.c1.w = colorAsFloat;
                }

                Resource.Set(linesAsSpan, color.a < 1);
            }
        }

        void Awake()
        {
            Resource.ElementScale = 0.01f;
            Color = Color.yellow;
            Layer = LayerType.IgnoreRaycast;
        }

        public void Reset()
        {
            Resource.Visible = false;
        }

        public Color Tint
        {
            get => Resource.Tint;
            set => Resource.Tint = value;
        }

        public void Set(Quaternion q, float scale = 0.3f)
        {
            q.ToAngleAxis(out float angle, out Vector3 axis);
            Set(angle * Mathf.Deg2Rad, axis, scale);
        }

        public void Set(in Vector3 rod, float scale = 0.3f)
        {
            float angle = rod.Magnitude();
            if (Mathf.Approximately(angle, 0))
            {
                Set(0, Vector3.zero, 0);
            }
            else
            {
                Vector3 axis = rod / angle;
                Set(angle, axis, scale);
            }
        }

        void Set(float angle, Vector3 axis, float scale = 0.3f)
        {
            if (Mathf.Approximately(angle, 0)
                || Mathf.Approximately(axis.MagnitudeSq(), 0))
            {
                Resource.Visible = false;
                return;
            }

            Resource.Visible = true;

            angle *= -1;

            if (axis.y < 0)
            {
                angle *= -1;
                axis *= -1;
            }

            lines.Clear();

            Vector3 notX = Vector3.forward;
            if (Mathf.Approximately(axis.Cross(notX).MagnitudeSq(), 0))
            {
                notX = Vector3.right;
            }

            Vector3 dirY = notX.Cross(axis).Normalized();
            Vector3 dirX = axis.Cross(dirY).Normalized();
            dirX *= scale;
            dirY *= scale;

            int n = (int)(Mathf.Abs(angle) / (2 * Mathf.PI) * 32 + 1);

            Vector3 v0 = dirX;
            Vector3 v1 = Vector3.zero;

            lines.Add(new LineWithColor(Vector3.zero, 1.2f * v0, Color));

            const float scaleFromAngle = 0.02f;

            foreach (int i in 1..(n + 1))
            {
                float a = i / (float)n * angle;
                float ax = Mathf.Cos(a);
                float ay = Mathf.Sin(a);
                v1 = ax * dirX + ay * dirY;
                v1 *= 1 + a * scaleFromAngle;
                lines.Add(new LineWithColor(v0, v1, Color));
                v0 = v1;
            }

            lines.Add(new LineWithColor(Vector3.zero, 1.2f * v1, Color));
            Resource.Set(lines.AsReadOnlySpan(), Color.a < 1);
        }

        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }
    }
}