using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Sphere
    {
        [DataMember] public float Radius { get; }

        internal Sphere(XmlNode node)
        {
            Radius = Utils.ParseFloat(node.Attributes?["radius"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}