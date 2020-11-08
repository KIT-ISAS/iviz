using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Visual
    {
        public string? Name { get; }
        public Origin Origin { get; }
        public Geometry Geometry { get; }
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
    }
}