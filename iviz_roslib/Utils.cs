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

        public static bool IsAlive(this IRosPublisher t)
        {
            return !t.CancellationToken.IsCancellationRequested;
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


        public readonly struct ZipEnumerable<TA, TB> : IReadOnlyList<(TA First, TB Second)>
        {
            readonly IReadOnlyList<TA> a;
            readonly IReadOnlyList<TB> b;

            public struct ZipEnumerator : IEnumerator<(TA First, TB Second)>
            {
                readonly IReadOnlyList<TA> a;
                readonly IReadOnlyList<TB> b;
                int currentIndex;

                internal ZipEnumerator(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
                {
                    this.a = a;
                    this.b = b;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    bool isLastIndex = currentIndex == Math.Min(a.Count, b.Count) - 1;
                    if (isLastIndex)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public void Reset()
                {
                    currentIndex = -1;
                }

                public (TA, TB) Current => (a[currentIndex], b[currentIndex]);

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal ZipEnumerable(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
            {
                this.a = a;
                this.b = b;
            }

            public ZipEnumerator GetEnumerator()
            {
                return new ZipEnumerator(a, b);
            }

            IEnumerator<(TA, TB)> IEnumerable<(TA First, TB Second)>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => Math.Min(a.Count, b.Count);

            public (TA First, TB Second) this[int index] => (a[index], b[index]);

            public (TA First, TB Second)[] ToArray()
            {
                (TA, TB)[] array = new (TA, TB)[Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }
        }

        public static ZipEnumerable<TA, TB> Zip<TA, TB>(this IReadOnlyList<TA> a, IReadOnlyList<TB> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            return new ZipEnumerable<TA, TB>(a, b);
        }

        public readonly struct SelectEnumerable<TA, TB> : IReadOnlyList<TB>
        {
            readonly IReadOnlyList<TA> a;
            readonly Func<TA, TB> f;

            public struct SelectEnumerator : IEnumerator<TB>
            {
                readonly IReadOnlyList<TA> a;
                readonly Func<TA, TB> f;
                int currentIndex;

                internal SelectEnumerator(IReadOnlyList<TA> a, Func<TA, TB> f)
                {
                    this.a = a;
                    this.f = f;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    bool isLastIndex = currentIndex == a.Count - 1;
                    if (isLastIndex)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public void Reset()
                {
                    currentIndex = -1;
                }

                public TB Current => f(a[currentIndex]);

                object? IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal SelectEnumerable(IReadOnlyList<TA> a, Func<TA, TB> f)
            {
                this.a = a;
                this.f = f;
            }

            public SelectEnumerator GetEnumerator()
            {
                return new SelectEnumerator(a, f);
            }

            IEnumerator<TB> IEnumerable<TB>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public TB[] ToArray()
            {
                TB[] array = new TB[a.Count];
                for (int i = 0; i < a.Count; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }

            public List<TB> ToList()
            {
                List<TB> array = new List<TB>(a.Count);
                foreach (var ta in a)
                {
                    array.Add(f(ta));
                }

                return array;
            }

            public int Count => a.Count;

            public TB this[int index] => f(a[index]);
        }

        public static SelectEnumerable<TA, TB> Select<TA, TB>(
            this IReadOnlyList<TA> a,
            Func<TA, TB> f)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            return new SelectEnumerable<TA, TB>(a, f);
        }

        public static void AddRange<TA, TB>(this List<TB> list, SelectEnumerable<TA, TB> tb)
        {
            list.Capacity = list.Count + tb.Count;
            foreach (TB b in tb)
            {
                list.Add(b);
            }
        }

        public readonly struct RefEnumerable<T>
        {
            readonly T[] a;

            public struct RefEnumerator
            {
                readonly T[] a;
                int currentIndex;

                public RefEnumerator(T[] a)
                {
                    this.a = a;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    if (currentIndex == a.Length - 1)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public ref T Current => ref a[currentIndex];
            }
            
            public RefEnumerable(T[] a) => this.a = a;

            public RefEnumerator GetEnumerator() => new RefEnumerator(a);
        }
        
        public static RefEnumerable<T> RefEnum<T>(this T[] a) =>
            new RefEnumerable<T>(a ?? throw new ArgumentNullException(nameof(a)));
        
        public static RefEnumerable<T>.RefEnumerator RefEnumerator<T>(this T[] a) =>
            new RefEnumerable<T>.RefEnumerator(a ?? throw new ArgumentNullException(nameof(a)));
    }
}