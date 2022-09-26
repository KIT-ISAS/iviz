using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using System.Runtime.CompilerServices;

namespace Iviz.Roslib;

/// <summary>
/// A subscriber queue that merges two or more <see cref="RosChannelReader{T}"/>
/// </summary>
public sealed class MergedChannelReader<TMessage> : IEnumerable<TMessage>, IAsyncEnumerable<TMessage>
    where TMessage : IMessage, new()
{
    readonly RosChannelReader<TMessage>[] sources;

    public MergedChannelReader(params RosChannelReader<TMessage>[] sources)
    {
        if (sources == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(sources));
        }

        if (sources.Length == 0)
        {
            BuiltIns.ThrowArgument(nameof(sources), "Value cannot be an empty collection.");
        }

        this.sources = sources;
    }

    public IEnumerable<TMessage> ReadAll(CancellationToken token = default)
    {
        var tasks = new Task[sources.Length];

        for (int i = 0; i < sources.Length; i++)
        {
            tasks[i] = sources[i].WaitToReadAsync(token).AsTask();
        }

        while (true)
        {
            int readyTaskId = Task.WaitAny(tasks, token);
            var readyTask = (Task<bool>)tasks[readyTaskId];
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

    public IEnumerable<TMessage> TryReadAll()
    {
        return sources.SelectMany(source => source.TryReadAll());
    }

    public IEnumerator<TMessage> GetEnumerator()
    {
        return ReadAll().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public async IAsyncEnumerable<TMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token = default)
    {
        var tasks = sources.Select(reader => reader.WaitToReadAsync(token).AsTask()).ToArray();

        while (true)
        {
            var readyTask = await Task.WhenAny(tasks);
            int index = Array.IndexOf(tasks, readyTask);
            if (!await readyTask)
            {
                throw new ObjectDisposedException($"{nameof(sources)}[{index}]", $"Channel {index} has been disposed!");
            }

            var sourceChannel = sources[index];
            yield return await sourceChannel.ReadAsync(token);
            tasks[index] = sourceChannel.WaitToReadAsync(token).AsTask();
        }
    }

    public IAsyncEnumerator<TMessage> GetAsyncEnumerator(CancellationToken token = default)
    {
        return ReadAllAsync(token).GetAsyncEnumerator(token);
    }
}

public sealed class MergedChannelReader : IEnumerable<IMessage>, IAsyncEnumerable<IMessage>
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
            BuiltIns.ThrowArgument(nameof(sources), "Value cannot be an empty collection.");
        }

        this.sources = sources;
    }

    public IEnumerable<IMessage> ReadAll(CancellationToken token = default)
    {
        var tasks = sources.Select(reader => (Task)reader.WaitToReadAsync(token).AsTask()).ToArray();

        while (true)
        {
            int index = Task.WaitAny(tasks, token);
            var readyTask = (Task<bool>)tasks[index];
            if (readyTask.IsCanceled)
            {
                throw new TaskCanceledException(readyTask);
            }

            if (!readyTask.Result)
            {
                throw new ObjectDisposedException($"{nameof(sources)}[{index}]",
                    $"Channel {index} has been disposed!");
            }

            yield return sources[index].Read(token);
            tasks[index] = sources[index].WaitToReadAsync(token).AsTask();
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

    public async IAsyncEnumerable<IMessage> ReadAllAsync([EnumeratorCancellation] CancellationToken token = default)
    {
        Task<bool>[] tasks = new Task<bool>[sources.Length];
        for (int i = 0; i < sources.Length; i++)
        {
            tasks[i] = sources[i].WaitToReadAsync(token).AsTask();
        }

        while (true)
        {
            var readyTask = await Task.WhenAny(tasks);
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
}