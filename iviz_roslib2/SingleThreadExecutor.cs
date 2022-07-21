using System.Collections.Concurrent;
using Iviz.Tools;

namespace Iviz.Roslib2;

internal sealed class SingleThreadExecutor : IAsyncDisposable
{
    readonly SemaphoreSlim signal = new(0);
    readonly Task task;
    readonly CancellationTokenSource tokenSource = new();
    readonly ConcurrentQueue<Action> queue = new();

    public SingleThreadExecutor()
    {
        task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
    }

    async Task Run()
    {
        while (!tokenSource.IsCancellationRequested)
        {
            while (queue.TryDequeue(out var action))
            {
                action();
            }

            await signal.WaitAsync();
        }
    }

    public Task Enqueue(Action action)
    {
        var ts = TaskUtils.CreateCompletionSource();
        queue.Enqueue(() =>
        {
            try
            {
                action();
                ts.TrySetResult();
            }
            catch (Exception e)
            {
                ts.TrySetException(e);
            }
        });

        signal.Release();
        return ts.Task;
    }

    public Task<T> Enqueue<T>(Func<T> action)
    {
        var ts = TaskUtils.CreateCompletionSource<T>();
        queue.Enqueue(() =>
        {
            try
            {
                ts.TrySetResult(action());
            }
            catch (Exception e)
            {
                ts.TrySetException(e);
            }
        });

        signal.Release();
        return ts.Task;
    }

    public async ValueTask DisposeAsync()
    {
        await Enqueue(() => tokenSource.Cancel());
        await task.AwaitNoThrow(this);
    }

    public override string ToString() => $"[{nameof(SingleThreadExecutor)}]";
}