using System.Xml;

namespace Iviz.Urdf
{
    public class Texture
    {
        public string Filename { get; }

        internal Texture(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes["filename"]);
        }
    }
}