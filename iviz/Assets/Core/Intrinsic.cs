using System.Runtime.Serialization;
using Iviz.Msgs;
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

        public Intrinsic([NotNull] double[] array) : this((float) array[0], (float) array[2], (float) array[4], (float) array[5])
        {
        }

        [NotNull]
        public override string ToString() => BuiltIns.ToJsonString(this);

        [NotNull]
        public double[] ToArray() => new double[] {Fx, 0, Cx, 0, Fy, Cy, 0, 0, 1};
    }
}