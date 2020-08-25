using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Box
    {
        public Vector3 Scale { get; } = Vector3.One;

        internal Box(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "scale")
            {
                Scale = new Vector3(node.ChildNodes[0]);
            }
        }
    }
}