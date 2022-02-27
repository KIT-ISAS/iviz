using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

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
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}