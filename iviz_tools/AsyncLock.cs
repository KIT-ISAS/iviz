using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Tools;

public sealed class AsyncLock
{
    readonly SemaphoreSlim signal = new(1, 1);

    public LockCore Lock(CancellationToken token) => new(signal, token);

    public ValueTask<AsyncLockCore> LockAsync(CancellationToken token)
    {
        var myLock = new AsyncLockCore(signal);
        return myLock.EnterAsync(token); 
    }

    public readonly struct LockCore : IDisposable
    {
        readonly SemaphoreSlim? signal; // must be null if wait failed

        public LockCore(SemaphoreSlim signal, CancellationToken token)
        {
            signal.Wait(token);
            this.signal = signal;
        }

        public void Dispose()
        {
            signal?.Release();
        }
    }

    public readonly struct AsyncLockCore : IDisposable
    {
        readonly SemaphoreSlim? signal; // must be null if wait failed

        public AsyncLockCore(SemaphoreSlim signal)
        {
            this.signal = signal; 
        }

        public async ValueTask<AsyncLockCore> EnterAsync(CancellationToken token)
        {
            if (signal == null) return default; // new AsyncLockCore(null)
            await signal.WaitAsync(token);
            return new AsyncLockCore(signal);

        }

        public void Dispose()
        {
            signal?.Release();
        }
    }
}