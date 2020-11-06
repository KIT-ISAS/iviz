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
    public interface ISubscriberChannelReader
    {
        Task<bool> WaitToReadAsync(CancellationToken token);
        IMessage Read(CancellationToken token);
        Task<IMessage> ReadAsync(CancellationToken token);
    }
    
    /// <summary>
    /// A subscriber queue that merges two or more <see cref="RosSubscriberChannelReader{T}"/>
    /// </summary>
    public class MergedChannelReader : IEnumerable<IMessage>
    {
        readonly ISubscriberChannelReader[] sources;

        public MergedChannelReader(params ISubscriberChannelReader[] sources)
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

        public MergedChannelReader(IEnumerable<ISubscriberChannelReader> sources)
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

        public IMessage Read(CancellationToken externalToken)
        {
            using CancellationTokenSource stopTokenCts = new CancellationTokenSource();
            using CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(stopTokenCts.Token, externalToken);

            Task[] tasks = sources
                .Select(async queue => { await queue.WaitToReadAsync(linkedCts.Token); })
                .ToArray();
            int readyTask = Task.WaitAny(tasks, externalToken);
            linkedCts.Cancel();

            externalToken.ThrowIfCancellationRequested();

            IMessage msg = sources[readyTask].Read(externalToken);
            return msg;
        }

        public async Task<IMessage> ReadAsync(CancellationToken externalToken)
        {
            using CancellationTokenSource stopTokenCts = new CancellationTokenSource();
            using CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(stopTokenCts.Token, externalToken);

            Task<bool>[] tasks = sources.Select(queue => queue.WaitToReadAsync(linkedCts.Token)).ToArray();
            Task<bool> readyTask = await Task.WhenAny(tasks);
            linkedCts.Cancel();

            externalToken.ThrowIfCancellationRequested();

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] == readyTask)
                {
                    IMessage msg = await sources[i].ReadAsync(externalToken);
                    return msg;
                }
            }

            return null!; // shouldn't happen
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

        public IEnumerable<IMessage> AsEnum(CancellationToken externalToken)
        {
            while (true)
            {
                IMessage msg;
                try
                {
                    msg = Read(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<IMessage> AsAsyncEnum([EnumeratorCancellation] CancellationToken externalToken)
        {
            while (true)
            {
                IMessage msg;
                try
                {
                    msg = await ReadAsync(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }
#endif
    }
}