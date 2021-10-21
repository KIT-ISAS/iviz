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

        static readonly AsyncCallback ConnectCallbackDel =
            result => ((TaskCompletionSource<IAsyncResult>)result.AsyncState!).TrySetResult(result);
        
        static readonly Action<object?> WhenComplete = tcs =>
            ((TaskCompletionSource<IAsyncResult>)tcs!).TrySetCanceled();

        public static async Task TryConnectAsync(this TcpClient client, string hostname, int port,
            CancellationToken token, int timeoutInMs = -1)
        {
            token.ThrowIfCancellationRequested();

            if (GlobalResolver.TryGetValue(hostname, out string? resolvedHostname))
            {
                hostname = resolvedHostname;
            }

            /*
#if NET5_0_OR_GREATER
            using var tokenSource = !token.CanBeCanceled
                ? new CancellationTokenSource()
                : CancellationTokenSource.CreateLinkedTokenSource(token);
            if (timeoutInMs != -1)
            {
                tokenSource.CancelAfter(timeoutInMs);
            }

            try
            {
                await client.ConnectAsync(hostname, port, tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                token.ThrowIfCancellationRequested();
                throw new TimeoutException($"Connection request to '{hostname}:{port}' timed out");
            }
#else
            Task connectionTask = IPAddress.TryParse(hostname, out var address)
                ? client.ConnectAsync(address, port)
                : client.ConnectAsync(hostname, port);

            if (timeoutInMs == -1)
            {
                if (!token.CanBeCanceled)
                {
                    throw new InvalidOperationException("Either a timeout or a cancellable token should be provided");
                }

                await connectionTask.AwaitWithToken(token);
            }
            else
            {
                if (!await connectionTask.AwaitFor(timeoutInMs, token))
                {
                    token.ThrowIfCancellationRequested();
                    throw new TimeoutException($"Connection request to '{hostname}:{port}' timed out");
                }
            }
#endif
*/
            
            var tcs = new TaskCompletionSource<IAsyncResult>();
            var socket = client.Client;

            socket.BeginConnect(hostname, port, ConnectCallbackDel, tcs);

            if (!tcs.Task.IsCompleted)
            {
                if (timeoutInMs != -1)
                {
                    using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                    linkedTokenSource.CancelAfter(timeoutInMs);
                    using var registration = linkedTokenSource.Token.Register(WhenComplete, tcs);
                    socket.EndConnect(await tcs.Task);
                }
                else
                {
                    using var registration = token.Register(WhenComplete, tcs);
                    socket.EndConnect(await tcs.Task);
                }
            }
            else
            {
                socket.EndReceive(await tcs.Task);
            }
            
        }

        public static void TryConnect(this UdpClient client, string hostname, int port)
        {
            if (GlobalResolver.TryGetValue(hostname, out string? resolvedHostname))
            {
                hostname = resolvedHostname;
            }

            if (IPAddress.TryParse(hostname, out var address))
            {
                client.Connect(new IPEndPoint(address, port));
                return;
            }

            client.Connect(hostname, port);
        }

        public static bool CheckIfAlive(this Socket? socket) =>
            socket != null && !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0) && socket.Connected;
    }
}