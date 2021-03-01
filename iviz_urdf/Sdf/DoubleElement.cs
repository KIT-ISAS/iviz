using System.Globalization;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public static class DoubleElement
    {
        internal static double ValueOf(XmlNode? node) => 
            node is null ? throw new MalformedSdfException() : 
            double.Parse(node.InnerText, NumberStyles.Any, Utils.Culture);
    }
}