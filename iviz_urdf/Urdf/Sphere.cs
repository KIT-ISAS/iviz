using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Sphere
    {
        public float Radius { get; }

        internal Sphere(XmlNode node)
        {
            Radius = Utils.ParseFloat(node.Attributes?["radius"]);
        }
    }
}