using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Mesh
    {
        [DataMember] public string Filename { get; }
        [DataMember] public Vector3f Scale { get; }

        internal Mesh(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes?["filename"]);
            Scale = Vector3f.Parse(node.Attributes?["scale"], Vector3f.One);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}