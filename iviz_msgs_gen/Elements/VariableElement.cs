using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    public sealed class VariableElement : IElement
    {
        public const int NotAnArray = -1;
        public const int DynamicSizeArray = 0;

        readonly string rosClassToken;

        public ElementType Type => ElementType.Variable;
        public string Comment { get; }
        public string RosFieldName { get; }
        public string RosClassName { get; }
        public string FieldName { get; }
        public string CsClassName { get; }
        internal int ArraySize { get; }
        public bool IsArray => ArraySize != NotAnArray;
        public bool IsDynamicSizeArray => ArraySize == DynamicSizeArray;
        public bool IsFixedSizeArray => ArraySize > 0;
        public int FixedArraySize => IsFixedSizeArray ? ArraySize : -1;
        public ClassInfo ClassInfo { get; internal set; }
        public bool ClassIsStruct => ClassInfo != null && ClassInfo.ForceStruct;

        static readonly HashSet<string> Keywords = new HashSet<string>
        {
            "default",
            "byte",
            "sbyte",
            "char",
            "short",
            "ushort",
            "int",
            "uint",
            "float",
            "double",
            "long",
            "ulong",
            "bool",
            "string",
            "readonly",
            "override",
        };

        internal VariableElement(string comment, string rosClassToken, string fieldName, string parentClassName)
        {
            Comment = comment;
            this.rosClassToken = rosClassToken;

            RosFieldName = fieldName;

            FieldName = MsgParser.Sanitize(fieldName);
            if (FieldName == parentClassName)
            {
                FieldName += "_"; // C# forbids fields with the same name as the class
            }

            if (Keywords.Contains(fieldName))
            {
                FieldName = $"@{fieldName}";
            }

            int bracketLeft = rosClassToken.IndexOf('[');
            int bracketRight = rosClassToken.IndexOf(']');
            if (bracketLeft != -1 && bracketRight != -1)
            {
                RosClassName = rosClassToken.Substring(0, bracketLeft);
                string arrayLengthStr = rosClassToken.Substring(bracketLeft + 1, bracketRight - bracketLeft - 1);

                if (string.IsNullOrWhiteSpace(arrayLengthStr))
                {
                    ArraySize = DynamicSizeArray;
                }
                else if (int.TryParse(arrayLengthStr, out int arraySize))
                {
                    ArraySize = arraySize;
                }
                else
                {
                    //TODO: error!
                    ArraySize = NotAnArray;
                }
            }
            else
            {
                RosClassName = rosClassToken;
                ArraySize = NotAnArray;
            }

            int slashIndex;
            if (RosClassName == "Header")
            {
                RosClassName = "std_msgs/Header";
                CsClassName = "StdMsgs.Header";
            }
            else if (MsgParser.BuiltInsMaps.TryGetValue(RosClassName, out string className))
            {
                CsClassName = className;
            }
            else if ((slashIndex = RosClassName.IndexOf('/')) != -1)
            {
                string packageName = RosClassName.Substring(0, slashIndex);
                string classProper = RosClassName.Substring(slashIndex + 1);
                CsClassName = $"{MsgParser.Sanitize(packageName)}.{classProper}";
            }
            else
            {
                CsClassName = RosClassName;
            }
        }

        public override string ToString()
        {
            return $"['{rosClassToken}' '{RosFieldName}' # '{Comment}']";
        }

        public IEnumerable<string> ToCsString(bool isInStruct)
        {
            string attrStr = (FieldName != RosFieldName)
                ? $"[DataMember (Name = \"{RosFieldName}\")]"
                : "[DataMember]";

            string result;
            switch (ArraySize)
            {
                case NotAnArray:
                    result = $"public {CsClassName} {FieldName} {{ get; set; }}";
                    break;
                case DynamicSizeArray:
                    result = $"public {CsClassName}[] {FieldName} {{ get; set; }}";
                    break;
                default:
                {
                    result = isInStruct
                        ? $"fixed {CsClassName} {FieldName}[{ArraySize}];"
                        : $"public {CsClassName}[/*{ArraySize}*/] {FieldName} {{ get; set; }}";
                    break;
                }
            }

            string csString = Comment == ""
                ? $"{attrStr} {result}"
                : $"{attrStr} {result} //{Comment}";

            if (ArraySize <= 0 || !isInStruct)
            {
                return new[] { csString };
            }

            List<string> list = new List<string> { csString };
            for (int i = 0; i < ArraySize; i++)
            {
                list.Add($"public {CsClassName} {FieldName}{i}");
                list.Add("{");
                list.Add($"    readonly get => {FieldName}[{i}];");
                list.Add($"    set => {FieldName}[{i}] = value;");
                list.Add("}");
            }

            return list;
        }

        public string GetEntryForMd5Hash()
        {
            if (ClassInfo != null)
            {
                return $"{ClassInfo.GetMd5()} {RosFieldName}";
            }

            switch (ArraySize)
            {
                case NotAnArray:
                    return $"{RosClassName} {RosFieldName}";
                case DynamicSizeArray:
                    return $"{RosClassName}[] {RosFieldName}";
                default:
                    return $"{RosClassName}[{ArraySize}] {RosFieldName}";
            }
        }
    }
}