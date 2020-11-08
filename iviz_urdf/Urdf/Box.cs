using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Box
    {
        public Vector3f Size { get; }

        internal Box(XmlNode node)
        {
            Size = new Vector3f(node.Attributes?["size"]);
        }
    }
}