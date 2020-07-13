using System.Xml;

namespace Iviz.Urdf
{
    public class Color
    {
        public Rgba Rgba { get; }

        public Color()
        {
            Rgba = new Rgba();
        }

        internal Color(XmlNode node)
        {
            Rgba = new Rgba(node.Attributes["rgba"]);
        }
    }
}