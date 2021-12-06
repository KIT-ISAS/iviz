using System.Runtime.Serialization;
using UnityEngine;

namespace Iviz.Common
{
    [DataContract]
    public struct SerializableVector3
    {
        [DataMember] public float X { get; set; }
        [DataMember] public float Y { get; set; }
        [DataMember] public float Z { get; set; }

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(in SerializableVector3 i) => new(i.X, i.Y, i.Z);
        public static implicit operator SerializableVector3(in Vector3 v) => new(v.x, v.y, v.z);
    }
}