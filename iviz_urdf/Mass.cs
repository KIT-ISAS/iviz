using System.Xml;

namespace Iviz.Urdf
{
    public class Mass
    {
        public float Value { get; }

        public Mass()
        {
            Value = 0;
        }

        internal Mass(XmlNode node)
        {
            Value = Utils.ParseFloat(node.Attributes["value"]);
        }
    }
}