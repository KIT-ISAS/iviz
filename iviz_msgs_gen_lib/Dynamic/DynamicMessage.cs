using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsGen;
using Buffer = Iviz.Msgs.Buffer;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsGen.Dynamic
{
    public sealed class DynamicMessage : IField, IMessage, IDeserializable<DynamicMessage>
    {
        static readonly Dictionary<string, Type> BaseTypes = new Dictionary<string, Type>
        {
            ["bool"] = typeof(bool),
            ["int8"] = typeof(sbyte),
            ["uint8"] = typeof(byte),
            ["int16"] = typeof(short),
            ["uint16"] = typeof(ushort),
            ["int32"] = typeof(int),
            ["uint32"] = typeof(uint),
            ["int64"] = typeof(long),
            ["uint64"] = typeof(ulong),
            ["float32"] = typeof(float),
            ["float64"] = typeof(double),
            ["time"] = typeof(time),
            ["duration"] = typeof(duration),
            ["char"] = typeof(char),
            ["byte"] = typeof(byte),
        };

        readonly (string Name, IField Value)[] fields;
        public string RosType { get; }
        public string? RosInstanceMd5Sum { get; }
        public string? RosInstanceDependencies { get; }

        public DynamicMessage()
        {
            RosType = "";
            fields = Array.Empty<(string Name, IField Value)>();
        }
        public DynamicMessage(ClassInfo classInfo, bool allowAssemblyLookup = true) : this(classInfo,
            new Dictionary<string, DynamicMessage>(), allowAssemblyLookup)
        {
        }

        DynamicMessage(ClassInfo classInfo, IDictionary<string, DynamicMessage> registered,
            bool allowAssemblyLookup = true)
        {
            if (classInfo == null)
            {
                throw new ArgumentNullException(nameof(classInfo));
            }

            if (registered == null)
            {
                throw new ArgumentNullException(nameof(registered));
            }

            RosType = classInfo.FullRosName;
            RosInstanceMd5Sum = classInfo.Md5Hash;
            RosInstanceDependencies = classInfo.CreateCatDependencies();

            registered[RosType] = this;

            string FullRosName(string rosType) => rosType.Contains("/") ? rosType : $"{classInfo.RosPackage}/{rosType}";

            List<(string Name, IField Value)> newFields = new List<(string Name, IField Value)>();
            foreach (VariableElement element in classInfo.Elements.OfType<VariableElement>())
            {
                IField field;
                string name = element.RosFieldName;
                string rosType = element.RosClassName;

                if (rosType == "string")
                {
                    field = element.ArraySize switch
                    {
                        VariableElement.NotAnArray => new StringField(),
                        VariableElement.DynamicSizeArray => new StringArrayField(),
                        _ => new StringArrayFieldFixed((uint) element.ArraySize)
                    };
                }
                else if (BaseTypes.TryGetValue(rosType, out Type? csType))
                {
                    object? tmpField = element.ArraySize switch
                    {
                        VariableElement.NotAnArray =>
                            Activator.CreateInstance(typeof(StructField<>).MakeGenericType(csType)),
                        VariableElement.DynamicSizeArray =>
                            Activator.CreateInstance(typeof(StructArrayField<>).MakeGenericType(csType)),
                        _ =>
                            Activator.CreateInstance(typeof(StructArrayFieldFixed<>).MakeGenericType(csType),
                                (uint) element.ArraySize)
                    };
                    field = (IField) (tmpField ?? throw new NullReferenceException());
                }
                else if (allowAssemblyLookup &&
                         (csType = BuiltIns.TryGetTypeFromMessageName(FullRosName(rosType))) != null)
                {
                    object? tmpField = element.ArraySize switch
                    {
                        VariableElement.NotAnArray =>
                            Activator.CreateInstance(typeof(MessageField<>).MakeGenericType(csType)),
                        VariableElement.DynamicSizeArray =>
                            Activator.CreateInstance(typeof(MessageArrayField<>).MakeGenericType(csType)),
                        _ =>
                            Activator.CreateInstance(typeof(MessageArrayFieldFixed<>).MakeGenericType(csType),
                                (uint) element.ArraySize)
                    };
                    field = (IField) (tmpField ?? throw new NullReferenceException());
                }
                else
                {
                    if (!registered.TryGetValue(name, out DynamicMessage? generator))
                    {
                        if (element.ClassInfo == null)
                        {
                            throw new MessageParseException(
                                $"Missing class info for '{element.RosClassName}' for field '{element.RosFieldName}'" +
                                $" in message '{classInfo.FullRosName}'");
                        }

                        generator = new DynamicMessage(element.ClassInfo, registered);
                    }

                    field = element.ArraySize switch
                    {
                        VariableElement.NotAnArray => new DynamicMessage(generator),
                        VariableElement.DynamicSizeArray => new DynamicMessageArrayField(generator),
                        _ => new DynamicMessageArrayFieldFixed((uint) element.ArraySize, generator)
                    };
                }

                newFields.Add((name, field));
            }

            fields = newFields.ToArray();
        }

        internal DynamicMessage(DynamicMessage other)
        {
            RosType = other.RosType;
            RosInstanceMd5Sum = other.RosInstanceMd5Sum;
            RosInstanceDependencies = other.RosInstanceDependencies;
            fields = new (string Name, IField Value)[other.fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = (other.fields[i].Name, other.fields[i].Value.Generate());
            }
        }

        public int RosMessageLength
        {
            get
            {
                int size = 0;
                foreach (var field in fields)
                {
                    size += field.Value.RosMessageLength;
                }

                return size;
            }
        }

        public void RosValidate()
        {
            foreach (var field in fields)
            {
                field.Value.RosValidate();
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            foreach (var field in fields)
            {
                field.Value.RosSerialize(ref b);
            }
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            foreach (var field in fields)
            {
                field.Value.RosDeserializeInPlace(ref b);
            }
        }

        IField IField.Generate()
        {
            return Generate();
        }

        object IField.Value => fields;

        DynamicMessage Generate()
        {
            return new DynamicMessage(this);
        }

        public DynamicMessage RosDeserialize(ref Buffer b)
        {
            DynamicMessage t = new DynamicMessage(this);
            t.RosDeserializeInPlace(ref b);
            return t;
        }

        ISerializable ISerializable.RosDeserialize(ref Buffer b)
        {
            return RosDeserialize(ref b);
        }

        public static bool IsDynamic<T>() where T : IMessage
        {
            return typeof(DynamicMessage) == typeof(T);
        }

        [Preserve] public const string RosMessageType = "*";

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "*";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 = "";


        public static ClassInfo CreateFromDependencyString(string msgName, string dependencies)
        {
            var msgDefinitionsWithHeader = dependencies
                .Split(new[] {ClassInfo.DependencySeparator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(def => def.Trim());
            var msgDefinitions = StripHeader(msgName, msgDefinitionsWithHeader);
            var classInfos = msgDefinitions.Select(tuple => new ClassInfo("", tuple.Name, tuple.Definition));

            PackageInfo packageInfo = new PackageInfo(classInfos, Array.Empty<ServiceInfo>());
            packageInfo.ResolveAll();
            return packageInfo.Messages[msgName];
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
                    if (newLineIndex < 5 || msgDefinition.Substring(0, 5) != "MSG: ")
                    {
                        throw new MessageParseException("Expected message name in the first line");
                    }

                    string name = msgDefinition.Substring(5, newLineIndex - 5).Trim();
                    yield return (name, msgDefinition.Substring(newLineIndex + 1));
                }
                else
                {
                    yield return (msgName, msgDefinition);
                    isFirst = false;
                }
            }
        }
    }
}