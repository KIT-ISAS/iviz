using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib;

internal class SenderQueue
{
    protected const int NumPacketsWithoutConstraint = 2;
    protected const int MaxPacketsInQueue = 2048;

    public long MaxQueueSizeInBytes { get; set; } = 50000;

    protected readonly IRosSender sender;
    protected readonly SemaphoreSlim signal = new(0);
    protected int messagesInQueue;

    public int Count => messagesInQueue;

    protected SenderQueue(IRosSender sender) => this.sender = sender;


    protected static async ValueTask WaitForSignal(TaskCompletionSource msgSignal, CancellationToken token)
    {
        // ReSharper disable once UseAwaitUsing
        using (token.Register(CallbackHelpers.OnCanceled, msgSignal))
        {
            await msgSignal.Task;
        }
    }

    public async ValueTask WaitAsync(CancellationToken token)
    {
        while (messagesInQueue == 0)
        {
            await signal.WaitAsync(token);
        }
    }
    
    protected RosQueueOverflowException CreateOverflowException() =>
        new($"Message could not be sent to node '{sender.RemoteCallerId}'", sender);

    protected RosQueueException CreateQueueException(Exception e) =>
        new($"An unexpected exception was thrown while sending to node '{sender.RemoteCallerId}'", e, sender);
}

internal sealed class SenderQueue<TMessage> : SenderQueue where TMessage : IMessage
{
    readonly ConcurrentQueue<Entry?> messageQueue = new();
    readonly List<Entry?> sendQueue = new();
    readonly Serializer<TMessage> serializer;

    public SenderQueue(IRosSender sender, Serializer<TMessage> serializer) : base(sender)
    {
        this.serializer = serializer;
    }

    public void Enqueue(in TMessage message, ref int numDropped, ref long bytesDropped)
    {
        int messageLength = serializer.RosMessageLength(message);
        if (messagesInQueue > MaxPacketsInQueue)
        {
            bytesDropped += messageLength;
            numDropped++;
            return;
        }

        messageQueue.Enqueue(new Entry(message, messageLength));
        Interlocked.Increment(ref messagesInQueue);
        signal.Release();
    }

    public void EnqueueEmpty()
    {
        messageQueue.Enqueue(null);
        Interlocked.Increment(ref messagesInQueue);
        signal.Release();
    }

    public ValueTask EnqueueAsync(in TMessage message, CancellationToken token, ref int numDropped,
        ref long bytesDropped)
    {
        int messageLength = serializer.RosMessageLength(message);
        if (messagesInQueue > MaxPacketsInQueue)
        {
            bytesDropped += messageLength;
            numDropped++;
            return default;
        }

        var tcs = TaskUtils.CreateCompletionSource();
        messageQueue.Enqueue(new Entry(message, messageLength, tcs));
        signal.Release();

        return token.CanBeCanceled
            ? WaitForSignal(tcs, token)
            : tcs.Task.AsValueTask();
    }

    public RangeEnumerable<Entry?> ReadAll(ref int numDropped, ref long bytesDropped)
    {
        long totalQueueSizeInBytes = ReadFromQueue();

        if (sendQueue.Count <= NumPacketsWithoutConstraint ||
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
        while (messageQueue.TryDequeue(out var entry))
        {
            sendQueue.Add(entry);
            if (entry is { } notNullElement)
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
        int sendQueueCount = sendQueue.Count;
        int c = sendQueueCount - 1;

        long remainingBytes = maxQueueSizeInBytes;
        for (int i = 0; i < NumPacketsWithoutConstraint; i++)
        {
            var element = sendQueue[c - i];
            if (element is { } notNullElement)
            {
                remainingBytes -= notNullElement.messageLength;
            }
        }

        int consideredPackets = NumPacketsWithoutConstraint;
        if (remainingBytes > 0)
        {
            // start discarding old messages
            for (int i = NumPacketsWithoutConstraint; i < sendQueueCount; i++)
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

        numDropped = sendQueueCount - consideredPackets;
        bytesDropped = totalQueueSizeInBytes - maxQueueSizeInBytes + remainingBytes;
    }

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

    public void DirectSendToLoopback(in RangeEnumerable<Entry?> queue, LoopbackReceiver<TMessage> loopbackReceiver,
        ref long numSent, ref long bytesSent)
    {
        if (loopbackReceiver == null) BuiltIns.ThrowArgumentNull(nameof(loopbackReceiver));

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
                msgSignal?.TrySetResult();
            }
        }
        catch (Exception e)
        {
            FlushFrom(queue, e);
            throw;
        }
    }

    internal readonly struct Entry
    {
        readonly TMessage message;
        public readonly int messageLength;
        public readonly TaskCompletionSource? signal;

        public Entry(in TMessage message, int messageLength, TaskCompletionSource? signal = null)
        {
            this.message = message;
            this.messageLength = messageLength;
            this.signal = signal;
        }

        public void Deconstruct(out TMessage outMessage, out int outMessageLength,
            out TaskCompletionSource? outSignal) =>
            (outMessage, outMessageLength, outSignal) = (message, messageLength, signal);
    }
}