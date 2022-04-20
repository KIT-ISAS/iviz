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
            "================================================================================"; // 80 '='

        internal const int UninitializedSize = -2;
        const int UnknownSizeAtCompileTime = -1;

        static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        static readonly Dictionary<string, int> BuiltInsSizes = new()
        {
            { "bool", 1 },
            { "int8", 1 },
            { "uint8", 1 },
            { "int16", 2 },
            { "uint16", 2 },
            { "int32", 4 },
            { "uint32", 4 },
            { "int64", 8 },
            { "uint64", 8 },
            { "float32", 4 },
            { "float64", 8 },
            { "time", 8 },
            { "duration", 8 },
            { "char", 1 },
            { "byte", 1 }
        };

        static readonly HashSet<string> BuiltInTypes = new()
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

        static readonly UTF8Encoding Utf8 = new(false);

        readonly ActionMessageType actionMessageType;
        readonly string? actionRoot;
        readonly IElement[] elements;
        readonly string fullMessageText;
        readonly VariableElement[] variables;
        string? md5;
        string? md5File;

        bool? requiresDispose;

        public int FixedSize { get; internal set; } = UninitializedSize;
        public bool HasFixedSize => FixedSize != UnknownSizeAtCompileTime && FixedSize != UninitializedSize;
        public ReadOnlyCollection<IElement> Elements { get; }
        public string RosPackage { get; }
        public string CsPackage { get; }
        public string Name { get; }
        public bool ForceStruct { get; }
        readonly bool isBlittable;
        public bool IsBlittable => isBlittable || (ForceStruct && FixedSize >= 0);
        public string FullRosName => $"{RosPackage}/{Name}";

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

        static bool IsVariableBlittable(string rosPackage, VariableElement v)
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
            return StructMessages.BlittableStructs.Contains(resolvedName);
        }

        /// <summary>
        /// Creates a message class for a single message defined in text form. 
        /// </summary>
        /// <param name="package">The name of the package. Leave this as null if the package name is included in <see cref="messageName"/>.</param>
        /// <param name="messageName">The name of the message. If this is a fully qualified name (e.g., std_msgs/Header instead of just Header), leave <see cref="package"/> as null.</param>
        /// <param name="messageDefinition">The definition of the message, as would be found in a .msg file.</param>
        /// <param name="forceStruct">Whether to construct this message as a struct instead of a class. You should probably leave this as false.</param>
        public ClassInfo(string? package, string messageName, string messageDefinition, bool forceStruct = false)
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
                    throw new ArgumentException(
                        "messageName contains a package, but package is not null. Only one of both must be set!");
                }

                package = messageName.Substring(0, lastSlash);
                messageName = messageName.Substring(lastSlash + 1);
            }

            if (string.IsNullOrWhiteSpace(package))
            {
                throw new InvalidOperationException("Could not find the package this message belongs to");
            }

            RosPackage = package!;
            CsPackage = MsgParser.CsIfy(package!);
            Name = messageName;
            fullMessageText = messageDefinition.Replace("\r\n", "\n");

            var lines = fullMessageText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            elements = MsgParser.ParseFile(lines, Name).ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);
            variables = elements.OfType<VariableElement>().ToArray();

            if (forceStruct || IsClassForceStruct($"{RosPackage}/{Name}"))
            {
                ForceStruct = true;
            }

            isBlittable = ForceStruct && variables.All(variable => IsVariableBlittable(RosPackage, variable));
        }


        public ClassInfo(string? package, string name, IEnumerable<IElement> newElements) :
            this(package, name, newElements, null, ActionMessageType.None)
        {
        }

        internal ClassInfo(string? package, string messageName, IEnumerable<IElement> newElements,
            string? actionRoot, ActionMessageType actionMessageType)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            int lastSlash = messageName.LastIndexOf('/');
            if (lastSlash != -1)
            {
                if (!string.IsNullOrEmpty(package))
                {
                    throw new ArgumentException(
                        "messageName contains a package, but package is not null. Only one of both must be set!");
                }

                package = messageName.Substring(0, lastSlash);
                messageName = messageName.Substring(lastSlash + 1);
            }

            if (messageName == null)
            {
                throw new ArgumentNullException(nameof(messageName));
            }

            if (newElements == null)
            {
                throw new ArgumentNullException(nameof(newElements));
            }

            Console.WriteLine($"-- Parsing synthetic '{package}/{messageName}'");

            RosPackage = package;
            CsPackage = MsgParser.CsIfy(package);
            Name = messageName;

            elements = newElements.ToArray();
            Elements = new ReadOnlyCollection<IElement>(elements);

            ForceStruct = false;
            fullMessageText = string.Join("\n", elements.Select(elem => elem.ToRosString()));

            variables = elements.OfType<VariableElement>().ToArray();

            this.actionRoot = actionRoot;
            this.actionMessageType = actionMessageType;
        }

        internal static bool IsClassForceStruct(string f)
        {
            return StructMessages.BlittableStructs.Contains(f) || StructMessages.ForceStructs.Contains(f);
        }

        internal static bool IsClassBlittable(string f)
        {
            return StructMessages.BlittableStructs.Contains(f);
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
                    "/// <summary> Constant size of this message. </summary> ",
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
                        fieldsWithSize.Add(size == 1
                            ? $"{variable.CsFieldName}.Length"
                            : $"{size} * {variable.CsFieldName}.Length");
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
                            fieldsWithSize.Add($"BuiltIns.GetStringSize({variable.CsFieldName})");
                        }
                        else if (variable.ClassIsBlittable)
                        {
                            fieldsWithSize.Add($"{variable.CsClassName}.RosFixedMessageLength");
                        }
                        else
                        {
                            fieldsWithSize.Add($"{variable.CsFieldName}.RosMessageLength");
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
                            fieldsWithSize.Add($"{variable.ClassInfo.FixedSize} * {variable.CsFieldName}.Length");
                        }
                        else if (variable.ClassIsBlittable)
                        {
                            fieldsWithSize.Add(
                                $" {variable.CsClassName}.RosFixedMessageLength * {variable.CsFieldName}.Length");
                        }
                        else
                        {
                            fieldsWithSize.Add($"BuiltIns.GetArraySize({variable.CsFieldName})");
                        }
                    }
                }
            }

            switch (fieldsWithSize.Count)
            {
                case 0:
                    return new[]
                    {
                        $"public {readOnlyId}int RosMessageLength => {fieldSize};"
                    };
                case 1:
                    return new[]
                    {
                        $"public {readOnlyId}int RosMessageLength => {fieldSize} + {fieldsWithSize[0]};"
                    };
                case 2:
                    return new[]
                    {
                        $"public {readOnlyId}int RosMessageLength => {fieldSize} + {fieldsWithSize[0]} + {fieldsWithSize[1]};"
                    };
                default:
                    var lines = new List<string>();
                    lines.Add($"public {readOnlyId}int RosMessageLength");
                    lines.Add("{");
                    lines.Add("    get {");
                    lines.Add($"        int size = {fieldSize};");
                    lines.AddRange(fieldsWithSize.Select(entry => $"        size += {entry};"));
                    lines.Add("        return size;");
                    lines.Add("    }");
                    lines.Add("}");
                    return lines;
            }
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
            string name, bool forceStruct, bool structIsBlittable, bool isAction)
        {
            var lines = new List<string>();

            if (!forceStruct)
            {
                lines.Add("/// Constructor for empty message.");
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
                            if (variable.RentHint)
                            {
                                lines.Add(
                                    $"    {variable.CsFieldName} = Tools.SharedRent<{variable.CsClassName}>.Empty;");
                            }
                            else
                            {
                                lines.Add(
                                    $"    {variable.CsFieldName} = System.Array.Empty<{variable.CsClassName}>();");
                            }
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
                            if (variable.ClassInfo is { FixedSize: 0 })
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

            if ((forceStruct || structIsBlittable || variables.Count <= 4) && variables.Count != 0)
            {
                lines.Add("/// Explicit constructor.");

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

            lines.Add("/// Constructor with buffer.");
            if (forceStruct)
            {
                lines.Add("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            }

            lines.Add($"public {name}(ref ReadBuffer b)");
            lines.Add("{");
            if (structIsBlittable)
            {
                if (variables.Any())
                {
                    lines.Add("    b.Deserialize(out this);");
                }
            }
            else
            {
                if (forceStruct)
                {
                    lines.Add("    Deserialize(ref b, out this);");
                    lines.Add("}");
                    lines.Add("");
                    lines.Add($"public static void Deserialize(ref ReadBuffer b, out {name} h)");
                    lines.Add("{");
                }

                string prefix = forceStruct ? "h." : "";
                foreach (VariableElement variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.RosClassName))
                    {
                        switch (variable.ArraySize)
                        {
                            case VariableElement.NotAnArray:
                                if (variable.CsClassName == "string")
                                {
                                    lines.Add(variable.IgnoreHint
                                        ? $"    {prefix}{variable.CsFieldName} = b.SkipString();"
                                        : $"    {prefix}{variable.CsFieldName} = b.DeserializeString();");
                                }
                                else
                                {
                                    lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                                
                                    //lines.Add(
                                    //    $"    {prefix}{variable.CsFieldName} = b.Deserialize<{variable.CsClassName}>();");
                                }

                                break;
                            case VariableElement.DynamicSizeArray:
                                if (variable.CsClassName == "string")
                                {
                                    lines.Add(variable.IgnoreHint
                                        ? $"    {prefix}{variable.CsFieldName} = b.SkipStringArray();"
                                        : $"    {prefix}{variable.CsFieldName} = b.DeserializeStringArray();");
                                }
                                else if (variable.RentHint)
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = b.DeserializeStructRent<{variable.CsClassName}>();");
                                }
                                else if (variable.IgnoreHint)
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = b.SkipStructArray<{variable.CsClassName}>();");
                                }
                                else
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                }

                                break;
                            default:
                                lines.Add(
                                    variable.CsClassName == "string"
                                        ? $"    {prefix}{variable.CsFieldName} = b.DeserializeStringArray({variable.ArraySize});"
                                        : $"    {prefix}{variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
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
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = {variable.CsClassName}.Singleton;");
                                }
                                else if (variable.ClassIsBlittable)
                                {
                                    lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                                }
                                else if (variable.ClassIsStruct && !isAction)
                                {
                                    lines.Add(
                                        $"    {variable.CsClassName}.Deserialize(ref b, out {prefix}{variable.CsFieldName});");
                                }
                                else
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = new {variable.CsClassName}(ref b);");
                                }

                                break;
                            case VariableElement.DynamicSizeArray when variable.ClassIsBlittable:
                                lines.Add(
                                    $"    {prefix}{variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>();");
                                break;
                            case VariableElement.DynamicSizeArray:
                                lines.Add(
                                    $"    {prefix}{variable.CsFieldName} = b.DeserializeArray<{variable.CsClassName}>();");
                                lines.Add($"    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                                lines.Add("    {");
                                lines.Add(variable.ClassIsStruct && !isAction
                                    ? $"        {variable.CsClassName}.Deserialize(ref b, out {prefix}{variable.CsFieldName}[i]);"
                                    : $"        {prefix}{variable.CsFieldName}[i] = new {variable.CsClassName}(ref b);");

                                lines.Add("    }");
                                break;
                            default:
                            {
                                if (variable.ClassIsBlittable)
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = b.DeserializeStructArray<{variable.CsClassName}>({variable.ArraySize});");
                                }
                                else
                                {
                                    lines.Add(
                                        $"    {prefix}{variable.CsFieldName} = b.DeserializeArray<{variable.CsClassName}>({variable.ArraySize});");
                                    lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                                    lines.Add("    {");
                                    lines.Add(variable.ClassIsStruct && !isAction
                                        ? $"        {variable.CsClassName}.Deserialize(ref b, out {prefix}{variable.CsFieldName}[i]);"
                                        : $"        {prefix}{variable.CsFieldName}[i] = new {variable.CsClassName}(ref b);");
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
            lines.Add(variables.Any()
                ? $"{readOnlyId}ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new {name}(ref b);"
                : $"{readOnlyId}ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;");

            lines.Add("");

            lines.Add(variables.Any()
                ? $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer b) => new {name}(ref b);"
                : $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer b) => Singleton;");

            if (forceStruct)
            {
                string myVars = string.Join(", ", variables.Select(x => x.CsFieldName));

                if (myVars.Length == 0)
                {
                    lines.Add("");
                    lines.Add("public readonly override int GetHashCode() => 0;");
                    lines.Add($"public readonly override bool Equals(object? o) => o is {name};");
                    lines.Add($"public readonly bool Equals({name} o) => true;");
                    lines.Add($"public static bool operator==(in {name} _, in {name} __) => true;");
                    lines.Add($"public static bool operator!=(in {name} _, in {name} __) => false;");
                }
                else
                {
                    lines.Add("");
                    lines.Add($"public readonly override int GetHashCode() => ({myVars}).GetHashCode();");
                    lines.Add($"public readonly override bool Equals(object? o) => o is {name} s && Equals(s);");

                    string oVars = string.Join(", ", variables.Select(x => $"o.{x.CsFieldName}"));

                    lines.Add($"public readonly bool Equals({name} o) => ({myVars}) == ({oVars});");
                    lines.Add($"public static bool operator==(in {name} a, in {name} b) => a.Equals(b);");
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
            bool forceStruct, bool isBlittable)
        {
            var lines = new List<string>();

            string readOnlyId = forceStruct ? "readonly " : "";

            lines.Add($"public {readOnlyId}void RosSerialize(ref WriteBuffer b)");
            lines.Add("{");
            if (isBlittable)
            {
                lines.Add("    b.Serialize(in this);");
            }
            else
            {
                foreach (VariableElement variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.RosClassName))
                    {
                        if (!variable.IsArray)
                        {
                            if (forceStruct && variable.RosClassName == "string")
                            {
                                lines.Add($"    b.Serialize({variable.CsFieldName} ?? \"\");");
                            }
                            else
                            {
                                lines.Add($"    b.Serialize({variable.CsFieldName});");
                            }
                        }
                        else
                        {
                            if (variable.ArraySize == 0)
                            {
                                if (forceStruct)
                                {
                                    lines.Add(variable.CsClassName == "string"
                                        ? $"    b.SerializeArray({variable.CsFieldName} ?? System.Array.Empty<string>());"
                                        : $"    b.SerializeStructArray({variable.CsFieldName} ?? System.Array.Empty<{variable.CsClassName}>());");
                                }
                                else
                                {
                                    lines.Add(variable.CsClassName == "string"
                                        ? $"    b.SerializeArray({variable.CsFieldName});"
                                        : $"    b.SerializeStructArray({variable.CsFieldName});");
                                }
                            }
                            else
                            {
                                if (forceStruct)
                                {
                                    lines.Add(variable.CsClassName == "string"
                                        ? $"    b.SerializeArray({variable.CsFieldName} ?? System.Array.Empty<string>(), {variable.ArraySize});"
                                        : $"    b.SerializeStructArray({variable.CsFieldName} ?? System.Array.Empty<{variable.CsClassName}>(), {variable.ArraySize});");
                                }
                                else
                                {
                                    lines.Add(variable.CsClassName == "string"
                                        ? $"    b.SerializeArray({variable.CsFieldName}, {variable.ArraySize});"
                                        : $"    b.SerializeStructArray({variable.CsFieldName}, {variable.ArraySize});");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!variable.IsArray)
                        {
                            lines.Add(variable.ClassIsBlittable
                                ? $"    b.Serialize(in {variable.CsFieldName});"
                                : $"    {variable.CsFieldName}.RosSerialize(ref b);");
                        }
                        else
                        {
                            if (variable.ArraySize == 0)
                            {
                                lines.Add(variable.ClassIsBlittable
                                    ? $"    b.SerializeStructArray({variable.CsFieldName});"
                                    : $"    b.SerializeArray({variable.CsFieldName});");
                            }
                            else
                            {
                                lines.Add(variable.ClassIsBlittable
                                    ? $"    b.SerializeStructArray({variable.CsFieldName}, {variable.ArraySize});"
                                    : $"    b.SerializeArray({variable.CsFieldName}, {variable.ArraySize});");
                            }
                        }
                    }
                }
            }

            lines.Add("}");

            lines.Add("");
            lines.Add($"public {readOnlyId}void RosValidate()");
            lines.Add("{");
            if (isBlittable)
            {
                lines.Add("}");
                return lines;
            }

            foreach (VariableElement variable in variables)
            {
                if (variable.ArraySize > 0)
                {
                    if (!forceStruct)
                    {
                        lines.Add(
                            $"    if ({variable.CsFieldName} is null) BuiltIns.ThrowNullReference(nameof({variable.CsFieldName}));");
                        lines.Add(
                            $"    if ({variable.CsFieldName}.Length != {variable.ArraySize}) " +
                            $"throw new RosInvalidSizeForFixedArrayException(nameof({variable.CsFieldName}), " +
                            $"{variable.CsFieldName}.Length, {variable.ArraySize});");
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
                    if (!forceStruct && (!variable.ClassIsStruct || variable.IsArray))
                    {
                        lines.Add(
                            $"    if ({variable.CsFieldName} is null) BuiltIns.ThrowNullReference(nameof({variable.CsFieldName}));");
                    }

                    if (!variable.IsArray && !variable.ClassIsStruct && variable.RosClassName != "string")
                    {
                        lines.Add($"    {variable.CsFieldName}.RosValidate();");
                    }
                }

                if (variable.IsArray)
                {
                    string ind;
                    if (forceStruct)
                    {
                        lines.Add($"    if ({variable.CsFieldName} != null)");
                        lines.Add($"    {{");
                        ind = "    ";
                    }
                    else
                    {
                        ind = "";
                    }

                    if (variable.RosClassName == "string")
                    {
                        lines.Add($"{ind}    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                        lines.Add($"{ind}    {{");
                        lines.Add(
                            $"{ind}        if ({variable.CsFieldName}[i] is null) BuiltIns.ThrowNullReference($\"{{nameof({variable.CsFieldName})}}[{{i}}]\");");
                        lines.Add($"{ind}    }}");
                    }
                    else if (!BuiltInTypes.Contains(variable.RosClassName) && !variable.ClassIsStruct)
                    {
                        lines.Add($"{ind}    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                        lines.Add($"{ind}    {{");
                        lines.Add(
                            $"{ind}        if ({variable.CsFieldName}[i] is null) BuiltIns.ThrowNullReference($\"{{nameof({variable.CsFieldName})}}[{{i}}]\");");
                        lines.Add($"{ind}        {variable.CsFieldName}[i].RosValidate();");
                        lines.Add($"{ind}    }}");
                    }

                    if (forceStruct)
                    {
                        lines.Add($"    }}");
                    }
                }
            }

            lines.Add("}");

            return lines;
        }

        public string ToCsString()
        {
            var str = new StringBuilder(200);

            str.AppendNewLine("/* This file was created automatically, do not edit! */");
            str.AppendNewLine();

            if (ForceStruct)
            {
                str.AppendNewLine("using System.Runtime.InteropServices;");
                str.AppendNewLine("using System.Runtime.CompilerServices;");
            }

            str.AppendNewLine("using System.Runtime.Serialization;");
            str.AppendNewLine();

            str.AppendNewLine($"namespace Iviz.Msgs.{CsPackage}");
            str.AppendNewLine("{");

            foreach (string entry in CreateClassContent())
            {
                str.Append("    ").AppendNewLine(entry);
            }

            str.AppendNewLine("}");

            return str.ToString();
        }

        static string Compress(string catDependencies)
        {
            var inputBytes = Utf8.GetBytes(catDependencies);

            using var outputStream = new MemoryStream();

            using (var gZipStream = new GZipStream(outputStream, CompressionLevel.Optimal))
            {
                gZipStream.Write(inputBytes, 0, inputBytes.Length);
            }

            return Convert.ToBase64String(outputStream.ToArray());
        }

        static IEnumerable<string> Split(string base64)
        {
            var lines = new List<string>();

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
            lines.Add($"[Preserve, DataContract (Name = RosMessageType)]");
            if (ForceStruct)
            {
                lines.Add("[StructLayout(LayoutKind.Sequential)]");
                lines.Add(variables.Any(element => element.IsFixedSizeArray)
                    ? $"public unsafe struct {Name} : IMessage, IDeserializable<{Name}>"
                    : $"public struct {Name} : IMessage, IDeserializable<{Name}>, System.IEquatable<{Name}>");
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

            var constructors = CreateConstructors(variables, Name, ForceStruct, IsBlittable,
                actionMessageType != ActionMessageType.None);
            foreach (string entry in constructors)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var serializer = CreateSerializers(variables, ForceStruct, IsBlittable);
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

            const string emptyMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
            const string emptyDependenciesBase64 = "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";

            lines.Add("    /// <summary> MD5 hash of a compact representation of the message. </summary>");

            string md5Hash = Md5Hash switch
            {
                "" => "null",
                emptyMd5Sum => "BuiltIns.EmptyMd5Sum",
                _ => $"\"{Md5Hash}\""
            };
            lines.Add($"    [Preserve] public const string RosMd5Sum = {md5Hash};");

            lines.Add("");

            lines.Add(
                "    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");

            string catDependencies = CreateCatDependencies();
            string compressedDeps = Compress(catDependencies);
            if (compressedDeps == emptyDependenciesBase64)
            {
                lines.Add(
                    "    [Preserve] public const string RosDependenciesBase64 = BuiltIns.EmptyDependenciesBase64;");
                lines.Add("");
            }
            else
            {
                lines.Add("    [Preserve] public const string RosDependenciesBase64 =");
                var splitDeps = Split(compressedDeps);
                foreach (string entry in splitDeps)
                {
                    lines.Add($"            {entry}");
                }
            }

            lines.Add("    public override string ToString() => Extensions.ToString(this);");

            if (RequiresDispose)
            {
                lines.Add("");
                lines.Add("    public void Dispose()");
                lines.Add("    {");
                foreach (var variable in elements.OfType<VariableElement>().Where(ElementRequiresDispose))
                {
                    if (variable.IsArray && variable.ClassInfo is { RequiresDispose: true })
                    {
                        lines.Add($"        foreach (var e in {variable.CsFieldName}) e.Dispose();");
                    }

                    if (variable.RentHint)
                    {
                        lines.Add($"        {variable.CsFieldName}.Dispose();");
                    }
                }

                lines.Add("    }");
            }

            if (Additions.Contents.TryGetValue($"{RosPackage}/{Name}", out var extraLines))
            {
                lines.Add("");
                lines.Add("    /// Custom iviz code");
                foreach (string entry in extraLines)
                {
                    lines.Add($"    {entry}");
                }
            }

            lines.Add("}");

            return lines;
        }

        static bool ElementRequiresDispose(IElement element) =>
            element is VariableElement variableElement
            && (variableElement.RentHint || variableElement.ClassInfo is { RequiresDispose: true });

        bool RequiresDispose =>
            requiresDispose is { } result
                ? result
                : (requiresDispose = elements.Any(ElementRequiresDispose)).Value;

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

            var builder = new StringBuilder(100);

            builder.AppendNewLine(fullMessageText);

            foreach (ClassInfo classInfo in dependencies)
            {
                builder.AppendNewLine(DependencySeparator);
                builder.AppendNewLine($"MSG: {classInfo.RosPackage}/{classInfo.Name}");
                builder.AppendNewLine(classInfo.fullMessageText);
            }

            return builder.ToString().Replace("\r", "");
        }

        public string Md5Hash => md5 ??= CalculateMd5();

        string CalculateMd5()
        {
            var str = new StringBuilder(100);

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

            string[] md5Variables;
            try
            {
                md5Variables = variables.Select(x => x.GetEntryForMd5Hash(RosPackage)).ToArray();
            }
            catch (MessageDependencyException e)
            {
                throw new MessageDependencyException($"Failed to generate md5 field for message {FullRosName}", e);
            }

            if (md5Variables.Any((string? md5String) => md5String == null))
            {
                return ""; // shouldn't happen, or exception would have been thrown earlier
            }

            str.Append(string.Join("\n", md5Variables));

            md5File = str.ToString();

            return GetMd5Hash(md5File);
        }

        internal static string GetMd5Hash(string input)
        {
            return Tools.BaseUtils.GetMd5Hash(Utf8.GetBytes(input));
        }
    }

    static class StringBuilderUtils
    {
        public static StringBuilder AppendNewLine(this StringBuilder str, string s)
        {
            str.Append(s).Append('\n');
            return str;
        }

        public static StringBuilder AppendNewLine(this StringBuilder str)
        {
            str.Append('\n');
            return str;
        }
    }
}