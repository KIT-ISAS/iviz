using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen
{
    public sealed class ClassInfo
    {
        internal const int UninitializedSize = -2;
        internal const int UnknownSizeAtCompileTime = -1;

        static readonly Dictionary<string, int> BuiltInsSizes = new Dictionary<string, int>
        {
            {"bool", 1},
            {"int8", 1},
            {"uint8", 1},
            {"int16", 2},
            {"uint16", 2},
            {"int32", 4},
            {"uint32", 4},
            {"int64", 8},
            {"uint64", 8},
            {"float32", 4},
            {"float64", 8},
            {"time", 8},
            {"duration", 8},
            {"char", 1},
            {"byte", 1}
        };


        static readonly HashSet<string> BuiltInTypes = new HashSet<string>
        {
            "bool",
            "int8",
            "uint8",
            "int16",
            "uint16",
            "int32",
            "uint32",
            "int64",
            "uint64",
            "float32",
            "float64",
            "time",
            "duration",
            "char",
            "byte",
            "string"
        };

        static readonly HashSet<string> ForceStructs = new HashSet<string>
        {
            "geometry_msgs/Vector3",
            "geometry_msgs/Point",
            "geometry_msgs/Point32",
            "geometry_msgs/Quaternion",
            "geometry_msgs/Pose",
            "geometry_msgs/Transform",
            "geometry_msgs/Twist",
            "geometry_msgs/Wrench",
            "geometry_msgs/Accel",
            "geometry_msgs/Inertia",
            "std_msgs/ColorRGBA",
            "std_msgs/Float32",
            "std_msgs/Float64",
            "std_msgs/Time",
            "std_msgs/Duration",
            "std_msgs/Int16",
            "std_msgs/Int32",
            "std_msgs/Int64",
            "std_msgs/Int8",
            "std_msgs/UInt16",
            "std_msgs/UInt32",
            "std_msgs/UInt64",
            "std_msgs/UInt8",
            "std_msgs/Byte",
            "std_msgs/Char",
            "iviz_msgs/Color",
            "iviz_msgs/Vector2",
            "iviz_msgs/Vector3",
            "iviz_msgs/Triangle",
            "iviz_msgs/BoundingBox"
        };

        static readonly UTF8Encoding Utf8 = new UTF8Encoding(false);

        readonly string csPackage;
        readonly List<IElement> elements;
        readonly string fullMessageText;
        readonly List<VariableElement> variables;

        int fixedSize = UninitializedSize;
        string md5;
        string md5File;

        internal ClassInfo(string package, string path)
        {
            Console.WriteLine($"-- Parsing '{path}'");

            RosPackage = package;
            csPackage = MsgParser.Sanitize(package);
            RosName = Path.GetFileNameWithoutExtension(path);
            fullMessageText = File.ReadAllText(path);

            var lines = File.ReadAllLines(path);
            elements = MsgParser.ParseFile(lines, RosName);

            ForceStruct = ForceStructs.Contains($"{package}/{RosName}");

            variables = elements.OfType<VariableElement>().ToList();
        }

        public string RosPackage { get; }
        public string RosName { get; }
        public bool ForceStruct { get; }

        internal static void DoResolveClasses(PackageInfo packageInfo, string package,
            IEnumerable<VariableElement> variables)
        {
            foreach (var variable in variables)
            {
                if (!BuiltInsSizes.ContainsKey(variable.RosClassName) &&
                    packageInfo.TryGet(variable.RosClassName, package, out var classInfo))
                {
                    variable.ClassInfo = classInfo;
                }
            }
        }

        internal void ResolveClasses(PackageInfo packageInfo)
        {
            DoResolveClasses(packageInfo, RosPackage, variables);
        }

        internal static int DoCheckFixedSize(ref int fixedSize, IEnumerable<VariableElement> variables)
        {
            if (fixedSize != UninitializedSize)
            {
                return fixedSize;
            }

            fixedSize = 0;
            foreach (var variable in variables)
            {
                if (!variable.IsDynamicSizeArray)
                {
                    if (BuiltInsSizes.TryGetValue(variable.RosClassName, out var size))
                    {
                        fixedSize += variable.IsFixedSizeArray ? size * variable.ArraySize : size;
                    }
                    else if (variable.ClassInfo != null &&
                             variable.ClassInfo.CheckFixedSize() != UnknownSizeAtCompileTime)
                    {
                        fixedSize += variable.IsFixedSizeArray
                            ? size * variable.ClassInfo.fixedSize
                            : variable.ClassInfo.fixedSize;
                    }
                    else
                    {
                        fixedSize = UnknownSizeAtCompileTime;
                        return UnknownSizeAtCompileTime;
                    }
                }
                else
                {
                    fixedSize = UnknownSizeAtCompileTime;
                    return UnknownSizeAtCompileTime;
                }
            }

            return fixedSize;
        }

        internal int CheckFixedSize()
        {
            return DoCheckFixedSize(ref fixedSize, variables);
        }


        internal static IEnumerable<string> CreateLengthProperty(IReadOnlyCollection<VariableElement> variables,
            int fixedSize,
            bool forceStruct)
        {
            var readOnlyId = forceStruct ? "readonly " : "";
            if (fixedSize != UnknownSizeAtCompileTime)
            {
                return new List<string>
                {
                    $"public {readOnlyId}int RosMessageLength => {fixedSize};"
                };
            }

            if (variables.Count == 0)
            {
                return new List<string>
                {
                    $"public {readOnlyId}int RosMessageLength => 0;"
                };
            }

            var fieldsWithSize = new List<string>();
            var fieldSize = 0;
            foreach (var variable in variables)
            {
                if (BuiltInsSizes.TryGetValue(variable.RosClassName, out var size))
                {
                    if (!variable.IsArray)
                    {
                        fieldSize += size;
                    }
                    else if (variable.IsDynamicSizeArray)
                    {
                        fieldsWithSize.Add($"size += {size} * {variable.FieldName}.Length;");
                        fieldSize += 4;
                    }
                    else
                    {
                        fieldSize += size * variable.ArraySize;
                    }
                }
                else
                {
                    if (!variable.IsArray)
                    {
                        if (variable.ClassInfo != null && variable.ClassInfo.fixedSize != UnknownSizeAtCompileTime)
                        {
                            fieldSize += variable.ClassInfo.fixedSize;
                        }
                        else
                        {
                            if (variable.CsClassName == "string")
                            {
                                fieldSize += 4;
                                fieldsWithSize.Add($"size += BuiltIns.UTF8.GetByteCount({variable.FieldName});");
                            }
                            else
                            {
                                fieldsWithSize.Add($"size += {variable.FieldName}.RosMessageLength;");
                            }
                        }
                    }
                    else
                    {
                        if (variable.IsDynamicSizeArray)
                        {
                            fieldSize += 4;
                        }

                        if (variable.ClassInfo != null && variable.ClassInfo.fixedSize != UnknownSizeAtCompileTime)
                        {
                            fieldsWithSize.Add(
                                $"size += {variable.ClassInfo.fixedSize} * {variable.FieldName}.Length;");
                        }
                        else
                        {
                            if (variable.CsClassName == "string")
                            {
                                fieldsWithSize.Add($"size += 4 * {variable.FieldName}.Length;");
                                fieldsWithSize.Add($"foreach (string s in {variable.FieldName})");
                                fieldsWithSize.Add("{");
                                fieldsWithSize.Add("    size += BuiltIns.UTF8.GetByteCount(s);");
                                fieldsWithSize.Add("}");
                            }
                            else
                            {
                                fieldsWithSize.Add($"foreach (var i in {variable.FieldName})");
                                fieldsWithSize.Add("{");
                                fieldsWithSize.Add("    size += i.RosMessageLength;");
                                fieldsWithSize.Add("}");
                            }
                        }
                    }
                }
            }

            var lines = new List<string>();
            lines.Add($"public {readOnlyId}int RosMessageLength");
            lines.Add("{");
            lines.Add("    get {");
            lines.Add($"        int size = {fieldSize};");
            foreach (var entry in fieldsWithSize)
            {
                lines.Add($"        {entry}");
            }

            lines.Add("        return size;");
            lines.Add("    }");
            lines.Add("}");

            return lines;
        }

        internal static IEnumerable<string> CreateConstructors(IReadOnlyCollection<VariableElement> variables,
            string name,
            bool forceStruct)
        {
            var lines = new List<string>();

            if (!forceStruct)
            {
                lines.Add("/// <summary> Constructor for empty message. </summary>");
                lines.Add($"public {name}()");
                lines.Add("{");
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.RosClassName))
                    {
                        if (variable.RosClassName == "string" && !variable.IsArray)
                        {
                            lines.Add($"    {variable.FieldName} = \"\";");
                        }
                        else if (variable.IsDynamicSizeArray)
                        {
                            lines.Add($"    {variable.FieldName} = System.Array.Empty<{variable.CsClassName}>();");
                        }
                        else if (variable.IsArray)
                        {
                            lines.Add($"    {variable.FieldName} = new {variable.CsClassName}[{variable.ArraySize}];");
                        }
                    }
                    else
                    {
                        if (variable.IsDynamicSizeArray)
                        {
                            lines.Add($"    {variable.FieldName} = System.Array.Empty<{variable.CsClassName}>();");
                        }
                        else if (variable.IsArray)
                        {
                            lines.Add($"    {variable.FieldName} = new {variable.CsClassName}[{variable.ArraySize}];");
                        }
                        else if (variable.ClassInfo == null || !variable.ClassInfo.ForceStruct)
                        {
                            lines.Add($"    {variable.FieldName} = new {variable.CsClassName}();");
                        }
                    }
                }

                lines.Add("}");
                lines.Add("");
            }

            if (variables.Any())
            {
                lines.Add("/// <summary> Explicit constructor. </summary>");

                //
                static string ParamToArg(VariableElement v)
                {
                    if (v.IsArray)
                    {
                        return $"{v.CsClassName}[] {v.FieldName}";
                    }

                    if (v.ClassInfo != null && v.ClassInfo.ForceStruct)
                    {
                        return $"in {v.CsClassName} {v.FieldName}";
                    }

                    return $"{v.CsClassName} {v.FieldName}";
                }

                //
                var args = string.Join(", ", variables.Select(ParamToArg));
                lines.Add($"public {name}({args})");
                lines.Add("{");
                foreach (var variable in variables)
                {
                    if (variable.ArraySize > 0 && forceStruct)
                    {
                        lines.Add(
                            $"    if ({variable.FieldName} is null) throw new System.ArgumentNullException(nameof({variable.FieldName}));");
                        lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                        lines.Add("    {");
                        lines.Add($"        this.{variable.FieldName}[i] = {variable.FieldName}[i];");
                        lines.Add("    }");
                    }
                    else
                    {
                        lines.Add($"    this.{variable.FieldName} = {variable.FieldName};");
                    }
                }

                lines.Add("}");
                lines.Add("");
            }

            lines.Add("/// <summary> Constructor with buffer. </summary>");
            lines.Add($"internal {name}(ref Buffer b)");
            lines.Add("{");
            if (forceStruct)
            {
                lines.Add("    b.Deserialize(out this);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.RosClassName))
                    {
                        switch (variable.ArraySize)
                        {
                            case VariableElement.NotAnArray:
                                lines.Add(variable.CsClassName == "string"
                                    ? $"    {variable.FieldName} = b.DeserializeString();"
                                    : $"    {variable.FieldName} = b.Deserialize<{variable.CsClassName}>();");
                                break;
                            case VariableElement.DynamicSizeArray:
                                lines.Add(variable.CsClassName == "string"
                                    ? $"    {variable.FieldName} = b.DeserializeStringArray();"
                                    : $"    {variable.FieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                break;
                            default:
                                lines.Add(
                                    variable.CsClassName == "string"
                                        ? $"    {variable.FieldName} = b.DeserializeStringArray({variable.ArraySize});"
                                        : $"    {variable.FieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
                                break;
                        }
                    }
                    else
                    {
                        switch (variable.ArraySize)
                        {
                            case VariableElement.NotAnArray:
                                lines.Add($"    {variable.FieldName} = new {variable.CsClassName}(ref b);");
                                break;
                            case VariableElement.DynamicSizeArray when variable.ClassIsStruct:
                                lines.Add(
                                    $"    {variable.FieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                break;
                            case VariableElement.DynamicSizeArray:
                                lines.Add($"    {variable.FieldName} = b.DeserializeArray<{variable.CsClassName}>();");
                                lines.Add($"    for (int i = 0; i < {variable.FieldName}.Length; i++)");
                                lines.Add("    {");
                                lines.Add($"        {variable.FieldName}[i] = new {variable.CsClassName}(ref b);");
                                lines.Add("    }");
                                break;
                            default:
                            {
                                if (variable.ClassIsStruct)
                                {
                                    lines.Add(
                                        $"    {variable.FieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
                                }
                                else
                                {
                                    lines.Add(
                                        $"    {variable.FieldName} = b.DeserializeArray<{variable.CsClassName}>({variable.ArraySize});");
                                    lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                                    lines.Add("    {");
                                    lines.Add($"        {variable.FieldName}[i] = new {variable.CsClassName}(ref b);");
                                    lines.Add("    }");
                                }

                                break;
                            }
                        }
                    }
                }
            }

            lines.Add("}");
            lines.Add("");

            var readOnlyId = forceStruct ? "readonly " : "";
            lines.Add($"public {readOnlyId}ISerializable RosDeserialize(ref Buffer b)");
            lines.Add("{");
            lines.Add($"    return new {name}(ref b);");
            lines.Add("}");
            lines.Add("");

            lines.Add($"{readOnlyId}{name} IDeserializable<{name}>.RosDeserialize(ref Buffer b)");
            lines.Add("{");
            lines.Add($"    return new {name}(ref b);");
            lines.Add("}");

            if (forceStruct)
            {
                var myVars = string.Join(", ", variables.Select(x => x.FieldName));

                lines.Add("");
                lines.Add($"public override readonly int GetHashCode() => ({myVars}).GetHashCode();");
                lines.Add("");
                lines.Add($"public override readonly bool Equals(object o) => o is {name} s && Equals(s);");
                lines.Add("");

                var oVars = string.Join(", ", variables.Select(x => $"o.{x.FieldName}"));

                lines.Add($"public readonly bool Equals({name} o) => ({myVars}) == ({oVars});");
                lines.Add("");
                lines.Add($"public static bool operator==(in {name} a, in {name} b) => a.Equals(b);");
                lines.Add("");
                lines.Add($"public static bool operator!=(in {name} a, in {name} b) => !a.Equals(b);");
            }

            return lines;
        }

        internal static IEnumerable<string> CreateSerializers(IReadOnlyCollection<VariableElement> variables,
            bool forceStruct)
        {
            var lines = new List<string>();

            var readOnlyId = forceStruct ? "readonly " : "";

            lines.Add($"public {readOnlyId}void RosSerialize(ref Buffer b)");
            lines.Add("{");
            if (forceStruct)
            {
                lines.Add("    b.Serialize(this);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.RosClassName))
                    {
                        if (!variable.IsArray)
                        {
                            lines.Add($"    b.Serialize({variable.FieldName});");
                        }
                        else
                        {
                            lines.Add(variable.CsClassName == "string"
                                ? $"    b.SerializeArray({variable.FieldName}, {variable.ArraySize});"
                                : $"    b.SerializeStructArray({variable.FieldName}, {variable.ArraySize});");
                        }
                    }
                    else
                    {
                        if (!variable.IsArray)
                        {
                            lines.Add($"    {variable.FieldName}.RosSerialize(ref b);");
                        }
                        else
                        {
                            lines.Add(variable.ClassIsStruct
                                ? $"    b.SerializeStructArray({variable.FieldName}, {variable.ArraySize});"
                                : $"    b.SerializeArray({variable.FieldName}, {variable.ArraySize});");
                        }
                    }
                }
            }

            lines.Add("}");

            lines.Add("");
            lines.Add($"public {readOnlyId}void RosValidate()");
            lines.Add("{");
            foreach (var variable in variables)
            {
                if (variable.ArraySize > 0)
                {
                    if (!forceStruct)
                    {
                        lines.Add(
                            $"    if ({variable.FieldName} is null) throw new System.NullReferenceException(nameof({variable.FieldName}));");
                        lines.Add(
                            $"    if ({variable.FieldName}.Length != {variable.ArraySize}) throw new System.IndexOutOfRangeException();");
                    }
                }
                else if (!variable.IsArray &&
                         (BuiltInTypes.Contains(variable.RosClassName) && variable.RosClassName != "string" ||
                          variable.ClassIsStruct))
                {
                    // do nothing
                }
                else
                {
                    lines.Add(
                        $"    if ({variable.FieldName} is null) throw new System.NullReferenceException(nameof({variable.FieldName}));");
                    if (!variable.IsArray && variable.RosClassName != "string")
                    {
                        lines.Add($"    {variable.FieldName}.RosValidate();");
                    }
                }

                if (variable.IsArray)
                {
                    if (variable.RosClassName == "string")
                    {
                        lines.Add($"    for (int i = 0; i < {variable.FieldName}.Length; i++)");
                        lines.Add("    {");
                        lines.Add(
                            $"        if ({variable.FieldName}[i] is null) throw new System.NullReferenceException($\"{{nameof({variable.FieldName})}}[{{i}}]\");");
                        lines.Add("    }");
                    }
                    else if (!BuiltInTypes.Contains(variable.RosClassName) &&
                             !(variable.ClassInfo?.ForceStruct ?? false))
                    {
                        lines.Add($"    for (int i = 0; i < {variable.FieldName}.Length; i++)");
                        lines.Add("    {");
                        lines.Add(
                            $"        if ({variable.FieldName}[i] is null) throw new System.NullReferenceException($\"{{nameof({variable.FieldName})}}[{{i}}]\");");
                        lines.Add($"        {variable.FieldName}[i].RosValidate();");
                        lines.Add("    }");
                    }
                }
            }

            lines.Add("}");

            return lines;
        }

        public string ToCString()
        {
            var str = new StringBuilder();

            str.AppendLine("/* This file was created automatically, do not edit! */");
            str.AppendLine();

            if (ForceStruct)
            {
                str.AppendLine("using System.Runtime.InteropServices;");
            }

            str.AppendLine("using System.Runtime.Serialization;");
            str.AppendLine();

            str.AppendLine($"namespace Iviz.Msgs.{csPackage}");
            str.AppendLine("{");

            foreach (var entry in CreateClassContent())
            {
                str.Append("    ").AppendLine(entry);
            }

            str.AppendLine("}");

            return str.ToString();
        }

        static IEnumerable<string> Compress(string catDependencies)
        {
            var lines = new List<string>();
            var inputBytes = Utf8.GetBytes(catDependencies);

            using var outputStream = new MemoryStream();

            using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gZipStream.Write(inputBytes, 0, inputBytes.Length);
            }

            var base64 = Convert.ToBase64String(outputStream.ToArray());

            const int lineWidth = 80;
            for (var i = 0; i < base64.Length; i += lineWidth)
            {
                bool last;
                int end;
                if (i + lineWidth < base64.Length)
                {
                    last = false;
                    end = i + lineWidth;
                }
                else
                {
                    last = true;
                    end = base64.Length;
                }

                var sub = base64.Substring(i, end - i);
                if (!last)
                {
                    lines.Add($"\"{sub}\" +");
                }
                else
                {
                    lines.Add($"\"{sub}\";");
                }
            }

            lines.Add("");

            return lines;
        }

        IEnumerable<string> CreateClassContent()
        {
            var lines = new List<string>();
            lines.Add($"[DataContract (Name = \"{RosPackage}/{RosName}\")]");
            if (ForceStruct)
            {
                lines.Add("[StructLayout(LayoutKind.Sequential)]");
                lines.Add(variables.Any(element => element.IsFixedSizeArray)
                    ? $"public unsafe struct {RosName} : IMessage, System.IEquatable<{RosName}>, IDeserializable<{RosName}>"
                    : $"public struct {RosName} : IMessage, System.IEquatable<{RosName}>, IDeserializable<{RosName}>");
            }
            else
            {
                lines.Add($"public sealed class {RosName} : IMessage, IDeserializable<{RosName}>");
            }

            lines.Add("{");

            var csElements = elements.SelectMany(element => element.ToCsString(ForceStruct));
            foreach (var entry in csElements)
            {
                lines.Add($"    {entry}");
            }

            if (elements.Count != 0)
            {
                lines.Add("");
            }

            var constructors = CreateConstructors(variables, RosName, ForceStruct);
            foreach (var entry in constructors)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var serializer = CreateSerializers(variables, ForceStruct);
            foreach (var entry in serializer)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var lengthProperty = CreateLengthProperty(variables, fixedSize, ForceStruct);
            foreach (var entry in lengthProperty)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var readOnlyId = ForceStruct ? "readonly " : "";
            lines.Add($"    public {readOnlyId}string RosType => RosMessageType;");

            lines.Add("");
            lines.Add("    /// <summary> Full ROS name of this message. </summary>");
            lines.Add($"    [Preserve] public const string RosMessageType = \"{RosPackage}/{RosName}\";");


            lines.Add("");

            lines.Add("    /// <summary> MD5 hash of a compact representation of the message. </summary>");
            lines.Add($"    [Preserve] public const string RosMd5Sum = \"{GetMd5Property()}\";");

            lines.Add("");

            lines.Add(
                "    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
            lines.Add("    [Preserve] public const string RosDependenciesBase64 =");

            var catDependencies = GetCatDependencies();
            var compressedDeps = Compress(catDependencies);
            foreach (var entry in compressedDeps)
            {
                lines.Add($"            {entry}");
            }

            if (Additions.Contents.TryGetValue($"{RosPackage}/{RosName}", out var extraLines))
            {
                lines.Add("    /// Custom iviz code");
                foreach (var entry in extraLines)
                {
                    lines.Add($"    {entry}");
                }
            }

            lines.Add("}");

            return lines;
        }

        public void AddDependencies(List<ClassInfo> dependencies)
        {
            foreach (var variable in variables)
            {
                if (variable.ClassInfo != null &&
                    !dependencies.Contains(variable.ClassInfo))
                {
                    dependencies.Add(variable.ClassInfo);
                    variable.ClassInfo.AddDependencies(dependencies);
                }
            }
        }

        string GetCatDependencies()
        {
            var dependencies = new List<ClassInfo>();
            AddDependencies(dependencies);

            var builder = new StringBuilder();
            builder.AppendLine(fullMessageText);

            foreach (var classInfo in dependencies)
            {
                builder.AppendLine("================================================================================");
                builder.AppendLine($"MSG: {classInfo.RosPackage}/{classInfo.RosName}");
                builder.AppendLine(classInfo.fullMessageText);
            }

            return builder.ToString();
        }

        string GetMd5Property()
        {
            GetMd5();
            var md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, md5File);
        }

        internal string GetMd5()
        {
            if (md5 != null)
            {
                return md5;
            }

            var str = new StringBuilder();

            var constants = elements.Where(x => x.Type == ElementType.Constant).Cast<ConstantElement>()
                .Select(x => x.ToMd5String()).ToArray();

            if (constants.Any())
            {
                str.AppendJoin("\n", constants);
                if (variables.Any())
                {
                    str.Append("\n");
                }
            }

            str.AppendJoin("\n", variables.Select(x => x.GetEntryForMd5Hash()));

            md5File = str.ToString();

            md5 = GetMd5Hash(MD5.Create(), md5File);

            return md5;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Utf8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}