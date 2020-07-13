using System;
using System.Xml;

namespace Iviz.Urdf
{
    public class Vector3
    {
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        internal Vector3(XmlAttribute attr)
        {
            string s = Utils.ParseString(attr);
            string[] elems = s.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 3)
            {
                throw new MalformedUrdfException(attr);
            }

            X = float.Parse(elems[0], Utils.Culture);
            Y = float.Parse(elems[1], Utils.Culture);
            Z = float.Parse(elems[2], Utils.Culture);
        }

        public static Vector3 Parse(XmlAttribute attr, Vector3 @default)
        {
            return attr is null ? @default : new Vector3(attr);
        }
    }
}