using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz
{
    public sealed class ParallelQueue<T> : IDisposable
    {
        readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        readonly object condVar = new object();
        readonly Task task;

        volatile bool keepGoing;

        public bool IsAlive => keepGoing;

        public Predicate<T> Callback { get; set; }

        int maxSize = 3;
        public int MaxSize
        {
            get => maxSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                maxSize = value;
            }
        }

        public ParallelQueue()
        {
            keepGoing = true;
            task = Task.Run(Run);
        }

        public ParallelQueue(Predicate<T> callback) : this()
        {
            Callback = callback;
        }

        public ParallelQueue(Predicate<T> callback, int maxSize) : this(callback)
        {
            MaxSize = maxSize;
        }

        void Run()
        {
            while (keepGoing)
            {
                lock (condVar)
                {
                    Monitor.Wait(condVar, 1000);
                }
                while (keepGoing && queue.Count > MaxSize && !queue.TryDequeue(out T _)) ;
                while (keepGoing && !queue.IsEmpty)
                {
                    T msg;
                    while (!queue.TryDequeue(out msg)) ;
                    try
                    {
                        if (!Callback(msg))
                        {
                            keepGoing = false;
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
            }
        }

        public void Enqueue(T msg)
        {
            if (!keepGoing)
            {
                return;
            }
            lock (condVar)
            {
                queue.Enqueue(msg);
                Monitor.Pulse(condVar);
            }
        }


        public void Stop()
        {
            keepGoing = false;
            lock (condVar)
            {
                Monitor.Pulse(condVar);
            }
            if (!task.IsCompleted)
            {
                task.Wait();
            }
        }

        public void Dispose()
        {
            Stop();
            task.Dispose();
        }
    }
}
