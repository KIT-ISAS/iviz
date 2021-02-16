using System;
using System.Buffers;

namespace Iviz.Msgs
{
    public readonly struct Rent<T> : IDisposable where T : struct
    {
        static readonly ArrayPool<T> Shared = ArrayPool<T>.Shared;

        public readonly int Count;
        public readonly T[] Array;

        public ArraySegment<T> Segment => new(Array, 0, Count);

#if !NETSTANDARD2_0
        public Span<T> Span => new(Array, 0, Count);
#endif

        public Rent(int count)
        {
            switch (count)
            {
                case < 0:
                    throw new ArgumentException("Count cannot be negative", nameof(count));
                case 0:
                    Array = System.Array.Empty<T>();
                    Count = 0;
                    break;
                default:
                    Array = Shared.Rent(count);
                    Count = count;
                    break;
            }
        }

        public void Dispose()
        {
            if (Count > 0)
            {
                Shared.Return(Array);
            }
        }

        public override string ToString()
        {
            return $"[Rent Type={typeof(T).Name} Count={Count} RealSize={(Array != null ? Array.Length : 0)}]";
        }
    }
}