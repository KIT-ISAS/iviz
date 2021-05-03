using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Origin
    {
        public static readonly Origin Identity = new Origin();

        [DataMember] public Vector3f Rpy { get; }
        [DataMember] public Vector3f Xyz { get; }

        Origin()
        {
            Rpy = Vector3f.Zero;
            Xyz = Vector3f.Zero;
        }

        internal Origin(XmlNode node)
        {
            Rpy = Vector3f.Parse(node.Attributes?["rpy"], Vector3f.Zero);
            Xyz = Vector3f.Parse(node.Attributes?["xyz"], Vector3f.Zero);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}