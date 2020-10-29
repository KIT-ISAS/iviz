using System;
using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public sealed class EmptyElement : IElement
    {
        public ElementType Type => ElementType.Empty;

        internal EmptyElement()
        {
        }

        public override string ToString()
        {
            return "[]";
        }

        public IEnumerable<string> ToCsString(bool _)
        {
            return Array.Empty<string>();
        }
    }
}