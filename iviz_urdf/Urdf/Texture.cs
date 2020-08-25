using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Texture
    {
        public string Filename { get; }

        internal Texture(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes?["filename"]);
        }
    }
}