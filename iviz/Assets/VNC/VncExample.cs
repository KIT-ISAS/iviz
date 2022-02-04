#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Rendering;
using MarcusW.VncClient.Security;
using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;

namespace VNC
{
    public class VncExample : MonoBehaviour
    {
        [SerializeField] Material? material;
        [SerializeField] MeshRenderer? mainRenderer;

        readonly TaskCompletionSource<object?> task = new();
        Texture2D? texture;

        public int Width => texture != null ? texture.width : 0;
        public int Height => texture != null ? texture.height : 0;

        void Start()
        {
            material = Instantiate(material);
            mainRenderer!.sharedMaterial = material;

            TaskUtils.Run(StartAsync);
        }

        async Task StartAsync()
        {
            try
            {
                var vncClient = new VncClient(NullLoggerFactory.Instance);

                // Configure the connect parameters
                var parameters = new ConnectParameters
                {
                    TransportParameters = new TcpTransportParameters
                    {
                        Host = "192.168.0.220",
                        Port = 5900
                    },
                    AuthenticationHandler = new AuthenticationHandler()
                    // There are many more parameters to explore...
                };

                // Start a new connection and save the returned connection object
                using var renderTarget = new RenderTarget();
                renderTarget.FrameArrived += OnFrameArrived;
                using var rfbConnection = await vncClient.ConnectAsync(parameters);
                rfbConnection.RenderTarget = renderTarget;

                await task.Task;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        void OnFrameArrived(IFramebufferReference frame)
        {
            if (material == null)
            {
                Debug.Log($"{this}: Display has not been set!");
                return;
            }

            try
            {
                if (texture != null && (texture.width, texture.height) != (frame.Size.Width, frame.Size.Height))
                {
                    Destroy(texture);
                    texture = null;
                }

                if (texture == null)
                {
                    texture = new Texture2D(frame.Size.Width, frame.Size.Height, TextureFormat.RGBA32, false);
                    material.mainTexture = texture;
                }

                ReadOnlySpan<byte> span;
                unsafe
                {
                    span = new ReadOnlySpan<byte>(frame.Address.ToPointer(), frame.Size.Width * frame.Size.Height * 4);
                }

                
                texture.GetRawTextureData<byte>().CopyFrom(span);
                texture.Apply();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception in {nameof(OnFrameArrived)}", e);
            }
        }

        void OnDestroy()
        {
            task.TrySetResult(null);
        }
    }

    public class AuthenticationHandler : IAuthenticationHandler
    {
        /// <inheritdoc />
        public Task<TInput> ProvideAuthenticationInputAsync<TInput>(RfbConnection connection,
            ISecurityType securityType, IAuthenticationInputRequest<TInput> request)
            where TInput : class, IAuthenticationInput
        {
            var exception = new InvalidOperationException(
                "The authentication input request is not supported by this authentication handler.");
            return Task.FromException<TInput>(exception);
        }
    }
}