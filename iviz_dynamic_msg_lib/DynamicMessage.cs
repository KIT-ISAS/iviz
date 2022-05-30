using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Tools;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.StdMsgs;
using ISerializable = Iviz.Msgs.ISerializable;


namespace Iviz.MsgsGen.Dynamic;

public sealed class DynamicMessage : IField, IMessage, IDeserializable<DynamicMessage>
{
    static Dictionary<string, IGenerator>? structTypes;

    static Dictionary<string, IGenerator> StructTypes => structTypes ??= new Dictionary<string, IGenerator>
    {
        ["bool"] = new StructGenerator<bool>(),
        ["int8"] = new StructGenerator<sbyte>(),
        ["uint8"] = new StructGenerator<byte>(),
        ["int16"] = new StructGenerator<short>(),
        ["uint16"] = new StructGenerator<ushort>(),
        ["int32"] = new StructGenerator<int>(),
        ["uint32"] = new StructGenerator<uint>(),
        ["int64"] = new StructGenerator<long>(),
        ["uint64"] = new StructGenerator<ulong>(),
        ["float32"] = new StructGenerator<float>(),
        ["float64"] = new StructGenerator<double>(),
        ["time"] = new StructGenerator<time>(),
        ["duration"] = new StructGenerator<duration>(),
        ["char"] = new StructGenerator<char>(),
        ["byte"] = new StructGenerator<byte>(),

        [Vector3.MessageType] = new MessageGenerator<Vector3>(),
        [Point.MessageType] = new MessageGenerator<Point>(),
        [Point32.MessageType] = new MessageGenerator<Point32>(),
        [Quaternion.MessageType] = new MessageGenerator<Quaternion>(),
        [Pose.MessageType] = new MessageGenerator<Pose>(),
        [Transform.MessageType] = new MessageGenerator<Transform>(),
        [ColorRGBA.MessageType] = new MessageGenerator<ColorRGBA>(),
        [Color32.MessageType] = new MessageGenerator<Color32>(),
        [Vector2f.MessageType] = new MessageGenerator<Vector2f>(),
        [Vector3f.MessageType] = new MessageGenerator<Vector3f>(),
        [Triangle.MessageType] = new MessageGenerator<Triangle>(),

        [Header.MessageType] = new MessageGenerator<Header>(),
        [TransformStamped.MessageType] = new MessageGenerator<TransformStamped>(),
        [Log.MessageType] = new MessageGenerator<Log>(),
    };

    public const string RosAny = "*";

    public IReadOnlyList<Property> Fields { get; }
    public string RosMessageType { get; }
    public string RosMd5Sum { get; }
    public string RosDependenciesBase64 { get; }

    public FieldType Type => FieldType.DynamicMessage;

    public bool IsInitialized => !string.IsNullOrEmpty(RosMessageType);

    public DynamicMessage()
    {
        RosMessageType = RosAny;
        RosMd5Sum = RosAny;
        RosDependenciesBase64 = RosAny;
        Fields = Array.Empty<Property>();
    }

    DynamicMessage(ClassInfo classInfo, bool allowAssemblyLookup = true) : this(classInfo,
        new Dictionary<string, DynamicMessage>(), allowAssemblyLookup)
    {
    }

    DynamicMessage(ClassInfo classInfo, Dictionary<string, DynamicMessage> registered,
        bool allowAssemblyLookup = true)
    {
        if (classInfo == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(classInfo));
        }

