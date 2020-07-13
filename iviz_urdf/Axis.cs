using System.Xml;

namespace Iviz.Urdf
{
    public class Axis
    {
        public static readonly Axis Right = new Axis();

        public Vector3 Xyz { get; }

        Axis()
        {
            Xyz = new Vector3(1, 0, 0);
        }

        internal Axis(XmlNode node)
        {
            Xyz = new Vector3(node.Attributes["xyz"]);
        }
    }
}