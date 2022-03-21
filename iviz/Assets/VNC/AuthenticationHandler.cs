using System;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Security;

namespace VNC
{
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