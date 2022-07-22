using System.Collections.Concurrent;
using System.Threading;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal abstract class TaskExecutor
{
    Task? task;
    readonly CancellationTokenSource tokenSource = new();
    readonly ConcurrentQueue<Action> queue = new();

    protected abstract void Wait();
    protected abstract void Signal();

    protected void Start()
    {
        task = Task.Run(Run);
    }

    void Run()
    {
        while (!tokenSource.IsCancellationRequested)
        {
            try
            {
                Wait();
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Unexpected exception in {1}! {2}", this, nameof(Wait), e);
            }

            while (queue.TryDequeue(out var action))
            {
                action();
            }
        }

        if (queue.Count != 0)
        {
            Logger.LogErrorFormat("{0}: {1} tasks left in queue!", this, queue.Count);
        }
    }

    protected Task Post(Action action, CancellationToken token = default)
    {
        if (task is { IsCompleted: true })
        {
            return Task.FromException(new ObjectDisposedException(ToString()));
        }
        
        if (token.IsCancellationRequested)
        {
            return Task.FromCanceled(token);
        }

        var ts = TaskUtils.CreateCompletionSource();
        queue.Enqueue(() =>
        {
            if (token.IsCancellationRequested)
            {
                ts.TrySetCanceled();
                return;
            }

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

    protected Task<T> Post<T>(Func<T> action, CancellationToken token = default)
    {
        if (task is { IsCompleted: true })
        {
            return Task.FromException<T>(new ObjectDisposedException(ToString()));
        }
        
        if (token.IsCancellationRequested)
        {
            return Task.FromCanceled<T>(token);
        }

        var ts = TaskUtils.CreateCompletionSource<T>();
        queue.Enqueue(() =>
        {
            if (token.IsCancellationRequested)
            {
                ts.TrySetCanceled();
                return;
            }

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

    protected void Stop()
    {
        tokenSource.Cancel();
    }

    public virtual async ValueTask DisposeAsync(CancellationToken token = default)
    {
        await task.AwaitNoThrow(this);
    }

    public override string ToString() => $"[{nameof(TaskExecutor)}]";
}