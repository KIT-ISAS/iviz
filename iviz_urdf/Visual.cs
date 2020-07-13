using System.Xml;

namespace Iviz.Urdf
{
    public class Visual
    {
        public string Name { get; }
        public Origin Origin { get; }
        public Geometry Geometry { get; }
        public Material Material { get; }

        internal Visual(XmlNode node)
        {
            Name = node.Attributes["name"]?.Value;

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
                    case "material":
                        Material = new Material(child);
                        break;
                }
            }

            if (Geometry is null)
            {
                throw new MalformedUrdfException(node);
            }

            if (Origin is null)
            {
                Origin = Origin.Identity;
            }
        }
    }
}