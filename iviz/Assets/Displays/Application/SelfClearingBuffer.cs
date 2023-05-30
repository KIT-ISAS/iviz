#nullable enable
using System.Buffers;
using Iviz.Core;
using Iviz.Msgs;
using Unity.Mathematics;

namespace Iviz.Displays.Helpers
{
    public sealed class SelfClearingBuffer
    {
        static ArrayPool<float4> Pool => ArrayPool<float4>.Shared;

        const float TimeUntilClearInSec = 10;

        float4[]? array;
        float lastUsed;
        bool disposed;

        public SelfClearingBuffer()
        {
            GameThread.EverySecond += CheckEverySecond;
        }

        public float4[] EnsureCapacity(int size)
        {
            if (disposed) BuiltIns.ThrowObjectDisposed(nameof(SelfClearingBuffer));
                
            lock (this)
            {
                lastUsed = GameThread.GameTime;

                if (array != null)
                {
                    if (array.Length >= size)
                    {
                        return array;
                    }

                    Pool.Return(array);
                }

                array = Pool.Rent(size);
                return array;
            }
        }

        void CheckEverySecond()
        {
            if (disposed) return;
            
            lock (this)
            {
                float time = GameThread.GameTime;
                if (time - lastUsed <= TimeUntilClearInSec) return;
                if (array is null) return;
                
                Pool.Return(array);
                array = null;
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            
            GameThread.EverySecond -= CheckEverySecond;

            lock (this)
            {
                if (array == null) return;
                
                Pool.Return(array);
                array = null;
            }
        }
    }
}