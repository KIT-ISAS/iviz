using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools;

public static class StreamUtils
{
    public static ValueTask<bool> ReadChunkAsync(this TcpClient client, Rent buffer, CancellationToken token)
    {
        return ReadChunkAsync(client, buffer.Array, buffer.Length, token);
    }

    public static ValueTask<bool> ReadChunkAsync(this TcpClient client, byte[] buffer, int toRead,
        CancellationToken token)
    {
        var socket = client.Client;
        if (socket.Available < toRead)
        {
            return DoReadChunkAsync(socket, buffer, toRead, token);
        }

        int received = socket.Receive(buffer, 0, toRead, SocketFlags.None);
        return (received != 0).AsTaskResult();
    }

    static async ValueTask<bool> DoReadChunkAsync(Socket socket, byte[] buffer, int toRead, CancellationToken token)
    {
        int numRead = 0;
        while (numRead < toRead)
        {
            int readNow = await ReadSubChunkAsync(socket, buffer, numRead, toRead - numRead, token);
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
            using var buffer = new Rent(toRead);
            return await ReadChunkAsync(client, buffer.Array, toRead, token);
        }
        else
        {
            int remaining = toRead;
            using var buffer = new Rent(toRead);
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
    
    public static ValueTask<int> ReadChunkAsync(this UdpClient udpClient, Rent buffer, CancellationToken token)
    {
        return ReadSubChunkAsync(udpClient.Client, buffer.Array, 0, buffer.Length, token);
    }

    static ValueTask<int> ReadSubChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
        CancellationToken token)
    {
        if (socket.Available == 0)
        {
            return DoReadSubChunkAsync(socket, buffer, offset, toRead, token);
        }

        int received = socket.Receive(buffer, offset, toRead, SocketFlags.None);
        return received.AsTaskResult();
    }

    static ValueTask<int> DoReadSubChunkAsync(Socket socket, byte[] buffer, int offset, int toRead,
        CancellationToken token)
    {
        var tcs = TaskUtils.CreateCompletionSource<IAsyncResult>();

        socket.BeginReceive(buffer, offset, toRead, SocketFlags.None, CallbackHelpers.OnComplete, tcs);

        return tcs.Task.IsCompleted
            ? socket.EndReceive(tcs.Task.Result).AsTaskResult()
            : DoReadSubChunkWithTokenAsync(tcs, socket, token);
    }

    static async ValueTask<int> DoReadSubChunkWithTokenAsync(TaskCompletionSource<IAsyncResult> tcs, Socket socket,
        CancellationToken token)
    {
        // ReSharper disable once UseAwaitUsing
        using (token.Register(CallbackHelpers.OnCanceled, tcs))
        {
            return socket.EndReceive(await tcs.Task);
        }
    }

    public static ValueTask WriteChunkAsync(this TcpClient client, Rent buffer, CancellationToken token) =>
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

    public static ValueTask<int> WriteChunkAsync(this UdpClient udpClient, Rent buffer, int toWrite,
        CancellationToken token) => DoWriteChunkAsync(udpClient.Client, buffer.Array, 0, toWrite, token);

    static ValueTask<int> DoWriteChunkAsync(Socket socket, byte[] buffer, int offset, int toWrite,
        CancellationToken token)
    {
        var tcs = TaskUtils.CreateCompletionSource<IAsyncResult>();

        socket.BeginSend(buffer, offset, toWrite, SocketFlags.None, CallbackHelpers.OnComplete, tcs);

        return tcs.Task.IsCompleted
            ? socket.EndSend(tcs.Task.Result).AsTaskResult()
            : DoWriteWithTokenAsync(tcs, socket, token);
    }

    static async ValueTask<int> DoWriteWithTokenAsync(TaskCompletionSource<IAsyncResult> tcs, Socket socket,
        CancellationToken token)
    {
        // ReSharper disable once UseAwaitUsing
        using (token.Register(CallbackHelpers.OnCanceled, tcs))
        {
            return socket.EndSend(await tcs.Task);
        }
    }

    public static async ValueTask WriteChunkAsync(this TcpClient client, Rent bytes, CancellationToken token,
        int timeoutInMs)
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

    public static async ValueTask WriteHeaderAsync(this TcpClient client, string[] contents, CancellationToken token)
    {
        int totalLength = 4 * contents.Length + contents.Sum(entry => Defaults.UTF8.GetByteCount(entry));

        using var array = new Rent(totalLength + 4);

        // ReSharper disable once UseAwaitUsing
        using var writer = new BinaryWriter(new MemoryStream(array.Array));

        writer.Write(totalLength);
        WriteHeaderEntries(writer, contents);
        await client.WriteChunkAsync(array, token);
    }

    public static byte[] WriteHeaderToArray(ReadOnlySpan<string> contents)
    {
        int totalLength = 4 * contents.Length + contents.Sum(entry => Defaults.UTF8.GetByteCount(entry));

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
            writer.Write(bytes);
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

    /// <summary>
    /// Enqueues a connection to the given port.
    /// Used in Dispose() functions to force a listener to get out of waiting, as simply
    /// closing it doesn't work in Mono. 
    /// </summary>
    public static void EnqueueConnection(int port, object caller)
    {
        try
        {
            using var client = new TcpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            client.Connect(IPAddress.Loopback, port);
        }
        catch
        {
            Logger.LogDebugFormat("{0}: Listener threw while disposing", caller);
        }
    }
    
    /// <summary>
    /// Enqueues a connection to the given port.
    /// Used in DisposeAsync() functions to force a listener to get out of waiting, as simply
    /// closing it doesn't work in Mono. 
    /// </summary>
    public static async Task EnqueueConnectionAsync(int port, object caller, CancellationToken token = default, int timeoutInMs = -1)
    {
        try
        {
            using var client = new TcpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            await client.TryConnectAsync("127.0.0.1", port, token, timeoutInMs);
            //await client.ConnectAsync(IPAddress.Loopback, port);
        }
        catch (OperationCanceledException)
        {
            Logger.LogDebugFormat("{0}: Listener timed out while disposing", caller);
        }
        catch
        {
            Logger.LogDebugFormat("{0}: Listener threw while disposing", caller);
        }
    }
}

public static class CallbackHelpers
{
    static AsyncCallback? onComplete;
    static Action<object?>? onCanceled;
    static Action<object?>? onTimeout;
    static Action<object?>? setResult;

    public static AsyncCallback OnComplete => onComplete ??=
        result => ((TaskCompletionSource<IAsyncResult>)result.AsyncState!).TrySetResult(result);

    public static Action<object?> OnCanceled => onCanceled ??=
        tcs => ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetCanceled();

    public static Action<object?> OnTimeout => onTimeout ??=
        tcs => ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetException(new TimeoutException());

    public static Action<object?> SetResult => setResult ??=
        o => ((TaskCompletionSource)o!).TrySetResult();
}