using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
#if !NETSTANDARD2_0
using System.Runtime.CompilerServices;

#endif

namespace Iviz.Roslib
{
    /// <summary>
    /// A subscriber queue that merges two or more <see cref="RosChannelReader{T}"/>
    /// </summary>
    public class MergedChannelReader : IEnumerable<IMessage>
    {
        readonly IRosChannelReader[] sources;

        public MergedChannelReader(params IRosChannelReader[] sources)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (sources.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(sources));
            }

            this.sources = sources;
        }

        public MergedChannelReader(IEnumerable<IRosChannelReader> sources)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            this.sources = sources.ToArray();

            if (this.sources.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(sources));
            }
        }

        public IMessage Read(CancellationToken token = default)
        {
            return ReadWithIndex(token).msg;
        }

        public (IMessage msg, int id) ReadWithIndex(CancellationToken token = default)
        {
            using CancellationTokenSource stopTokenCts = new CancellationTokenSource();
            using CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(stopTokenCts.Token, token);

            Task[] tasks = sources
                .Select(async queue => { await queue.WaitToReadAsync(linkedCts.Token); })
                .ToArray();
            int readyTask = Task.WaitAny(tasks, token);
            linkedCts.Cancel();

            token.ThrowIfCancellationRequested();

            IMessage msg = sources[readyTask].Read(token);
            return (msg, readyTask);
        }

        public async Task<IMessage> ReadAsync(CancellationToken token = default)
        {
            return (await ReadWithIndexAsync(token)).msg;
        }

        public async Task<(IMessage msg, int index)> ReadWithIndexAsync(CancellationToken token = default)
        {
            using CancellationTokenSource stopTokenCts = new CancellationTokenSource();
            using CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(stopTokenCts.Token, token);

            Task<bool>[] tasks = sources.Select(queue => queue.WaitToReadAsync(linkedCts.Token)).ToArray();
            Task<bool> readyTask = await Task.WhenAny(tasks);
            linkedCts.Cancel();

            token.ThrowIfCancellationRequested();

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] == readyTask)
                {
                    return (await sources[i].ReadAsync(token), i);
                }
            }

            return default; // shouldn't happen
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            yield return Read(CancellationToken.None);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IMessage> ReadAll(CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                yield return Read(token);
            }

            token.ThrowIfCancellationRequested();
        }

        public IEnumerable<(IMessage msg, int index)> ReadAllWithIndex(CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                yield return ReadWithIndex(token);
            }

            token.ThrowIfCancellationRequested();
        }

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<IMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token = default)
        {
            while (true)
            {
                yield return await ReadAsync(token);
            }
        }

        public async IAsyncEnumerable<(IMessage msg, int index)>
            ReadAllWithIndexAsync([EnumeratorCancellation] CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                yield return await ReadWithIndexAsync(token);
            }

            token.ThrowIfCancellationRequested();
        }
#endif
    }
}