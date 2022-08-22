using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen;

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

[Flags]
public enum RosVersion
{
    Ros1 = 1,
    Ros2 = 2,
    Common = 3
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

    static readonly Dictionary<string, int> BuiltInAlignments = new()
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
        { "time", 4 },
        { "duration", 4 },
        { "char", 1 },
        { "byte", 1 }
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

    public int Ros1FixedSize { get; internal set; } = UninitializedSize;
    public bool HasFixedSize => Ros1FixedSize != UnknownSizeAtCompileTime && Ros1FixedSize != UninitializedSize;

    public int Ros2FixedSize { get; internal set; } = UninitializedSize;
    public int Ros2HeadAlignment { get; internal set; } = UninitializedSize;

    public ReadOnlyCollection<IElement> Elements { get; }
    public string RosPackage { get; }
    public string CsPackage { get; }
    public string Name { get; }
    public bool ForceStruct { get; }
    readonly bool isBlittable;
    public bool IsBlittable => isBlittable || (ForceStruct && Ros1FixedSize >= 0);
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
        if (v.IsArray || v.RosClassType == "string")
        {
            return false;
        }

        if (BuiltInTypes.Contains(v.RosClassType))
        {
            return true;
        }

        string resolvedName = v.RosClassType.Contains("/") ? $"{rosPackage}/{v.RosClassType}" : v.RosClassType;
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

