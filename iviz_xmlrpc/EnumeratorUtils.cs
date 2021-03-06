using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.XmlRpc
{
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
                int index;

                internal ZipEnumerator(IReadOnlyList<TA> na, IReadOnlyList<TB> nb) => (a, b, index) = (na, nb, -1);
                public bool MoveNext() => ++index < Math.Min(a.Count, b.Count);
                public void Reset() => index = -1;
                public (TA, TB) Current => (a[index], b[index]);
                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal ZipEnumerable(IReadOnlyList<TA> a, IReadOnlyList<TB> b) => (this.a, this.b) = (a, b);

            public ZipEnumerator GetEnumerator() => new ZipEnumerator(a, b);

            IEnumerator<(TA, TB)> IEnumerable<(TA First, TB Second)>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public int Count => Math.Min(a.Count, b.Count);

            public (TA First, TB Second) this[int index] => (a[index], b[index]);

            public (TA First, TB Second)[] ToArray()
            {
                if (Count == 0)
                {
                    return Array.Empty<(TA, TB)>();
                }

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
                int index;

                internal SelectEnumerator(IReadOnlyList<TA> na, Func<TA, TB> nf) => (a, f, index) = (na, nf, -1);
                public bool MoveNext() => ++index < a.Count;
                public void Reset() => index = -1;
                public TB Current => f(a[index]);
                object? IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal SelectEnumerable(IReadOnlyList<TA> a, Func<TA, TB> f) => (this.a, this.f) = (a, f);
            public SelectEnumerator GetEnumerator() => new SelectEnumerator(a, f);
            IEnumerator<TB> IEnumerable<TB>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public TB[] ToArray()
            {
                if (a.Count == 0)
                {
                    return Array.Empty<TB>();
                }

                TB[] array = new TB[a.Count];
                for (int i = 0; i < a.Count; i++)
                {
                    array[i] = f(a[i]);
                }

                return array;
            }

            public List<TB> ToList()
            {
                List<TB> array = new List<TB>(a.Count);
                foreach (var ta in a)
                {
                    array.Add(f(ta));
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
                int index;
                public RefEnumerator(T[] a) => (this.a, index) = (a, -1);
                public bool MoveNext() => ++index < a.Length;
                public ref T Current => ref a[index];
            }

            public RefEnumerable(T[] a) => this.a = a;
            public RefEnumerator GetEnumerator() => new RefEnumerator(a);
        }

        public static RefEnumerable<T> Ref<T>(this T[] a) =>
            new RefEnumerable<T>(a ?? throw new ArgumentNullException(nameof(a)));

        public static RefEnumerable<T>.RefEnumerator RefEnumerator<T>(this T[] a) =>
            new RefEnumerable<T>.RefEnumerator(a ?? throw new ArgumentNullException(nameof(a)));

        public delegate void RefAction<T>(ref T t);

        public static T[] ForEach<T>(this T[] a, RefAction<T> action)
        {
            for (int i = 0; i < a.Length; i++)
            {
                action(ref a[i]);
            }

            return a;
        }
    }
}