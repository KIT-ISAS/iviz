#nullable enable

using System;
using System.Threading.Tasks;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Security;
using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;

namespace VNC
{
    public class VncExample : MonoBehaviour
    {
        void Start()
        {
            TaskUtils.Run(StartAsync);
        }

        async Task StartAsync()
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
            using var rfbConnection = await vncClient.ConnectAsync(parameters);
            rfbConnection.RenderTarget = renderTarget;
            await Task.Delay(10000);
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