using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Axis
    {
        public static readonly Axis Right = new();

        [DataMember] public Vector3f Xyz { get; }

        Axis()
        {
            Xyz = Vector3f.UnitX;
        }

        internal Axis(XmlNode node)
        {
            Xyz = Vector3f.Parse(node.Attributes?["xyz"], Vector3f.UnitX);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}