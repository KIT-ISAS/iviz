#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Displays
{
    /// <summary>
    /// Interface that provides a ROS service call. Implemented by RoslibConnection.
    /// This interface exists only to ensure that the Iviz.Core project does not need to have Iviz.Ros as reference.   
    /// </summary>
    public interface IExternalServiceProvider
    {
        ValueTask<bool> CallServiceAsync<T>(string service, T srv, int timeoutInMs, CancellationToken token)
            where T : IService;
    }
}