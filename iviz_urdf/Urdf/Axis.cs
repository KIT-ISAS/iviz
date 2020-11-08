using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Axis
    {
        public static readonly Axis Right = new Axis();

        public Vector3f Xyz { get; }

        Axis()
        {
            Xyz = new Vector3f(1, 0, 0);
        }

        internal Axis(XmlNode node)
        {
            Xyz = new Vector3f(node.Attributes?["xyz"]);
        }
    }
}