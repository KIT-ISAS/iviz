using System.Collections.Generic;
using System.Linq;

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
            if (!MsgParser.BuiltInsMaps.TryGetValue(ClassName, out string? alias))
            {
                return Enumerable.Empty<string>();
            }

            switch (alias)
            {
                case "string":
                    // const string have no comments
                    return new[] { $"public const string {FieldName} = \"{Value}\";" };

                case "time" or "duration":
                    return Enumerable.Empty<string>();
                
                default:
                    string result = $"public const {alias} {FieldName} = {Value};";
                    string comment = Comment.Trim();
                    if (comment.Length == 0)
                    {
                        return new[] { result };
                    }
                    
                    return new[]
                        {
                            $"/// <summary> {char.ToUpper(comment[0]).ToString()}{comment[1..]} </summary>",
                            result
                        };
            }
        }

        public string GetEntryForMd5Hash()
        {
            return $"{ClassName} {FieldName}={Value}";
        }

        public string ToRosString()
        {
            return $"{ClassName} {FieldName}={Value}";
        }
    }
}