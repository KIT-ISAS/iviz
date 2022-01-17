using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Visual
    {
        /// Specifies a name for a part of a link's geometry. This is useful to be able to refer to specific bits of the geometry of a link
        [DataMember]
        public string? Name { get; }

        /// The reference frame of the visual element with respect to the reference frame of the link
        [DataMember]
        public Origin Origin { get; }

        /// The shape of the visual object
        [DataMember]
        public Geometry Geometry { get; }

        /// The material of the visual element
        [DataMember]
        public Material? Material { get; }

        internal Visual(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            Geometry? geometry = null;
            Origin? origin = null;

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "origin":
                        origin = new Origin(child);
                        break;
                    case "geometry":
                        geometry = new Geometry(child);
                        break;
                    case "material":
                        Material = new Material(child);
                        break;
                }
            }

            Geometry = geometry ?? throw new MalformedUrdfException(node);
            Origin = origin ?? Origin.Identity;
        }

        public void Deconstruct(out string? name, out Origin origin, out Geometry geometry, out Material? material) =>
            (name, origin, geometry, material) = (Name, Origin, Geometry, Material);

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}