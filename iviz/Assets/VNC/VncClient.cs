#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace VNC
{
    public sealed class VncClient
    {
        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<EventMessage> messages = new();
        readonly CancellationTokenSource tokenSource = new();

        public event Action<ConnectionState>? ConnectionStateChanged;

        public Task StartAsync(VncScreen screen)
        {
            var startSignal = new TaskCompletionSource();

            TaskUtils.Run(async () =>
            {
                try
                {
                    await DoStartAsync(screen, startSignal);
                }
                catch (Exception e)
                {
                    startSignal.TrySetException(e);
                }
            });

            return startSignal.Task;
        }

        async Task DoStartAsync(VncScreen screen, TaskCompletionSource startSignal)
        {
            var vncClient = new MarcusW.VncClient.VncClient(NullLoggerFactory.Instance);

            //var renderTarget = new RenderTarget(onFrameArrived);
            var renderTarget = new DeferredRenderTarget(screen);

            // Configure the connect parameters
            var parameters = new ConnectParameters
            {
                TransportParameters = new TcpTransportParameters
                {
                    //Host = "192.168.0.17",
                    //Port = 5903
                    Host = "141.3.59.5",
                    Port = 5902
                },
                AuthenticationHandler = new AuthenticationHandler(),
                InitialRenderTarget = renderTarget,
                //JpegQualityLevel = 90,
                //JpegSubsamplingLevel = JpegSubsamplingLevel.ChrominanceSubsampling16X,
            };

            var token = tokenSource.Token;

            // Start a new connection and save the returned connection object
            using var rfbConnection = await vncClient.ConnectAsync(parameters, token);
            rfbConnection.PropertyChanged += (obj, args) =>
            {
                var connection = (RfbConnection)obj;
                Debug.Log(args.PropertyName);
                if (args.PropertyName == nameof(connection.ConnectionState))
                {
                    GameThread.Post(() => ConnectionStateChanged?.Invoke(connection.ConnectionState));
                }
            };

            startSignal.TrySetResult();

            while (!token.IsCancellationRequested)
            {
                await signal.WaitAsync(token);

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
            messages.Enqueue(msg);
            signal.Release();
        }

        public void Enqueue(KeyEventMessage msg) => Enqueue(new EventMessage(msg));

        public void Enqueue(PointerEventMessage msg) => Enqueue(new EventMessage(msg));

        public void Dispose()
        {
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

        sealed class DeferredRenderTarget : IRenderTarget
        {
            readonly VncScreen screen;
            DeferredFrameBuffer? cachedFrameBuffer;

            public DeferredRenderTarget(VncScreen screen)
            {
                this.screen = screen;
            }

            public IFramebufferReference GrabFramebufferReference(Size size,
                IImmutableSet<MarcusW.VncClient.Screen> layout)
            {
                if (cachedFrameBuffer != null && cachedFrameBuffer.Size == size)
                {
                    return cachedFrameBuffer;
                }

                cachedFrameBuffer = new DeferredFrameBuffer(screen, size);
                return cachedFrameBuffer;
            }
        }
    }
}