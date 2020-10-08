using System;
using System.Globalization;
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
            
            if (!float.TryParse(elems[0], NumberStyles.Any, Utils.Culture, out float r) ||
                !float.TryParse(elems[1], NumberStyles.Any, Utils.Culture, out float g) ||
                !float.TryParse(elems[2], NumberStyles.Any, Utils.Culture, out float b) || 
                !float.TryParse(elems[3], NumberStyles.Any, Utils.Culture, out float a))
            {
                throw new MalformedUrdfException("Expected RGBA at ", attr);
            }            

            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public override string ToString()
        {
            return $"[{R} {G} {B} {A}]";
        }
    }
}