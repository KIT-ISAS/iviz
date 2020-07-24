using System.Xml;

namespace Iviz.Urdf
{
    public class Box
    {
        public Vector3 Size { get; }

        internal Box(XmlNode node)
        {
            Size = new Vector3(node.Attributes?["size"]);
        }
    }
}