using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public interface IRosChannelReader : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        Task<bool> WaitToReadAsync(CancellationToken token = default);
        IMessage Read(CancellationToken token);
        Task<IMessage> ReadAsync(CancellationToken token = default);
        public Task StartAsync(IRosClient client, string topic, CancellationToken token = default);
        public void Start(IRosClient client, string topic);
        public IEnumerable<IMessage> ReadAll(CancellationToken token = default);
        public IEnumerable<IMessage> TryReadAll();
#if !NETSTANDARD2_0
        public IAsyncEnumerable<IMessage> ReadAllAsync(CancellationToken token = default);
#endif
    }
}