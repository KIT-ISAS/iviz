using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Color
    {
        [DataMember] public Rgba Rgba { get; }

        public Color()
        {
            Rgba = new Rgba();
        }

        internal Color(XmlNode node)
        {
            Rgba = new Rgba(node.Attributes?["rgba"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}