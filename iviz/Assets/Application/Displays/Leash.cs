#nullable enable

using Iviz.Core;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Leash : DisplayWrapperResource, IRecyclable
    {
        LineResource? resource;

        LineResource Resource =>
            resource != null ? resource : (resource = ResourcePool.RentDisplay<LineResource>(transform));

        protected override IDisplay Display => Resource;
        public Color Color { get; set; } = Color.white;

        void Awake()
        {
            Resource.ElementScale = 0.005f;
        }

        public void Set(in Ray pointerRay, in Vector3 target)
        {
            var (start, tangent) = pointerRay;
            var direction = (target - start).normalized;
            var tangentReflected = tangent - Vector3.Dot(tangent, direction) * 2 * direction;

            const float dirScale = 0.5f;

            var f = new float3x4(
                start,
                start + dirScale * tangent,
                target + dirScale * tangentReflected,
                target);

            Resource.ElementScale = 0.005f;

            var colorBase = Color;

            Resource.SetDirect(array =>
            {
                float3 p = default, q = default;
                var line = new float4x2();

                ref var c0 = ref f.c0;
                ref var c3 = ref f.c3;

                ref var l0 = ref line.c0;
                ref var l1 = ref line.c1;

                (p.x, p.y, p.z) = c0;

                float colorA = colorBase.a;

                Color32 color = colorBase;
                color.a = 0;

                l0.w = PointWithColor.RecastToFloat(color);

                int numSegments = (int)(math.length(c0 - c3) * 16);

                for (int i = 0; i < numSegments; i++)
                {
                    float t = (i + 1f) / numSegments;
                    (q.x, q.y, q.z) = Bezier(t, f);

                    var delta = q - p;
                    (l0.x, l0.y, l0.z) = p + 0.25f * delta;
                    (l1.x, l1.y, l1.z) = p + 0.75f * delta;

                    color.a = (byte)(AlphaFromDistance(q, c0, c3) * colorA * 255);
                    l1.w = PointWithColor.RecastToFloat(color);

                    array.Add(line);

                    p = q;
                    l0.w = l1.w;
                }

                return true;
            }, 32);
        }

        static float AlphaFromDistance(in float3 p, in float3 start, in float3 end)
        {
            const float minDistance = 0.1f;
            const float minOpaque = 0.5f;

            float a = 1;
            float d1Sq = math.lengthsq(start - p);
            switch (d1Sq)
            {
                case < minDistance * minDistance:
                    a = 0;
                    break;
                case < minOpaque * minOpaque:
                    a *= d1Sq / (minOpaque * minOpaque);
                    break;
            }

            float d2Sq = math.lengthsq(end - p);
            switch (d2Sq)
            {
                case < minDistance * minDistance:
                    a = 0;
                    break;
                case < minOpaque * minOpaque:
                    a *= d2Sq / (minOpaque * minOpaque);
                    break;
            }

            return a;
        }

        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }

        static float3 Bezier(float t, in float3x4 f)
        {
            float mt = 1 - t;
            float mt2 = mt * mt;
            float mt3 = mt2 * mt;

            float t2 = t * t;
            float t3 = t2 * t;

            return mt3 * f.c0
                   + (3 * mt2 * t) * f.c1
                   + (3 * mt * t2) * f.c2
                   + t3 * f.c3;
        }
    }
}