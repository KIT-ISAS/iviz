using System;
using System.Xml;

namespace Iviz.Urdf
{
    public class MalformedUrdfException : Exception
    {
        public XmlNode? Node { get; }

        public MalformedUrdfException(XmlNode? node) :
            base(node == null
                ? $"Error at node '(null)'"
                : $"Error at node '{node}'")
        {
            Node = node;
        }

        public MalformedUrdfException(string message, XmlNode? node)
            : base(node == null
                ? $"{message} ('null')"
                : $"{message}' ({node}')")
        {
            Node = node;
        }

        public MalformedUrdfException()
        {
        }

        public MalformedUrdfException(string message) : base(message)
        {
        }

        public MalformedUrdfException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}