using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Newtonsoft.Json;

namespace Iviz.Roslib
{
    /// <summary>
    /// Simple class that overrides the ToString() method to produce a JSON representation. 
    /// </summary>
    public abstract class JsonToString
    {
        public override string ToString()
        {
            return Utils.ToJsonString(this);
        }
    }

    public static class Utils
    {
        public const string GenericExceptionFormat = "{0}: {1}";

        public static bool HasPrefix(this string check, string prefix)
        {
            if (check is null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (check.Length < prefix.Length)
            {
                return false;
            }

            for (int i = 0; i < prefix.Length; i++)
            {
                if (check[i] != prefix[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasSuffix(this string check, string suffix)
        {
            if (check is null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (check.Length < suffix.Length)
            {
                return false;
            }

            int offset = check.Length - suffix.Length;
            for (int i = 0; i < suffix.Length; i++)
            {
                if (check[offset + i] != suffix[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static void PrintBuffer(byte[] bytes, int start, int size)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            for (int i = 0; i < size; i++)
            {
                Logger.Log($"[{i}]: {(int) bytes[start + i]} --> {(char) bytes[start + i]}");
            }
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

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> t)
        {
            return new ReadOnlyCollection<T>(t);
        }

        public static bool RemovePair<TT, TU>(this ConcurrentDictionary<TT, TU> dictionary, TT t, TU u)
            where TT : notnull
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return ((ICollection<KeyValuePair<TT, TU>>) dictionary).Remove(new KeyValuePair<TT, TU>(t, u));
        }

        public static int Sum<T>(this T[] ts, Func<T, int> selector)
        {
            int sum = 0;
            foreach (T t in ts)
            {
                sum += selector(t);
            }

            return sum;
        }

        public static bool Any<T>(this T[] ts, Predicate<T> predicate)
        {
            foreach (var t in ts)
            {
                if (predicate(t))
                {
                    return true;
                }
            }

            return false;
        }

        internal static List<string> ParseHeader(byte[] readBuffer, int totalLength = -1)
        {
            int numRead = 0;
            int toRead = totalLength != -1 ? totalLength : readBuffer.Length;

            List<string> contents = new List<string>();
            while (numRead < toRead)
            {
                int length = BitConverter.ToInt32(readBuffer, numRead);
                numRead += 4;
                string entry = BuiltIns.UTF8.GetString(readBuffer, numRead, length);
                numRead += length;

                contents.Add(entry);
            }

            return contents;
        }

        internal static async Task<bool> ReadChunkAsync(this NetworkStream stream, byte[] buffer, int toRead,
            CancellationToken token = default)
        {
            int numRead = 0;
            while (numRead < toRead)
            {
#if !NETSTANDARD2_0
                int readNow = await stream.ReadAsync(new Memory<byte>(buffer, numRead, toRead - numRead), token);
#else
                int readNow = await stream.ReadAsync(buffer, numRead, toRead - numRead, token).Caf();
#endif
                if (readNow == 0)
                {
                    return false;
                }

                numRead += readNow;
            }

            return true;
        }

        internal static async Task WriteHeaderAsync(NetworkStream stream, string[] contents,
            CancellationToken token = default)
        {
            int totalLength = 4 * contents.Length + contents.Sum(entry => entry.Length);

            byte[] array = new byte[totalLength + 4];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (string t in contents)
                {
                    writer.Write(t.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(t));
                }
            }

            await stream.WriteAsync(array, 0, array.Length, token).Caf();
        }


        /// <summary>
        /// A string hash that does not change every run unlike GetHashCode
        /// </summary>
        /// <param name="str">String to calculate the hash from</param>
        /// <returns>A hash integer</returns>
        public static int GetDeterministicHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        static readonly Func<(byte b1, byte b2), byte> And = b => (byte) (b.b1 & b.b2);

        public static bool IsInSameSubnet(IPAddress addressA, IPAddress addressB, IPAddress subnetMask)
        {
            byte[] addressABytes = addressA.GetAddressBytes();
            byte[] addressBBytes = addressB.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();
            var broadcastABytes = addressABytes.Zip(subnetMaskBytes).Select(And);
            var broadcastBBytes = addressBBytes.Zip(subnetMaskBytes).Select(And);
            return broadcastABytes.SequenceEqual(broadcastBBytes);
        }

        public static IEnumerable<UnicastIPAddressInformation> GetInterfaceCandidates(NetworkInterfaceType type)
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(@interface => @interface.NetworkInterfaceType == type &&
                                     @interface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
        }

        public static Uri? GetUriFromInterface(NetworkInterfaceType type, int usingPort)
        {
            UnicastIPAddressInformation? ipInfo = GetInterfaceCandidates(type).FirstOrDefault();
            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort}/");
        }
    }
}