using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
#if !NETSTANDARD2_0
using Nito.AsyncEx;
using System.Runtime.CompilerServices;
#endif

namespace Iviz.Roslib
{
    /// <summary>
    /// A subscriber queue that merges two or more <see cref="RosChannelReader{T}"/>
    /// </summary>
    public sealed class MergedChannelReader : IEnumerable<IMessage>
#if !NETSTANDARD2_0
        , IAsyncEnumerable<IMessage>
#endif
    {
        readonly IRosChannelReader[] sources;

        public MergedChannelReader(params IRosChannelReader[] sources)
        {
            if (sources == null)
            {
                BuiltIns.ThrowArgumentNull(nameof(sources));
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
                BuiltIns.ThrowArgumentNull(nameof(sources));
            }

            this.sources = sources.ToArray();

            if (this.sources.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(sources));
            }
        }

        public IEnumerable<IMessage> ReadAll(CancellationToken token = default)
        {
            Task[] tasks = new Task[sources.Length];

            for (int i = 0; i < sources.Length; i++)
            {
                tasks[i] = sources[i].WaitToReadAsync(token).AsTask();
            }

            while (true)
            {
                int readyTaskId = Task.WaitAny(tasks, token);
                var readyTask = (Task<bool>) tasks[readyTaskId];
                if (readyTask.IsCanceled)
                {
                    throw new TaskCanceledException(readyTask);
                }

                if (!readyTask.Result)
                {
                    throw new ObjectDisposedException($"{nameof(sources)}[{readyTaskId}]",
                        $"Channel {readyTaskId} has been disposed!");
                }

                yield return sources[readyTaskId].Read(token);
                tasks[readyTaskId] = sources[readyTaskId].WaitToReadAsync(token).AsTask();
            }
        }

        public IEnumerable<IMessage> TryReadAll()
        {
            return sources.SelectMany(source => source.TryReadAll());
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            return ReadAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<IMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token = default)
        {
            Task<bool>[] tasks = new Task<bool>[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                tasks[i] = sources[i].WaitToReadAsync(token).AsTask();
            }

            while (true)
            {
                Task<bool> readyTask = await tasks.WhenAny(token);
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i] != readyTask)
                    {
                        continue;
                    }

                    if (!await readyTask)
                    {
                        throw new ObjectDisposedException($"{nameof(sources)}[{i}]", $"Channel {i} has been disposed!");
                    }

                    yield return await sources[i].ReadAsync(token);
                    tasks[i] = sources[i].WaitToReadAsync(token).AsTask();
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