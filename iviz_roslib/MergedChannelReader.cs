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

        public IMessage Read(CancellationToken token)
        {
            return ReadWithIndex(token).msg;
        }

        public (IMessage msg, int id) ReadWithIndex(CancellationToken token)
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

        public async Task<IMessage> ReadAsync(CancellationToken externalToken)
        {
            return (await ReadWithIndexAsync(externalToken)).msg;
        }

        public async Task<(IMessage msg, int index)> ReadWithIndexAsync(CancellationToken token)
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
                    IMessage msg = await sources[i].ReadAsync(token);
                    return (msg, i);
                }
            }

            return default; // shouldn't happen
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            IMessage msg;
            try
            {
                msg = Read(CancellationToken.None);
            }
            catch (OperationCanceledException)
            {
                yield break;
            }

            yield return msg;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IMessage> ReadAll(CancellationToken token)
        {
            while (true)
            {
                IMessage msg;
                try
                {
                    msg = Read(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

        public IEnumerable<(IMessage msg, int index)> ReadAllWithIndex(CancellationToken token)
        {
            while (true)
            {
                IMessage msg;
                int index;
                try
                {
                    (msg, index) = ReadWithIndex(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return (msg, index);
            }
        }

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<IMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token)
        {
            while (true)
            {
                IMessage msg;
                try
                {
                    msg = await ReadAsync(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

        public async IAsyncEnumerable<(IMessage msg, int index)>
            ReadAllWithIndexAsync([EnumeratorCancellation] CancellationToken token)
        {
            while (true)
            {
                IMessage msg;
                int index;
                try
                {
                    (msg, index) = await ReadWithIndexAsync(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return (msg, index);
            }
        }
#endif
    }
}