namespace Iviz.Octree
{
    internal struct BinNodeIterator
    {
        //public const int UnknownNode = 0;
        //public const int FreeNode = 1; // 10 flipped
        public const int OccupiedNode = 2; // 01 flipped
        public const int HasChildren = 3;

        public readonly OcTreeKey key;
        public readonly int depth;
        int position;
        readonly ushort bitset;

        BinNodeIterator(int depth, int position, in OcTreeKey key, ushort bitset)
        {
            this.depth = depth;
            this.position = position;
            this.key = key;
            this.bitset = bitset;
        }

        public static BinNodeIterator Start(ushort bitset)
        {
            return new BinNodeIterator(0, -2, new OcTreeKey(OctreeHelper.TreeMaxVal), bitset);
        }

        public bool MoveNext()
        {
            if (position >= 14)
            {
                return false;
            }

            position += 2;
            return true;
        }

        public int ChildBits => (bitset & (3 << position)) >> position;

        public OcTreeKey ChildKey => key.ComputeChildKey(position / 2, OctreeHelper.TreeMaxVal >> (depth + 1));

        public void CreateChild(ushort childBitset, out BinNodeIterator childIt)
        {
            childIt = new BinNodeIterator(depth + 1, -2, ChildKey, childBitset);
        }
    }
}