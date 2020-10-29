using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public enum ElementType
    {
        Empty,
        Comment,
        Variable,
        Constant,
        Invalid,
        ServiceSeparator
    }
    
    public interface IElement
    {
        ElementType Type { get; }
        IEnumerable<string> ToCsString(bool isInStruct = false);
    }
}