#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;

namespace Iviz.Displays
{
    /// <summary>
    /// Interface that provides a ROS service call. Implemented by RoslibConnection.
    /// This interface exists only to ensure that the Iviz.Core project does not need to have Iviz.Ros as reference.   
    /// </summary>
    public abstract class ServiceProvider
    {
        public abstract ValueTask<bool> CallModelServiceAsync(string service, GetModelResource srv, int timeoutInMs,
            CancellationToken token);
        public abstract ValueTask<bool> CallModelServiceAsync(string service, GetModelTexture srv, int timeoutInMs,
            CancellationToken token);
        public abstract ValueTask<bool> CallModelServiceAsync(string service, GetSdf srv, int timeoutInMs,
            CancellationToken token);
    }
}