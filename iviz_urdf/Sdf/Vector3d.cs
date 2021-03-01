using System;
using System.Globalization;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Vector3d
    {
        internal static readonly char[] Separator = {' '};

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        internal Vector3d(XmlNode node)
        {
            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }

            string[] elems = node.InnerText.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 3)
            {
                throw new MalformedSdfException(node);
            }

            X = double.Parse(elems[0], NumberStyles.Any, Utils.Culture);
            Y = double.Parse(elems[1], NumberStyles.Any, Utils.Culture);
            Z = double.Parse(elems[2], NumberStyles.Any, Utils.Culture);
        }

        internal Vector3d(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString()
        {
            return $"[{X} {Y} {Z}]";
        }

        public static readonly Vector3d One = new Vector3d(1, 1, 1);

        public static readonly Vector3d Zero = new Vector3d(0, 0, 0);

        public static readonly Vector3d Up = new Vector3d(0, 0, 1);

        public static readonly Vector3d Down = new Vector3d(0, 0, -1);
    }
}