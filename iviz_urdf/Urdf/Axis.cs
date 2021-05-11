using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Axis
    {
        public static readonly Axis Right = new Axis();

        [DataMember] public Vector3f Xyz { get; }

        Axis()
        {
            Xyz = new Vector3f(1, 0, 0);
        }

        internal Axis(XmlNode node)
        {
            Xyz = new Vector3f(node.Attributes?["xyz"]);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}