using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public static class DoubleElement
    {
        internal static double ValueOf(XmlNode node) => double.Parse(node.Value, Utils.Culture);
    }
}