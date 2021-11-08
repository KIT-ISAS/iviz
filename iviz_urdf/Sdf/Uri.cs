using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Uri
    {
        public string Value { get; }

        public System.Uri ToUri() => new System.Uri(Value);
        
        internal Uri(XmlNode node)
        {
            Value = node.InnerText ?? throw new MalformedSdfException();
        }        

        internal Uri(System.Uri value)
        {
            Value = value.ToString();
        }        
        
        public override string ToString()
        {
            return $"[{Value}]";
        }
    }
}