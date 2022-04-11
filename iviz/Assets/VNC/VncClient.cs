#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using Nito.AsyncEx;

namespace VNC
{
    public sealed class VncClient
    {
        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<EventMessage> messages = new();
        readonly CancellationTokenSource tokenSource;

        public event Action<ConnectionState>? ConnectionStateChanged;

        public VncClient(CancellationToken token)
        {
            tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        }

        public Task StartAsync(VncController controller)
        {
            var startSignal = TaskUtils.CreateCompletionSource();

            TaskUtils.Run(async () =>
            {
                try
                {
                    await DoStartAsync(controller, startSignal);
                }
                catch (Exception e)
                {
                    startSignal.TrySetException(e);
                }
            });

            return startSignal.Task;
        }

        async Task DoStartAsync(VncController controller, TaskCompletionSource startSignal)
        {
            var (hostname, port) = await GameThread.PostAsync(controller.RequestServerAsync);

            var vncClient = new MarcusW.VncClient.VncClient(NullLoggerFactory.Instance);

            using var renderTarget = new RenderTarget(controller.Screen);

            // Configure the connect parameters
            var parameters = new ConnectParameters
            {
                TransportParameters = new TcpTransportParameters
                {
                    Host = hostname,
                    Port = port,
                },
                AuthenticationHandler = new AuthenticationHandler(controller),
                InitialRenderTarget = renderTarget,
                ConnectTimeout = TimeSpan.FromSeconds(3)
            };

            var token = tokenSource.Token;

            // Start a new connection and save the returned connection object
            using var rfbConnection = await vncClient.ConnectAsync(parameters, token);
            rfbConnection.PropertyChanged += (obj, args) =>
            {
                var connection = (RfbConnection)obj;
                switch (args.PropertyName)
                {
                    case nameof(connection.ConnectionState):
                        var connectionState = connection.ConnectionState;
                        GameThread.Post(() => ConnectionStateChanged?.Invoke(connectionState));
                        break;
                }
            };

            startSignal.TrySetResult();

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await signal.WaitAsync(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                while (messages.TryDequeue(out var message))
                {
                    switch (message.type)
                    {
                        case EventMessage.Type.Pointer:
                            rfbConnection.EnqueueMessage(message.pointerEventMessage!);
                            break;
                        case EventMessage.Type.Key:
                            rfbConnection.EnqueueMessage(message.keyEventMessage!);
                            break;
                    }
                }
            }

            await rfbConnection.CloseAsync();
        }

        void Enqueue(in EventMessage msg)
        {
            if (tokenSource.IsCancellationRequested)
            {
                return;
            }

            messages.Enqueue(msg);
            signal.Release();
        }

        public void Enqueue(KeyEventMessage msg) => Enqueue(new EventMessage(msg));

        public void Enqueue(PointerEventMessage msg) => Enqueue(new EventMessage(msg));

        public void Dispose()
        {
            messages.Clear();
            tokenSource.Cancel();
        }

        readonly struct EventMessage
        {
            public enum Type
            {
                Pointer,
                Key
            }

            public readonly Type type;
            public readonly PointerEventMessage? pointerEventMessage;
            public readonly KeyEventMessage? keyEventMessage;

            public EventMessage(PointerEventMessage msg) : this() => (type, pointerEventMessage) = (Type.Pointer, msg);
            public EventMessage(KeyEventMessage msg) : this() => (type, keyEventMessage) = (Type.Key, msg);
        }

        sealed class RenderTarget : IRenderTarget, IDisposable
        {
            readonly VncScreen screen;
            DeferredFrameBuffer? cachedFrameBuffer;

            public RenderTarget(VncScreen screen)
            {
                this.screen = screen;
            }

            public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
            {
                if (cachedFrameBuffer != null && cachedFrameBuffer.Size == size)
                {
                    return cachedFrameBuffer;
                }

                cachedFrameBuffer?.DisposeAllFrames();
                cachedFrameBuffer = new DeferredFrameBuffer(screen, size);
                return cachedFrameBuffer;
            }

            public void Dispose()
            {
                cachedFrameBuffer?.DisposeAllFrames();
                cachedFrameBuffer = null;
            }
        }
    }
}