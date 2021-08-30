namespace Iviz.Tools
{
    public readonly struct RefEnumerable<T>
    {
        readonly T[] a;

        public struct Enumerator
        {
            readonly T[] a;
            int index;
            public Enumerator(T[] a) => (this.a, index) = (a, -1);
            public bool MoveNext() => ++index < a.Length;
            public ref T Current => ref a[index];
        }

        public RefEnumerable(T[] a) => this.a = a;
        public Enumerator GetEnumerator() => new(a);
    }
}