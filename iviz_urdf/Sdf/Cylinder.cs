using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Cylinder
    {
        public double Radius { get; }
        public double Length { get; }

        internal Cylinder(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "radius": 
                        Radius = DoubleElement.ValueOf(child);
                        break;
                    case "length":
                        Length = DoubleElement.ValueOf(child);
                        break;
                }
            }     
        }
    }
}