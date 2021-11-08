using System;
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
    public class RosException : Exception
    {
        public RosException(string msg) : base(msg)
        {
        }
        
        public RosException(string msg, Exception e) : base(msg, e)
        {
        }
    }

    public class RosInvalidMessageException : RosException
    {
        public RosInvalidMessageException(string msg) : base(msg)
        {
        }
    }

    public class RosInvalidSizeForFixedArrayException : RosInvalidMessageException
    {
        public RosInvalidSizeForFixedArrayException() : base(
            "Array size does not match the fixed size of the message definition")
        {
        }

        public RosInvalidSizeForFixedArrayException(string name, int size, int expected) : base(
            $"Array '{name}' with size {size.ToString()} does not match the fixed size " +
            $"{expected.ToString()} of the message definition")
        {
        }
    }

    public static class BuiltIns
    {
        public static readonly UTF8Encoding UTF8 = Defaults.UTF8;

        public static readonly CultureInfo Culture = Defaults.Culture;
        
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
        /// <param name="T">The message type. Should derive from IMessage.</param>
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

            int? constant = (int?) type.GetField("RosFixedMessageLength")?.GetRawConstantValue();
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

            StringBuilder str = new();
            using var outputBytes = new Rent<byte>(32);
            using var inputStream = new MemoryStream(inputBytes);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);

            int read;
            do
            {
                read = gZipStream.Read(outputBytes.Array, 0, outputBytes.Length);
                str.Append(UTF8.GetString(outputBytes.Array, 0, read));
            } while (read != 0);

            return str.ToString();
        }

        static string RosNameToCs(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StringBuilder str = new();
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
            return ToJsonString((object) o, indented);
        }

        public static string ToJsonString(this IService o, bool indented = true)
        {
            return ToJsonString((object) o, indented);
        }

        public static string ToJsonString(object o, bool indented = true)
        {
            return JsonConvert.SerializeObject(o, indented ? Formatting.Indented : Formatting.None);
        }

        public static byte[] SerializeToArray<T>(this T o) where T : ISerializable
        {
            o.RosValidate();
            byte[] bytes = new byte[o.RosMessageLength];
            Buffer.Serialize(o, bytes);
            return bytes;
        }

        public static uint SerializeToArray<T>(this T o, byte[] bytes, int offset = 0) where T : ISerializable
        {
            return Buffer.Serialize(o, bytes, offset);
        }

        public static T DeserializeFromArray<T>(this T generator, byte[] bytes, int size = -1, int offset = 0)
            where T : ISerializable
        {
            return Buffer.Deserialize(generator, bytes, size, offset);
        }

        public static T DeserializeFromArray<T>(this IDeserializable<T> generator, byte[] bytes, int size = -1,
            int offset = 0) where T : ISerializable
        {
            return Buffer.Deserialize(generator, bytes, size, offset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetArraySize<T>(T[]? array) where T : IMessage
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetArraySize(string[]? array)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetStringSize(string? s)
        {
            return s == null ? 0 : UTF8.GetByteCount(s);            
        } 
    }
}