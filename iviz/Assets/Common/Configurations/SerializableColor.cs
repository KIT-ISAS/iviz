using System.Runtime.Serialization;
using UnityEngine;

namespace Iviz.Core
{
    [DataContract]
    public struct SerializableColor
    {
        [DataMember] public float R { get; set; }
        [DataMember] public float G { get; set; }
        [DataMember] public float B { get; set; }
        [DataMember] public float A { get; set; }

        public SerializableColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator Color(in SerializableColor i) => new(i.R, i.G, i.B, i.A);

        public static implicit operator SerializableColor(in Color color) => new(color.r, color.g, color.b, color.a);
    }
}