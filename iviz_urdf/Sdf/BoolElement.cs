using System.Xml;
using Iviz.Msgs.MeshMsgs;

namespace Iviz.Sdf
{
    public static class BoolElement
    {
        internal static bool ValueOf(XmlNode node) => 
            node.InnerText == "1" || node.InnerText == "true";
    }
}