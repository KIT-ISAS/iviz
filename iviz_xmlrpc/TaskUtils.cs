using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Task utilities
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Waits for the task to complete.
        /// Only use this if the async function that generated the task does not support a cancellation token.
        /// Note: A timeout doesn't cancel the task in the argument. You need to cancel that task through other means.
        /// </summary>
        /// <param name="task">The task to be awaited</param>
        /// <param name="timeoutInMs">The maximal amount to wait</param>
        /// <param name="token">An optional token to cancel the waiting</param>
        /// <returns>An awaitable task, with true if the task in the argument finished before the given time</returns>
        /// <exception cref="TaskCanceledException">If the token expires</exception>
        public static async Task<bool> WaitFor(this Task task, int timeoutInMs, CancellationToken token = default)
        {
            using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource.CancelAfter(timeoutInMs);

            var timeout = new TaskCompletionSource<object>();
            var timeoutTask = timeout.Task;
            using var registration = tokenSource.Token.Register(() => timeout.TrySetCanceled());

            Task result = await (task, timeoutTask).WhenAny().Caf();
            return result == task;
        }

        /// <summary>
        /// Waits for the task to complete, and throws a timeout exception if it doesn't.
        /// Only use this if the async function that generated the task does not support a cancellation token.
        /// Note: A timeout doesn't cancel the task in the argument. You need to cancel that task through other means.
        /// </summary>
        /// <param name="task">The task to be awaited</param>
        /// <param name="timeoutInMs">The maximal amount to wait</param>
        /// <param name="errorMessage">Optional error message to appear in the timeout exception</param>
        /// <param name="token">An optional cancellation token</param>
        /// <exception cref="TimeoutException">If the task did not complete in time</exception>
        public static async Task WaitForWithTimeout(this Task? task, int timeoutInMs, string? errorMessage = null,
            CancellationToken token = default)
        {
            if (task == null)
            {
                return;
            }

            using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource.CancelAfter(timeoutInMs);

            var timeout = new TaskCompletionSource<object>();
            var timeoutTask = timeout.Task;
            using var registration = tokenSource.Token.Register(() => timeout.TrySetCanceled());

            Task result = await (task, timeoutTask).WhenAny().Caf();
            if (result != task)
            {
                throw new TimeoutException(errorMessage);
            }
        }


        /// <summary>
        /// Returns whether the task ran to completion (i.e., completed but not cancelled or faulted)
        /// </summary>
        /// <param name="task">The task to be checked</param>
        /// <returns>Whether the task ran to completion</returns>
        public static bool RanToCompletion(this Task task)
        {
            return task.Status == TaskStatus.RanToCompletion;
        }

        /// <summary>
        /// Set ConfigureAwait(false) for a task.
        /// </summary>
        /// <param name="task">Task to be caffed</param>
        /// <returns>The caffed task</returns>
        public static ConfiguredTaskAwaitable Caf(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        /// <summary>
        /// Sets ConfigureAwait(false) for a task.
        /// </summary>
        /// <param name="task">Task to be caffed</param>
        /// <returns>The caffed task</returns>        
        public static ConfiguredTaskAwaitable<T> Caf<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }

#if !NETSTANDARD2_0
        public static ConfiguredValueTaskAwaitable Caf(this ValueTask task)
        {
            return task.ConfigureAwait(false);
        }
