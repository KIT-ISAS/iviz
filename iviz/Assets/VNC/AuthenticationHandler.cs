#nullable enable

#if false

using System;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Security;

namespace VNC
{
    public sealed class AuthenticationHandler : IAuthenticationHandler
    {
        readonly VncController controller;
        
        public AuthenticationHandler(VncController controller)
        {
            this.controller = controller;
        }
        
        /// <inheritdoc />
        public async Task<TInput> ProvideAuthenticationInputAsync<TInput>(RfbConnection connection,
            ISecurityType securityType, IAuthenticationInputRequest<TInput> request)
            where TInput : class, IAuthenticationInput
        {
            if (request is not IAuthenticationInputRequest<PasswordAuthenticationInput>)
            {
                throw new InvalidOperationException("The authentication input request is not supported " +
                                                    "by this authentication handler.");
            }

            string password = await controller.RequestPasswordAsync();
            if (new PasswordAuthenticationInput(password) is not TInput input)
            {
                // should not happen!
                throw new InvalidOperationException("Internal error: Authentication type does not match");
            }

            return input;

        }
    }
}
#endif
