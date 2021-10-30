using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools
{
    /// <summary>
    /// Task utilities
    /// </summary>
    public static class TaskUtils
    {
        public static Task StartLongTask(Func<Task> task, CancellationToken token = default)
        {
            // need some logging here
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
            using var registration = tokenSource.Token.Register(SetResult, timeout);
            Task result = await (task, timeout.Task).WhenAny();
            return result == task;
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
            using var registration = tokenSource.Token.Register(SetResult, timeout);

            Task result = await (task, timeoutTask).WhenAny();

            if (result != task)
            {
                Logger.LogErrorFormat<object>("{0}: Task wait timed out", caller);
                return;
            }

            await task.AwaitNoThrow(caller);
        }


        /// <summary>
        /// Returns whether the task ran to completion (i.e., completed but not cancelled or faulted)
        /// </summary>
        /// <param name="task">The task to be checked</param>
        /// <returns>Whether the task ran to completion</returns>
        public static bool RanToCompletion(this Task task) => task.Status == TaskStatus.RanToCompletion;

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
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }
        }

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
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e.InnerException);
                    return;
                }

                if (e is not OperationCanceledException)
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }
        }

        /// <summary>
        /// Waits for the task to finish. If an exception happens, unwraps the aggregated exception.
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
                if (e is not OperationCanceledException)
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }
        }
        
        public static async ValueTask<T?> AwaitNoThrow<T>(this ValueTask<T> t, object caller) where T : class
        {
            try
            {
                return await t;
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }

            return default;
        }
        
        public static async Task WhenAll<TA>(this SelectEnumerable<IReadOnlyList<TA>, TA, Task> ts)
        {
            if (ts.Count == 0)
            {
                return;
            }

            List<Exception>? es = null;
            foreach (var t in ts)
            {
                try
                {
                    await t;
                }
                catch (Exception e)
                {
                    es ??= new List<Exception>();
                    es.Add(e);
                }
            }

            if (es != null)
            {
                throw new AggregateException(es);
            }
        }

        public static async Task WhenAll(this (Task, Task) ts)
        {
            List<Exception>? es = null;
            var (task1, task2) = ts;

            try
            {
                await task1;
            }
            catch (Exception e)
            {
                es ??= new List<Exception> { e };
            }

            try
            {
                await task2;
            }
            catch (Exception e)
            {
                es ??= new List<Exception>();
                es.Add(e);
            }

            if (es != null)
            {
                throw new AggregateException(es);
            }
        }

        public static ValueTask<Task> WhenAny(this (Task t1, Task t2) ts)
        {
            var (t1, t2) = ts;
            return t1.IsCompleted
                ? new ValueTask<Task>(t1)
                : t2.IsCompleted
                    ? new ValueTask<Task>(t2)
                    : new ValueTask<Task>(Task.WhenAny(t1, t2));
        }
    }
}