#endif

        public static ConfiguredValueTaskAwaitable<T> Caf<T>(this ValueTask<T> task)
        {
            return task.ConfigureAwait(false);
        }

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
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }
        }

        public static T WaitNoThrow<T>(this Task<T>? t, object caller)
        {
            if (t == null)
            {
                return default!;
            }

            try
            {
                return t.GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }

            return default!;
        }

        public static T WaitNoThrow<T>(this ValueTask<T> t, object caller)
        {
            try
            {
                return t.GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }

                return default!;
            }
        }


        /// <summary>
        /// Waits for the task to finish. If an exception happens, unwraps the aggregated exception.
        /// </summary>
        /// <param name="t">The task to await.</param>
        public static void WaitAndRethrow(this Task? t)
        {
            if (t == null)
            {
                return;
            }

            try
            {
                t.GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                throw;
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
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (AggregateException e) when (e.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(e.InnerException)?.Throw();
                throw;
            }
        }

        public static async Task AwaitNoThrow(this Task? t, object caller)
        {
            if (t == null)
            {
                return;
            }

            try
            {
                await t.Caf();
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }
        }

        public static async Task<T?> AwaitNoThrow<T>(this Task<T>? t, object caller) where T : class
        {
            if (t == null)
            {
                return default;
            }

            try
            {
                return await t.Caf();
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }

            return default;
        }

        public static async ValueTask<T?> AwaitNoThrow<T>(this ValueTask<T> t, object caller) where T : class
        {
            try
            {
                return await Caf(t);
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.LogErrorFormat("{0}: Error in task wait: {1}", caller, e);
                }
            }

            return default;
        }

        public static void ThrowIfCanceled(this CancellationToken t, Task task)
        {
            if (t.IsCancellationRequested)
            {
                throw new TaskCanceledException(task);
            }
        }


        public static async Task WhenAll<TA>(this EnumeratorUtils.SelectEnumerable<TA, Task> ts)
        {
            List<Exception>? es = null;
            foreach (var t in ts)
            {
                try
                {
                    await t.Caf();
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
                await task1.Caf();
            }
            catch (Exception e)
            {
                es ??= new List<Exception>();
                es.Add(e);
            }

            try
            {
                await task2.Caf();
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

        public static async ValueTask<Task> WhenAny(this (Task, Task) ts)
        {
            return await Task.WhenAny(ts.Item1, ts.Item2).Caf();
        }
    }

    public static class StreamUtils
    {
        internal static async Task WriteChunkAsync(this StreamWriter writer, string text, CancellationToken token,
            int timeoutInMs)
        {
            using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource.CancelAfter(timeoutInMs);

#if !NETSTANDARD2_0
            await writer.WriteAsync(text.AsMemory(), tokenSource.Token).Caf();
#else
            var timeout = new TaskCompletionSource<object>();
            var timeoutTask = timeout.Task;
            using var registration = tokenSource.Token.Register(() => timeout.TrySetCanceled());

            Task writeTask = writer.WriteAsync(text);
            Task resultTask = await (writeTask, timeoutTask).WhenAny().Caf();
            if (resultTask == timeoutTask)
            {
                token.ThrowIfCanceled(timeoutTask);
                throw new TimeoutException($"Writing operation timed out");
            }

            await writeTask.Caf();
#endif
        }
    }

    public static class ConnectionUtils
    {
        public static readonly Dictionary<string, string> GlobalResolver = new Dictionary<string, string>();

        public static async Task TryConnectAsync(this TcpClient client, string hostname, int port,
            CancellationToken token, int timeoutInMs)
        {
            if (GlobalResolver.TryGetValue(hostname, out string? resolvedHostname))
            {
                hostname = resolvedHostname;
            }

            using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource.CancelAfter(timeoutInMs);
#if NET5_0
            try
            {
                await client.ConnectAsync(hostname, port, tokenSource.Token).Caf();
            }
            catch (OperationCanceledException)
            {
                token.ThrowIfCancellationRequested();
                throw new TimeoutException($"Connection to '{hostname}:{port} timed out");
            }
#else
            var timeout = new TaskCompletionSource<object>();
            var timeoutTask = timeout.Task;
            using var registration = tokenSource.Token.Register(() => timeout.TrySetCanceled());

            Task connectionTask = client.ConnectAsync(hostname, port);
            Task resultTask = await (timeoutTask, connectionTask).WhenAny().Caf();
            if (resultTask == timeoutTask)
            {
                token.ThrowIfCanceled(timeoutTask);
                throw new TimeoutException($"Connection to '{hostname}:{port} timed out");
            }

            await connectionTask.Caf();
#endif
        }
    }
}