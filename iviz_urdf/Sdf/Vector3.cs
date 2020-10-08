using System;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Vector3
    {
        internal static readonly char[] Separator = {' '};
        
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        internal Vector3(XmlNode node)
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

            X = double.Parse(elems[0], Utils.Culture);
            Y = double.Parse(elems[1], Utils.Culture);
            Z = double.Parse(elems[2], Utils.Culture);            
        }

        internal Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        public override string ToString()
        {
            return $"[{X} {Y} {Z}]";
        }        
        
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        
        public static readonly Vector3 Up = new Vector3(0, 0, 1);
        
        public static readonly Vector3 Down = new Vector3(0, 0, -1);
    }
}