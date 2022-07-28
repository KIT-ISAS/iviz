﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib;

internal sealed class ServiceListener
{
    readonly Func<IService, ValueTask> callback;
    readonly HashSet<ServiceRequest> requests = new();
    readonly TcpListener listener;
    readonly ServiceInfo serviceInfo;
    readonly Task task;

    readonly CancellationTokenSource tokenSource = new();
    bool KeepRunning => !tokenSource.IsCancellationRequested;

    bool disposed;

    public ServiceListener(ServiceInfo serviceInfo, string host, Func<IService, ValueTask> callback)
    {
        this.serviceInfo = serviceInfo;
        this.callback = callback;

        listener = new TcpListener(IPAddress.IPv6Any, 0) { Server = { DualMode = true } };
        listener.Start();

        var localEndpoint = (IPEndPoint)listener.LocalEndpoint;
        Uri = new Uri($"rosrpc://{host}:{localEndpoint.Port.ToString()}/");

        Logger.LogDebugFormat("{0}: Starting!", this);

        task = TaskUtils.Run(() => RunLoop().AwaitNoThrow(this));
    }

    public Uri Uri { get; }
    public string Service => serviceInfo.Service;
    public string ServiceType => serviceInfo.Type;


    async ValueTask RunLoop()
    {
        try
        {
            while (KeepRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                if (!KeepRunning)
                {
                    break;
                }

                IPEndPoint? endPoint;
                if ((endPoint = (IPEndPoint?)client.Client.RemoteEndPoint) == null)
                {
                    Logger.LogFormat("{0}: Received a request, but failed to initialize connection.", this);
                    continue;
                }

                var sender = new ServiceRequest(serviceInfo, client, new Endpoint(endPoint), callback);
                requests.Add(sender);

                await CleanupAsync(tokenSource.Token);
            }
        }
        catch (Exception e)
        {
            if (e is not (ObjectDisposedException or OperationCanceledException))
            {
                Logger.LogFormat("{0}: Stopped thread {1}", this, e);
            }

            return;
        }

        Logger.LogDebugFormat("{0}: Leaving task", this); // also expected
    }

    async ValueTask CleanupAsync(CancellationToken token)
    {
        var toRemove = requests.Where(request => !request.IsAlive).ToList();
        var tasks = toRemove.Select(async request =>
        {
            Logger.LogDebugFormat("{0}: Removing service connection with '{1}' - dead x_x",
                this, request.Hostname);
            await request.StopAsync(token);
            requests.Remove(request);
        }).ToArray();
        await tasks.WhenAll().AwaitNoThrow(this);
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        tokenSource.Cancel();

        // try to make the listener come out
        await StreamUtils.EnqueueConnectionAsync(Uri.Port, this, token);

        listener.Stop();
        if (!await task.AwaitFor(2000, token))
        {
            Logger.LogDebugFormat("{0}: Listener stuck. Abandoning.", this);
        }

        await requests.Select(request => request.StopAsync(token).AsTask()).WhenAll().AwaitNoThrow(this);
        requests.Clear();
    }

    public override string ToString()
    {
        return $"[{nameof(ServiceListener)} {Service} [{ServiceType}] at {Uri}]";
    }
}