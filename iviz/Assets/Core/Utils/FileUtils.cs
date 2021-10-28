using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static class FileUtils
    {
        static async Task WriteAllBytesAsync([NotNull] string filePath, [NotNull] byte[] bytes, int count,
            CancellationToken token)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await stream.WriteAsync(bytes, 0, count, token);
            }
        }

        [NotNull]
        public static Task WriteAllBytesAsync([NotNull] string filePath, Rent<byte> bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
        }

        [NotNull]
        public static Task WriteAllBytesAsync([NotNull] string filePath, [NotNull] byte[] bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes, bytes.Length, token);
        }

        public static async ValueTask<Rent<byte>> ReadAllBytesAsync([NotNull] string filePath, CancellationToken token)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true))
            {
                var rent = new Rent<byte>((int)stream.Length);
                await stream.ReadAsync(rent.Array, 0, rent.Length, token);
                return rent;
            }
        }

        [NotNull]
        public static async Task WriteAllTextAsync([NotNull] string filePath, [NotNull] string text,
            CancellationToken token)
        {
            using (var bytes = text.AsRent())
            {
                await WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
            }
        }

        [ItemNotNull]
        public static async ValueTask<string> ReadAllTextAsync([NotNull] string filePath, CancellationToken token)
        {
            using (var bytes = await ReadAllBytesAsync(filePath, token))
            {
                return BuiltIns.UTF8.GetString(bytes.Array, 0, bytes.Length);
            }
        }
    }
}