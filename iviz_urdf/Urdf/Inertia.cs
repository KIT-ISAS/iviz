using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Inertia
    {
        [DataMember] public float Ixx { get; }
        [DataMember] public float Ixy { get; }
        [DataMember] public float Ixz { get; }
        [DataMember] public float Iyy { get; }
        [DataMember] public float Iyz { get; }
        [DataMember] public float Izz { get; }

        public Inertia()
        {
        }

        internal Inertia(XmlNode node)
        {
            if (node.Attributes == null)
            {
                throw new MalformedUrdfException();
            }

            Ixx = Utils.ParseFloat(node.Attributes["ixx"]);
            Ixy = Utils.ParseFloat(node.Attributes["ixy"]);
            Ixz = Utils.ParseFloat(node.Attributes["ixz"]);
            Iyy = Utils.ParseFloat(node.Attributes["iyy"]);
            Iyz = Utils.ParseFloat(node.Attributes["iyz"]);
            Izz = Utils.ParseFloat(node.Attributes["izz"]);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}