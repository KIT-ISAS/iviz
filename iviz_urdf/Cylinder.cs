using System.Xml;

namespace Iviz.Urdf
{
    public class Cylinder
    {
        public float Radius { get; }
        public float Length { get; }

        internal Cylinder(XmlNode node)
        {
            Radius = Utils.ParseFloat(node.Attributes["radius"]);
            Length = Utils.ParseFloat(node.Attributes["length"]);
        }
    }
}