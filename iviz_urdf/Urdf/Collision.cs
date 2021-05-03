using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Collision
    {
        [DataMember] public string? Name { get; }
        [DataMember] public Origin Origin { get; }
        [DataMember] public Geometry? Geometry { get; }

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
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}