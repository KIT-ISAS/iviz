using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools;

/// <summary>
/// Task utilities
/// </summary>
public static class TaskUtils
{
    const string GenericExceptionFormat = "{0}: Exception thrown.{1}";

    /// <summary>
    /// Same as <see cref="Task.Run(Func{Task})"/>, but prevents the use of <see cref="ValueTask"/>
    /// as this would call the overload <see cref="Task.Run(Action)"/> instead
    /// </summary> 
    public static Task Run(Func<Task> task, CancellationToken token = default)
    {
        return Task.Run(task, token);
    }

    /// <summary>
    /// Same as <see cref="Task.Run(Func{Task})"/>, but prevents the use of <see cref="ValueTask{T}"/>
    /// as this would call the overload <see cref="Task.Run(Action)"/> instead
    /// </summary> 
    public static Task<T> Run<T>(Func<Task<T>> task, CancellationToken token = default)
    {
        return Task.Run(task, token);
    }

    static readonly Action<object?> SetResult = o => ((TaskCompletionSource<object?>)o!).TrySetResult(null);

    /// <summary>
    /// Waits for the task to complete.
    /// Only use this if the async function that generated the task does not support a cancellation token.
    /// </summary>
    /// <param name="task">The task to be awaited</param>
    /// <param name="timeoutInMs">The maximal amount to wait</param>
    /// <param name="token">An optional token to cancel the waiting</param>
    /// <returns>An awaitable task, with true if the task in the argument finished before the given time</returns>
    /// <exception cref="TaskCanceledException">If the token expires</exception>
    public static async Task<bool> AwaitFor(this Task task, int timeoutInMs, CancellationToken token = default)
    {
        if (timeoutInMs == -1)
        {
            throw new ArgumentOutOfRangeException(nameof(timeoutInMs));
        }

        token.ThrowIfCancellationRequested();
        if (task.IsCompleted)
        {
            return true;
        }

        using var tokenSource = !token.CanBeCanceled
            ? new CancellationTokenSource()
            : CancellationTokenSource.CreateLinkedTokenSource(token);
        tokenSource.CancelAfter(timeoutInMs);

        var timeout = new TaskCompletionSource<object?>();

#if !NETSTANDARD2_0
        await using (tokenSource.Token.Register(SetResult, timeout))
#else
            using (tokenSource.Token.Register(SetResult, timeout))
#endif
        {
            Task result = await (task, timeout.Task).WhenAny();
            return result == task;
        }
    }

    /// <summary>
    /// Waits a given time for the task to complete, and suppresses all exceptions.
    /// Only use this if the async function that generated the task does not support a cancellation token.
    /// </summary>
    /// <param name="task">The task to be awaited</param>
    /// <param name="caller">The name of the caller, used in the error message</param>
    /// <param name="timeoutInMs">The maximal amount to wait</param>
    /// <param name="token">An optional cancellation token</param>
    public static async Task AwaitNoThrow(this Task? task, int timeoutInMs, object caller,
        CancellationToken token = default)
    {
        if (task == null || task.IsCompleted)
        {
            return;
        }

        using var tokenSource = !token.CanBeCanceled
            ? new CancellationTokenSource()
            : CancellationTokenSource.CreateLinkedTokenSource(token);
        tokenSource.CancelAfter(timeoutInMs);

        var timeout = new TaskCompletionSource<object?>();
        var timeoutTask = timeout.Task;

#if !NETSTANDARD2_0
        await using (tokenSource.Token.Register(SetResult, timeout))
#else
            using (tokenSource.Token.Register(SetResult, timeout))
#endif
        {
            Task result = await (task, timeoutTask).WhenAny();
            if (result != task)
            {
                Logger.LogErrorFormat<object>(GenericExceptionFormat, caller, new TimeoutException());
                return;
            }

            await task.AwaitNoThrow(caller);
        }
    }


    /// <summary>
    /// Returns whether the task ran to completion (i.e., completed but not cancelled or faulted)
    /// </summary>
    /// <param name="task">The task to be checked</param>
    /// <returns>Whether the task ran to completion</returns>
    static bool RanToCompletion(this Task task) => task.Status == TaskStatus.RanToCompletion;

    /// <summary>
    /// Waits for the task and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static void WaitNoThrow(this Task? t, object caller)
    {
        if (t == null)
        {
            return;
        }

        try
        {
            t.GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            if (e is not OperationCanceledException)
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }
    }

