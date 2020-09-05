using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Material
    {
        bool Lighting { get; } = true;
        public Color Ambient { get; } = Color.Black;
        public Color Diffuse { get; } = Color.Black;
        public Color Specular { get; } = Color.Black;
        public Color Emissive { get; } = Color.Black;

        internal Material(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "lighting":
                        Lighting = BoolElement.ValueOf(child);
                        break;
                    case "ambient":
                        Ambient = new Color(child);
                        break;
                    case "diffuse":
                        Diffuse = new Color(child);
                        break;
                    case "specular":
                        Specular = new Color(child);
                        break;
                    case "emissive":
                        Emissive = new Color(child);
                        break;
                }
            }
        }
    }
}