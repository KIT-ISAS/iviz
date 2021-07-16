using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs.GeometryMsgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Vector3f
    {
        public static readonly Vector3f Zero = new Vector3f(0, 0, 0);
        public static readonly Vector3f One = new Vector3f(1, 1, 1);

        [DataMember] public float X { get; }
        [DataMember] public float Y { get; }
        [DataMember] public float Z { get; }

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        internal Vector3f(XmlAttribute? attr)
        {
            string s = Utils.ParseString(attr);
            string[] elems = s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 3)
            {
                throw new MalformedUrdfException(attr);
            }

            if (!float.TryParse(elems[0], NumberStyles.Any, Utils.Culture, out float x) ||
                !float.TryParse(elems[1], NumberStyles.Any, Utils.Culture, out float y) ||
                !float.TryParse(elems[2], NumberStyles.Any, Utils.Culture, out float z))
            {
                throw new MalformedUrdfException(attr);
            }

            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3f Parse(XmlAttribute? attr, Vector3f @default)
        {
            return attr is null ? @default : new Vector3f(attr);
        }

        public void Deconstruct(out float x, out float y, out float z) => (x, y, z) = (X, Y, Z);

        public override string ToString() => JsonConvert.SerializeObject(this);

        public static implicit operator Vector3(Vector3f v) => (v.X, v.Y, v.Z);
        public static implicit operator Msgs.IvizMsgs.Vector3f(Vector3f v) => (v.X, v.Y, v.Z);
    }
}