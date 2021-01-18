using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public interface IRosChannelWriter : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        public IRosPublisher Publisher { get; }
        public Task StartAsync(IRosClient client, string topic, bool requestNoDelay = false);
        public void Start(IRosClient client, string topic, bool requestNoDelay = false);
        public void Write(IMessage msg);
        public void WriteAll(IEnumerable<IMessage> messages);
#if !NETSTANDARD2_0
        public ValueTask WriteAllAsync(IAsyncEnumerable<IMessage> messages,
            RosPublishPolicy policy = RosPublishPolicy.DoNotWait, CancellationToken token = default);
#endif
    }
}