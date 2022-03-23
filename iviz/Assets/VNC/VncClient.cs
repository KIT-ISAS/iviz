#nullable enable

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Rendering;
using Microsoft.Extensions.Logging.Abstractions;

namespace VNC
{
    public sealed class VncClient
    {
        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<EventMessage> messages = new();
        readonly CancellationTokenSource tokenSource = new();
        
        public async Task StartAsync(Action<IFramebufferReference> onFrameArrived)
        {
            var vncClient = new MarcusW.VncClient.VncClient(NullLoggerFactory.Instance);

            var renderTarget = new RenderTarget(onFrameArrived);

            // Configure the connect parameters
            var parameters = new ConnectParameters
            {
                TransportParameters = new TcpTransportParameters
                {
                    //Host = "192.168.0.17",
                    //Port = 5901
                    Host = "141.3.59.5",
                    Port = 5902
                },
                AuthenticationHandler = new AuthenticationHandler(),
                InitialRenderTarget = renderTarget,
                JpegQualityLevel = 90,
                JpegSubsamplingLevel = JpegSubsamplingLevel.ChrominanceSubsampling16X,
            };

            var token = tokenSource.Token;
            
            // Start a new connection and save the returned connection object
            using var rfbConnection = await vncClient.ConnectAsync(parameters, token);

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
    }
}