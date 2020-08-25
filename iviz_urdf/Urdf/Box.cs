using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Box
    {
        public Vector3 Size { get; }

        internal Box(XmlNode node)
        {
            Size = new Vector3(node.Attributes?["size"]);
        }
    }
}