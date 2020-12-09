using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

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
            Task result = await Task.WhenAny(task, Task.Delay(timeoutInMs, token)).Caf();
            return result == task;
        }
        
        /// <summary>
        /// Waits for the task to complete, and throws a timeout exception if it doesn't.
        /// Only use this if the async function that generated the task does not support a cancellation token.
        /// Note: A timeout doesn't cancel the task in the argument. You need to cancel that task through other means.
        /// </summary>
        /// <param name="task">The task to be awaited</param>
        /// <param name="timeoutInMs">The maximal amount to wait</param>
        /// <exception cref="TimeoutException">If the task did not complete in time</exception>
        public static async Task WaitForWithTimeout(this Task task, int timeoutInMs)
        {
            Task result = await Task.WhenAny(task, Task.Delay(timeoutInMs)).Caf();
            if (result != task)
            {
                throw new TimeoutException();
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
    }
}