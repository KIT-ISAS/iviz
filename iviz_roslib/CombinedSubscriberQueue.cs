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
    public interface ISubscriberQueue
    {
        Task<bool> WaitToReadAsync(CancellationToken token);
        IMessage Read(CancellationToken token);
        Task<IMessage> ReadAsync(CancellationToken token);
    }
    
    public class CombinedSubscriberQueue : IEnumerable<IMessage>
    {
        readonly ISubscriberQueue[] sources;

        public CombinedSubscriberQueue(ISubscriberQueue[] sources)
        {
            this.sources = sources;
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

            return null; // shouldn't happen
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
    
    
    public class CombinedSubscriberQueue<T>  where T : IMessage, IDeserializable<T>, new()
    {
        readonly RosSubscriberChannel<T>[] sources;

        public CombinedSubscriberQueue(RosSubscriberChannel<T>[] sources)
        {
            this.sources = sources;
        }

        public T Read(CancellationToken externalToken)
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

            T msg = sources[readyTask].Read(externalToken);
            return msg;
        }

        public async Task<T> ReadAsync(CancellationToken externalToken)
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
                    T msg = await sources[i].ReadAsync(externalToken);
                    return msg;
                }
            }

            return default; // shouldn't happen
        }

        public IEnumerable<T> AsEnum(CancellationToken externalToken)
        {
            T msg;
            try
            {
                msg = Read(externalToken);
            }
            catch (OperationCanceledException)
            {
                yield break;
            }

            yield return msg;
        }

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<T> AsAsyncEnum([EnumeratorCancellation] CancellationToken externalToken)
        {
            T msg;
            try
            {
                msg = await ReadAsync(externalToken);
            }
            catch (OperationCanceledException)
            {
                yield break;
            }

            yield return msg;
        }
#endif
    }    
}