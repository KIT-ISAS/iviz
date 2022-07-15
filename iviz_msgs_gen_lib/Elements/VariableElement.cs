using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Iviz.MsgsGen
{
    public sealed class VariableElement : IElement
    {
        public const bool UseShared = false;

        const string CachedHeaderMd5 = "2176decaecbce78abc3b96ef049fabed";

        public const int NotAnArray = -1;
        public const int DynamicSizeArray = 0;

        readonly string rosClassToken;
        readonly bool serializeAsProperty;

        public ElementType Type => ElementType.Variable;
        public string Comment { get; }
        public string RosFieldName { get; }
        public string RosClassType { get; }
        public string CsFieldName { get; }
        public string CsClassType { get; }
        public int ArraySize { get; }
        public bool RentHint { get; }
        public bool IgnoreHint { get; }
        public bool IsArray => ArraySize != NotAnArray;
        public bool IsDynamicSizeArray => ArraySize == DynamicSizeArray;
        public bool IsFixedSizeArray => ArraySize > 0;
        public int FixedArraySize => IsFixedSizeArray ? ArraySize : -1;
        public ClassInfo? ClassInfo { get; internal set; }
        public bool ClassIsStruct => ClassInfo?.ForceStruct ?? ClassInfo.IsClassForceStruct(RosClassType);
        public bool ClassIsBlittable => ClassInfo?.IsBlittable ?? ClassInfo.IsClassBlittable(RosClassType);
        public bool ClassHasFixedSize => ClassInfo != null && ClassInfo.HasFixedSize;

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

        internal VariableElement(string comment, string rosClassToken, string fieldName,
            string? parentClassName = null,
            ClassInfo? classInfo = null,
            bool serializeAsProperty = false
        )
        {
            Comment = comment.Trim();
            this.rosClassToken = rosClassToken;
            this.serializeAsProperty = serializeAsProperty;

            RosFieldName = fieldName;

            CsFieldName = MsgParser.CsIfy(fieldName);
            if (CsFieldName == parentClassName)
            {
                CsFieldName += "_"; // C# forbids fields with the same name as the class
            }

            if (Keywords.Contains(fieldName))
            {
                CsFieldName = $"@{fieldName}";
            }

            int bracketLeft = rosClassToken.IndexOf('[');
            int bracketRight = rosClassToken.IndexOf(']');
            if (bracketLeft != -1 && bracketRight != -1)
            {
                RosClassType = rosClassToken.Substring(0, bracketLeft);
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
                RosClassType = rosClassToken;
                ArraySize = NotAnArray;
            }

            switch (RosClassType)
            {
                case "Header":
                    RosClassType = "std_msgs/Header";
                    CsClassType = "StdMsgs.Header";
                    break;
                case "builtin_interfaces/Time":
                    RosClassType = "time";
                    CsClassType = "time";
                    break;
                case "builtin_interfaces/Duration":
                    RosClassType = "duration";
                    CsClassType = "duration";
                    break;
                default:
                {
                    if (MsgParser.BuiltInsMaps.TryGetValue(RosClassType, out string? className))
                    {
                        CsClassType = className;
                    }
                    else
                    {
                        int slashIndex;
                        if ((slashIndex = RosClassType.IndexOf('/')) != -1)
                        {
                            string packageName = RosClassType.Substring(0, slashIndex);
                            string classProper = RosClassType.Substring(slashIndex + 1);
                            CsClassType = $"{MsgParser.CsIfy(packageName)}.{classProper}";
                        }
                        else
                        {
                            CsClassType = RosClassType;
                        }
                    }

                    break;
                }
            }

            ClassInfo = classInfo;
            RentHint = IsDynamicSizeArray && Comment.StartsWith("[Rent]");
            IgnoreHint = Comment.StartsWith("[Ignore]");
        }

        public override string ToString()
        {
            return $"['{rosClassToken}' '{RosFieldName}' // '{Comment}']";
        }

        public IEnumerable<string> ToCsString(bool isInStruct)
        {
            string attrStr = (CsFieldName != RosFieldName)
                ? $"[DataMember (Name = \"{RosFieldName}\")]"
                : "[DataMember]";

            string result;
            switch (ArraySize)
            {
                case NotAnArray when CsClassType == "string":
                {
                    if (serializeAsProperty)
                    {
                        result = isInStruct
                            ? $"public string? {CsFieldName};"
                            : $"public string {CsFieldName} {{ get; set; }}";
                    }
                    else
                    {
                        result = $"public string {CsFieldName};";
                    }
                }

                    break;
                case NotAnArray:
                    if (serializeAsProperty)
                    {
                        result = isInStruct
                            ? $"public {CsClassType} {CsFieldName};"
                            : $"public {CsClassType} {CsFieldName} {{ get; set; }}";
                    }
                    else
                    {
                        result = $"public {CsClassType} {CsFieldName};";
                    }

                    break;
                case DynamicSizeArray:
                {
                    if (serializeAsProperty)
                    {
                        result = isInStruct
                            ? $"public {CsClassType}[]? {CsFieldName};"
                            : $"public {CsClassType}[] {CsFieldName} {{ get; set; }}";
                    }
                    else
                    {
                        result = RentHint
                            ? $"public Tools.SharedRent<{CsClassType}> {CsFieldName};"
                            : $"public {CsClassType}[] {CsFieldName};";
                    }
                }

                    break;
                default:
                {
                    if (serializeAsProperty)
                    {
                        result = isInStruct
                            ? $"public {CsClassType}[/*{ArraySize}*/]? {CsFieldName} {{ get; set; }}"
                            : $"public {CsClassType}[/*{ArraySize}*/] {CsFieldName} {{ get; set; }}";
                    }
                    else
                    {
                        result = $"public {CsClassType}[/*{ArraySize}*/] {CsFieldName};";
                    }

                    break;
                }
            }

            string comment = Comment.Trim();
            if (comment.Length == 0)
            {
                return new[] { $"{attrStr} {result}" };
            }

            return new[]
            {
                $"/// <summary> {char.ToUpper(comment[0]).ToString()}{comment[1..]} </summary>",
                $"{attrStr} {result}"
            };
        }

        public string ToRosString()
        {
            return $"{rosClassToken} {RosFieldName}";
        }

        public string GetEntryForMd5Hash(string parentPackageName)
        {
            if (ClassInfo != null)
            {
                return $"{ClassInfo.Md5Hash} {RosFieldName}";
            }

            if (ClassInfo.IsBuiltinType(RosClassType))
            {
                return ArraySize switch
                {
                    NotAnArray => $"{RosClassType} {RosFieldName}",
                    DynamicSizeArray => $"{RosClassType}[] {RosFieldName}",
                    _ => $"{RosClassType}[{ArraySize}] {RosFieldName}"
                };
            }

            // now we start improvising
            if (RosClassType == "std_msgs/Header")
            {
                return $"{CachedHeaderMd5} {RosFieldName}";
            }

            string fullRosClassName =
                RosClassType.Contains("/") ? RosClassType : $"{parentPackageName}/{RosClassType}";

            // is it in the assembly?
            Type? guessType = BuiltIns.TryGetTypeFromMessageName(fullRosClassName);
            if (guessType == null)
            {
                // nope? we bail out
                throw new MessageDependencyException(
                    $"Cannot find md5 for type '{RosClassType}' or '{fullRosClassName}'.");
            }

            string md5Sum = BuiltIns.GetMd5Sum(guessType);
            return $"{md5Sum} {RosFieldName}";
        }
    }

    // this class copies functions from Iviz.Msgs to avoid depending on that project 
    internal static class BuiltIns
    {
        public static Type? TryGetTypeFromMessageName(string fullRosMessageName, string assemblyName = "Iviz.Msgs")
        {
            // caller assembly should reference Iviz.Msgs!
            string guessName = $"Iviz.Msgs.{RosNameToCs(fullRosMessageName)}, {assemblyName}";
            return Type.GetType(guessName);
        }

        public static string GetMd5Sum(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetClassStringConstant(type, "Md5Sum");
        }

        static string GetClassStringConstant(Type type, string name)
        {
            string? constant = (string?)type.GetField(name)?.GetRawConstantValue();
            if (constant == null)
            {
                throw new ArgumentException($"Failed to resolve constant '{name}' in class {type.FullName}",
                    nameof(name));
            }

            return constant;
        }

        public static string RosNameToCs(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StringBuilder str = new();
            str.Append(char.ToUpper(name[0], CultureInfo.InvariantCulture));
            for (int i = 1; i < name.Length; i++)
            {
                switch (name[i])
                {
                    case '_' when i != name.Length - 1:
                        str.Append(char.ToUpper(name[i + 1], CultureInfo.InvariantCulture));
                        i++;
                        break;
                    case '/':
                        str.Append('.');
                        break;
                    default:
                        str.Append(name[i]);
                        break;
                }
            }

            return str.ToString();
        }
    }
}