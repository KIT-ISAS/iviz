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
#if !NETSTANDARD2_0
        , IAsyncEnumerable<IMessage>
#endif
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
            using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);

            Task[] tasks = sources
                .Select(async queue => { await queue.WaitToReadAsync(linkedCts.Token); })
                .ToArray();
            int readyTask = Task.WaitAny(tasks, token);
            linkedCts.Cancel();

            IMessage msg = sources[readyTask].Read(token);
            return (msg, readyTask);
        }

        public async Task<IMessage> ReadAsync(CancellationToken token = default)
        {
            return (await ReadWithIndexAsync(token)).msg;
        }

        public async Task<(IMessage msg, int index)> ReadWithIndexAsync(CancellationToken token = default)
        {
            using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);

            Task<bool>[] tasks = sources.Select(queue => queue.WaitToReadAsync(linkedCts.Token)).ToArray();
            Task<bool> readyTask = await Task.WhenAny(tasks);
            linkedCts.Cancel();

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
            yield return Read();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IMessage> ReadAll(CancellationToken token = default)
        {
            Task[] tasks = new Task[sources.Length];

            while (true)
            {
                using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);

                for (int i = 0; i < sources.Length; i++)
                {
                    tasks[i] = sources[i].WaitToReadAsync(linkedCts.Token);
                }

                int readyTask = Task.WaitAny(tasks, token);
                linkedCts.Cancel();

                yield return sources[readyTask].Read(token);
            }
        }

        public IEnumerable<IMessage> TryReadAll()
        {
            return sources.SelectMany(source => source.TryReadAll());
        }        
        
#if !NETSTANDARD2_0
        public async IAsyncEnumerable<IMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token = default)
        {
            Task<bool>[] tasks = new Task<bool>[sources.Length];

            while (true)
            {
                using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);

                for (int i = 0; i < sources.Length; i++)
                {
                    tasks[i] = sources[i].WaitToReadAsync(linkedCts.Token);
                }

                Task<bool> readyTask = await Task.WhenAny(tasks);
                linkedCts.Cancel();

                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i] == readyTask)
                    {
                        yield return await sources[i].ReadAsync(token);
                    }
                }
            }
        }

        public IAsyncEnumerator<IMessage> GetAsyncEnumerator(CancellationToken token = default)
        {
            return ReadAllAsync(token).GetAsyncEnumerator(token);
        }
#endif
    }
}