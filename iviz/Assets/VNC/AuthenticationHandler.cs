#nullable enable

using System;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Security;

namespace VNC
{
    public sealed class AuthenticationHandler : IAuthenticationHandler
    {
        /// <inheritdoc />
        public Task<TInput> ProvideAuthenticationInputAsync<TInput>(RfbConnection connection,
            ISecurityType securityType, IAuthenticationInputRequest<TInput> request)
            where TInput : class, IAuthenticationInput
        {
            if (typeof(TInput) == typeof(PasswordAuthenticationInput))
            {
                //string password = "abcdabcd"; // Retrieve the password somehow
                string password = "kinect360"; // Retrieve the password somehow

                var result = (TInput)(IAuthenticationInput) new PasswordAuthenticationInput(password);
                return Task.FromResult(result);
            }            
            
            var exception = new InvalidOperationException(
                "The authentication input request is not supported by this authentication handler.");
            return Task.FromException<TInput>(exception);
        }
    }
}