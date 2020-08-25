using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Sphere
    {
        public double Radius { get; } = 1;

        internal Sphere(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "radius")
            {
                Radius = DoubleElement.ValueOf(node.ChildNodes[0]);
            }
        }
    }
}