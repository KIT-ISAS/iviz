/*************************************************************************
MIT License

Copyright (c) 2017 NeoSmart Technologies

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*************************************************************************/

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.XmlRpc
{
    public class AsyncLock
    {
        //We do not have System.Threading.Thread.* on .NET Standard without additional dependencies
        //Work around is easy: create a new ThreadLocal<T> with a random value and this is our thread id :)
        const long UnlockedThreadId = 0; //"owning" thread id when unlocked

        static int globalThreadCounter;

        static readonly ThreadLocal<int> threadId =
            new ThreadLocal<int>(() => Interlocked.Increment(ref globalThreadCounter));

        readonly object reentrancy = new object();

        //We are using this SemaphoreSlim like a posix condition variable
        //we only want to wake waiters, one or more of whom will try to obtain a different lock to do their thing
        //so long as we can guarantee no wakes are missed, the number of awakees is not important
        //ideally, this would be "friend" for access only from InnerLock, but whatever.
        readonly SemaphoreSlim retry = new SemaphoreSlim(0, 1);
        
        long owningId = UnlockedThreadId;

        int reentrances;

        //We generate a unique id from the thread ID combined with the task ID, if any
        static long ThreadId => (long) ((ulong) threadId.Value << 32) | (uint) (Task.CurrentId ?? 0);

        public InnerLock Lock()
        {
            var @lock = new InnerLock(this);
            @lock.ObtainLock();
            return @lock;
        }

        public async ValueTask<InnerLock> LockAsync()
        {
            var @lock = new InnerLock(this);
            await @lock.ObtainLockAsync();
            return @lock;
        }

        public async ValueTask<InnerLock> LockAsync(CancellationToken ct)
        {
            var @lock = new InnerLock(this);
            await @lock.ObtainLockAsync(ct);
            return @lock;
        }

        public readonly struct InnerLock : IDisposable
        {
            readonly AsyncLock parent;

            internal InnerLock(AsyncLock parent)
            {
                this.parent = parent;
            }

            internal async Task ObtainLockAsync()
            {
                while (!TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    await parent.retry.WaitAsync();
                }
            }

            internal async Task ObtainLockAsync(CancellationToken ct)
            {
                while (!TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    await parent.retry.WaitAsync(ct);
                }
            }

            internal void ObtainLock()
            {
                while (!TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    parent.retry.Wait();
                }
            }

            bool TryEnter()
            {
                lock (parent.reentrancy)
                {
                    if (parent.owningId != UnlockedThreadId && parent.owningId != ThreadId)
                    {
                        //another thread currently owns the lock
                        return false;
                    }

                    //we can go in
                    Interlocked.Increment(ref parent.reentrances);
                    parent.owningId = ThreadId;
                    return true;
                }
            }

            public void Dispose()
            {
                lock (parent.reentrancy)
                {
                    Interlocked.Decrement(ref parent.reentrances);
                    if (parent.reentrances == 0)
                    {
                        //the owning thread is always the same so long as we are in a nested stack call
                        //we reset the owning id to null only when the lock is fully unlocked
                        parent.owningId = UnlockedThreadId;
                        if (parent.retry.CurrentCount == 0)
                        {
                            parent.retry.Release();
                        }
                    }
                }
            }
        }
    }
}