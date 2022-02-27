using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Texture
    {
        [DataMember] public string Filename { get; }

        internal Texture(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes?["filename"]);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}