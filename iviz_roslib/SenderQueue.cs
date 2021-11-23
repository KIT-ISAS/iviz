using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib
{
    internal sealed class SenderQueue<T> where T : IMessage
    {
        const int MaxPacketsWithoutConstraint = 2;
        const int MaxPacketsInQueue = 2048;

        readonly IRosSender sender;
        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<Entry?> messageQueue = new();
        readonly List<Entry?> sendQueue = new();
        int messagesInQueue;
        
        public long MaxQueueSizeInBytes { get; set; } = 50000;

        public int Count => messagesInQueue;

        public SenderQueue(IRosSender sender) => this.sender = sender;

        public void Enqueue(in T message, ref int numDropped, ref long bytesDropped)
        {
            if (messagesInQueue > MaxPacketsInQueue)
            {
                bytesDropped += message.RosMessageLength;
                numDropped++;
                return;
            }
            
            messageQueue.Enqueue(new Entry(message));
            Interlocked.Increment(ref messagesInQueue);
            signal.Release();
        }

        public void EnqueueEmpty()
        {
            messageQueue.Enqueue(null);
            Interlocked.Increment(ref messagesInQueue);
            signal.Release();
        }

        public ValueTask EnqueueAsync(in T message, CancellationToken token, ref int numDropped, ref long bytesDropped)
        {
            if (messagesInQueue > MaxPacketsInQueue)
            {
                bytesDropped += message.RosMessageLength;
                numDropped++;
                return default;
            }
            
            var msgSignal = new TaskCompletionSource<object?>();
            messageQueue.Enqueue(new Entry(message, msgSignal));
            signal.Release();
            
            return token.CanBeCanceled
                ? WaitForSignal(msgSignal, token)
                : new ValueTask(msgSignal.Task);
        }

        static async ValueTask WaitForSignal(TaskCompletionSource<object?> msgSignal, CancellationToken token)
        {
#if !NETSTANDARD2_0
            await using (token.Register(StreamUtils.OnCanceled, msgSignal))
#else
            using (token.Register(StreamUtils.OnCanceled, msgSignal))
#endif
            {
                await msgSignal.Task;
            }
        }

        public Task WaitAsync(CancellationToken token)
        {
            return signal.WaitAsync(token);
        }

        public RangeEnumerable<Entry?> ReadAll(ref int numDropped, ref long bytesDropped)
        {
            long totalQueueSizeInBytes = ReadFromQueue();

            if (sendQueue.Count <= MaxPacketsWithoutConstraint ||
                totalQueueSizeInBytes < MaxQueueSizeInBytes)
            {
                return sendQueue.Skip(0);
            }

            DiscardOldMessages(totalQueueSizeInBytes, MaxQueueSizeInBytes, out int newNumDropped,
                out long newBytesDropped);
            numDropped += newNumDropped;
            bytesDropped += newBytesDropped;

            foreach (var entry in sendQueue.Take(newNumDropped))
            {
                entry?.signal?.TrySetException(CreateOverflowException());
            }

            return sendQueue.Skip(newNumDropped);
        }

        long ReadFromQueue()
        {
            long totalQueueSizeInBytes = 0;

            sendQueue.Clear();
            while (messageQueue.TryDequeue(out var element))
            {
                sendQueue.Add(element);
                if (element is { } notNullElement)
                {
                    totalQueueSizeInBytes += notNullElement.messageLength;
                }

                Interlocked.Decrement(ref messagesInQueue);
            }

            return totalQueueSizeInBytes;
        }

        void DiscardOldMessages(long totalQueueSizeInBytes, long maxQueueSizeInBytes,
            out int numDropped, out long bytesDropped)
        {
            int c = sendQueue.Count - 1;

            long remainingBytes = maxQueueSizeInBytes;
            for (int i = 0; i < MaxPacketsWithoutConstraint; i++)
            {
                var element = sendQueue[c - i];
                if (element is { } notNullElement)
                {
                    remainingBytes -= notNullElement.messageLength;
                }
            }

            int consideredPackets = MaxPacketsWithoutConstraint;
            if (remainingBytes > 0)
            {
                // start discarding old messages
                for (int i = MaxPacketsWithoutConstraint; i < sendQueue.Count; i++)
                {
                    var element = sendQueue[c - i];
                    if (element is not { } notNullElement)
                    {
                        continue;
                    }

                    long currentMsgLength = notNullElement.messageLength;
                    if (currentMsgLength > remainingBytes)
                    {
                        break;
                    }

                    remainingBytes -= currentMsgLength;
                    consideredPackets++;
                }
            }

            numDropped = sendQueue.Count - consideredPackets;
            bytesDropped = totalQueueSizeInBytes - maxQueueSizeInBytes + remainingBytes;
        }

        RosQueueOverflowException CreateOverflowException() =>
            new($"Message could not be sent to node '{sender.RemoteCallerId}'", sender);

        RosQueueException CreateQueueException(Exception e) =>
            new($"An unexpected exception was thrown while sending to node '{sender.RemoteCallerId}'", e, sender);

        public void FlushRemaining()
        {
            foreach (var entry in messageQueue)
            {
                entry?.signal?.TrySetException(new RosQueueException(
                    $"Connection for '{sender.RemoteCallerId}' is shutting down", sender));
            }
        }

        public void FlushFrom(in RangeEnumerable<Entry?> queue, Exception exception)
        {
            foreach (var entry in queue)
            {
                if (entry is not var (_, _, msgSignal) || msgSignal is null || msgSignal.Task.IsCompleted)
                {
                    continue;
                }

                msgSignal.TrySetException(CreateQueueException(exception));
            }
        }

        public void DirectSendToLoopback(in RangeEnumerable<Entry?> queue, ILoopbackReceiver<T> loopbackReceiver,
            ref long numSent, ref long bytesSent)
        {
            if (loopbackReceiver == null)
            {
                throw new ArgumentNullException(nameof(loopbackReceiver));
            }
            
            try
            {
                foreach (var entry in queue)
                {
                    if (entry is not var (msg, msgLength, msgSignal))
                    {
                        continue;
                    }

                    loopbackReceiver.Post(msg, msgLength);

                    numSent++;
                    bytesSent += msgLength + 4;
                    msgSignal?.TrySetResult(null);
                }
            }
            catch (Exception e)
            {
                FlushFrom(queue, e);
                throw;
            }
        }

        public readonly struct Entry
        {
            readonly T message;
            public readonly int messageLength;
            public readonly TaskCompletionSource<object?>? signal;

            public Entry(in T message, TaskCompletionSource<object?>? signal = null)
            {
                (this.message, messageLength, this.signal) = (message, message.RosMessageLength, signal);
                if (messageLength < 0)
                {
                    throw new RosMessageSizeOverflowException();
                }
            }

            public void Deconstruct(out T outMessage, out int outMessageLength,
                out TaskCompletionSource<object?>? outSignal) =>
                (outMessage, outMessageLength, outSignal) = (message, messageLength, signal);
        }
    }
}