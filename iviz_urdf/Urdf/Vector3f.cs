using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Vector3f : IEquatable<Vector3f>
    {
        public static readonly Vector3f Zero = new(0, 0, 0);
        public static readonly Vector3f One = new(1, 1, 1);

        public static readonly Vector3f UnitX = new(1, 0, 0);
        public static readonly Vector3f UnitY = new(0, 1, 0);
        public static readonly Vector3f UnitZ = new(0, 0, 1);

        [DataMember] public float X { get; }
        [DataMember] public float Y { get; }
        [DataMember] public float Z { get; }

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3f Parse(XmlAttribute? attr, Vector3f @default)
        {
            if (attr?.Value is null)
            {
                return @default;
            }

            string s = Utils.ParseString(attr);
            string[] elems = s.Split(Utils.Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 3)
            {
                throw new MalformedUrdfException(attr);
            }

            switch (elems[0], elems[1], elems[2])
            {
                case ("0", "0", "0"):
                    return Zero;
                case ("1", "0", "0"):
                    return UnitX;
                case ("0", "1", "0"):
                    return UnitY;
                case ("0", "0", "1"):
                    return UnitZ;
                case ("1", "1", "1"):
                    return One;
            }

            if (!float.TryParse(elems[0], NumberStyles.Any, Utils.Culture, out float x) ||
                !float.TryParse(elems[1], NumberStyles.Any, Utils.Culture, out float y) ||
                !float.TryParse(elems[2], NumberStyles.Any, Utils.Culture, out float z))
            {
                throw new MalformedUrdfException(attr);
            }

            return new Vector3f(x, y, z);
        }

        public void Deconstruct(out float x, out float y, out float z) => (x, y, z) = (X, Y, Z);

        public override string ToString() => BuiltIns.ToJsonString(this);

        public static implicit operator Vector3(Vector3f v) => (v.X, v.Y, v.Z);
        public static implicit operator Point(Vector3f v) => (v.X, v.Y, v.Z);
        public static implicit operator Msgs.IvizMsgs.Vector3f(Vector3f v) => (v.X, v.Y, v.Z);

        public bool Equals(Vector3f? other) =>
            !ReferenceEquals(null, other) &&
            (ReferenceEquals(this, other) || X == other.X && Y == other.Y && Z == other.Z);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || obj is Vector3f other && Equals(other);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();

        public static bool operator ==(Vector3f? left, Vector3f? right) => Equals(left, right);

        public static bool operator !=(Vector3f? left, Vector3f? right) => !Equals(left, right);
    }
}