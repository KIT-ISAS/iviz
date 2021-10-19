using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools
{
    public static class StreamUtils
    {
        public static ValueTask<bool> ReadChunkAsync(this TcpClient client, byte[] buffer, int toRead,
            CancellationToken token)
        {
            var socket = client.Client;
            if (socket.Available >= toRead)
            {
                int received = socket.Receive(buffer, 0, toRead, SocketFlags.None);
                return new ValueTask<bool>(received == toRead);
            }

            return DoReadChunkAsync(socket, buffer, toRead, token);
        }

        static async ValueTask<bool> DoReadChunkAsync(Socket socket, byte[] buffer, int toRead, CancellationToken token)
        {
            int numRead = 0;
            while (numRead < toRead)
            {
                int readNow = await ReadChunkAsync(socket, buffer, numRead, toRead - numRead, token);
                if (readNow == 0)
                {
                    return false;
                }

                numRead += readNow;
            }

            return true;
        }

        public static async ValueTask<bool> ReadAndIgnoreAsync(this TcpClient client, int toRead,
            CancellationToken token)
        {
            const int bufferSize = 4096;
            if (toRead <= bufferSize)
            {
                using var buffer = new Rent<byte>(toRead);
                return await ReadChunkAsync(client, buffer.Array, toRead, token);
            }
            else
            {
                int remaining = toRead;
                using var buffer = new Rent<byte>(toRead);
                while (remaining > bufferSize)
                {
                    if (!await ReadChunkAsync(client, buffer.Array, bufferSize, token))
                    {
                        return false;
                    }

                    remaining -= bufferSize;
                }

                return await ReadChunkAsync(client, buffer.Array, remaining, token);
            }
        }


        public static ValueTask<int> ReadChunkAsync(this UdpClient udpClient, byte[] buffer, CancellationToken token)
        {
            return ReadChunkAsync(udpClient.Client, buffer, 0, buffer.Length, token);
        }

        static readonly AsyncCallback ReceiveCallbackDel =
            result => ((TaskCompletionSource<IAsyncResult>)result.AsyncState!).TrySetResult(result);

        static readonly Action<object?> WhenComplete = tcs =>
            ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetCanceled();

        static ValueTask<int> ReadChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
            CancellationToken token)
        {
            if (socket.Available != 0)
            {
                int received = socket.Receive(buffer, offset, toRead, SocketFlags.None);
                return new ValueTask<int>(received);
            }

            return DoReadChunkAsync(socket, buffer, offset, toRead, token);
        }

        static async ValueTask<int> DoReadChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
            CancellationToken token)
        {
            var tcs = new TaskCompletionSource<IAsyncResult>();

            socket.BeginReceive(buffer, offset, toRead, SocketFlags.None, ReceiveCallbackDel, tcs);

            if (!tcs.Task.IsCompleted)
            {
                using var registration = token.Register(WhenComplete, tcs);
                var result = await tcs.Task;
                return socket.EndReceive(result);
            }
            else
            {
                var result = await tcs.Task;
                return socket.EndReceive(result);
            }
        }

        public static async Task WriteChunkAsync(this TcpClient client, byte[] buffer, int toWrite,
            CancellationToken token)
        {
            var socket = client.Client;
            int numWritten = 0;
            while (numWritten < toWrite)
            {
                int writeNow = await DoWriteChunkAsync(socket, buffer, numWritten, toWrite - numWritten, token);
                if (writeNow == 0)
                {
                    return;
                }

                numWritten += writeNow;
            }
        }

        public static Task WriteChunkAsync(this UdpClient udpClient, byte[] buffer, int offset, int toWrite,
            CancellationToken token)
        {
            return DoWriteChunkAsync(udpClient.Client, buffer, offset, toWrite, token).AsTask();
        }

        static async ValueTask<int> DoWriteChunkAsync(Socket socket, byte[] buffer, int offset, int toWrite,
            CancellationToken token)
        {
            var tcs = new TaskCompletionSource<IAsyncResult>();

            socket.BeginSend(buffer, offset, toWrite, SocketFlags.None, ReceiveCallbackDel, tcs);

            if (!tcs.Task.IsCompleted)
            {
                using var registration = token.Register(WhenComplete, tcs);
                var result = await tcs.Task;
                return socket.EndSend(result);
            }
            else
            {
                var result = await tcs.Task;
                return socket.EndReceive(result);
            }
        }

        public static async Task WriteHeaderAsync(this TcpClient client, string[] contents, CancellationToken token)
        {
            int totalLength = 4 * contents.Length + contents.Sum(entry => Defaults.UTF8.GetByteCount(entry));

            using var array = new Rent<byte>(totalLength + 4);
            using var writer = new BinaryWriter(new MemoryStream(array.Array));

            writer.Write(totalLength);
            WriteHeaderEntries(writer, contents);
            await client.WriteChunkAsync(array.Array, array.Length, token);
        }

        public static byte[] WriteHeaderToArray(string[] contents)
        {
            int totalLength = 4 * contents.Length + contents.Sum(entry => Defaults.UTF8.GetByteCount(entry));

            byte[] array = new byte[totalLength];
            using var writer = new BinaryWriter(new MemoryStream(array));
            WriteHeaderEntries(writer, contents);
            return array;
        }

        static void WriteHeaderEntries(BinaryWriter writer, string[] contents)
        {
            foreach (string entry in contents)
            {
                byte[] bytes = Defaults.UTF8.GetBytes(entry);
                writer.Write(bytes.Length);
                writer.Write(bytes);
            }
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