    /// <summary>
    /// Waits for the task and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static void WaitNoThrow(this ValueTask t, object caller)
    {
        try
        {
            t.GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            if (e is not OperationCanceledException)
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }
    }

    /// <summary>
    /// Waits for the task up to a given time and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="timeoutInMs">The time to wait in milliseconds</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static void WaitNoThrow(this Task? t, int timeoutInMs, object caller)
    {
        if (t == null || t.IsCompleted)
        {
            return;
        }

        try
        {
            t.Wait(timeoutInMs);
        }
        catch (Exception e)
        {
            if (e is AggregateException && e.InnerException != null)
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e.InnerException);
                return;
            }

            if (e is not OperationCanceledException)
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }
    }

    /// <summary>
    /// Waits for the task to finish. If an exception happens, unwraps the aggregated exception and rethrows it.
    /// </summary>
    /// <param name="t">The task to await.</param>
    public static void WaitAndRethrow(this Task? t)
    {
        if (t == null || t.RanToCompletion())
        {
            return;
        }

        try
        {
            t.GetAwaiter().GetResult();
        }
        catch (AggregateException e) when (e.InnerException != null)
        {
            ExceptionDispatchInfo.Capture(e.InnerException)?.Throw();
        }
    }

    /// <summary>
    /// Waits for the task to finish. If an exception happens, unwraps the aggregated exception and rethrows it.
    /// </summary>
    /// <param name="t">The task to await.</param>
    public static T WaitAndRethrow<T>(this Task<T>? t)
    {
        if (t == null)
        {
            return default!;
        }

        try
        {
            return t.GetAwaiter().GetResult();
        }
        catch (AggregateException e) when (e.InnerException != null)
        {
            ExceptionDispatchInfo.Capture(e.InnerException)?.Throw();
            throw;
        }
    }

    /// <summary>
    /// Waits for the task and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static async Task AwaitNoThrow(this Task? t, object caller)
    {
        if (t == null || t.RanToCompletion())
        {
            return;
        }

        try
        {
            await t;
        }
        catch (Exception e)
        {
            if (e is not (OperationCanceledException or TimeoutException))
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }
    }

    /// <summary>
    /// Waits for the task and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static async Task<T?> AwaitNoThrow<T>(this ValueTask<T> t, object caller)
    {
        try
        {
            return await t;
        }
        catch (Exception e)
        {
            if (e is not (OperationCanceledException or TimeoutException))
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }

        return default;
    }

    /// <summary>
    /// Waits for the task and suppresses all exceptions, prints an error message instead.
    /// </summary>
    /// <param name="t">The task to await</param>
    /// <param name="caller">The name of the caller to use in the error message</param>
    public static async Task AwaitNoThrow(this ValueTask t, object caller)
    {
        try
        {
            await t;
        }
        catch (Exception e)
        {
            if (e is not (OperationCanceledException or TimeoutException))
            {
                Logger.LogErrorFormat(GenericExceptionFormat, caller, e);
            }
        }
    }

    public static Task WhenAll<TA>(this SelectEnumerable<IReadOnlyList<TA>, TA, Task> ts)
    {
        return Task.WhenAll(ts.ToArray());
    }

    public static Task WhenAll(this (Task, Task) ts)
    {
        var (task1, task2) = ts;
        return task1.RanToCompletion() && task2.RanToCompletion()
            ? Task.CompletedTask
            : Task.WhenAll(task1, task2);
    }

    public static ValueTask<Task> WhenAny(this (Task t1, Task t2) ts)
    {
        var (t1, t2) = ts;
        return t1.IsCompleted
            ? ValueTask2.FromResult(t1)
            : t2.IsCompleted
                ? ValueTask2.FromResult(t2)
                : Task.WhenAny(t1, t2).AsValueTask();
    }
}

public static class ValueTask2
{
    public static ValueTask<T> FromResult<T>(T t) => new(t);
    public static ValueTask<T> FromException<T>(Exception e) => new(Task.FromException<T>(e));
    public static ValueTask FromException(Exception e) => new(Task.FromException(e));
    public static ValueTask<T> AsValueTask<T>(this Task<T> t) => new(t);
    public static ValueTask AsValueTask(this Task t) => new(t);
}