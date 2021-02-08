using System;
using System.Globalization;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Color
    {
        public double R { get; }
        public double G { get; }
        public double B { get; }
        public double A { get; }

        internal Color(XmlNode node)
        {
            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }
            
            string[] elems = node.InnerText.Split(Vector3d.Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 4)
            {
                throw new MalformedSdfException(node);
            }

            R = double.Parse(elems[0], NumberStyles.Any, Utils.Culture);
            G = double.Parse(elems[1], NumberStyles.Any, Utils.Culture);
            B = double.Parse(elems[2], NumberStyles.Any, Utils.Culture);            
            A = double.Parse(elems[3], NumberStyles.Any, Utils.Culture);            
        }

        internal Color(double R, double G, double B, double A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public override string ToString()
        {
            return $"[{R} {G} {B} {A}]";
        }
        
        public static readonly Color Black = new Color(0, 0, 0, 1);
        public static readonly Color White = new Color(1, 1, 1, 1);
    }
}