using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Parent
    {
        [DataMember] public string Link { get; }

        internal Parent(XmlNode node)
        {
            Link = Utils.ParseString(node.Attributes?["link"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}