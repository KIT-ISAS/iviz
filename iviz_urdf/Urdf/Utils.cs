using System.Globalization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    internal static class Utils
    {
        public static CultureInfo Culture => BuiltIns.Culture;

        public static float ParseFloat(XmlAttribute? attr)
        {
            if (attr is null)
            {
                throw new MalformedUrdfException("Expected a float, got null");
            }

            if (float.TryParse(attr.Value, NumberStyles.Any, Culture, out float f))
            {
                return f;
            } 

            throw new MalformedUrdfException("Expected float at ", attr);
        }

        public static float ParseFloat(XmlAttribute? attr, float @default)
        {
            if (attr is null)
            {
                return @default;
            }

            if (float.TryParse(attr.Value, NumberStyles.Any, Culture, out float f))
            {
                return f;
            } 
                
            throw new MalformedUrdfException("Expected float at ", attr);
        }

        public static string ParseString(XmlAttribute? attr)
        {
            if (attr?.Value is null)
            {
                throw new MalformedUrdfException("Expected a string attribute, got null");
            }

            return attr.Value;
        }        
        
        public static string ParseString(XmlAttribute? attr, string @default)
        {
            return attr?.Value ?? @default;
        }
    }
}