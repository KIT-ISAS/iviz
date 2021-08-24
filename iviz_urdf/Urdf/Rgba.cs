using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs.StdMsgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Rgba
    {
        [DataMember] public float R { get; }
        [DataMember] public float G { get; }
        [DataMember] public float B { get; }
        [DataMember] public float A { get; }

        internal Rgba(XmlAttribute? attr)
        {
            string s = Utils.ParseString(attr);
            string[] elems = s.Split(Utils.Separator, StringSplitOptions.RemoveEmptyEntries);
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
        
        public override string ToString() => JsonConvert.SerializeObject(this);
        
        public void Deconstruct(out float r, out float g, out float b, out float a) => (r, g, b, a) = (R, G, B, A);

        public static implicit operator ColorRGBA(Rgba v) => (v.R, v.G, v.B, v.A);
    }
}