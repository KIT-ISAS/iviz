namespace Iviz.Octree
{
    internal readonly struct OcTreeKey
    {
        public readonly int a;
        public readonly int b;
        public readonly int c;

        OcTreeKey(int a, int b, int c) => (this.a, this.b, this.c) = (a, b, c);
        public OcTreeKey(int val) => a = b = c = val;

        public OcTreeKey ComputeChildKey(int pos, int centerOffsetKey)
        {
            if (centerOffsetKey == 0)
            {
                return ComputeChildKey(pos);
            }

            int na = (pos & 1) != 0 ? a + centerOffsetKey : a - centerOffsetKey;
            int nb = (pos & 2) != 0 ? b + centerOffsetKey : b - centerOffsetKey;
            int nc = (pos & 4) != 0 ? c + centerOffsetKey : c - centerOffsetKey;
            return new OcTreeKey(na, nb, nc);
        }

        OcTreeKey ComputeChildKey(int pos)
        {
            int na = (pos & 1) != 0 ? a : a - 1;
            int nb = (pos & 2) != 0 ? b : b - 1;
            int nc = (pos & 4) != 0 ? c : c - 1;
            return new OcTreeKey(na, nb, nc);
        }

        public override string ToString() => $"[{a.ToString()} {b.ToString()} {c.ToString()}]";
    }
}