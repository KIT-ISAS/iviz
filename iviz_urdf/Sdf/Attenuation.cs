using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Attenuation
    {
        public double Range { get; } = 10;
        public double Linear { get; } = 1;
        public double Constant { get; } = 1;
        public double Quadratic { get; }
        
        internal Attenuation(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "range":
                        Range = DoubleElement.ValueOf(child);
                        break;
                    case "linear":
                        Linear = DoubleElement.ValueOf(child);
                        break;
                    case "constant":
                        Constant = DoubleElement.ValueOf(child);
                        break;
                    case "quadratic":
                        Quadratic = DoubleElement.ValueOf(child);
                        break;
                }
            }
        }
        
        Attenuation() {}

        public static readonly Attenuation Default = new Attenuation();                
    }
}