using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Tools;
using Newtonsoft.Json;

#if !NETSTANDARD2_0
#endif

namespace Iviz.Msgs
{
    public static class BuiltIns
    {
        public static UTF8Encoding UTF8 => Defaults.UTF8;

        public static CultureInfo Culture => Defaults.Culture;

        public const string EmptyMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";

        public const string EmptyDependenciesBase64 = "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";

        static string GetClassStringConstant(Type type, string name)
        {
            Type? currentType = type;
            string? constant;
            do
            {
                constant = currentType.GetField(name)?.GetRawConstantValue() as string
                           ?? currentType.GetProperty(name)?.GetValue(null) as string;
                currentType = currentType.BaseType;
            } while (constant == null && currentType != null);

            if (constant == null)
            {
                throw new RosInvalidMessageException($"Failed to resolve constant '{name}' in class {type.FullName}");
            }

            return constant;
        }

        /// <summary>
        /// Returns the ROS message name of the given message type.
        /// </summary>
        /// <param name="type">The message type. Should derive from IMessage.</param>
        /// <returns>The ROS message type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type is null</exception>
        /// <exception cref="ArgumentException">Thrown if the type does not implement IMessage.</exception>
        public static string GetMessageType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetClassStringConstant(type, "RosMessageType");
        }

        public static string GetMessageType<T>() where T : IMessage => GetMessageType(typeof(T));

