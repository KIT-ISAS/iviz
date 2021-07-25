using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.MsgsWrapper;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.Core
{
    [DataContract]
    public readonly struct Intrinsic
    {
        [DataMember] public float Fx { get; }
        [DataMember] public float Cx { get; }
        [DataMember] public float Fy { get; }
        [DataMember] public float Cy { get; }

        public Intrinsic(float fx, float cx, float fy, float cy) => (Fx, Cx, Fy, Cy) = (fx, cx, fy, cy);

        public Intrinsic([NotNull] double[] array) : this((float) array[0], (float) array[2], (float) array[4],
            (float) array[5])
        {
        }

        [NotNull]
        public override string ToString() => BuiltIns.ToJsonString(this);

        public Intrinsic Scale(float f) => new Intrinsic(Fx * f, Cx * f, Fy * f, Cy * f);

        public Vector3f Unproject(in Vector2f v, float z)
        {
            return new Vector3f((v.X - Cx) / Fx * z, (v.Y - Cy) / Fy * z, z);
        }

        [NotNull]
        public double[] ToArray() => new double[] {Fx, 0, Cx, 0, Fy, Cy, 0, 0, 1};

        public bool Equals(in Intrinsic other) => (Fx, Cx, Fy, Cy) == (other.Fx, other.Cx, other.Fy, other.Cy);

        public override bool Equals(object obj) => obj is Intrinsic other && Equals(other);

        public override int GetHashCode() => (Fx, Cx, Fy, Cy).GetHashCode();
    }
} 