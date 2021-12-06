using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
            if (socket.Available < toRead)
            {
                return DoReadChunkAsync(socket, buffer, toRead, token);
            }

            int received = socket.Receive(buffer, 0, toRead, SocketFlags.None);
            return ValueTask2.FromResult(received == toRead);
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

        static readonly AsyncCallback OnComplete =
            result => ((TaskCompletionSource<IAsyncResult>)result.AsyncState!).TrySetResult(result);

        public static readonly Action<object?> OnCanceled = tcs =>
            ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetCanceled();

        static ValueTask<int> ReadChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
            CancellationToken token)
        {
            if (socket.Available == 0)
            {
                return DoReadChunkAsync(socket, buffer, offset, toRead, token);
            }

            int received = socket.Receive(buffer, offset, toRead, SocketFlags.None);
            return ValueTask2.FromResult(received);
        }

        static async ValueTask<int> DoReadChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
            CancellationToken token)
        {
            var tcs = new TaskCompletionSource<IAsyncResult>();

            socket.BeginReceive(buffer, offset, toRead, SocketFlags.None, OnComplete, tcs);

            if (tcs.Task.IsCompleted)
            {
                return socket.EndReceive(await tcs.Task);
            }

            await using (token.Register(OnCanceled, tcs))
            {
                return socket.EndReceive(await tcs.Task);
            }
        }

        public static ValueTask WriteChunkAsync(this TcpClient client, Rent<byte> buffer, CancellationToken token) =>
            WriteChunkAsync(client, buffer.Array, buffer.Length, token);

        public static async ValueTask WriteChunkAsync(this TcpClient client, byte[] buffer, int toWrite,
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

        public static ValueTask<int> WriteChunkAsync(this UdpClient udpClient, byte[] buffer, int offset, int toWrite,
            CancellationToken token) => DoWriteChunkAsync(udpClient.Client, buffer, offset, toWrite, token);

        static async ValueTask<int> DoWriteChunkAsync(Socket socket, byte[] buffer, int offset, int toWrite,
            CancellationToken token)
        {
            var tcs = new TaskCompletionSource<IAsyncResult>();

            socket.BeginSend(buffer, offset, toWrite, SocketFlags.None, OnComplete, tcs);

            if (tcs.Task.IsCompleted)
            {
                return socket.EndSend(await tcs.Task);
            }

            await using (token.Register(OnCanceled, tcs))
            {
                return socket.EndSend(await tcs.Task);
            }
        }

        public static async ValueTask WriteChunkAsync(this TcpClient client, Rent<byte> bytes, CancellationToken token, int timeoutInMs)
        {
            if (timeoutInMs == -1)
            {
                await WriteChunkAsync(client, bytes, token);
                return;
            }

            try
            {
                using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                linkedTokenSource.CancelAfter(timeoutInMs);
                await WriteChunkAsync(client, bytes, linkedTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                if (token.IsCancellationRequested)
                {
                    throw;
                }

                throw new TimeoutException("Writing operation timed out");
            }
        }

        public static async ValueTask WriteHeaderAsync(this TcpClient client, string[] contents,
            CancellationToken token)
        {
            int totalLength = 4 * contents.Length + contents.Sum(entry => Defaults.UTF8.GetByteCount(entry));

            using var array = new Rent<byte>(totalLength + 4);
            await using var writer = new BinaryWriter(new MemoryStream(array.Array));

            writer.Write(totalLength);
            WriteHeaderEntries(writer, contents);
            await client.WriteChunkAsync(array, token);
        }

        public static byte[] WriteHeaderToArray(ReadOnlySpan<string> contents)
        {
            int totalLength = 4 * contents.Length;
            foreach (string entry in contents)
            {
                totalLength += Defaults.UTF8.GetByteCount(entry);
            }

            byte[] array = new byte[totalLength];
            using var writer = new BinaryWriter(new MemoryStream(array));
            WriteHeaderEntries(writer, contents);
            return array;
        }

        static void WriteHeaderEntries(BinaryWriter writer, ReadOnlySpan<string> contents)
        {
            foreach (string entry in contents)
            {
                using var bytes = entry.AsRent();
                writer.Write(bytes.Length);
                writer.Write(bytes.Array, 0, bytes.Length);
            }
        }

        public static string CheckMessage(this Exception e)
        {
            if (e is not SocketException se)
            {
                return e.Message;
            } 
            
            // fix mono bug!
            if (!se.Message.HasPrefix("mono-io-layer-error"))
            {
                return CheckSocketExceptionMessage(e.Message);
            }

            int fixedErrorCode = (se.ErrorCode is <= (int)SocketError.Success or >= (int)SocketError.OperationAborted)
                ? se.ErrorCode
                : se.ErrorCode + 10000;

            // we only need the text, but the only way to get it is to create an exception
            return CheckSocketExceptionMessage(new SocketException(fixedErrorCode).Message);
        }

        static string CheckSocketExceptionMessage(string message)
        {
            // in windows and hololens the socket error may be padded with \0s after a \r
            int terminatorIndex = message.IndexOf('\r'); 
            return terminatorIndex != -1 ? message[..terminatorIndex] : message;
        }
    }
}