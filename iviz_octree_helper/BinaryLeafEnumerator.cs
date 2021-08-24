using System.Collections;
using System.Collections.Generic;

namespace Iviz.Octree
{
    public struct BinaryLeafEnumerator : IEnumerable<Float4>, IEnumerator<Float4>
    {
        readonly OctreeHelper parent;
        readonly Stack<BinNodeIterator> stack;
        readonly int maxDepth;
        Reader reader;
        Float4 current;

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
                            parent.KeyToPosition(currentIt.ChildKey, currentIt.depth + 1, out current);
                            return true;
                        }

                        break;
                    case BinNodeIterator.HasChildren:
                        ushort bitset = reader.ReadUshort();
                        currentIt.CreateChild(bitset, out var childIt);

                        stack.Push(childIt);
                        if (childIt.depth == maxDepth)
                        {
                            parent.KeyToPosition(childIt.key, childIt.depth, out current);
                            return true;
                        }

                        break;
                }
            }

            return false;
        }

        public int NumberOfNodes => reader.Size / 2;
        
        public Float4 Current => current;
        
        public BinaryLeafEnumerator GetEnumerator() => this;
        
        public void Reset() => throw new System.NotSupportedException();
        
        object IEnumerator.Current => Current;
        
        IEnumerator<Float4> IEnumerable<Float4>.GetEnumerator() => this;
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Dispose()
        {
        }
    }
}