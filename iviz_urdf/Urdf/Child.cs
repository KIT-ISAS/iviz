using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Child
    {
        [DataMember] public string Link { get; }

        internal Child(XmlNode node)
        {
            Link = Utils.ParseString(node.Attributes?["link"]);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}