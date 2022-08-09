using System;
using System.Globalization;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Vector2d
    {
        public double X { get; }
        public double Y { get; }

        internal Vector2d(XmlNode node)
        {
            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }

            string[] elems = node.InnerText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 2)
            {
                throw new MalformedSdfException(node);
            }

            X = double.Parse(elems[0], NumberStyles.Any, Utils.Culture);
            Y = double.Parse(elems[1], NumberStyles.Any, Utils.Culture);
        }

        Vector2d(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return $"[{X} {Y}]";
        }

        public static readonly Vector2d One = new Vector2d(1, 1);

        public static readonly Vector2d Zero = new Vector2d(0, 0);
    }
}