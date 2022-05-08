#nullable enable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class FileUtils
    {
        static async ValueTask WriteAllBytesAsync(string filePath, ReadOnlyMemory<byte> bytes, CancellationToken token)
        {
            await using var stream =
                new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            await stream.WriteAsync(bytes, token);
        }

        public static ValueTask WriteAllBytesAsync(string filePath, Rent<byte> bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, (ReadOnlyMemory<byte>)bytes, token);
        }

        public static ValueTask WriteAllBytesAsync(string filePath, byte[] bytes, CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, (ReadOnlyMemory<byte>)bytes, token);
        }

        public static async ValueTask WriteAllTextAsync(string filePath, string text, CancellationToken token)
        {
            using var bytes = text.AsRent();
            await WriteAllBytesAsync(filePath, bytes, token);
        }
        
        public static async ValueTask<Rent<byte>> ReadAllBytesAsync(string filePath, CancellationToken token)
        {
            await using var stream =
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
            
            var rent = new Rent<byte>((int)stream.Length);

            int remaining = rent.Length;
            int offset = 0;

            while (remaining > 0)
            {
                int numRead = await stream.ReadAsync(rent.Array, offset, remaining, token);
                remaining -= numRead;
                offset += numRead;
            }

            return rent;
        }

        public static async ValueTask<string> ReadAllTextAsync(string filePath, CancellationToken token)
        {
            using var bytes = await ReadAllBytesAsync(filePath, token);
            return BuiltIns.UTF8.GetString(bytes);
        }
    }
}