using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.XmlRpc
{
    public static class StreamUtils
    {
        public static async ValueTask<bool> ReadChunkAsync(this NetworkStream stream, byte[] buffer, int toRead,
            CancellationToken token)
        {
            int numRead = 0;
            while (numRead < toRead)
            {
#if !NETSTANDARD2_0
                int readNow = await stream.ReadAsync(buffer.AsMemory(numRead, toRead - numRead), token);
#else
                int readNow;
                if (stream.DataAvailable)
                {
                    token.ThrowIfCancellationRequested();
                    readNow = stream.Read(buffer, numRead, toRead - numRead);
                }
                else
                {
                    Task<int> readTask = stream.ReadAsync(buffer, numRead, toRead - numRead, token);
                    var tokenTaskSource = new TaskCompletionSource<object?>();
                    Action whenComplete = () => tokenTaskSource.TrySetResult(null);

                    using var registration = token.Register(whenComplete);
                    readTask.GetAwaiter().OnCompleted(whenComplete);
                    
                    await tokenTaskSource.Task;
                    token.ThrowIfCancellationRequested();
                    readNow = await readTask;
                }

#endif
                if (readNow == 0)
                {
                    return false;
                }

                numRead += readNow;
            }

            return true;
        }

        public static async ValueTask<bool> ReadAndIgnoreAsync(this NetworkStream stream, int toRead,
            CancellationToken token)
        {
            const int bufferSize = 4096;
            if (toRead <= bufferSize)
            {
                using var buffer = new Rent<byte>(toRead);
                return await ReadChunkAsync(stream, buffer.Array, toRead, token);
            }
            else
            {
                int remaining = toRead;
                using var buffer = new Rent<byte>(toRead);
                while (remaining > bufferSize)
                {
                    if (!await ReadChunkAsync(stream, buffer.Array, bufferSize, token))
                    {
                        return false;
                    }

                    remaining -= bufferSize;
                }

                return await ReadChunkAsync(stream, buffer.Array, remaining, token);
            }
        }

        public static async Task WriteChunkAsync(this NetworkStream stream, byte[] buffer, int count,
            CancellationToken token)
        {
#if !NETSTANDARD2_0
            await stream.WriteAsync(buffer.AsMemory(0, count), token);
#else
            var tokenTaskSource = new TaskCompletionSource<object?>();
            var tokenTask = tokenTaskSource.Task;
            using var registration = token.Register(() => tokenTaskSource.TrySetResult(null));

            Task writeTask = stream.WriteAsync(buffer, 0, count, token);
            Task resultTask = await (writeTask, tokenTask).WhenAny();
            if (resultTask == tokenTask)
            {
                token.ThrowIfCancellationRequested();
                throw new TimeoutException("Writing operation timed out");
            }

            await writeTask;
#endif
        }

        public static async Task WriteHeaderAsync(this NetworkStream stream, string[] contents, CancellationToken token)
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

            await stream.WriteChunkAsync(array.Array, array.Length, token);
        }    
        
        public static async Task WriteChunkAsync(this StreamWriter writer, string text, CancellationToken token,
            int timeoutInMs = -1)
        {

#if !NETSTANDARD2_0
            using CancellationTokenSource tokenSource = !token.CanBeCanceled
                ? new CancellationTokenSource()
                : CancellationTokenSource.CreateLinkedTokenSource(token);
            if (timeoutInMs != -1)
            {
                tokenSource.CancelAfter(timeoutInMs);
            }

            await writer.WriteAsync(text.AsMemory(), tokenSource.Token);
#else
            if (timeoutInMs == -1)
            {
                await writer.WriteAsync(text).AwaitWithToken(token);
            }
            else
            {
                Task connectionTask = writer.WriteAsync(text);
                if (!await connectionTask.AwaitFor(timeoutInMs, token))
                {
                    token.ThrowIfCancellationRequested();
                    throw new TimeoutException("Writing operation timed out");
                }
            }
#endif
        }
    }
}