#nullable enable

using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Leash : DisplayWrapperResource, IRecyclable
    {
        [SerializeField] MeshMarkerResource? reticle;
        LineResource? resource;

        LineResource Resource =>
            resource != null ? resource : (resource = ResourcePool.RentDisplay<LineResource>(transform));

        MeshMarkerResource Reticle => reticle.AssertNotNull(nameof(reticle));

        protected override IDisplay Display => Resource;

        public Color Color { get; set; } = Color.white;

        public float Width
        {
            set => Resource.ElementScale = value;
        }

        void Awake()
        {
            Resource.ElementScale = 0.005f;
            Resource.RenderType = LineResource.LineRenderType.AlwaysCapsule;
        }

        public void Set(in Ray pointerRay, in Vector3 target, in Vector3 normal, float offset = 0)
        {
            Reticle.Visible = true;
            Reticle.Transform.SetPositionAndRotation(target, Quaternion.LookRotation(normal));

            float scale = 0.03f * Vector3.Distance(target, Settings.MainCameraTransform.position);
            Reticle.Transform.localScale = scale * Vector3.one;

            BuildLeash(pointerRay, target + normal * offset);
        }

        public void Set(in Ray pointerRay, in Vector3 target)
        {
            Reticle.Visible = false;
            BuildLeash(pointerRay, target);
        }

        void BuildLeash(in Ray pointerRay, in Vector3 target)
        {
            var (start, tangent) = pointerRay;
            float distance = Vector3.Distance(target, start);

            if (distance.ApproximatelyZero())
            {
                Resource.Reset();
                return;
            }

            var direction = (target - start) / distance;
            var tangentReflected = tangent - Vector3.Dot(tangent, direction) * 2 * direction;

            float dirScale = distance / 3;

            var f = new float3x4(
                start,
                start + dirScale * tangent,
                target + dirScale * tangentReflected,
                target);

            var colorBase = Color;
            int numSegments = (int)(math.length(start - target) * 16);

            Resource.SetDirect(array =>
            {
                var line = new float4x2();

                ref var c0 = ref f.c0;
                ref var c3 = ref f.c3;

                ref var l0 = ref line.c0;
                ref var l1 = ref line.c1;

                var p = c0;

                float colorA = colorBase.a;

                Color32 color = colorBase;
                color.a = 0;

                l0.w = UnityUtils.AsFloat(color);

                foreach (int i in ..numSegments)
                {
                    float t = (i + 1f) / numSegments;
                    var q = Bezier(t, f);

                    var delta = q - p;
                    (l0.x, l0.y, l0.z) = p + 0.25f * delta;
                    (l1.x, l1.y, l1.z) = p + 0.75f * delta;

                    color.a = (byte)(AlphaFromDistance(q, c0, c3) * colorA * 255);

                    l1.w = UnityUtils.AsFloat(color);

                    array.Add(line);

                    p = q;
                    l0.w = l1.w;
                }

                return true;
            }, numSegments);
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

        void IRecyclable.SplitForRecycle()
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