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
        public const string DependencySeparator =
            "================================================================================";

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
            "std_msgs/ColorRGBA",
            "iviz_msgs/Color32",
            "iviz_msgs/Vector2f",
            "iviz_msgs/Vector3f",
            "iviz_msgs/Triangle",
        };

        static readonly UTF8Encoding Utf8 = new UTF8Encoding(false);

        readonly ActionMessageType actionMessageType;
        readonly string? actionRoot;
        readonly string csPackage;
        readonly IElement[] elements;
        readonly string fullMessageText;
        readonly VariableElement[] variables;
        string? md5;
        string? md5File;

        public ClassInfo(string packageName, string messageFilePath, bool forceStruct = false) :
            this(packageName, Path.GetFileNameWithoutExtension(messageFilePath),
                ReadDefinitionFromFile(messageFilePath), forceStruct)
        {
        }

        static string ReadDefinitionFromFile(string messageFilePath)
        {
            if (messageFilePath == null)
            {
                throw new ArgumentNullException(nameof(messageFilePath));
            }

            if (!File.Exists(messageFilePath))
            {
                throw new FileNotFoundException($"File {messageFilePath} does not exist.");
            }

            Console.WriteLine($"-- Parsing '{messageFilePath}'");

            return File.ReadAllText(messageFilePath);
        }

        static bool IsVariableForStruct(string rosPackage, VariableElement v)
        {
            if (v.IsArray || v.RosClassName == "string")
            {
                return false;
            }

            if (BuiltInTypes.Contains(v.RosClassName))
            {
                return true;
            }

            string resolvedName = v.RosClassName.Contains("/") ? $"{rosPackage}/{v.RosClassName}" : v.RosClassName;
            return ForceStructs.Contains(resolvedName);
        }

        public ClassInfo(string package, string messageName, string messageDefinition, bool forceStruct = false)
        {
            if (messageName == null)
            {
                throw new ArgumentNullException(nameof(messageName));
            }

            int lastSlash = messageName.LastIndexOf('/');
            if (lastSlash != -1)
            {
                if (!string.IsNullOrEmpty(package))
                {
                    throw new ArgumentException("Message name contains a package, but package argument is also set!");
                }

                package = messageName.Substring(0, lastSlash);
                messageName = messageName.Substring(lastSlash + 1);
            }

            if (string.IsNullOrWhiteSpace(package))
            {
                throw new InvalidOperationException("Could not find the package this message belongs to");
            }

            RosPackage = package;
            csPackage = MsgParser.CsIfiy(package);
            Name = messageName;
            fullMessageText = messageDefinition;

            var lines = fullMessageText.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            elements = MsgParser.ParseFile(lines, Name).ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);
            if (forceStruct)
            {
                if (!variables.All(variable => IsVariableForStruct(RosPackage, variable)))
                {
                    throw new MessageStructException();
                }

                ForceStruct = true;
            }
            else if (ForceStructs.Contains($"{RosPackage}/{Name}"))
            {
                ForceStruct = true;
            }

            variables = elements.OfType<VariableElement>().ToArray();
        }


        public ClassInfo(string package, string name, IEnumerable<IElement> newElements) :
            this(package, name, newElements, null, ActionMessageType.None)
        {
        }

        internal ClassInfo(string package, string name, IEnumerable<IElement> newElements,
            string? actionRoot, ActionMessageType actionMessageType)
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
            csPackage = MsgParser.CsIfiy(package);
            Name = name;

            elements = newElements.ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);

            ForceStruct = false;
            fullMessageText = string.Join("\n", elements.Select(elem => elem.ToRosString()));

            variables = elements.OfType<VariableElement>().ToArray();

            this.actionRoot = actionRoot;
            this.actionMessageType = actionMessageType;
        }

        public int FixedSize { get; internal set; } = UninitializedSize;
        public bool HasFixedSize => FixedSize != UnknownSizeAtCompileTime && FixedSize != UninitializedSize;
        public ReadOnlyCollection<IElement> Elements { get; }
        public string RosPackage { get; }
        public string Name { get; }
        public bool ForceStruct { get; }
        public string FullRosName => $"{RosPackage}/{Name}";

        internal static bool IsClassForceStruct(string f)
        {
            return ForceStructs.Contains(f);
        }

        internal static bool IsBuiltinType(string f)
        {
            return BuiltInTypes.Contains(f);
        }

        internal static void DoResolveClasses(PackageInfo packageInfo, string package,
            IEnumerable<VariableElement> variables)
        {
            foreach (VariableElement variable in variables)
            {
                if (!BuiltInsSizes.ContainsKey(variable.RosClassName) &&
                    packageInfo.TryGet(variable.RosClassName, package, out ClassInfo? classInfo))
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
            foreach (VariableElement variable in variables)
            {
                if (!variable.IsDynamicSizeArray)
                {
                    if (BuiltInsSizes.TryGetValue(variable.RosClassName, out int size))
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
            string readOnlyId = forceStruct ? "readonly " : "";
            if (fixedSize != UnknownSizeAtCompileTime)
            {
                return new[]
                {
                    "/// <summary> Constant size of this message. </summary>",
                    $"[Preserve] public const int RosFixedMessageLength = {fixedSize};",
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
            int fieldSize = 0;
            foreach (VariableElement variable in variables)
            {
                if (BuiltInsSizes.TryGetValue(variable.RosClassName, out int size))
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
                        else if (variable.CsClassName == "string")
                        {
                            fieldSize += 4;
                            fieldsWithSize.Add($"size += BuiltIns.UTF8.GetByteCount({variable.CsFieldName});");
                        }
                        else if (variable.ClassIsStruct)
                        {
                            fieldsWithSize.Add($"size += {variable.CsClassName}.RosFixedMessageLength;");
                        }
                        else
                        {
                            fieldsWithSize.Add($"size += {variable.CsFieldName}.RosMessageLength;");
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
            foreach (string entry in fieldsWithSize)
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
                foreach (VariableElement variable in variables)
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
                            if (variable.ClassInfo != null && variable.ClassInfo.FixedSize == 0)
                            {
                                lines.Add($"    {variable.CsFieldName} = {variable.CsClassName}.Singleton;");
                            }
                            else
                            {
                                lines.Add($"    {variable.CsFieldName} = new {variable.CsClassName}();");
                            }
                        }
                    }
                }

                lines.Add("}");
                lines.Add("");
            }

            if (variables.Any())
            {
                lines.Add("/// <summary> Explicit constructor. </summary>");

                string args = string.Join(", ", variables.Select(ParamToArg));
                lines.Add($"public {name}({args})");
                lines.Add("{");
                foreach (VariableElement variable in variables)
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
            lines.Add($"public {name}(ref Buffer b)");
            lines.Add("{");
            if (forceStruct)
            {
                if (variables.Any())
                {
                    lines.Add("    b.Deserialize(out this);");
                }
            }
            else
            {
                foreach (VariableElement variable in variables)
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
                                if (variable.ClassInfo != null && variable.ClassInfo.FixedSize == 0)
                                {
                                    lines.Add($"    {variable.CsFieldName} = {variable.CsClassName}.Singleton;");
                                }
                                else
                                {
                                    lines.Add($"    {variable.CsFieldName} = new {variable.CsClassName}(ref b);");
                                }

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


            string readOnlyId = forceStruct ? "readonly " : "";
            lines.Add($"public {readOnlyId}ISerializable RosDeserialize(ref Buffer b)");
            lines.Add("{");
            if (variables.Any())
            {
                lines.Add($"    return new {name}(ref b);");
            }
            else
            {
                lines.Add("    return Singleton;");
            }

            lines.Add("}");
            lines.Add("");

            lines.Add($"{readOnlyId}{name} IDeserializable<{name}>.RosDeserialize(ref Buffer b)");
            lines.Add("{");
            if (variables.Any())
            {
                lines.Add($"    return new {name}(ref b);");
            }
            else
            {
                lines.Add("    return Singleton;");
            }

            lines.Add("}");

            if (forceStruct)
            {
                string myVars = string.Join(", ", variables.Select(x => x.CsFieldName));

                if (myVars.Length == 0)
                {
                    lines.Add("");
                    lines.Add("public override readonly int GetHashCode() => 0;");
                    lines.Add("");
                    lines.Add($"public override readonly bool Equals(object? o) => o is {name};");
                    lines.Add("");

                    lines.Add($"public readonly bool Equals({name} o) => true;");
                    lines.Add("");
                    lines.Add($"public static bool operator==(in {name} _, in {name} __) => true;");
                    lines.Add("");
                    lines.Add($"public static bool operator!=(in {name} _, in {name} __) => false;");
                }
                else
                {
                    lines.Add("");
                    lines.Add($"public override readonly int GetHashCode() => ({myVars}).GetHashCode();");
                    lines.Add("");
                    lines.Add($"public override readonly bool Equals(object? o) => o is {name} s && Equals(s);");
                    lines.Add("");

                    string oVars = string.Join(", ", variables.Select(x => $"o.{x.CsFieldName}"));

                    lines.Add($"public readonly bool Equals({name} o) => ({myVars}) == ({oVars});");
                    lines.Add("");
                    lines.Add($"public static bool operator==(in {name} a, in {name} b) => a.Equals(b);");
                    lines.Add("");
                    lines.Add($"public static bool operator!=(in {name} a, in {name} b) => !a.Equals(b);");
                }
            }

            if (!variables.Any())
            {
                lines.Add("");
                lines.Add($"public static readonly {name} Singleton = new {name}();");
            }

            return lines;
        }

        internal static IEnumerable<string> CreateSerializers(IReadOnlyCollection<VariableElement> variables,
            bool forceStruct)
        {
            var lines = new List<string>();

            string readOnlyId = forceStruct ? "readonly " : "";

            lines.Add($"public {readOnlyId}void RosSerialize(ref Buffer b)");
            lines.Add("{");
            if (forceStruct)
            {
                lines.Add("    b.Serialize(this);");
            }
            else
            {
                foreach (VariableElement variable in variables)
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
            foreach (VariableElement variable in variables)
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
            StringBuilder str = new StringBuilder();

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

            foreach (string entry in CreateClassContent())
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

            using MemoryStream outputStream = new MemoryStream();

            using (GZipStream gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gZipStream.Write(inputBytes, 0, inputBytes.Length);
            }

            string base64 = Convert.ToBase64String(outputStream.ToArray());

            const int lineWidth = 80;
            for (int i = 0; i < base64.Length; i += lineWidth)
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

                string sub = base64.Substring(i, end - i);
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
            lines.Add($"[Preserve, DataContract (Name = \"{RosPackage}/{Name}\")]");
            if (ForceStruct)
            {
                lines.Add("[StructLayout(LayoutKind.Sequential)]");
                lines.Add(variables.Any(element => element.IsFixedSizeArray)
                    ? $"public unsafe readonly struct {Name} : IMessage, System.IEquatable<{Name}>, IDeserializable<{Name}>"
                    : $"public readonly struct {Name} : IMessage, System.IEquatable<{Name}>, IDeserializable<{Name}>");
            }
            else
            {
                string line = $"public sealed class {Name} : IDeserializable<{Name}>";
                string fullLine = actionMessageType switch
                {
                    ActionMessageType.None => $"{line}, IMessage",
                    ActionMessageType.Goal => $"{line}, IGoal<{actionRoot}ActionGoal>",
                    ActionMessageType.Feedback => $"{line}, IFeedback<{actionRoot}ActionFeedback>",
                    ActionMessageType.Result => $"{line}, IResult<{actionRoot}ActionResult>",
                    ActionMessageType.ActionGoal => $"{line}, IActionGoal<{actionRoot}Goal>",
                    ActionMessageType.ActionFeedback => $"{line}, IActionFeedback<{actionRoot}Feedback>",
                    ActionMessageType.ActionResult => $"{line}, IActionResult<{actionRoot}Result>",
                    ActionMessageType.Action =>
                        $"{line},\n\t\tIAction<{actionRoot}ActionGoal, {actionRoot}ActionFeedback, {actionRoot}ActionResult>",
                    _ => throw new MessageParseException($"Unknown action message type {actionMessageType}")
                };
                lines.Add(fullLine);
            }

            lines.Add("{");

            var csElements = elements.SelectMany(element => element.ToCsString(ForceStruct));
            foreach (string entry in csElements)
            {
                lines.Add($"    {entry}");
            }

            if (elements.Length != 0)
            {
                lines.Add("");
            }

            var constructors = CreateConstructors(variables, Name, ForceStruct);
            foreach (string entry in constructors)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var serializer = CreateSerializers(variables, ForceStruct);
            foreach (string entry in serializer)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");

            CheckFixedSize();
            var lengthProperty = CreateLengthProperty(variables, FixedSize, ForceStruct);
            foreach (string entry in lengthProperty)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            string readOnlyId = ForceStruct ? "readonly " : "";
            lines.Add($"    public {readOnlyId}string RosType => RosMessageType;");

            lines.Add("");
            lines.Add("    /// <summary> Full ROS name of this message. </summary>");
            lines.Add($"    [Preserve] public const string RosMessageType = \"{RosPackage}/{Name}\";");


            lines.Add("");

            lines.Add("    /// <summary> MD5 hash of a compact representation of the message. </summary>");
            lines.Add(
                $"    [Preserve] public const string RosMd5Sum = {(Md5Hash.Length == 0 ? "null" : $"\"{Md5Hash}\"")};");

            lines.Add("");

            lines.Add(
                "    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
            lines.Add("    [Preserve] public const string RosDependenciesBase64 =");

            string catDependencies = CreateCatDependencies();
            var compressedDeps = Compress(catDependencies);
            foreach (string entry in compressedDeps)
            {
                lines.Add($"            {entry}");
            }

            if (Additions.Contents.TryGetValue($"{RosPackage}/{Name}", out var extraLines))
            {
                lines.Add("    /// Custom iviz code");
                foreach (string entry in extraLines)
                {
                    lines.Add($"    {entry}");
                }
            }

            lines.Add("}");

            return lines;
        }

        void AddDependencies(ICollection<ClassInfo> dependencies)
        {
            foreach (VariableElement variable in variables)
            {
                if (variable.ClassInfo != null &&
                    !dependencies.Contains(variable.ClassInfo))
                {
                    dependencies.Add(variable.ClassInfo);
                    variable.ClassInfo.AddDependencies(dependencies);
                }
            }
        }

        public string CreateCatDependencies()
        {
            var dependencies = new List<ClassInfo>();
            AddDependencies(dependencies);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(fullMessageText);

            foreach (ClassInfo classInfo in dependencies)
            {
                builder.AppendLine(DependencySeparator);
                builder.AppendLine($"MSG: {classInfo.RosPackage}/{classInfo.Name}");
                builder.AppendLine(classInfo.fullMessageText);
            }

            return builder.ToString();
        }

        public string Md5Hash => md5 ??= CalculateMd5();

        string CalculateMd5()
        {
            StringBuilder str = new StringBuilder();

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

            var md5Variables = variables.Select(x => x.GetEntryForMd5Hash(RosPackage)).ToArray();
            if (md5Variables.Any(md5String => md5String == null))
            {
                return ""; // shouldn't happen, exception thrown earlier
            }

            str.Append(string.Join("\n", md5Variables));

            md5File = str.ToString();

#pragma warning disable CA5351
            using MD5 md5Hash = MD5.Create();
#pragma warning restore CA5351

            return GetMd5Hash(md5Hash, md5File);
        }

        internal static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Utf8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (byte b in data)
            {
                sBuilder.Append(b.ToString("x2", Culture));
            }

            return sBuilder.ToString();
        }
    }
}