        if (registered == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(registered));
        }

        RosMessageType = classInfo.FullRosName;
        RosMd5Sum = classInfo.Md5Hash;
        RosDependenciesBase64 = classInfo.CreateCatDependencies();

        registered[RosMessageType] = this;

        string FullRosName(string rosType) => rosType.Contains("/") ? rosType : $"{classInfo.RosPackage}/{rosType}";

        var newFields = new List<Property>();
        foreach (var element in classInfo.Elements.OfType<VariableElement>())
        {
            IField field;
            string fieldName = element.RosFieldName;
            string fieldRosType = element.RosClassType;

            if (fieldRosType == "string")
            {
                field = element.ArraySize switch
                {
                    VariableElement.NotAnArray => new StringField(),
                    VariableElement.DynamicSizeArray => new StringArrayField(),
                    _ => new StringFixedArrayField(element.ArraySize)
                };
            }
            else if (StructTypes.TryGetValue(fieldRosType, out var baseGenerator))
            {
                field = element.ArraySize switch
                {
                    VariableElement.NotAnArray => baseGenerator.CreateField(),
                    VariableElement.DynamicSizeArray => baseGenerator.CreateArrayField(),
                    _ => baseGenerator.CreateFixedArrayField(element.ArraySize)
                };
            }
            else if (allowAssemblyLookup &&
                     BuiltIns.TryGetTypeFromMessageName(FullRosName(fieldRosType)) is { } csType)
            {
                object? tmpField = element.ArraySize switch
                {
                    VariableElement.NotAnArray =>
                        Activator.CreateInstance(typeof(MessageField<>).MakeGenericType(csType)),
                    VariableElement.DynamicSizeArray =>
                        Activator.CreateInstance(typeof(MessageArrayField<>).MakeGenericType(csType)),
                    _ =>
                        Activator.CreateInstance(typeof(MessageFixedArrayField<>).MakeGenericType(csType),
                            element.ArraySize)
                };
                field = (IField)(tmpField ?? throw new NullReferenceException());
            }
            else
            {
                if (!registered.TryGetValue(fieldRosType, out DynamicMessage? dynamicGenerator))
                {
                    if (element.ClassInfo == null)
                    {
                        throw new MessageParseException(
                            $"Missing class info for '{element.RosClassType}' for field '{element.RosFieldName}'" +
                            $" in message '{classInfo.FullRosName}'");
                    }

                    dynamicGenerator = new DynamicMessage(element.ClassInfo, registered, allowAssemblyLookup);
                }

                field = element.ArraySize switch
                {
                    VariableElement.NotAnArray => new DynamicMessage(dynamicGenerator),
                    VariableElement.DynamicSizeArray => new DynamicMessageArrayField(dynamicGenerator),
                    _ => new DynamicMessageFixedArrayField(element.ArraySize, dynamicGenerator)
                };
            }

            newFields.Add(new Property(fieldName, field));
        }

        Fields = newFields;
    }

    internal DynamicMessage(DynamicMessage other)
    {
        RosMessageType = other.RosMessageType;
        RosMd5Sum = other.RosMd5Sum;
        RosDependenciesBase64 = other.RosDependenciesBase64;
        var fields = new Property[other.Fields.Count];
        foreach (int i in ..fields.Length)
        {
            fields[i] = new Property(other.Fields[i].Name, other.Fields[i].Value.Generate());
        }

        Fields = fields;
    }

    int IField.RosLength => RosMessageLength;

    internal int RosLength => RosMessageLength;

    public int RosMessageLength
    {
        get
        {
            int size = 0;
            foreach (var field in Fields)
            {
                size += field.Value.RosLength;
            }

            return size;
        }
    }

    public void RosValidate()
    {
        foreach (var field in Fields)
        {
            field.Value.RosValidate();
        }
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        foreach (var field in Fields)
        {
            field.Value.RosSerialize(ref b);
        }
    }

    internal void RosDeserializeInPlace(ref ReadBuffer b)
    {
        foreach (var field in Fields)
        {
            field.Value.RosDeserializeInPlace(ref b);
        }
    }

    void IField.RosDeserializeInPlace(ref ReadBuffer b) => RosDeserializeInPlace(ref b);

    IField IField.Generate()
    {
        return new DynamicMessage(this);
    }

    object IField.Value => Fields;

    public DynamicMessage RosDeserialize(ref ReadBuffer b)
    {
        var t = new DynamicMessage(this);
        t.RosDeserializeInPlace(ref b);
        return t;
    }

    ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b)
    {
        return RosDeserialize(ref b);
    }

    public static bool IsDynamic(Type T) => typeof(DynamicMessage) == T;

    public static bool IsGeneric(Type T) => typeof(IMessage) == T || IsDynamic(T);

    public override string ToString() => this.ToJsonString();

    public void Dispose()
    {
    }

    static ClassInfo CreateDefinitionFromDependencyString(string fullRosMsgName, string dependencies)
    {
        if (fullRosMsgName == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(fullRosMsgName));
        }

        if (dependencies == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(dependencies));
        }

        var msgDefinitionsWithHeader = dependencies
            .Split(new[] { ClassInfo.DependencySeparator }, StringSplitOptions.RemoveEmptyEntries)
            .Select(def => def.Trim());
        var msgDefinitions = StripHeader(fullRosMsgName, msgDefinitionsWithHeader);
        var classInfos = msgDefinitions.Select(tuple => new ClassInfo("", tuple.Name, tuple.Definition));

        PackageInfo packageInfo = new(classInfos, Array.Empty<ServiceInfo>());
        packageInfo.ResolveAll();
        return packageInfo.Messages[fullRosMsgName];
    }

    public static DynamicMessage CreateFromDependencyString(string fullRosMsgName, string dependencies,
        bool allowAssemblyLookup = true)
    {
        return new DynamicMessage(
            CreateDefinitionFromDependencyString(fullRosMsgName, dependencies),
            allowAssemblyLookup);
    }

    static IEnumerable<(string Name, string Definition)> StripHeader(string msgName,
        IEnumerable<string> msgDefinitions)
    {
        bool isFirst = true;
        foreach (string msgDefinition in msgDefinitions)
        {
            if (!isFirst)
            {
                int newLineIndex = msgDefinition.IndexOf('\n');
                if (newLineIndex < 5 || msgDefinition[..5] != "MSG: ")
                {
                    throw new MessageParseException("Expected message name in the first line");
                }

                string name = msgDefinition.Substring(5, newLineIndex - 5).Trim();
                yield return (name, msgDefinition[(newLineIndex + 1)..]);
            }
            else
            {
                yield return (msgName, msgDefinition);
                isFirst = false;
            }
        }
    }

    interface IGenerator
    {
        IField CreateField();
        IField CreateArrayField();
        IField CreateFixedArrayField(int size);
    }

    sealed class StructGenerator<T> : IGenerator where T : unmanaged
    {
        public IField CreateField() => new StructField<T>();
        public IField CreateArrayField() => new StructArrayField<T>();
        public IField CreateFixedArrayField(int size) => new StructFixedArrayField<T>(size);
    }

    sealed class MessageGenerator<T> : IGenerator where T : IMessage, IDeserializable<T>, new()
    {
        public IField CreateField() => new MessageField<T>();
        public IField CreateArrayField() => new MessageArrayField<T>();
        public IField CreateFixedArrayField(int size) => new MessageFixedArrayField<T>(size);
    }
}