using System;
using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public sealed class ServiceSeparatorElement : IElement
    {
        public ElementType Type => ElementType.ServiceSeparator;

        internal ServiceSeparatorElement()
        {
        }

        public override string ToString()
        {
            return "[---]";
        }

        public IEnumerable<string> ToCsString(bool _)
        {
            return Array.Empty<string>();
        }

        public string ToRosString()
        {
            return "";
        }
    }
}