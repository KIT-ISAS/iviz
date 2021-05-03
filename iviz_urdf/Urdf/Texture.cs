using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

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
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}