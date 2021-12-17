using System.Runtime.Serialization;
using UnityEngine;

namespace Iviz.Common
{
    [DataContract]
    public struct SerializableQuaternion
    {
        [DataMember] public float X { get; set; }
        [DataMember] public float Y { get; set; }
        [DataMember] public float Z { get; set; }
        [DataMember] public float W { get; set; }

        public SerializableQuaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static implicit operator Quaternion(in SerializableQuaternion i) => new(i.X, i.Y, i.Z, i.W);
        public static implicit operator SerializableQuaternion(in Quaternion v) => new(v.x, v.y, v.z, v.w);
    }
}