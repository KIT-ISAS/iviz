using System.Collections.Generic;
using Unity.Mathematics;

namespace Iviz.Octree
{
    public struct BinaryLeafEnumerator
    {
        readonly OctreeHelper parent;
        readonly Stack<BinNodeIterator> stack;
        readonly int maxDepth;
        Reader reader;
        float4 current;

        public BinaryLeafEnumerator(OctreeHelper parent, sbyte[] src, uint offset, int maxDepth)
        {
            this.parent = parent;
            stack = new Stack<BinNodeIterator>(OctreeHelper.TreeMaxDepth + 1);
            reader = new Reader(src, offset);
            this.maxDepth = maxDepth;
            current = default;

            var startIt = BinNodeIterator.Start(reader.ReadUshort());
            stack.Push(startIt);
        }

        public bool MoveNext()
        {
            if (stack.Count == 0)
            {
                return false;
            }

            while (stack.Count != 0)
            {
                var currentIt = stack.Pop();
                if (currentIt.depth >= OctreeHelper.TreeMaxDepth)
                {
                    throw new MalformedOctreeException();
                }

                if (!currentIt.MoveNext())
                {
                    continue;
                }

                stack.Push(currentIt);

                switch (currentIt.ChildBits)
                {
                    case BinNodeIterator.OccupiedNode:
                        if (currentIt.depth < maxDepth)
                        {
                            current = parent.KeyToPosition(currentIt.ChildKey, currentIt.depth + 1);
                            return true;
                        }

                        break;
                    case BinNodeIterator.HasChildren:
                        ushort bitset = reader.ReadUshort();
                        currentIt.CreateChild(bitset, out var childIt);

                        stack.Push(childIt);
                        if (childIt.depth == maxDepth)
                        {
                            current = parent.KeyToPosition(childIt.key, childIt.depth);
                            return true;
                        }

                        break;
                }
            }

            return false;
        }

        public int NumberOfNodes => reader.Size / 2;
        public float4 Current => current;
        public BinaryLeafEnumerator GetEnumerator() => this;
    }
}