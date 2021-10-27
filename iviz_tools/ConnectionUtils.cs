using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools
{
    public static class ConnectionUtils
    {
        public static Dictionary<string, string> GlobalResolver { get; } = new(StringComparer.OrdinalIgnoreCase);

        static readonly AsyncCallback OnComplete =
            result => ((TaskCompletionSource<IAsyncResult>)result.AsyncState!).TrySetResult(result);

        static readonly Action<object?> OnCanceled = tcs =>
            ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetCanceled();

        static readonly Action<object?> OnTimeout = tcs =>
            ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetException(new TimeoutException());

        public static async Task TryConnectAsync(this TcpClient client, string hostname, int port,
            CancellationToken token, int timeoutInMs = -1)
        {
            token.ThrowIfCancellationRequested();

            string resolvedHostname = GlobalResolver.TryGetValue(hostname, out string? newHostname)
                ? newHostname
                : hostname;

            var tcs = new TaskCompletionSource<IAsyncResult>();
            var socket = client.Client;

            socket.BeginConnect(resolvedHostname, port, OnComplete, tcs);

            if (tcs.Task.IsCompleted)
            {
                socket.EndConnect(await tcs.Task);
                return;
            }

            if (timeoutInMs == -1)
            {
                using (token.Register(OnCanceled, tcs))
                {
                    socket.EndConnect(await tcs.Task);
                    return;
                }
            }

            using var timeoutTs = new CancellationTokenSource(timeoutInMs);
            using (token.Register(OnCanceled, tcs))
            using (timeoutTs.Token.Register(OnTimeout, tcs))
            {
                socket.EndConnect(await tcs.Task);
            }
        }

        public static void TryConnect(this UdpClient client, string hostname, int port)
        {
            string resolvedHostname = GlobalResolver.TryGetValue(hostname, out string? newHostname)
                ? newHostname
                : hostname;

            if (IPAddress.TryParse(resolvedHostname, out var address))
            {
                client.Connect(new IPEndPoint(address, port));
                return;
            }

            client.Connect(resolvedHostname, port);
        }

        public static bool CheckIfAlive(this Socket? socket) =>
            socket is { Connected: true } && !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
    }
}