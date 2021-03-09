using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

#if !NETSTANDARD2_0
#endif

namespace Iviz.Msgs
{
    public static class BuiltIns
    {
        public static UTF8Encoding UTF8 { get; } = new(false);

        public static CultureInfo Culture { get; } = CultureInfo.InvariantCulture;

        static string GetClassStringConstant(Type type, string name)
        {
            string? constant = (string?) type.GetField(name)?.GetRawConstantValue();
            if (constant == null)
            {
                throw new ArgumentException($"Failed to resolve constant '{name}' in class {type.FullName}",
                    nameof(name));
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

        /// <summary>
        /// Checks if the size of the ROS message type is fixed, and returns it.
        /// </summary>
        /// <param name="type">The message type. Should derive from IMessage.</param>
        /// <param name="size">The fixed size, if it exists.</param>
        /// <returns>True if the message has a fixed size.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type is null.</exception>
        public static bool TryGetFixedSize(Type type, out int size)
        {
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

        public static string RosNameToCs(string name)
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
            string guessName = $"Iviz.Msgs.{RosNameToCs(fullRosMessageName)}, {assemblyName}";
            return Type.GetType(guessName);
        }

        public static void DisposeElements<T>(this UniqueRef<T> tt) where T : IDisposable
        {
            foreach (var t in tt)
            {
                t.Dispose();
            }
        }

        public static void DisposeElements<T>(this SharedRef<T> tt) where T : IDisposable
        {
            foreach (var t in tt)
            {
                t.Dispose();
            }
        }

        public static void DisposeElements<T>(this T[] tt) where T : IDisposable
        {
        }

        public static UniqueRef<StringRef> AsRef(this string[] tt, UniqueRef<StringRef> _)
        {
            var uref = new UniqueRef<StringRef>(tt.Length);
            for (int i = 0; i < tt.Length; i++)
            {
                uref[i] = tt[i];
            }

            return uref;
        }

        public static string[] AsRef(this string[] tt, string[] _)
        {
            return tt;
        }

        public static string[] AsArray(this UniqueRef<StringRef> tt)
        {
            var array = new string[tt.Length];
            for (int i = 0; i < tt.Length; i++)
            {
                array[i] = tt[i];
            }

            return array;
        }

        public static Rent<byte> AsRent(this string text)
        {
            var bytes = new Rent<byte>(UTF8.GetByteCount(text));
            UTF8.GetBytes(text, 0, text.Length, bytes.Array, 0);
            return bytes;
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
    }
}