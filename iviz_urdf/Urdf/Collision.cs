using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Collision
    {
        /// Specifies a name for a part of a link's geometry. This is useful to be able to refer to specific bits of the geometry of a link
        [DataMember]
        public string? Name { get; }

        /// The reference frame of the collision element, relative to the reference frame of the link
        [DataMember]
        public Origin Origin { get; }

        /// The shape of the visual object
        [DataMember]
        public Geometry? Geometry { get; }

        internal Collision(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            Origin? origin = null;
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "origin":
                        origin = new Origin(child);
                        break;
                    case "geometry":
                        Geometry = new Geometry(child);
                        break;
                }
            }

            Origin = origin ?? Origin.Identity;
        }

        public void Deconstruct(out string? name, out Origin origin, out Geometry geometry) =>
            (name, origin, geometry) = (Name, Origin, Geometry);

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}