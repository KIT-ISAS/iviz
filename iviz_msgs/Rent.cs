using System;
using System.Buffers;

namespace Iviz.Msgs
{
    public readonly struct Rent<T> : IDisposable where T : struct
    {
        static readonly ArrayPool<T> Shared = ArrayPool<T>.Shared;

        public int Count { get; }
        public T[] Array { get; }

        public ArraySegment<T> Segment => new ArraySegment<T>(Array, 0, Count);

#if !NETSTANDARD2_0
        public Span<T> Span => new Span<T>(Array, 0, Count);
#endif

        public Rent(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count cannot be negative", nameof(count));
            }

            if (count == 0)
            {
                Array = System.Array.Empty<T>();
                Count = 0;
                return;
            }

            Array = Shared.Rent(count);
            Count = count;
        }

        public void Dispose()
        {
            if (Count > 0)
            {
                Shared.Return(Array);
            }
        }
    }
}