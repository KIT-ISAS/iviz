using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

public interface IRosChannelWriter : IDisposable, IAsyncDisposable
{
    bool Started { get; }

    public IRosPublisher Publisher { get; }

    //public ValueTask StartAsync(IRosClient client, string topic, DynamicMessage generator, CancellationToken token = default);
    
    public ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default);

    public void Start(IRosClient client, string topic);

    //public void Start(IRosClient client, string topic, DynamicMessage generator);
    
    public void Write(IMessage msg);
    
    public void WriteAll(IEnumerable<IMessage> messages);

    public ValueTask WriteAllAsync(IAsyncEnumerable<IMessage> messages,
        RosPublishPolicy policy = RosPublishPolicy.DoNotWait, CancellationToken token = default);
}