        /// <summary>
        /// Returns the ROS service name of the given service type.
        /// </summary>
        /// <param name="type">The message type. Should derive from IService.</param>
        /// <returns>The ROS service type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type is null</exception>
        /// <exception cref="ArgumentException">Thrown if the type does not implement IService.</exception>
        public static string GetServiceType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetClassStringConstant(type, "RosServiceType");
        }

        public static string GetServiceType<T>() where T : IService => GetServiceType(typeof(T));

        /// <summary>
        /// Returns the MD5 value of the given message or service type.
        /// </summary>
        /// <param name="type">The message type. Should derive from IMessage or IService.</param>
        /// <returns>The MD5 value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type is null</exception>
        /// <exception cref="ArgumentException">Thrown if the type does not implement IMessage or IService.</exception>
        public static string GetMd5Sum(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetClassStringConstant(type, "RosMd5Sum");
        }

        public static string GetMd5Sum<T>() => GetMd5Sum(typeof(T));

        /// <summary>
        /// Checks if the size of the ROS message type is fixed, and returns it.
        /// </summary>
        /// <typeparam name="T">The message type. Should derive from IMessage.</typeparam>
        /// <param name="size">The fixed size, if it exists.</param>
        /// <returns>True if the message has a fixed size.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type is null.</exception>
        public static bool TryGetFixedSize<T>(out int size) where T : ISerializable
        {
            var type = typeof(T);
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            int? constant = (int?)type.GetField("RosFixedMessageLength")?.GetRawConstantValue();
            if (constant == null)
            {
                size = default;
                return false;
            }

            size = constant.Value;
            return true;
        }

        static string GetDependenciesBase64(Type type)
        {
            return GetClassStringConstant(type, "RosDependenciesBase64");
        }

        public static string DecompressDependencies<T>() where T : ISerializable => DecompressDependencies(typeof(T));

        public static string DecompressDependencies(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string dependenciesBase64 = GetDependenciesBase64(type);
            byte[] inputBytes = Convert.FromBase64String(dependenciesBase64);

            Span<byte> outputBytes = stackalloc byte[32];
            using var inputStream = new MemoryStream(inputBytes);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            using var str = BuilderPool.Rent();

            int read;
            do
            {
                read = gZipStream.Read(outputBytes);
                str.Append(UTF8.GetString(outputBytes[..read]));
            } while (read != 0);

            return str.ToString();
        }

        static string RosNameToCs(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            using var str = BuilderPool.Rent();
            str.Append(char.ToUpper(name[0], Culture));
            for (int i = 1; i < name.Length; i++)
            {
                switch (name[i])
                {
                    case '_' when i != name.Length - 1:
                        str.Append(char.ToUpper(name[i + 1], Culture));
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

        public static Type? TryGetTypeFromMessageName(string fullRosMessageName, string assemblyName = "Iviz.Msgs")
        {
            string guessName = $"{assemblyName}.{RosNameToCs(fullRosMessageName)}, {assemblyName}";
            var type = Type.GetType(guessName);
            if (type != null
                && typeof(IMessage).IsAssignableFrom(type)
                && (type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null))
            {
                return type;
            }

            return null;
        }

        public static string ToJsonString(this ISerializable o, bool indented = true)
        {
            return ToJsonString((object)o, indented);
        }

        public static string ToJsonString(this IService o, bool indented = true)
        {
            return ToJsonString((object)o, indented);
        }

        public static string ToJsonString(object o, bool indented = true)
        {
            return JsonConvert.SerializeObject(o, indented ? Formatting.Indented : Formatting.None);
        }

        public static byte[] SerializeToArray<T>(this T o) where T : ISerializable
        {
            o.RosValidate();
            byte[] bytes = new byte[o.RosMessageLength];
            WriteBuffer.Serialize(o, bytes);
            return bytes;
        }

        public static uint SerializeTo<T>(this T o, Span<byte> bytes) where T : ISerializable
        {
            return WriteBuffer.Serialize(o, bytes);
        }

        public static ISerializable DeserializeFrom(this ISerializable generator, ReadOnlySpan<byte> bytes)
        {
            return ReadBuffer.Deserialize(generator, bytes);
        }

        public static T DeserializeFrom<T>(this IDeserializable<T> generator, ReadOnlySpan<byte> bytes)
            where T : ISerializable
        {
            return ReadBuffer.Deserialize(generator, bytes);
        }

        public static T DeserializeFrom<T>(this T generator, ReadOnlySpan<byte> bytes)
            where T : ISerializable, IDeserializable<T>
        {
            return ReadBuffer.Deserialize(generator, bytes);
        }

        public static T DeserializeMessage<T>(ReadOnlySpan<byte> bytes)
            where T : ISerializable, IDeserializable<T>, new()
        {
            return ReadBuffer.Deserialize(new T(), bytes);
        }

        /// Returns the size in bytes of a message array when deserialized in ROS
        public static int GetArraySize<T>(T[]? array) where T : IMessage
        {
            if (array == null)
            {
                return 0;
            }

            int size = 0;
            for (int i = 0; i < array.Length; i++)
            {
                size += array[i].RosMessageLength;
            }

            return size;
        }

        /// Returns the size in bytes of a string array when deserialized in ROS
        public static int GetArraySize(string[]? array)
        {
            if (array == null)
            {
                return 0;
            }

            int size = 4 * array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                size += UTF8.GetByteCount(array[i]);
            }

            return size;
        }

        /// Returns the size in bytes of a string when deserialized in ROS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetStringSize(string? s)
        {
            return s == null ? 0 : UTF8.GetByteCount(s);
        }

        [DoesNotReturn]
        public static void ThrowArgumentNull(string arg) => throw new ArgumentNullException(arg);

        [DoesNotReturn]
        public static void ThrowNullReference(string name) => throw new NullReferenceException(name);

        [DoesNotReturn]
        public static void ThrowNullReference(string name, int i) => throw new NullReferenceException($"{name}[{i}]");

        [DoesNotReturn]
        public static void ThrowNullReference() => throw new NullReferenceException("Message fields cannot null.");

        [DoesNotReturn]
        public static void ThrowBufferOverflow(int off, int remaining) =>
            throw new RosBufferException($"Requested {off} bytes, but only {remaining} remain!");

        [DoesNotReturn]
        public static void ThrowInvalidSizeForFixedArray(int size, int expected) =>
            throw new RosInvalidSizeForFixedArrayException(size, expected);
    }
}

#if NETSTANDARD2_1
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that the <c>.locals init</c>
    /// flag should not be set in method headers.
    /// </summary>
    /// <remarks>
    /// This attribute is unsafe because it may reveal uninitialized memory to
    /// the application in certain instances (e.g., reading from uninitialized
    /// stackalloc'd memory). If applied to a method directly, the attribute
    /// applies to that method and all nested functions (lambdas, local
    /// functions) below it. If applied to a type or module, it applies to all
    /// methods nested inside. This attribute is intentionally not permitted on
    /// assemblies. Use at the module level instead to apply to multiple type
    /// declarations.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Module
                    | AttributeTargets.Class
                    | AttributeTargets.Struct
                    | AttributeTargets.Interface
                    | AttributeTargets.Constructor
                    | AttributeTargets.Method
                    | AttributeTargets.Property
                    | AttributeTargets.Event, Inherited = false)]
    public sealed class SkipLocalsInitAttribute : Attribute
    {
    }
}
#endif