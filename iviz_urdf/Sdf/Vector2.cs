using System;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Vector2
    {
        public double X { get; }
        public double Y { get; }

        internal Vector2(XmlNode node)
        {
            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }
            
            string[] elems = node.InnerText.Split(Vector3.Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 2)
            {
                throw new MalformedSdfException(node);
            }

            X = double.Parse(elems[0], Utils.Culture);
            Y = double.Parse(elems[1], Utils.Culture);
        }

        Vector2(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        public override string ToString()
        {
            return $"[{X} {Y}]";
        }        
        
        public static readonly Vector2 One = new Vector2(1, 1);

        public static readonly Vector2 Zero = new Vector2(0, 0);
    }
}