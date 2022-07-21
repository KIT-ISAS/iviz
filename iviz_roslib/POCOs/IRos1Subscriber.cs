using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Roslib;

public interface IRos1Subscriber : IRosSubscriber
{
    /// <summary>
    /// Timeout in milliseconds to wait for a publisher handshake.
    /// </summary>
    public int TimeoutInMs { set; }
    
    /// <summary>
    /// The number of publishers in the topic. Only includes established connections.
    /// </summary>
    public int NumActivePublishers { get; }
    
    /// <summary>
    /// Called by the ROS client to notify the subscriber that the list of publishers has changed.
    /// </summary>
    /// <param name="publisherUris">The new list of publishers.</param>
    /// <param name="token">A cancellation token</param>
    internal ValueTask PublisherUpdateRpcAsync(IEnumerable<Uri> publisherUris, CancellationToken token);
}