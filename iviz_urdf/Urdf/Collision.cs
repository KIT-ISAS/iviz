using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Collision
    {
        public string? Name { get; }
        public Origin Origin { get; }
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
    }
}