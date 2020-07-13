using System.Globalization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    internal static class Utils
    {
        public static CultureInfo Culture => BuiltIns.Culture;

        public static float ParseFloat(XmlAttribute attr)
        {
            if (attr is null)
            {
                throw new MalformedUrdfException("Expected a float, got null");
            }

            return float.Parse(attr.Value, Culture);
        }

        public static float ParseFloat(XmlAttribute attr, float @default)
        {
            return attr is null ? @default : float.Parse(attr.Value, Culture);
        }

        public static string ParseString(XmlAttribute attr)
        {
            if (attr is null)
            {
                throw new MalformedUrdfException("Expected a string, got null");
            }

            return attr.Value;
        }        
    }
}