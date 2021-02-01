using System;
using System.Collections;
using System.Collections.Generic;
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

            Task result = await Task.WhenAny(task, Task.Delay(timeoutInMs, token)).Caf();
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

        public static void ThrowIfCanceled(this CancellationToken t)
        {
            if (t.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
        }
    }

    public static class EnumeratorUtils
    {
        public readonly struct ZipEnumerable<TA, TB> : IReadOnlyList<(TA First, TB Second)>
        {
            readonly IReadOnlyList<TA> a;
            readonly IReadOnlyList<TB> b;

            public struct ZipEnumerator : IEnumerator<(TA First, TB Second)>
            {
                readonly IReadOnlyList<TA> a;
                readonly IReadOnlyList<TB> b;
                int currentIndex;

                internal ZipEnumerator(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
                {
                    this.a = a;
                    this.b = b;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    bool isLastIndex = currentIndex == Math.Min(a.Count, b.Count) - 1;
                    if (isLastIndex)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public void Reset()
                {
                    currentIndex = -1;
                }

                public (TA, TB) Current => (a[currentIndex], b[currentIndex]);

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal ZipEnumerable(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
            {
                this.a = a;
                this.b = b;
            }

            public ZipEnumerator GetEnumerator()
            {
                return new ZipEnumerator(a, b);
            }

            IEnumerator<(TA, TB)> IEnumerable<(TA First, TB Second)>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => Math.Min(a.Count, b.Count);

            public (TA First, TB Second) this[int index] => (a[index], b[index]);

            public (TA First, TB Second)[] ToArray()
            {
                (TA, TB)[] array = new (TA, TB)[Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }
        }

        public static ZipEnumerable<TA, TB> Zip<TA, TB>(this IReadOnlyList<TA> a, IReadOnlyList<TB> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            return new ZipEnumerable<TA, TB>(a, b);
        }

        public readonly struct SelectEnumerable<TA, TB> : IReadOnlyList<TB>
        {
            readonly IReadOnlyList<TA> a;
            readonly Func<TA, TB> f;

            public struct SelectEnumerator : IEnumerator<TB>
            {
                readonly IReadOnlyList<TA> a;
                readonly Func<TA, TB> f;
                int currentIndex;

                internal SelectEnumerator(IReadOnlyList<TA> a, Func<TA, TB> f)
                {
                    this.a = a;
                    this.f = f;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    bool isLastIndex = currentIndex == a.Count - 1;
                    if (isLastIndex)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public void Reset()
                {
                    currentIndex = -1;
                }

                public TB Current => f(a[currentIndex]);

                object? IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal SelectEnumerable(IReadOnlyList<TA> a, Func<TA, TB> f)
            {
                this.a = a;
                this.f = f;
            }

            public SelectEnumerator GetEnumerator()
            {
                return new SelectEnumerator(a, f);
            }

            IEnumerator<TB> IEnumerable<TB>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public TB[] ToArray()
            {
                TB[] array = new TB[a.Count];
                for (int i = 0; i < a.Count; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }

            public List<TB> ToList()
            {
                List<TB> array = new List<TB>(a.Count);
                for (int i = 0; i < a.Count; i++)
                {
                    array.Add(this[i]);
                }

                return array;
            }

            public int Count => a.Count;

            public TB this[int index] => f(a[index]);
        }

        public static SelectEnumerable<TA, TB> Select<TA, TB>(
            this IReadOnlyList<TA> a,
            Func<TA, TB> f)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            return new SelectEnumerable<TA, TB>(a, f);
        }

        public static void AddRange<TA, TB>(this List<TB> list, SelectEnumerable<TA, TB> tb)
        {
            list.Capacity = list.Count + tb.Count;
            foreach (TB b in tb)
            {
                list.Add(b);
            }
        }

        public readonly struct RefEnumerable<T>
        {
            readonly T[] a;

            public struct RefEnumerator
            {
                readonly T[] a;
                int currentIndex;

                public RefEnumerator(T[] a)
                {
                    this.a = a;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    if (currentIndex == a.Length - 1)
                    {
                        return false;
                    }

                    currentIndex++;
                    return true;
                }

                public ref T Current => ref a[currentIndex];
            }

            public RefEnumerable(T[] a) => this.a = a;

            public RefEnumerator GetEnumerator() => new RefEnumerator(a);
        }

        public static RefEnumerable<T> Ref<T>(this T[] a) =>
            new RefEnumerable<T>(a ?? throw new ArgumentNullException(nameof(a)));

        public static RefEnumerable<T>.RefEnumerator EnumeratorRef<T>(this T[] a) =>
            new RefEnumerable<T>.RefEnumerator(a ?? throw new ArgumentNullException(nameof(a)));

        public delegate void ActionRef<T>(ref T t);

        public static T[] ForEachRef<T>(this T[] a, ActionRef<T> action)
        {
            for (int i = 0; i < a.Length; i++)
            {
                action(ref a[i]);
            }

            return a;
        }
    }
}