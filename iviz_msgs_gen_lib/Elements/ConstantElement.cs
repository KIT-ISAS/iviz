using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public sealed class ConstantElement : IElement
    {
        public ElementType Type => ElementType.Constant;
        public string Comment { get; }
        public string ClassName { get; }
        public string FieldName { get; }
        public string Value { get; }

        internal ConstantElement(string comment, string className, string fieldName, string value)
        {
            Comment = comment;
            ClassName = className;
            FieldName = fieldName;
            Value = value;
        }

        public override string ToString()
        {
            return $"['{ClassName}' '{FieldName}' = '{Value}' // '{Comment}']";
        }

        public IEnumerable<string> ToCsString(bool _)
        {
            string commentStr = Comment.Length == 0 ? "" : $" //{Comment}";   

            string result;
            if (MsgParser.BuiltInsMaps.TryGetValue(ClassName, out string alias))
            {
                if (alias != "time" && alias != "duration")
                {
                    result = $"public const {alias} {FieldName} = {Value};{commentStr}";
                }
                else
                {
                    result = $"public static readonly {alias} {FieldName} = {Value};{commentStr}";
                }
            }
            else
            {
                // not really a valid constant!
                result = "";
            }


            return new[] {result};
        }

        public string GetEntryForMd5Hash()
        {
            return $"{ClassName} {FieldName}={Value}";
        }
    }
}