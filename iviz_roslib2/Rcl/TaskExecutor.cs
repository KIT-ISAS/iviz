using System.Collections.Concurrent;
using System.Runtime.InteropServices;
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

        var tcs = TaskUtils.CreateCompletionSource();
        queue.Enqueue(() =>
        {
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return;
            }

            try
            {
                action();
                tcs.TrySetResult();
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
            }
        });

        Signal();
        return tcs.Task;
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

        var tcs = TaskUtils.CreateCompletionSource<T>();
        queue.Enqueue(() =>
        {
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return;
            }

            try
            {
                tcs.TrySetResult(action());
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
            }
        });

        Signal();
        return tcs.Task;
    }

    protected void Stop()
    {
        tokenSource.Cancel();
    }

    public virtual ValueTask DisposeAsync(CancellationToken token)
    {
        return task.AwaitNoThrow(this).AsValueTask();
    }

    public override string ToString() => $"[{nameof(TaskExecutor)}]";
}
