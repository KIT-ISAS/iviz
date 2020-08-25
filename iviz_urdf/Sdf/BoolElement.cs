using System.Xml;
using Iviz.Msgs.MeshMsgs;

namespace Iviz.Sdf
{
    public static class BoolElement
    {
        internal static bool ValueOf(XmlNode node) => node.Value == "1" || node.Value == "true";
    }
}