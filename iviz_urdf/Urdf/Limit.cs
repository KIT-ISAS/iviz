using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Limit
    {
        public static readonly Limit Empty = new();

        /// An attribute specifying the lower joint limit (radians for revolute joints, meters for prismatic joints). Omit if joint is continuous 
        [DataMember]
        public float Lower { get; }

        /// AAn attribute specifying the upper joint limit (radians for revolute joints, meters for prismatic joints). Omit if joint is continuous
        [DataMember]
        public float Upper { get; }

        /// An attribute for enforcing the maximum joint velocity
        [DataMember]
        public float Velocity { get; }

        Limit()
        {
            Lower = 0;
            Upper = 0;
            Velocity = 0;
        }

        internal Limit(XmlNode node)
        {
            if (node.Attributes == null)
            {
                throw new MalformedUrdfException();
            }

            Lower = Utils.ParseFloat(node.Attributes["lower"], 0);
            Upper = Utils.ParseFloat(node.Attributes["upper"], 0);
            Velocity = Utils.ParseFloat(node.Attributes["velocity"], 0);
        }

        public void Deconstruct(out float lower, out float upper, out float velocity)
        {
            lower = Lower;
            upper = Upper;
            velocity = Velocity;
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}