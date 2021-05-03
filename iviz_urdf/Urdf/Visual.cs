using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Visual
    {
        [DataMember] public string? Name { get; }
        [DataMember] public Origin Origin { get; }
        [DataMember] public Geometry Geometry { get; }
        [DataMember] public Material? Material { get; }

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

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}