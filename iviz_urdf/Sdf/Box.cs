using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Box
    {
        public Vector3d Scale { get; } = Vector3d.One;

        internal Box(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "scale")
            {
                Scale = new Vector3d(node.ChildNodes[0]);
            }
        }
    }
}