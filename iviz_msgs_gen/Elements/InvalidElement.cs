using System;
using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public class InvalidElement : IElement
    {
        public ElementType Type => ElementType.Invalid;
        public string Text { get; }

        internal InvalidElement(string text)
        {
            Console.WriteLine($"Adding invalid '{text}'");
            Text = text;
        }

        public override string ToString()
        {
            return $"[XXX '{Text}']";
        }

        public IEnumerable<string> ToCsString(bool _)
        {
            return Array.Empty<string>();
        }
    }
}