using System;
using System.Xml;

namespace Iviz.Sdf
{
    public class MalformedSdfException : Exception
    {
        public XmlNode? Node { get; }

        public MalformedSdfException(XmlNode node) : base("Error at or around node " + node)
        {
            Node = node;
        }

        public MalformedSdfException()
        {
        }

        public MalformedSdfException(string message) : base(message)
        {
        }

        public MalformedSdfException(XmlNode node, string message) : base(message)
        {
            Node = node;
        }

        public MalformedSdfException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}