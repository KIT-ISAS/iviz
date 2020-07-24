using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Iviz.Msgs
{
    public static class BuiltIns
    {
        public static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);

        public static CultureInfo Culture { get; } = CultureInfo.InvariantCulture;

        public static string InvalidArrayLengthStr { get; } = "Invalid array length.";

        public static string GetClassStringConstant(Type type, string name)
        {
            string constant = (string)type?.GetField(name)?.GetRawConstantValue();
            if (constant == null)
            {
                throw new ArgumentException("Failed to resolve constant '" + name + "' in class " + type.FullName, nameof(name));
            }
            return constant;
        }

        public static string GetMessageType(Type type)
        {
            return GetClassStringConstant(type, "RosMessageType");
        }

        public static string GetServiceType(Type type)
        {
            return GetClassStringConstant(type, "RosServiceType");
        }

        public static string GetMd5Sum(Type type)
        {
            return GetClassStringConstant(type, "RosMd5Sum");
        }

        public static string GetDependenciesBase64(Type type)
        {
            return GetClassStringConstant(type, "RosDependenciesBase64");
        }

        public static IMessage CreateGenerator(Type type)
        {
            return (IMessage)Activator.CreateInstance(type);
        }

        public static string DecompressDependency(Type type)
        {
            string dependenciesBase64 = GetDependenciesBase64(type);
            byte[] inputBytes = Convert.FromBase64String(dependenciesBase64);

            StringBuilder str = new StringBuilder();
            byte[] outputBytes = new byte[32];

            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                int read;
                do
                {
                    read = gZipStream.Read(outputBytes, 0, outputBytes.Length);
                    str.Append(UTF8.GetString(outputBytes, 0, read));
                }
                while (read != 0);
                return str.ToString();
            }
        }
    }

}