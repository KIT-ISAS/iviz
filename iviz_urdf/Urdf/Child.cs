using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Child
    {
        public string Link { get; }

        internal Child(XmlNode node)
        {
            Link = Utils.ParseString(node.Attributes?["link"]);
        }
    }
}