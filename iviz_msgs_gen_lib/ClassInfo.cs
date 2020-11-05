using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen
{
    internal enum ActionMessageType
    {
        None,
        Goal,
        Feedback,
        Result,
        ActionGoal,
        ActionFeedback,
        ActionResult,
        Action
    }

    public sealed class ClassInfo
    {
        internal const int UninitializedSize = -2;
        const int UnknownSizeAtCompileTime = -1;

        static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

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

        internal static bool IsClassForceStruct(string f) => ForceStructs.Contains(f);

        static readonly UTF8Encoding Utf8 = new UTF8Encoding(false);

        readonly string csPackage;
        readonly IElement[] elements;
        readonly string fullMessageText;
        readonly VariableElement[] variables;

        public int FixedSize { get; set; } = UninitializedSize;
        public bool HasFixedSize => FixedSize != UnknownSizeAtCompileTime && FixedSize != UninitializedSize;
        public ReadOnlyCollection<IElement> Elements { get; }

        ActionMessageType ActionMessageType { get; }
        string ActionRoot { get; }

        string md5;
        string md5File;

        public ClassInfo(string package, string path)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException($"File {path} does not exist.");
            }

            Console.WriteLine($"-- Parsing '{path}'");

            RosPackage = package;
            csPackage = MsgParser.Sanitize(package);
            Name = Path.GetFileNameWithoutExtension(path);
            fullMessageText = File.ReadAllText(path);

            var lines = File.ReadAllLines(path);
            elements = MsgParser.ParseFile(lines, Name).ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);

            ForceStruct = ForceStructs.Contains($"{package}/{Name}");

            variables = elements.OfType<VariableElement>().ToArray();
        }

        public ClassInfo(string package, string name, IEnumerable<IElement> newElements) :
            this(package, name, newElements, null, ActionMessageType.None)
        {
        }

        internal ClassInfo(string package, string name, IEnumerable<IElement> newElements,
            string actionRoot, ActionMessageType actionMessageType)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (newElements == null)
            {
                throw new ArgumentNullException(nameof(newElements));
            }

            Console.WriteLine($"-- Parsing synthetic '{package}/{name}'");

            RosPackage = package;
            csPackage = MsgParser.Sanitize(package);
            Name = name;

            elements = newElements.ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);

            ForceStruct = false;
            fullMessageText = string.Join("\n", elements.Select(elem => elem.ToRosString()));

            variables = elements.OfType<VariableElement>().ToArray();

            ActionRoot = actionRoot;
            ActionMessageType = actionMessageType;
        }

        public string RosPackage { get; }
        public string Name { get; }
        public bool ForceStruct { get; }
        public string FullRosName => $"{RosPackage}/{Name}";

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

        internal static int DoCheckFixedSize(IEnumerable<VariableElement> variables)
        {
            int fixedSize = 0;
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
                            ? size * variable.ClassInfo.FixedSize
                            : variable.ClassInfo.FixedSize;
                    }
                    else
                    {
                        return UnknownSizeAtCompileTime;
                    }
                }
                else
                {
                    return UnknownSizeAtCompileTime;
                }
            }

            return fixedSize;
        }

        internal int CheckFixedSize()
        {
            if (FixedSize == UninitializedSize)
            {
                FixedSize = DoCheckFixedSize(variables);
            }

            return FixedSize;
        }


        internal static IEnumerable<string> CreateLengthProperty(IReadOnlyCollection<VariableElement> variables,
            int fixedSize,
            bool forceStruct)
        {
            var readOnlyId = forceStruct ? "readonly " : "";
            if (fixedSize != UnknownSizeAtCompileTime)
            {
                return new[]
                {
                    $"/// <summary> Constant size of this message. </summary>",
                    $"public const int RosFixedMessageLength = {fixedSize};",
                    "",
                    $"public {readOnlyId}int RosMessageLength => RosFixedMessageLength;"
                };
            }

            if (variables.Count == 0)
            {
                return new[]
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
                        fieldsWithSize.Add($"size += {size} * {variable.CsFieldName}.Length;");
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
                        if (variable.ClassInfo != null && variable.ClassHasFixedSize)
                        {
                            fieldSize += variable.ClassInfo.FixedSize;
                        }
                        else
                        {
                            if (variable.CsClassName == "string")
                            {
                                fieldSize += 4;
                                fieldsWithSize.Add($"size += BuiltIns.UTF8.GetByteCount({variable.CsFieldName});");
                            }
                            else
                            {
                                fieldsWithSize.Add($"size += {variable.CsFieldName}.RosMessageLength;");
                            }
                        }
                    }
                    else
                    {
                        if (variable.IsDynamicSizeArray)
                        {
                            fieldSize += 4;
                        }

                        if (variable.ClassInfo != null && variable.ClassInfo.FixedSize != UnknownSizeAtCompileTime)
                        {
                            fieldsWithSize.Add(
                                $"size += {variable.ClassInfo.FixedSize} * {variable.CsFieldName}.Length;");
                        }
                        else if (variable.ClassIsStruct)
                        {
                            fieldsWithSize.Add(
                                $"size += {variable.CsClassName}.RosFixedMessageLength * {variable.CsFieldName}.Length;");
                        }
                        else
                        {
                            if (variable.CsClassName == "string")
                            {
                                fieldsWithSize.Add($"size += 4 * {variable.CsFieldName}.Length;");
                                fieldsWithSize.Add($"foreach (string s in {variable.CsFieldName})");
                                fieldsWithSize.Add("{");
                                fieldsWithSize.Add("    size += BuiltIns.UTF8.GetByteCount(s);");
                                fieldsWithSize.Add("}");
                            }
                            else
                            {
                                fieldsWithSize.Add($"foreach (var i in {variable.CsFieldName})");
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

        static string ParamToArg(VariableElement v)
        {
            if (v.IsArray)
            {
                return $"{v.CsClassName}[] {v.CsFieldName}";
            }

            if (v.ClassIsStruct)
            {
                return $"in {v.CsClassName} {v.CsFieldName}";
            }

            return $"{v.CsClassName} {v.CsFieldName}";
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
                            lines.Add($"    {variable.CsFieldName} = \"\";");
                        }
                        else if (variable.IsDynamicSizeArray)
                        {
                            lines.Add($"    {variable.CsFieldName} = System.Array.Empty<{variable.CsClassName}>();");
                        }
                        else if (variable.IsArray)
                        {
                            lines.Add(
                                $"    {variable.CsFieldName} = new {variable.CsClassName}[{variable.ArraySize}];");
                        }
                    }
                    else
                    {
                        if (variable.IsDynamicSizeArray)
                        {
                            lines.Add($"    {variable.CsFieldName} = System.Array.Empty<{variable.CsClassName}>();");
                        }
                        else if (variable.IsArray)
                        {
                            lines.Add(
                                $"    {variable.CsFieldName} = new {variable.CsClassName}[{variable.ArraySize}];");
                        }
                        else if (!variable.ClassIsStruct)
                        {
                            lines.Add($"    {variable.CsFieldName} = new {variable.CsClassName}();");
                        }
                    }
                }

                lines.Add("}");
                lines.Add("");
            }

            if (variables.Any())
            {
                lines.Add("/// <summary> Explicit constructor. </summary>");

                var args = string.Join(", ", variables.Select(ParamToArg));
                lines.Add($"public {name}({args})");
                lines.Add("{");
                foreach (var variable in variables)
                {
                    if (variable.ArraySize > 0 && forceStruct)
                    {
                        lines.Add(
                            $"    if ({variable.CsFieldName} is null) throw new System.ArgumentNullException(nameof({variable.CsFieldName}));");
                        lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                        lines.Add("    {");
                        lines.Add($"        this.{variable.CsFieldName}[i] = {variable.CsFieldName}[i];");
                        lines.Add("    }");
                    }
                    else
                    {
                        lines.Add($"    this.{variable.CsFieldName} = {variable.CsFieldName};");
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
                                    ? $"    {variable.CsFieldName} = b.DeserializeString();"
                                    : $"    {variable.CsFieldName} = b.Deserialize<{variable.CsClassName}>();");
                                break;
                            case VariableElement.DynamicSizeArray:
                                lines.Add(variable.CsClassName == "string"
                                    ? $"    {variable.CsFieldName} = b.DeserializeStringArray();"
                                    : $"    {variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                break;
                            default:
                                lines.Add(
                                    variable.CsClassName == "string"
                                        ? $"    {variable.CsFieldName} = b.DeserializeStringArray({variable.ArraySize});"
                                        : $"    {variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
                                break;
                        }
                    }
                    else
                    {
                        switch (variable.ArraySize)
                        {
                            case VariableElement.NotAnArray:
                                lines.Add($"    {variable.CsFieldName} = new {variable.CsClassName}(ref b);");
                                break;
                            case VariableElement.DynamicSizeArray when variable.ClassIsStruct:
                                lines.Add(
                                    $"    {variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                break;
                            case VariableElement.DynamicSizeArray:
                                lines.Add(
                                    $"    {variable.CsFieldName} = b.DeserializeArray<{variable.CsClassName}>();");
                                lines.Add($"    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                                lines.Add("    {");
                                lines.Add($"        {variable.CsFieldName}[i] = new {variable.CsClassName}(ref b);");
                                lines.Add("    }");
                                break;
                            default:
                            {
                                if (variable.ClassIsStruct)
                                {
                                    lines.Add(
                                        $"    {variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
                                }
                                else
                                {
                                    lines.Add(
                                        $"    {variable.CsFieldName} = b.DeserializeArray<{variable.CsClassName}>({variable.ArraySize});");
                                    lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                                    lines.Add("    {");
                                    lines.Add(
                                        $"        {variable.CsFieldName}[i] = new {variable.CsClassName}(ref b);");
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
                var myVars = string.Join(", ", variables.Select(x => x.CsFieldName));

                lines.Add("");
                lines.Add($"public override readonly int GetHashCode() => ({myVars}).GetHashCode();");
                lines.Add("");
                lines.Add($"public override readonly bool Equals(object? o) => o is {name} s && Equals(s);");
                lines.Add("");

                var oVars = string.Join(", ", variables.Select(x => $"o.{x.CsFieldName}"));

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
                            lines.Add($"    b.Serialize({variable.CsFieldName});");
                        }
                        else
                        {
                            lines.Add(variable.CsClassName == "string"
                                ? $"    b.SerializeArray({variable.CsFieldName}, {variable.ArraySize});"
                                : $"    b.SerializeStructArray({variable.CsFieldName}, {variable.ArraySize});");
                        }
                    }
                    else
                    {
                        if (!variable.IsArray)
                        {
                            lines.Add($"    {variable.CsFieldName}.RosSerialize(ref b);");
                        }
                        else
                        {
                            lines.Add(variable.ClassIsStruct
                                ? $"    b.SerializeStructArray({variable.CsFieldName}, {variable.ArraySize});"
                                : $"    b.SerializeArray({variable.CsFieldName}, {variable.ArraySize});");
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
                            $"    if ({variable.CsFieldName} is null) throw new System.NullReferenceException(nameof({variable.CsFieldName}));");
                        lines.Add(
                            $"    if ({variable.CsFieldName}.Length != {variable.ArraySize}) throw new System.IndexOutOfRangeException();");
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
                        $"    if ({variable.CsFieldName} is null) throw new System.NullReferenceException(nameof({variable.CsFieldName}));");
                    if (!variable.IsArray && variable.RosClassName != "string")
                    {
                        lines.Add($"    {variable.CsFieldName}.RosValidate();");
                    }
                }

                if (variable.IsArray)
                {
                    if (variable.RosClassName == "string")
                    {
                        lines.Add($"    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                        lines.Add("    {");
                        lines.Add(
                            $"        if ({variable.CsFieldName}[i] is null) throw new System.NullReferenceException($\"{{nameof({variable.CsFieldName})}}[{{i}}]\");");
                        lines.Add("    }");
                    }
                    else if (!BuiltInTypes.Contains(variable.RosClassName) && !variable.ClassIsStruct)
                    {
                        lines.Add($"    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                        lines.Add("    {");
                        lines.Add(
                            $"        if ({variable.CsFieldName}[i] is null) throw new System.NullReferenceException($\"{{nameof({variable.CsFieldName})}}[{{i}}]\");");
                        lines.Add($"        {variable.CsFieldName}[i].RosValidate();");
                        lines.Add("    }");
                    }
                }
            }

            lines.Add("}");

            return lines;
        }

        public string ToCsString()
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
            lines.Add($"[DataContract (Name = \"{RosPackage}/{Name}\")]");
            if (ForceStruct)
            {
                lines.Add("[StructLayout(LayoutKind.Sequential)]");
                lines.Add(variables.Any(element => element.IsFixedSizeArray)
                    ? $"public unsafe struct {Name} : IMessage, System.IEquatable<{Name}>, IDeserializable<{Name}>"
                    : $"public struct {Name} : IMessage, System.IEquatable<{Name}>, IDeserializable<{Name}>");
            }
            else
            {
                string line = $"public sealed class {Name} : IDeserializable<{Name}>";
                string fullLine = ActionMessageType switch
                {
                    ActionMessageType.None => $"{line}, IMessage",
                    ActionMessageType.Goal => $"{line}, IGoal<{ActionRoot}ActionGoal>",
                    ActionMessageType.Feedback => $"{line}, IFeedback<{ActionRoot}ActionFeedback>",
                    ActionMessageType.Result => $"{line}, IResult<{ActionRoot}ActionResult>",
                    ActionMessageType.ActionGoal => $"{line}, IActionGoal<{ActionRoot}Goal>",
                    ActionMessageType.ActionFeedback => $"{line}, IActionFeedback<{ActionRoot}Feedback>",
                    ActionMessageType.ActionResult => $"{line}, IActionResult<{ActionRoot}Result>",
                    ActionMessageType.Action =>
                        $"{line},\n\t\tIAction<{ActionRoot}ActionGoal, {ActionRoot}ActionFeedback, {ActionRoot}ActionResult>",
                    _ => throw new ArgumentOutOfRangeException($"Unknown action message type {ActionMessageType}")
                };
                lines.Add(fullLine);
            }

            lines.Add("{");

            var csElements = elements.SelectMany(element => element.ToCsString(ForceStruct));
            foreach (var entry in csElements)
            {
                lines.Add($"    {entry}");
            }

            if (elements.Length != 0)
            {
                lines.Add("");
            }

            var constructors = CreateConstructors(variables, Name, ForceStruct);
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

            CheckFixedSize();
            var lengthProperty = CreateLengthProperty(variables, FixedSize, ForceStruct);
            foreach (var entry in lengthProperty)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var readOnlyId = ForceStruct ? "readonly " : "";
            lines.Add($"    public {readOnlyId}string RosType => RosMessageType;");

            lines.Add("");
            lines.Add("    /// <summary> Full ROS name of this message. </summary>");
            lines.Add($"    [Preserve] public const string RosMessageType = \"{RosPackage}/{Name}\";");


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

            if (Additions.Contents.TryGetValue($"{RosPackage}/{Name}", out var extraLines))
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

        void AddDependencies(ICollection<ClassInfo> dependencies)
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
                builder.AppendLine($"MSG: {classInfo.RosPackage}/{classInfo.Name}");
                builder.AppendLine(classInfo.fullMessageText);
            }

            return builder.ToString();
        }

        string GetMd5Property()
        {
            GetMd5();

#pragma warning disable CA5351
            using MD5 md5Hash = MD5.Create();
#pragma warning restore CA5351

            return GetMd5Hash(md5Hash, md5File);
        }

        internal string GetMd5()
        {
            if (md5 != null)
            {
                return md5;
            }

            var str = new StringBuilder();

            var md5Constants = elements
                .OfType<ConstantElement>()
                .Select(x => x.GetEntryForMd5Hash()).ToArray();

            if (md5Constants.Any())
            {
                str.Append(string.Join("\n", md5Constants));
                if (variables.Any())
                {
                    str.Append('\n');
                }
            }

            var md5Variables = variables.Select(x => x.GetEntryForMd5Hash());
            str.Append(string.Join("\n", md5Variables));

            md5File = str.ToString();

#pragma warning disable CA5351
            using MD5 md5Hash = MD5.Create();
#pragma warning restore CA5351

            md5 = GetMd5Hash(md5Hash, md5File);

            return md5;
        }

        internal static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Utf8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var b in data)
            {
                sBuilder.Append(b.ToString("x2", Culture));
            }

            return sBuilder.ToString();
        }
    }
}