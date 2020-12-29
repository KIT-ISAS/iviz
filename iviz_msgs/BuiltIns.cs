using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

#if !NETSTANDARD2_0
using System.Buffers;
#endif

namespace Iviz.Msgs
{
    public static class BuiltIns
    {
        public static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);

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

        public static string DecompressDependency(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string dependenciesBase64 = GetDependenciesBase64(type);
            byte[] inputBytes = Convert.FromBase64String(dependenciesBase64);

            StringBuilder str = new StringBuilder();
            byte[] outputBytes = new byte[32];

            using var inputStream = new MemoryStream(inputBytes);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);

            int read;
            do
            {
                read = gZipStream.Read(outputBytes, 0, outputBytes.Length);
                str.Append(UTF8.GetString(outputBytes, 0, read));
            } while (read != 0);

            return str.ToString();
        }

        public static string RosNameToCs(string name)
        {
            StringBuilder str = new StringBuilder();
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
    }

#if !NETSTANDARD2_0
    public struct Renter : IDisposable
    {
        public byte[] Array { get; }
        public int Size { get; }

        public Renter(int size)
        {
            Array = ArrayPool<byte>.Shared.Rent(size);
            Size = size;
        }

        public void Dispose()
        {
            ArrayPool<byte>.Shared.Return(Array);
        }
    }
#endif
}