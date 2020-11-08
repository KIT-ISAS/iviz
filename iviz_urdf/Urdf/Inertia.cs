using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Inertia
    {
        public float Ixx { get; }
        public float Ixy { get; }
        public float Ixz { get; }
        public float Iyy { get; }
        public float Iyz { get; }
        public float Izz { get; }

        public Inertia()
        {
        }

        internal Inertia(XmlNode node)
        {
            if (node.Attributes == null)
            {
                throw new MalformedUrdfException();
            }

            Ixx = Utils.ParseFloat(node.Attributes["ixx"]);
            Ixy = Utils.ParseFloat(node.Attributes["ixy"]);
            Ixz = Utils.ParseFloat(node.Attributes["ixz"]);
            Iyy = Utils.ParseFloat(node.Attributes["iyy"]);
            Iyz = Utils.ParseFloat(node.Attributes["iyz"]);
            Izz = Utils.ParseFloat(node.Attributes["izz"]);
        }
    }
}