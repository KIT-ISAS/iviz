using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Box
    {
        [DataMember] public Vector3f Size { get; }

        internal Box(XmlNode node)
        {
            Size = new Vector3f(node.Attributes?["size"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}