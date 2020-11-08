using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Box
    {
        public Vector3f Scale { get; } = Vector3f.One;

        internal Box(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "scale")
            {
                Scale = new Vector3f(node.ChildNodes[0]);
            }
        }
    }
}