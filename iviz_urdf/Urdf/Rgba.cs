using System;
using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Rgba
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }

        public Rgba()
        {
            R = 1;
            G = 1;
            B = 1;
            A = 1;
        }

        internal Rgba(XmlAttribute attr)
        {
            string s = Utils.ParseString(attr);
            string[] elems = s.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 4)
            {
                throw new MalformedUrdfException(attr);
            }

            R = float.Parse(elems[0], Utils.Culture);
            G = float.Parse(elems[1], Utils.Culture);
            B = float.Parse(elems[2], Utils.Culture);
            A = float.Parse(elems[3], Utils.Culture);
        }
    }
}