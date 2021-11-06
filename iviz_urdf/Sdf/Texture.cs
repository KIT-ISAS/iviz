using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Texture
    {
        public double Size { get; }
        public string? Diffuse { get; }
        public string? Normal { get; }

        internal Texture(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "size":
                        Size = DoubleElement.ValueOf(child);
                        break;
                    case "diffuse":
                        Diffuse = child.InnerText;
                        break;
                    case "normal":
                        Normal = child.InnerText;
                        break;
                }
            }
        }
    }
}