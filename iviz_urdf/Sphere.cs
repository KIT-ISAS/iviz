using System.Xml;

namespace Iviz.Urdf
{
    public class Sphere
    {
        public float Radius { get; }

        internal Sphere(XmlNode node)
        {
            Radius = Utils.ParseFloat(node.Attributes?["radius"]);
        }
    }
}