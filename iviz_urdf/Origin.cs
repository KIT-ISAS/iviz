using System.Xml;

namespace Iviz.Urdf
{
    public class Origin
    {
        public static readonly Origin Identity = new Origin();

        public Vector3 Rpy { get; }
        public Vector3 Xyz { get; }

        Origin()
        {
            Rpy = Vector3.Zero;
            Xyz = Vector3.Zero;
        }

        internal Origin(XmlNode node)
        {
            Rpy = Vector3.Parse(node.Attributes["rpy"], Vector3.Zero);
            Xyz = Vector3.Parse(node.Attributes["xyz"], Vector3.Zero);
        }
    }
}