using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public sealed class CommentElement : IElement
    {
        public ElementType Type => ElementType.Comment;
        public string Text { get; }

        internal CommentElement(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return $"[# '{Text}']";
        }

        public IEnumerable<string> ToCsString(bool _)
        {
            return new[] {$"//{Text}"};
        }

        public string ToRosString()
        {
            return "";
        }
    }
}