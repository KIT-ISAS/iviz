using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public static class Utils
    {
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
            return new(t);
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

        internal static async Task<bool> ReadChunkAsync(this NetworkStream stream, byte[] buffer, int toRead,
            CancellationToken token)
        {
            int numRead = 0;
#if !NETSTANDARD2_0
            while (numRead < toRead)
            {
                int readNow = await stream.ReadAsync(buffer.AsMemory(numRead, toRead - numRead), token).Caf();
#else
            var tokenTaskSource = new TaskCompletionSource<object>();
            var tokenTask = tokenTaskSource.Task;
            using var registration = token.Register(() => tokenTaskSource.TrySetCanceled());

            while (numRead < toRead)
            {
                Task<int> readTask = stream.ReadAsync(buffer, numRead, toRead - numRead, token);
                Task resultTask = await Task.WhenAny(readTask, tokenTask).Caf();
                if (resultTask == tokenTask)
                {
                    token.ThrowIfCanceled(tokenTask);
                    throw new TimeoutException("Reading operation timed out");
                }

                int readNow = await readTask.Caf();
#endif
                if (readNow == 0)
                {
                    return false;
                }

                numRead += readNow;
            }

            return true;
        }

        internal static async Task WriteChunkAsync(this NetworkStream stream, byte[] buffer, int count,
            CancellationToken token)
        {
#if !NETSTANDARD2_0
            await stream.WriteAsync(buffer.AsMemory(0, count), token).Caf();
#else
            var tokenTaskSource = new TaskCompletionSource<object>();
            var tokenTask = tokenTaskSource.Task;
            using var registration = token.Register(() => tokenTaskSource.TrySetCanceled());

            Task writeTask = stream.WriteAsync(buffer, 0, count, token);
            Task resultTask = await Task.WhenAny(writeTask, tokenTask).Caf();
            if (resultTask == tokenTask)
            {
                token.ThrowIfCanceled(tokenTask);
                throw new TimeoutException("Writing operation timed out");
            }

            await writeTask.Caf();
#endif
        }

        internal static async Task WriteHeaderAsync(NetworkStream stream, string[] contents, CancellationToken token)
        {
            int totalLength = 4 * contents.Length + contents.Sum(entry => entry.Length);

            using var array = new Rent<byte>(totalLength + 4);
            using var writer = new BinaryWriter(new MemoryStream(array.Array));

            writer.Write(totalLength);
            foreach (string t in contents)
            {
                writer.Write(t.Length);
                writer.Write(BuiltIns.UTF8.GetBytes(t));
            }

            await stream.WriteChunkAsync(array.Array, array.Length, token).Caf();
        }


        /// <summary>
        ///     A string hash that does not change every run unlike GetHashCode
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
            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort}/");
        }
    }

    /// <summary>
    ///     Simple class that overrides the ToString() method to produce a JSON representation.
    /// </summary>
    public abstract class JsonToString
    {
        public override string ToString()
        {
            return Utils.ToJsonString(this);
        }
    }

    internal sealed class ResizableRent<T> : IDisposable where T : unmanaged
    {
        bool disposed;
        Rent<T> buffer;

        public T[] Array => buffer.Array;

        public ResizableRent(int size)
        {
            buffer = new Rent<T>(size);
        }

        public void EnsureCapability(int size)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Dispose() has already been called on this object.");
            }
            
            if (buffer.Array.Length >= size)
            {
                return;
            }

            buffer.Dispose();
            buffer = new Rent<T>(size);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            buffer.Dispose();
        }
    }
}