    internal static void DoResolveClasses(PackageInfo packageInfo, string package, VariableElement[] variables)
    {
        foreach (VariableElement variable in variables)
        {
            if (!BuiltInsSizes.ContainsKey(variable.RosClassType) &&
                packageInfo.TryGet(variable.RosClassType, package, out ClassInfo? classInfo))
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
            if (variable.IsDynamicSizeArray)
            {
                return UnknownSizeAtCompileTime;
            }

            if (BuiltInsSizes.TryGetValue(variable.RosClassType, out int size))
            {
                fixedSize += variable.IsFixedSizeArray ? size * variable.ArraySize : size;
            }
            else if (variable.ClassInfo != null &&
                     variable.ClassInfo.CheckFixedSize() != UnknownSizeAtCompileTime)
            {
                fixedSize += variable.IsFixedSizeArray
                    ? size * variable.ClassInfo.Ros1FixedSize
                    : variable.ClassInfo.Ros1FixedSize;
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
        if (Ros1FixedSize == UninitializedSize)
        {
            Ros1FixedSize = DoCheckFixedSize(variables);
        }

        return Ros1FixedSize;
    }

    internal static (int fixedSize, int headAlignment) DoCheckRos2FixedSize(
        VariableElement[] variables)
    {
        int fixedSize = 0;
        if (variables.Length == 0)
        {
            return (0, 1);
        }

        int headAlignment = UninitializedSize;
        for (int i = 0; i < variables.Length; i++)
        {
            var variable = variables[i];

            if (variable.IsDynamicSizeArray)
            {
                return (UnknownSizeAtCompileTime, UninitializedSize);
            }

            if (BuiltInsSizes.TryGetValue(variable.RosClassType, out int size))
            {
                int alignment = BuiltInAlignments[variable.RosClassType];
                int alignmentMask = alignment - 1;
                fixedSize = (fixedSize + alignmentMask) & ~alignmentMask;
                fixedSize += variable.IsFixedSizeArray ? size * variable.ArraySize : size;

                if (i == 0)
                {
                    headAlignment = alignment;
                }
            }
            else if (variable.ClassInfo != null &&
                     variable.ClassInfo.CheckRos2FixedSize() != UnknownSizeAtCompileTime)
            {
                int alignment = variable.ClassInfo.Ros2HeadAlignment;

                int alignmentMask = alignment - 1;
                if (alignmentMask != -1)
                {
                    fixedSize = (fixedSize + alignmentMask) & ~alignmentMask;
                }

                fixedSize += variable.IsFixedSizeArray
                    ? size * variable.ClassInfo.Ros1FixedSize
                    : variable.ClassInfo.Ros1FixedSize;

                if (i == 0)
                {
                    headAlignment = alignment;
                }
            }
            else
            {
                return (UnknownSizeAtCompileTime, UninitializedSize);
            }
        }

        return (fixedSize, headAlignment);
    }

    internal int CheckRos2FixedSize()
    {
        if (Ros2FixedSize == UninitializedSize)
        {
            (Ros2FixedSize, Ros2HeadAlignment) = DoCheckRos2FixedSize(variables);
        }

        return Ros2FixedSize;
    }

    internal static IEnumerable<string> CreateLengthProperty2(IReadOnlyCollection<VariableElement> variables,
        int fixedSize, bool forceStruct, int ros2HeadAlignment = UninitializedSize)
    {
        string readOnlyId = forceStruct ? "readonly " : "";

        if (variables.Count == 0)
        {
            return new[]
            {
                $"public {readOnlyId}int Ros2MessageLength => 0;",
                "",
                $"public {readOnlyId}int AddRos2MessageLength(int c) => c;"
            };
        }

        if (ros2HeadAlignment != UninitializedSize)
        {
            return new[]
            {
                $"public const int Ros2FixedMessageLength = {fixedSize};",
                "",
                $"public {readOnlyId}int Ros2MessageLength => Ros2FixedMessageLength;",
                "",
                ros2HeadAlignment != 1
                    ? $"public {readOnlyId}int AddRos2MessageLength(int c) => WriteBuffer2.Align{ros2HeadAlignment}(c) + Ros2FixedMessageLength;"
                    : $"public {readOnlyId}int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;",
                "",
            };
        }

        var fields = new List<string>();
        if (fixedSize != UnknownSizeAtCompileTime)
        {
            fields.Add($"public const int Ros2FixedMessageLength = {fixedSize};");
            fields.Add("");
            fields.Add($"public {readOnlyId}int Ros2MessageLength => Ros2FixedMessageLength;");
        }
        else
        {
            fields.Add($"public {readOnlyId}int Ros2MessageLength => AddRos2MessageLength(0);");
        }

        fields.Add("");

        int currentAlignment = -1;

        fields.Add($"public {readOnlyId}int AddRos2MessageLength(int d)");
        fields.Add("{");
        fields.Add("    int c = d;"); // weird bug fix
        foreach (var variable in variables)
        {
            if ((variable.Version & RosVersion.Ros2) == 0)
            {
                continue;
            }

            if (BuiltInsSizes.TryGetValue(variable.RosClassType, out int tmpFieldFixedSize))
            {
                int fieldFixedSize = tmpFieldFixedSize;
                int fieldAlignment = BuiltInAlignments[variable.RosClassType];
                WriteFixedSize(fieldAlignment, fieldFixedSize);
            }
            else if (variable.ClassInfo is { Ros2FixedSize: > 0 } classInfo)
            {
                int fieldAlignment = classInfo.Ros2HeadAlignment;
                int fieldFixedSize = classInfo.Ros2FixedSize;
                WriteFixedSize(fieldAlignment, fieldFixedSize);
            }
            else if (variable.RosClassType == "string")
            {
                if (!variable.IsFixedSizeArray)
                {
                    WriteAlign(4);
                    fields.Add($"    c = WriteBuffer2.AddLength(c, {variable.CsFieldName});");
                }
                else
                {
                    fields.Add(
                        $"    c = WriteBuffer2.AddLength(c, {variable.CsFieldName}, {variable.FixedArraySize});");
                }

                currentAlignment = -1;
            }
            else if (variable.IsArray)
            {
                WriteAlign(4);
                fields.Add($"    c += 4; // {variable.CsFieldName}.Length");
                fields.Add($"    foreach (var t in {variable.CsFieldName})");
                fields.Add($"    {{");
                fields.Add($"        c = t.AddRos2MessageLength(c);");
                fields.Add($"    }}");

                currentAlignment = -1;
            }
            else
            {
                fields.Add($"    c = {variable.CsFieldName}.AddRos2MessageLength(c);");
                currentAlignment = -1;
            }

            // ------------------

            void WriteFixedSize(int fieldAlignment, int fieldFixedSize)
            {
                int selfPadding;
                if (fieldAlignment > 1)
                {
                    int fieldAlignmentMask = fieldAlignment - 1;
                    int selfAlignedFixedSize = (fieldFixedSize + fieldAlignmentMask) & (~fieldAlignmentMask);
                    selfPadding = selfAlignedFixedSize - fieldFixedSize;
                }
                else
                {
                    selfPadding = 0;
                }

                if (variable.IsFixedSizeArray)
                {
                    WriteAlign(fieldAlignment);
                    fields.Add(selfPadding == 0
                        ? $"    c += {fieldFixedSize} * {variable.FixedArraySize}; // {variable.CsFieldName}"
                        : $"    c += ({fieldFixedSize} + {selfPadding}) * {variable.FixedArraySize} - {selfPadding}; // {variable.CsFieldName}");
                }
                else if (variable.IsDynamicSizeArray)
                {
                    WriteAlign(4);
                    fields.Add($"    c += 4; // {variable.CsFieldName} length");
                    currentAlignment = 4;
                    WriteAlign(fieldAlignment);

                    fields.Add(selfPadding == 0
                        ? $"    c += {fieldFixedSize} * {variable.CsFieldName}.Length;"
                        : $"    c += ({fieldFixedSize} + {selfPadding}) * {variable.CsFieldName}.Length - {selfPadding};");
                }
                else
                {
                    WriteAlign(fieldAlignment);
                    fields.Add($"    c += {fieldFixedSize}; // {variable.CsFieldName}");
                }

                currentAlignment = fieldFixedSize % fieldAlignment == 0
                    ? fieldAlignment
                    : -1;
            }

            void WriteAlign(int fieldAlignment)
            {
                if (fieldAlignment > 1 && (currentAlignment == -1 || fieldAlignment > currentAlignment))
                {
                    fields.Add($"    c = WriteBuffer2.Align{fieldAlignment}(c);");
                    currentAlignment = fieldAlignment;
                }
            }
        }

        fields.Add($"    return c;");
        fields.Add("}");

        return fields;
    }

    internal static IEnumerable<string> CreateLengthProperty1(IReadOnlyCollection<VariableElement> variables,
        int fixedSize, bool forceStruct)
    {
        string readOnlyId = forceStruct ? "readonly " : "";
        if (fixedSize != UnknownSizeAtCompileTime)
        {
            return new[]
            {
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
        int fieldSize = 0;
        foreach (var variable in variables)
        {
            if ((variable.Version & RosVersion.Ros1) == 0)
            {
                continue;
            }

            if (BuiltInsSizes.TryGetValue(variable.RosClassType, out int size))
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
                        fieldSize += variable.ClassInfo.Ros1FixedSize;
                    }
                    else if (variable.CsClassType == "string")
                    {
                        fieldSize += 4;
                        fieldsWithSize.Add($"WriteBuffer.GetStringSize({variable.CsFieldName})");
                    }
                    else if (variable.ClassIsBlittable)
                    {
                        fieldsWithSize.Add($"{variable.CsClassType}.RosFixedMessageLength");
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

                    if (variable.ClassInfo != null && variable.ClassInfo.Ros1FixedSize != UnknownSizeAtCompileTime)
                    {
                        fieldsWithSize.Add($"{variable.ClassInfo.Ros1FixedSize} * {variable.CsFieldName}.Length");
                    }
                    else if (variable.ClassIsBlittable)
                    {
                        fieldsWithSize.Add(
                            $" {variable.CsClassType}.RosFixedMessageLength * {variable.CsFieldName}.Length");
                    }
                    else
                    {
                        fieldsWithSize.Add($"WriteBuffer.GetArraySize({variable.CsFieldName})");
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
            return $"{v.CsClassType}[] {v.CsFieldName}";
        }

        if (v.ClassIsStruct)
        {
            return $"in {v.CsClassType} {v.CsFieldName}";
        }

        return $"{v.CsClassType} {v.CsFieldName}";
    }

    internal static IEnumerable<string> CreateConstructors(IReadOnlyCollection<VariableElement> variables,
        string name, bool forceStruct, bool structIsBlittable, bool isAction, RosVersion version)
    {
        var lines = new List<string>();

        if (!forceStruct)
        {
            lines.Add($"public {name}()");
            lines.Add("{");
            foreach (var variable in variables)
            {
                if (GetDefaultFor(variable) is { } entry)
                {
                    lines.Add("    " + entry);
                }
            }

            lines.Add("}");
            lines.Add("");
        }

        if ((forceStruct || structIsBlittable || variables.Count <= 4) && variables.Count != 0)
        {
            string args = string.Join(", ", variables.Select(ParamToArg));
            lines.Add($"public {name}({args})");
            lines.Add("{");
            foreach (VariableElement variable in variables)
            {
                if (variable.ArraySize > 0 && forceStruct)
                {
                    lines.Add(
                        $"    if ({variable.CsFieldName} is null) BuiltIns.ThrowArgumentNull(nameof({variable.CsFieldName}));");
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

        switch (version)
        {
            case RosVersion.Ros1:
                CreateConstructorWithBuffer(lines, variables, name, forceStruct, structIsBlittable, isAction, false);
                break;
            case RosVersion.Ros2:
                CreateConstructorWithBuffer(lines, variables, name, forceStruct, structIsBlittable, isAction, true);
                break;
            case RosVersion.Common:
                CreateConstructorWithBuffer(lines, variables, name, forceStruct, structIsBlittable, isAction, false);
                lines.Add("");
                CreateConstructorWithBuffer(lines, variables, name, forceStruct, structIsBlittable, isAction, true);
                break;
            default:
                throw new IndexOutOfRangeException();
        }

        lines.Add("");

        string readOnlyId = forceStruct ? "readonly " : "";

        if ((version & RosVersion.Ros1) != 0)
        {
            lines.Add(variables.Any()
                ? $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer b) => new {name}(ref b);"
                : $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer b) => Singleton;");
        }

        if (version == RosVersion.Common)
        {
            lines.Add("");
        }

        if ((version & RosVersion.Ros2) != 0)
        {
            lines.Add(variables.Any()
                ? $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer2 b) => new {name}(ref b);"
                : $"public {readOnlyId}{name} RosDeserialize(ref ReadBuffer2 b) => Singleton;");
        }

        if (!variables.Any())
        {
            lines.Add("");
            lines.Add($"static {name}? singleton;");
            lines.Add($"public static {name} Singleton => singleton ??= new {name}();");
        }

        return lines;
    }

    static string? GetDefaultFor(VariableElement variable, bool forceInit = false)
    {
        if (BuiltInTypes.Contains(variable.RosClassType))
        {
            if (variable.RosClassType == "string" && !variable.IsArray)
            {
                return $"{variable.CsFieldName} = \"\";";
            }

            if (variable.IsDynamicSizeArray)
            {
                return variable.RentHint
                    ? $"{variable.CsFieldName} = Tools.SharedRent.Empty;"
                    : $"{variable.CsFieldName} = EmptyArray<{variable.CsClassType}>.Value;";
            }

            if (variable.IsArray)
            {
                return $"{variable.CsFieldName} = new {variable.CsClassType}[{variable.ArraySize}];";
            }
        }
        else
        {
            if (variable.IsDynamicSizeArray)
            {
                return $"{variable.CsFieldName} = EmptyArray<{variable.CsClassType}>.Value;";
            }

            if (variable.IsArray)
            {
                return $"{variable.CsFieldName} = new {variable.CsClassType}[{variable.ArraySize}];";
            }

            if (!variable.ClassIsStruct)
            {
                return variable.ClassInfo is { Ros1FixedSize: 0 }
                    ? $"{variable.CsFieldName} = {variable.CsClassType}.Singleton;"
                    : $"{variable.CsFieldName} = new {variable.CsClassType}();";
            }
        }

        return forceInit
            ? $"{variable.CsFieldName} = default;"
            : null;
    }

    static void CreateConstructorWithBuffer(List<string> lines, IEnumerable<VariableElement> variables, string name,
        bool forceStruct, bool structIsBlittable, bool isAction, bool isRos2)
    {
        if (forceStruct)
        {
            lines.Add("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        }

        string ros2Suffix = isRos2 ? "2" : "";

        lines.Add($"public {name}(ref ReadBuffer{ros2Suffix} b)");
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
                lines.Add($"public static void Deserialize(ref ReadBuffer{ros2Suffix} b, out {name} h)");
                lines.Add("{");
            }

            int currentAlignment = -1;

            string prefix = forceStruct ? "h." : "";
            foreach (var variable in variables)
            {
                if (isRos2 && (variable.Version & RosVersion.Ros2) == 0 ||
                    !isRos2 && (variable.Version & RosVersion.Ros1) == 0)
                {
                    if (GetDefaultFor(variable, true) is { } entry)
                    {
                        lines.Add($"    {prefix}{entry}");
                    }

                    continue;
                }

                if (BuiltInTypes.Contains(variable.RosClassType))
                {
                    switch (variable.ArraySize)
                    {
                        case VariableElement.NotAnArray:
                            if (variable.CsClassType == "string")
                            {
                                AddAlign(4);
                                lines.Add(variable.IgnoreHint
                                    ? $"    b.SkipString(out {prefix}{variable.CsFieldName});"
                                    : $"    b.DeserializeString(out {prefix}{variable.CsFieldName});");
                            }
                            else if (isRos2 && BuiltInAlignments.TryGetValue(variable.RosClassType, out int alignment))
                            {
                                AddAlign(alignment);
                                lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                                break;
                            }
                            else
                            {
                                lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                            }

                            currentAlignment = -1;
                            break;
                        case VariableElement.DynamicSizeArray:
                            if (variable.CsClassType == "string")
                            {
                                AddAlign(4);
                                lines.Add(variable.IgnoreHint
                                    ? $"    b.SkipStringArray(out {prefix}{variable.CsFieldName});"
                                    : $"    b.DeserializeStringArray(out {prefix}{variable.CsFieldName});");
                            }
                            else if (variable.RentHint)
                            {
                                AddAlign(4);
                                lines.Add($"    b.DeserializeStructRent(out {prefix}{variable.CsFieldName});");
                            }
                            else if (variable.IgnoreHint)
                            {
                                AddAlign(4);
                                lines.Add($"    b.SkipStructArray(out {prefix}{variable.CsFieldName});");
                            }
                            else
                            {
                                //lines.Add($"    b.DeserializeStructArray(out {prefix}{variable.CsFieldName});");
                                lines.Add($"    unsafe");
                                lines.Add($"    {{");
                                AddAlign(4, "    ");
                                lines.Add($"        int n = b.DeserializeArrayLength();");
                                lines.Add($"        {prefix}{variable.CsFieldName} = n == 0");
                                lines.Add($"            ? EmptyArray<{variable.CsClassType}>.Value");
                                lines.Add($"            : new {variable.CsClassType}[n];");
                                lines.Add($"        if (n != 0)");
                                lines.Add($"        {{");
                                if (isRos2)
                                {
                                    int alignment3 = BuiltInAlignments[variable.RosClassType];
                                    AddAlign(alignment3, "        ");
                                }

                                lines.Add(
                                    $"            b.DeserializeStructArray(Unsafe.AsPointer(ref {prefix}{variable.CsFieldName}[0]), n * {BuiltInsSizes[variable.RosClassType]});");
                                lines.Add($"        }}");
                                lines.Add($"    }}");

                                break;
                            }

                            currentAlignment = -1;
                            break;
                        default:
                            if (variable.CsClassType == "string")
                            {
                                lines.Add(
                                    $"    b.DeserializeStringArray({variable.ArraySize}, out {prefix}{variable.CsFieldName});");
                                currentAlignment = -1;
                                break;
                            }

                            int fixedSize = BuiltInsSizes[variable.RosClassType];
                            int arraySize = variable.ArraySize;

                            lines.Add($"    unsafe");
                            lines.Add($"    {{");
                            if (isRos2)
                            {
                                int alignment3 = BuiltInAlignments[variable.RosClassType];
                                AddAlign(alignment3, "    ");
                            }

                            lines.Add(
                                $"        {prefix}{variable.CsFieldName} = new {variable.CsClassType}[{arraySize}];");
                            lines.Add(
                                $"        b.DeserializeStructArray(Unsafe.AsPointer(ref {prefix}{variable.CsFieldName}[0]), {arraySize} * {fixedSize});");
                            lines.Add($"    }}");

                            //lines.Add(
                            //        $"    b.DeserializeStructArray({variable.ArraySize}, out {prefix}{variable.CsFieldName});");

                            break;
                    }
                }
                else
                {
                    switch (variable.ArraySize)
                    {
                        case VariableElement.NotAnArray:
                            if (variable.ClassInfo is { Ros1FixedSize: 0 })
                            {
                                lines.Add($"    {prefix}{variable.CsFieldName} = {variable.CsClassType}.Singleton;");
                            }
                            else if (variable.ClassIsBlittable)
                            {
                                if (isRos2 && variable.ClassInfo?.Ros2HeadAlignment is { } alignment)
                                {
                                    AddAlign(alignment);
                                    lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                                    break;
                                }

                                lines.Add($"    b.Deserialize(out {prefix}{variable.CsFieldName});");
                            }
                            else if (variable.ClassIsStruct && !isAction)
                            {
                                lines.Add(
                                    $"    {variable.CsClassType}.Deserialize(ref b, out {prefix}{variable.CsFieldName});");
                            }
                            else
                            {
                                lines.Add(
                                    $"    {prefix}{variable.CsFieldName} = new {variable.CsClassType}(ref b);");
                            }

                            currentAlignment = -1;
                            break;
                        case VariableElement.DynamicSizeArray when variable.ClassIsBlittable:
                            //lines.Add($"    b.DeserializeStructArray(out {prefix}{variable.CsFieldName});");

                            lines.Add($"    unsafe");
                            lines.Add($"    {{");
                            AddAlign(4, "    ");
                            lines.Add($"        int n = b.DeserializeArrayLength();");
                            lines.Add($"        {prefix}{variable.CsFieldName} = n == 0");
                            lines.Add($"            ? EmptyArray<{variable.CsClassType}>.Value");
                            lines.Add($"            : new {variable.CsClassType}[n];");
                            lines.Add($"        if (n != 0)");
                            lines.Add($"        {{");
                            if (isRos2)
                            {
                                int alignment3 = variable.ClassInfo?.Ros2HeadAlignment ?? 0;
                                AddAlign(alignment3, "        ");
                            }

                            lines.Add(
                                $"            b.DeserializeStructArray(Unsafe.AsPointer(ref {prefix}{variable.CsFieldName}[0]), n * {variable.ClassInfo!.Ros1FixedSize});");
                            lines.Add($"        }}");
                            lines.Add($"    }}");


                            if (isRos2 && variable.ClassInfo?.Ros2HeadAlignment is { } alignment2)
                            {
                                currentAlignment = alignment2;
                                break;
                            }

                            currentAlignment = -1;
                            break;
                        case VariableElement.DynamicSizeArray:
                            //lines.Add($"    b.DeserializeArray(out {prefix}{variable.CsFieldName});");
                            lines.Add($"    {{");
                            AddAlign(4, "    ");
                            lines.Add($"        int n = b.DeserializeArrayLength();");
                            lines.Add($"        {prefix}{variable.CsFieldName} = n == 0");
                            lines.Add($"            ? EmptyArray<{variable.CsClassType}>.Value");
                            lines.Add($"            : new {variable.CsClassType}[n];");
                            lines.Add($"        for (int i = 0; i < n; i++)");
                            lines.Add($"        {{");
                            lines.Add(variable.ClassIsStruct && !isAction
                                ? $"            {variable.CsClassType}.Deserialize(ref b, out {prefix}{variable.CsFieldName}[i]);"
                                : $"            {prefix}{variable.CsFieldName}[i] = new {variable.CsClassType}(ref b);");
                            lines.Add($"        }}");
                            lines.Add($"    }}");
                            currentAlignment = -1;
                            break;
                        default:
                        {
                            if (variable.ClassIsBlittable)
                            {
                                int fixedSize = variable.ClassInfo?.Ros1FixedSize ?? 0;
                                int arraySize = variable.ArraySize;

                                lines.Add($"    unsafe");
                                lines.Add($"    {{");
                                if (isRos2)
                                {
                                    int alignment3 = variable.ClassInfo?.Ros2HeadAlignment ?? 0;
                                    AddAlign(alignment3, "    ");
                                }

                                lines.Add(
                                    $"        {prefix}{variable.CsFieldName} = new {variable.CsClassType}[{arraySize}];");
                                lines.Add(
                                    $"        b.DeserializeStructArray(Unsafe.AsPointer(ref {prefix}{variable.CsFieldName}[0]), {arraySize} * {fixedSize});");
                                lines.Add($"    }}");
                                //lines.Add(
                                //    $"    b.DeserializeStructArray({variable.ArraySize}, out {prefix}{variable.CsFieldName});");
                                break;
                            }

                            lines.Add(
                                $"    {prefix}{variable.CsFieldName} = b.DeserializeArray<{variable.CsClassType}>({variable.ArraySize});");
                            lines.Add($"    for (int i = 0; i < {variable.ArraySize}; i++)");
                            lines.Add("    {");
                            lines.Add(variable.ClassIsStruct && !isAction
                                ? $"        {variable.CsClassType}.Deserialize(ref b, out {prefix}{variable.CsFieldName}[i]);"
                                : $"        {prefix}{variable.CsFieldName}[i] = new {variable.CsClassType}(ref b);");
                            lines.Add("    }");

                            currentAlignment = -1;
                            break;
                        }
                    }
                }

                void AddAlign(int alignment, string alPrefix = "")
                {
                    if (!isRos2) return;
                    if (currentAlignment != -1 && currentAlignment >= alignment)
                    {
                        currentAlignment = alignment;
                        return;
                    }

                    if (alignment != 1)
                    {
                        lines.Add($"    {alPrefix}b.Align{alignment}();");
                    }

                    currentAlignment = alignment;
                }
            }
        }

        lines.Add("}");
    }

    internal static IEnumerable<string> CreateSerializers(IReadOnlyCollection<VariableElement> variables,
        bool forceStruct, bool isBlittable, RosVersion version)
    {
        var lines = new List<string>();

        switch (version)
        {
            case RosVersion.Ros1:
                CreateSerializer(lines, variables, forceStruct, isBlittable, false);
                break;
            case RosVersion.Ros2:
                CreateSerializer(lines, variables, forceStruct, isBlittable, true);
                break;
            case RosVersion.Common:
                CreateSerializer(lines, variables, forceStruct, isBlittable, false);
                lines.Add("");
                CreateSerializer(lines, variables, forceStruct, isBlittable, true);
                break;
            default:
                throw new IndexOutOfRangeException();
        }

        lines.Add("");

        CreateValidator(lines, variables, forceStruct, isBlittable);

        return lines;
    }

    static void CreateSerializer(List<string> lines, IEnumerable<VariableElement> variables, bool forceStruct,
        bool isBlittable, bool isRos2)
    {
        string readOnlyId = forceStruct ? "readonly " : "";

        if (!isRos2)
        {
            lines.Add($"public {readOnlyId}void RosSerialize(ref WriteBuffer b)");
        }
        else
        {
            lines.Add($"public {readOnlyId}void RosSerialize(ref WriteBuffer2 b)");
        }

        lines.Add("{");
        if (isBlittable)
        {
            lines.Add("    b.Serialize(in this);");
        }
        else
        {
            foreach (VariableElement variable in variables)
            {
                if (isRos2 && (variable.Version & RosVersion.Ros2) == 0 ||
                    !isRos2 && (variable.Version & RosVersion.Ros1) == 0)
                {
                    continue;
                }

                if (BuiltInTypes.Contains(variable.RosClassType))
                {
                    if (!variable.IsArray)
                    {
                        if (forceStruct && variable.RosClassType == "string")
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
                                lines.Add(variable.CsClassType == "string"
                                    ? $"    b.SerializeArray({variable.CsFieldName} ?? EmptyArray<string>.Value);"
                                    : $"    b.SerializeStructArray({variable.CsFieldName} ?? EmptyArray<{variable.CsClassType}>.Value);");
                            }
                            else
                            {
                                lines.Add(variable.CsClassType == "string"
                                    ? $"    b.SerializeArray({variable.CsFieldName});"
                                    : $"    b.SerializeStructArray({variable.CsFieldName});");
                            }
                        }
                        else
                        {
                            if (forceStruct)
                            {
                                lines.Add(variable.CsClassType == "string"
                                    ? $"    b.SerializeArray({variable.CsFieldName} ?? EmptyArray<string>.Value, {variable.ArraySize});"
                                    : $"    b.SerializeStructArray({variable.CsFieldName} ?? EmptyArray<{variable.CsClassType}>.Value, {variable.ArraySize});");
                            }
                            else
                            {
                                lines.Add(variable.CsClassType == "string"
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
                            if (variable.ClassIsBlittable)
                            {
                                lines.Add($"    b.SerializeStructArray({variable.CsFieldName});");
                            }
                            else
                            {
                                lines.Add($"    b.Serialize({variable.CsFieldName}.Length);");
                                lines.Add($"    foreach (var t in {variable.CsFieldName})");
                                lines.Add($"    {{");
                                lines.Add($"        t.RosSerialize(ref b);");
                                lines.Add($"    }}");
                            }
                        }
                        else
                        {
                            /*
                            lines.Add(variable.ClassIsBlittable
                                ? $"    b.SerializeStructArray({variable.CsFieldName}, {variable.ArraySize});"
                                : $"    b.SerializeArray({variable.CsFieldName}, {variable.ArraySize});");
                                */
                            if (variable.ClassIsBlittable)
                            {
                                lines.Add($"    b.SerializeStructArray({variable.CsFieldName});");
                            }
                            else
                            {
                                lines.Add($"    foreach (int i = 0; i < {variable.ArraySize}; i++)");
                                lines.Add($"    {{");
                                lines.Add($"        {variable.CsFieldName}[i].RosSerialize(ref b);");
                                lines.Add($"    }}");
                            }
                        }

                        /*
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
                        */
                    }
                }
            }
        }

        lines.Add("}");
    }

    static void CreateValidator(List<string> lines, IEnumerable<VariableElement> variables, bool forceStruct,
        bool isBlittable)
    {
        string readOnlyId = forceStruct ? "readonly " : "";

        lines.Add($"public {readOnlyId}void RosValidate()");
        lines.Add("{");
        if (isBlittable)
        {
            lines.Add("}");
            return;
        }

        foreach (VariableElement variable in variables)
        {
            if (variable.ArraySize > 0)
            {
                if (!forceStruct)
                {
                    lines.Add(
                        $"    if ({variable.CsFieldName} is null) BuiltIns.ThrowNullReference();");
                    lines.Add(
                        $"    if ({variable.CsFieldName}.Length != {variable.ArraySize}) " +
                        $"BuiltIns.ThrowInvalidSizeForFixedArray(" +
                        $"{variable.CsFieldName}.Length, {variable.ArraySize});");
                }
            }
            else if (!variable.IsArray &&
                     (BuiltInTypes.Contains(variable.RosClassType) && variable.RosClassType != "string" ||
                      variable.ClassIsStruct))
            {
                // do nothing
            }
            else
            {
                if (!forceStruct && (!variable.ClassIsStruct || variable.IsArray))
                {
                    lines.Add(
                        $"    if ({variable.CsFieldName} is null) BuiltIns.ThrowNullReference();");
                }

                if (!variable.IsArray && !variable.ClassIsStruct && variable.RosClassType != "string")
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

                if (variable.RosClassType == "string")
                {
                    lines.Add($"{ind}    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                    lines.Add($"{ind}    {{");
                    lines.Add(
                        $"{ind}        if ({variable.CsFieldName}[i] is null) BuiltIns.ThrowNullReference(nameof({variable.CsFieldName}), i);");
                    lines.Add($"{ind}    }}");
                }
                else if (!BuiltInTypes.Contains(variable.RosClassType) && !variable.ClassIsStruct)
                {
                    lines.Add($"{ind}    for (int i = 0; i < {variable.CsFieldName}.Length; i++)");
                    lines.Add($"{ind}    {{");
                    lines.Add(
                        $"{ind}        if ({variable.CsFieldName}[i] is null) BuiltIns.ThrowNullReference(nameof({variable.CsFieldName}), i);");
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
    }

    static string Compress(string catDependencies)
    {
        byte[] inputBytes = Utf8.GetBytes(catDependencies);

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

    IEnumerable<string> CreateClassContent(RosVersion version)
    {
        var lines = new List<string>();
        lines.Add($"[DataContract]");

        string messageType = version switch
        {
            RosVersion.Ros1 => "IMessageRos1",
            RosVersion.Ros2 => "IMessageRos2",
            RosVersion.Common => "IMessage",
            _ => throw new IndexOutOfRangeException()
        };

        string deserializableType = version switch
        {
            RosVersion.Ros1 => $"IDeserializableRos1<{Name}>",
            RosVersion.Ros2 => $"IDeserializableRos2<{Name}>",
            RosVersion.Common => $"IDeserializable<{Name}>",
            _ => throw new IndexOutOfRangeException()
        };

        if (ForceStruct)
        {
            lines.Add("[StructLayout(LayoutKind.Sequential)]");
            lines.Add(variables.Any(element => element.IsFixedSizeArray)
                ? $"public unsafe struct {Name} : {messageType}, {deserializableType}"
                : $"public struct {Name} : {messageType}, {deserializableType}");
        }
        else
        {
            string line = $"public sealed class {Name} : {deserializableType}";
            string fullLine = actionMessageType switch
            {
                ActionMessageType.None => $"{line}, {messageType}",
                ActionMessageType.Goal => $"{line}, {messageType}, IGoal<{actionRoot}ActionGoal>",
                ActionMessageType.Feedback => $"{line}, {messageType}, IFeedback<{actionRoot}ActionFeedback>",
                ActionMessageType.Result => $"{line}, {messageType}, IResult<{actionRoot}ActionResult>",
                ActionMessageType.ActionGoal => $"{line}, {messageType}, IActionGoal<{actionRoot}Goal>",
                ActionMessageType.ActionFeedback => $"{line}, {messageType}, IActionFeedback<{actionRoot}Feedback>",
                ActionMessageType.ActionResult => $"{line}, {messageType}, IActionResult<{actionRoot}Result>",
                ActionMessageType.Action =>
                    $"{line}, {messageType},\n\t\tIAction<{actionRoot}ActionGoal, {actionRoot}ActionFeedback, {actionRoot}ActionResult>",
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
            actionMessageType != ActionMessageType.None, version);
        foreach (string entry in constructors)
        {
            lines.Add($"    {entry}");
        }

        lines.Add("");
        var serializer = CreateSerializers(variables, ForceStruct, IsBlittable, version);
        foreach (string entry in serializer)
        {
            lines.Add($"    {entry}");
        }

        lines.Add("");

        CheckFixedSize();
        CheckRos2FixedSize();

        var lengthProperty = version switch
        {
            RosVersion.Ros1 => CreateLengthProperty1(variables, Ros1FixedSize, ForceStruct),
            RosVersion.Ros2 => CreateLengthProperty2(variables, Ros2FixedSize, ForceStruct, Ros2HeadAlignment),
            RosVersion.Common =>
                CreateLengthProperty1(variables, Ros1FixedSize, ForceStruct)
                    .Append("")
                    .Concat(CreateLengthProperty2(variables, Ros2FixedSize, ForceStruct, Ros2HeadAlignment)),
            _ => throw new IndexOutOfRangeException()
        };

        foreach (string entry in lengthProperty)
        {
            lines.Add($"    {entry}");
        }

        lines.Add("");
        lines.Add($"    public const string MessageType = \"{RosPackage}/{Name}\";");
        lines.Add("");
        string readOnlyId = ForceStruct ? "readonly " : "";
        lines.Add($"    public {readOnlyId}string RosMessageType => MessageType;");

        lines.Add("");

        if ((version & RosVersion.Ros1) != 0)
        {
            const string emptyMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
            const string emptyDependenciesBase64 = "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";

            lines.Add("    /// MD5 hash of a compact representation of the ROS1 message");
            string md5Hash = Md5Hash switch
            {
                "" => "null",
                emptyMd5Sum => "BuiltIns.EmptyMd5Sum",
                _ => $"\"{Md5Hash}\""
            };

            lines.Add($"    public const string Md5Sum = {md5Hash};");
            lines.Add("");
            lines.Add($"    public {readOnlyId}string RosMd5Sum => Md5Sum;");

            lines.Add("");

            lines.Add(
                "    /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file");

            string catDependencies = CreateCatDependencies();
            string compressedDeps = Compress(catDependencies);
            if (compressedDeps == emptyDependenciesBase64)
            {
                lines.Add(
                    $"    public {readOnlyId}string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;");
                lines.Add("");
            }
            else
            {
                lines.Add($"    public {readOnlyId}string RosDependenciesBase64 =>");
                var splitDeps = Split(compressedDeps);
                foreach (string entry in splitDeps)
                {
                    lines.Add($"            {entry}");
                }
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

        if ((version & RosVersion.Ros1) != 0 &&
            Additions.Contents.TryGetValue($"{RosPackage}/{Name}", out var extraLines))
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

    public string ToCsString(RosVersion version = RosVersion.Common)
    {
        var str = new StringBuilder(200);

        str.AppendNewLine("/* This file was created automatically, do not edit! */");
        str.AppendNewLine();

        if (ForceStruct)
        {
            str.AppendNewLine("using System.Runtime.InteropServices;");
        }

        str.AppendNewLine("using System.Runtime.CompilerServices;");
        str.AppendNewLine("using System.Runtime.Serialization;");


        if (version == RosVersion.Ros2)
        {
            str.AppendNewLine("using Iviz.Msgs;");
        }

        string @namespace = version == RosVersion.Ros2 ? "Msgs2" : "Msgs";

        str.AppendNewLine();

        str.AppendNewLine($"namespace Iviz.{@namespace}.{CsPackage}");
        str.AppendNewLine("{");

        foreach (string entry in CreateClassContent(version))
        {
            str.Append("    ").AppendNewLine(entry);
        }

        str.AppendNewLine("}");

        return str.ToString();
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