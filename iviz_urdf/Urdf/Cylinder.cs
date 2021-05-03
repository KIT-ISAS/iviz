using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Cylinder
    {
        [DataMember] public float Radius { get; }
        [DataMember] public float Length { get; }

        internal Cylinder(XmlNode node)
        {
            Radius = Utils.ParseFloat(node?.Attributes?["radius"]);
            Length = Utils.ParseFloat(node?.Attributes?["length"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}