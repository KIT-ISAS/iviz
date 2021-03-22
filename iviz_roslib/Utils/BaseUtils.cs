using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib.Utils
{
    public static class BaseUtils
    {
        public static readonly Random Random = new();

        public const string GenericExceptionFormat = "{0}: {1}";

        static readonly Func<(byte b1, byte b2), byte> And = b => (byte) (b.b1 & b.b2);

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

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> t)
        {
            return new(t);
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

        internal static List<string> ParseHeader(in Rent<byte> readBuffer)
        {
            return ParseHeader(readBuffer.Array, readBuffer.Length);
        }

        internal static List<string> ParseHeader(byte[] readBuffer, int toRead)
        {
            int numRead = 0;

            List<string> contents = new();
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

        /// <summary>
        ///     A string hash that does not change every run unlike <see cref="string.GetHashCode()"/>
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
                    {
                        break;
                    }

                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + hash2 * 1566083941;
            }
        }

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
            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort.ToString()}/");
        }
    }
    
    public sealed class ConcurrentSet<T> : IEnumerable<T> where T : notnull 
    {
        readonly ConcurrentDictionary<T, object?> backend = new();
        public IEnumerator<T> GetEnumerator() => backend.Keys.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Add(T s) => backend[s] = null;
        public bool Remove(T s) => backend.TryRemove(s, out _);
        public int Count => backend.Count;
        public void Clear() => backend.Clear();
    }    
}