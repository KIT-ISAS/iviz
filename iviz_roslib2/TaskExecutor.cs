using System.Collections.Concurrent;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal abstract class TaskExecutor
{
    readonly Task task;
    readonly CancellationTokenSource tokenSource = new();
    readonly ConcurrentQueue<Action> queue = new();

    protected abstract void Wait();
    protected abstract void Signal();

    protected TaskExecutor()
    {
        task = Task.Run(Run);
    }
    
    void Run()
    {
        while (!tokenSource.IsCancellationRequested)
        {
            while (queue.TryDequeue(out var action))
            {
                action();
            }

            Wait();
        }
    }

    protected Task Enqueue(Action action)
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

        Signal();
        return ts.Task;
    }

    protected Task<T> Enqueue<T>(Func<T> action)
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

        Signal();
        return ts.Task;
    }

    public virtual async ValueTask DisposeAsync()
    {
        await Enqueue(tokenSource.Cancel);
        await task.AwaitNoThrow(this);
    }

    public override string ToString() => $"[{nameof(TaskExecutor)}]";
}