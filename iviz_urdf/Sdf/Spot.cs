using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Spot
    {
        public double InnerAngle { get; }
        public double OuterAngle { get; }
        public double Falloff { get; }
        
        internal Spot(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "inner_angle":
                        InnerAngle = DoubleElement.ValueOf(child);
                        break;
                    case "outer_angle":
                        OuterAngle = DoubleElement.ValueOf(child);
                        break;
                    case "falloff":
                        Falloff = DoubleElement.ValueOf(child);
                        break;
                }
            }
        }
        
        Spot() {}

        public static readonly Spot Default = new Spot();                
    }
}