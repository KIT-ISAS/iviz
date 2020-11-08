using System;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Vector2f
    {
        public double X { get; }
        public double Y { get; }

        internal Vector2f(XmlNode node)
        {
            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }
            
            string[] elems = node.InnerText.Split(Vector3f.Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 2)
            {
                throw new MalformedSdfException(node);
            }

            X = double.Parse(elems[0], Utils.Culture);
            Y = double.Parse(elems[1], Utils.Culture);
        }

        Vector2f(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        public override string ToString()
        {
            return $"[{X} {Y}]";
        }        
        
        public static readonly Vector2f One = new Vector2f(1, 1);

        public static readonly Vector2f Zero = new Vector2f(0, 0);
    }
}