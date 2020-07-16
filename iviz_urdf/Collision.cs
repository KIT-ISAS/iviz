using System.Xml;

namespace Iviz.Urdf
{
    public class Collision
    {
        public string Name { get; }
        public Origin Origin { get; }
        public Geometry Geometry { get; }

        internal Collision(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "origin":
                        Origin = new Origin(child);
                        break;
                    case "geometry":
                        Geometry = new Geometry(child);
                        break;
                }
            }

            Origin ??= Origin.Identity;
        }
    }
}