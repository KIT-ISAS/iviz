using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.XmlRpc;

/// <summary>
/// A very simple HTTP listener for XML-RPC calls
/// </summary>
public sealed class HttpListener
{
    const int AnyPort = 0;
    const int DefaultHttpPort = 80;
    const int DefaultTimeoutInMs = 2000;
    const int BackgroundTimeoutInMs = 5000;

    readonly List<(DateTime start, Task task)> backgroundTasks = new();
    readonly TcpListener listener;

    bool started;
    bool disposed;

    readonly CancellationTokenSource runningTs = new();

    /// <summary>
    /// The port on which the listener is listening
    /// </summary>
    public int LocalPort { get; }

    /// <summary>
    /// Creates a new HTTP listener that listens on the given port.
    /// </summary>
    /// <param name="requestedPort">The port to listen on. Ports 0 and 80 are assumed to be 'any'.</param>
    public HttpListener(int requestedPort = AnyPort)
    {
        int port = requestedPort == DefaultHttpPort ? AnyPort : requestedPort;
        listener = new TcpListener(IPAddress.IPv6Any, port) { Server = { DualMode = true } };
        listener.Start();

        IPEndPoint endpoint = (IPEndPoint)listener.LocalEndpoint;
        LocalPort = endpoint.Port;
    }

    /// <summary>
    /// Starts listening.
    /// </summary>
    /// <param name="handler">
    /// Function to call when a request arrives.
    /// </param>
    /// <param name="runInBackground">
    /// If true, multiple requests can run at the same time.
    /// If false, the listener will wait for each request before accepting the next one.
    /// </param>
    /// <returns>An awaitable task.</returns>
    /// <exception cref="ArgumentNullException">Thrown if handler is null</exception>
    public async ValueTask StartAsync(Func<HttpListenerContext,
            CancellationToken, ValueTask> handler,
        bool runInBackground)
    {
        if (handler is null) BaseUtils.ThrowArgumentNull(nameof(handler));

        var token = runningTs.Token;

        started = true;
        while (!token.IsCancellationRequested)
            try
            {
                var client = await listener.AcceptTcpClientAsync();
                client.NoDelay = true;

                if (token.IsCancellationRequested)
                {
                    client.Dispose();
                    break;
                }

                async Task CreateContextTask()
                {
                    using var context = new HttpListenerContext(client);
                    await handler(context, token);
                }

                if (runInBackground)
                {
                    AddToBackgroundTasks(TaskUtils.Run(CreateContextTask, token));
                }
                else
                {
                    await CreateContextTask().AwaitNoThrow(2000, this, default);
                }
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException or OperationCanceledException)
                {
                    break;
                }

                Logger.LogFormat("{0}: Leaving thread {1}", this, e);
                return;
            }

        Logger.LogDebugFormat("{0}: Leaving task", this);
    }


    void AddToBackgroundTasks(Task task)
    {
        backgroundTasks.RemoveAll(tuple => tuple.task.IsCompleted);
        backgroundTasks.Add((DateTime.Now, task));
    }

    /// <summary>
    /// If <see cref="StartAsync" /> was called with runInBackground,
    /// this waits for the handlers in the background to finish.
    /// </summary>
    /// <param name="timeoutInMs">Maximal time to wait</param>
    /// <returns>An awaitable task</returns>
    public Task AwaitRunningTasks(int timeoutInMs = DefaultTimeoutInMs)
    {
        backgroundTasks.RemoveAll(tuple => tuple.task.IsCompleted);
        if (backgroundTasks.Count == 0)
        {
            return Task.CompletedTask;
        }

        var now = DateTime.Now;
        int count = backgroundTasks.Count(tuple => (tuple.start - now).TotalMilliseconds > BackgroundTimeoutInMs);
        if (count > 0)
        {
            Logger.LogFormat("{0}: There appear to be {1} task(s) deadlocked!", this, count);
        }

        return backgroundTasks.Select(tuple => tuple.task).WhenAll().AwaitNoThrow(timeoutInMs, this);
    }


    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
        disposed = true;

        runningTs.Cancel();

        if (!started)
        {
            listener.Stop();
            return;
        }

        Logger.LogDebugFormat("{0}: Disposing listener...", this);

        // note: listener.Stop() does not guarantee that AcceptTcpClientAsync() will return in il2cpp!
        // so we enqueue a connection to make it return
        await StreamUtils.EnqueueLocalConnectionAsync(LocalPort, this, timeoutInMs: 2000);

        // now we stop it!
        listener.Stop();
        Logger.LogDebugFormat("{0}: Listener dispose out", this);
    }

    public override string ToString()
    {
        return $"[{nameof(HttpListener)} :{LocalPort.ToString()}]";
    }
}