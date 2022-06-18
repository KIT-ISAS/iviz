using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.Msgs
{
    public static class BuiltIns
    {
        public static UTF8Encoding UTF8 => Defaults.UTF8;

        public static CultureInfo Culture => Defaults.Culture;

        public const string EmptyMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";

        public const string EmptyDependenciesBase64 = "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";

        /// <summary>
        /// Returns the constant field <see cref="IMessage.RosMessageType"/> for the given type T. 
        /// </summary>
        public static string GetMessageType<T>() where T : IMessage, new()
        {
            return new T().RosMessageType;
        }

        /// <summary>
        /// Returns the constant field <see cref="IService.RosServiceType"/> for the given type T. 
        /// </summary>
        public static string GetServiceType<T>() where T : IService, new()
        {
            return new T().RosServiceType;
        }

        /// <summary>
        /// Returns the constant field <see cref="IService.RosMd5Sum"/> for the given type T. 
        /// </summary>
        public static string GetMd5Sum<T>() where T : IMessage, new()
        {
            return new T().RosMd5Sum;
        }

        /// <summary>
        /// Decompresses the dependencies of an <see cref="IMessage.RosDependenciesBase64"/> field. 
        /// </summary>
        public static string DecompressDependencies(string dependenciesBase64)
        {
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
                ThrowArgumentNull(nameof(name));
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

        /// Obtains the C# class type for a given ROS message, if it exists. 
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

        /// Obtains an instance of a C# ROS message, if it exists. 
        public static IMessage? TryGetGeneratorFromMessageName(string fullRosMessageName,
            string assemblyName = "Iviz.Msgs")
        {
            if (TryGetTypeFromMessageName(fullRosMessageName, assemblyName) is { } lookupMsgType
                && Activator.CreateInstance(lookupMsgType) is IMessage message)
            {
                return message;
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

        public static int GetArraySize(GeometryMsgs.TransformStamped[]? array)
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

        /// Returns the size in bytes of a message array when deserialized in ROS
        public static int GetArraySize(IMessage[]? array)
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

        // we use here the fact that 99% of the strings we get are ascii and with length <= 64 (i.e., frames in headers)
        // so we do a simple check and if it's ascii, we do a quick conversion that gets auto-vectorized
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        internal static unsafe string GetStringSimple(byte* spanPtr, int length)
        {
            if (!CheckIfAllAscii(spanPtr, length))
            {
                return UTF8.GetString(spanPtr, length);
            }

            char* buffer = stackalloc char[64];
            ConvertToChar(spanPtr, (ushort*)buffer, length);
            return new string(buffer, 0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe bool CheckIfAllAscii(byte* ptr, int size)
        {
            int result = 0;
            for (int i = 0; i < size; i++) result |= ptr[i];
            return (result & 0x80) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void ConvertToChar(byte* src, ushort* dst, int size)
        {
            for (int i = 0; i < size; i++) dst[i] = src[i];
        }

        [DoesNotReturn, AssertionMethod]
        public static void ThrowArgumentNull(string arg) => throw new ArgumentNullException(arg);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowNullReference(string name) => throw new NullReferenceException(name);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowNullReference(string name, int i) => throw new NullReferenceException($"{name}[{i}]");

        [DoesNotReturn, AssertionMethod]
        public static void ThrowNullReference() => throw new NullReferenceException("Message fields cannot be null.");

        [DoesNotReturn, AssertionMethod]
        public static void ThrowBufferOverflow(int off, int remaining) =>
            throw new RosBufferException($"Requested {off} bytes, but only {remaining} remain!");

        [DoesNotReturn, AssertionMethod]
        public static void ThrowImplausibleBufferSize() =>
            throw new RosBufferException("Implausible message requested more than 1TB elements.");

        [DoesNotReturn, AssertionMethod]
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