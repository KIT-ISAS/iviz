#nullable enable

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class FileUtils
    {
        static async ValueTask WriteAllBytesAsync(string filePath, byte[] bytes, int count,
            CancellationToken token)
        {
            await using var stream =
                new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            await stream.WriteAsync(bytes, 0, count, token);
        }

        public static ValueTask WriteAllBytesAsync(string filePath, Rent<byte> bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
        }

        public static ValueTask WriteAllBytesAsync(string filePath, byte[] bytes, CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes, bytes.Length, token);
        }

        public static async ValueTask<Rent<byte>> ReadAllBytesAsync(string filePath, CancellationToken token)
        {
            await using var stream =
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
            var rent = new Rent<byte>((int) stream.Length);
            await stream.ReadAsync(rent.Array, 0, rent.Length, token);
            return rent;
        }

        public static async ValueTask WriteAllTextAsync(string filePath, string text, CancellationToken token)
        {
            using var bytes = text.AsRent();
            await WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
        }

        public static async ValueTask<string> ReadAllTextAsync(string filePath, CancellationToken token)
        {
            using var bytes = await ReadAllBytesAsync(filePath, token);
            return BuiltIns.UTF8.GetString(bytes.Array, 0, bytes.Length);
        }
    }
}