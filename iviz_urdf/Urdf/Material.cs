using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Material
    {
        public string Name { get; }
        public Color? Color { get; }
        public Texture? Texture { get; }

        internal Material(XmlNode node)
        {
            Name = Utils.ParseString(node.Attributes?["name"]);
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "color":
                        Color = new Color(child);
                        break;
                    case "texture":
                        Texture = new Texture(child);
                        break;
                }
            }
        }
    }
}