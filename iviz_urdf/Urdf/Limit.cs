using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Limit
    {
        public static readonly Limit Empty = new Limit();
        public float Lower { get; }
        public float Upper { get; }
        public float Velocity { get; }

        Limit()
        {
            Lower = 0;
            Upper = 0;
            Velocity = 0;
        }

        internal Limit(XmlNode node)
        {
            if (node.Attributes == null)
            {
                throw new MalformedUrdfException();
            }
            Lower = Utils.ParseFloat(node.Attributes["lower"], 0);
            Upper = Utils.ParseFloat(node.Attributes["upper"], 0);
            Velocity = Utils.ParseFloat(node.Attributes["velocity"], 0);
        }
    }
}