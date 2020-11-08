using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Origin
    {
        public static readonly Origin Identity = new Origin();

        public Vector3f Rpy { get; }
        public Vector3f Xyz { get; }

        Origin()
        {
            Rpy = Vector3f.Zero;
            Xyz = Vector3f.Zero;
        }

        internal Origin(XmlNode node)
        {
            Rpy = Vector3f.Parse(node.Attributes?["rpy"], Vector3f.Zero);
            Xyz = Vector3f.Parse(node.Attributes?["xyz"], Vector3f.Zero);
        }
    }
}