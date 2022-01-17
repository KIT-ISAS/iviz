using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Color
    {
        [DataMember] public Rgba Rgba { get; }

        internal Color(XmlNode node)
        {
            Rgba = new Rgba(node.Attributes?["rgba"]);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}