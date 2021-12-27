#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Common
{
    [DataContract]
    public readonly struct Intrinsic : IEquatable<Intrinsic>
    {
        [DataMember] public float Fx { get; }
        [DataMember] public float Cx { get; }
        [DataMember] public float Fy { get; }
        [DataMember] public float Cy { get; }

        public Intrinsic(float fx, float cx, float fy, float cy) => (Fx, Cx, Fy, Cy) = (fx, cx, fy, cy);

        public Intrinsic(float fovInRad, int width, int height)
        {
            Fx = Fy = width / (2 * Mathf.Tan(fovInRad / 2));
            Cx = width / 2f + 0.5f;
            Cy = height / 2f + 0.5f;
        }

        public Intrinsic(double[] array) :
            this((float)array[0], (float)array[2], (float)array[4], (float)array[5])
        {
        }

        public override string ToString() => BuiltIns.ToJsonString(this);

        public Intrinsic Scale(float f) => new(Fx * f, Cx * f, Fy * f, Cy * f);

        public Vector3f Unproject(in Vector2f v, float z) => new((v.X - Cx) / Fx * z, (v.Y - Cy) / Fy * z, z);

        public double[] ToArray() => new double[] { Fx, 0, Cx, 0, Fy, Cy, 0, 0, 1 };

        public void CopyTo(Span<float> span)
        {
            span[0] = Fx;
            span[1] = 0;
            span[2] = Cx;
            span[3] = 0;
            span[4] = Fy;
            span[5] = Cy;
            if (span.Length < 9)
            {
                return;
            }

            span[6] = 0;
            span[7] = 0;
            span[8] = 1;
        }

        public float GetHorizontalFovInRad(float imageWidth) =>
            Fx != 0 && imageWidth != 0 ? 2 * Mathf.Atan(imageWidth / (2 * Fx)) : 0;

        public float GetVerticalFovInRad(float imageHeight) =>
            Fy != 0 && imageHeight != 0 ? 2 * Mathf.Atan(imageHeight / (2 * Fy)) : 0;

        public bool Equals(Intrinsic? other) => other is { } validatedOther && Equals(validatedOther);

        public bool Equals(Intrinsic other) => (Fx, Cx, Fy, Cy) == (other.Fx, other.Cx, other.Fy, other.Cy);

        public bool Equals(double[] array) =>
            (Fx, Cx, Fy, Cy) == (array[0], array[2], array[4], array[5]);

        public override int GetHashCode() => (Fx, Cx, Fy, Cy).GetHashCode();
    }
}