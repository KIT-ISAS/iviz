using System.Collections.Generic;
using Unity.Mathematics;

namespace Iviz.Octree
{
    public struct LeafEnumerator
    {
        const float MinProbabilityForOccupied = 0.5f;
        static readonly float MinLogOdds = OctreeHelper.LogOdds(MinProbabilityForOccupied);

        readonly OctreeHelper parent;
        readonly Stack<NodeIterator> stack;
        readonly int maxDepth;
        readonly uint strideAfterValue;
        Reader reader;
        float4 current;

        public LeafEnumerator(OctreeHelper parent, sbyte[] src, uint offset, uint valueStride, int maxDepth)
        {
            this.parent = parent;
            stack = new Stack<NodeIterator>(OctreeHelper.TreeMaxDepth);
            reader = new Reader(src, offset);
            this.maxDepth = maxDepth;
            strideAfterValue = valueStride - 4;
            current = default;

            reader.Skip(valueStride);
            var startIt = NodeIterator.Start(reader.ReadByte());
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

                if (!currentIt.HasChild)
                {
                    continue;
                }

                float logOdds = reader.ReadFloat();
                reader.Skip(strideAfterValue);
                sbyte bitset = reader.ReadByte();

                currentIt.CreateChild(bitset, out var childIt);
                if (!childIt.IsLeaf)
                {
                    stack.Push(childIt);
                }

                if (logOdds > MinLogOdds &&
                    (childIt.IsLeaf && childIt.depth <= maxDepth || childIt.depth == maxDepth))
                {
                    current = parent.KeyToPosition(childIt.key, childIt.depth);
                    return true;
                }
            }

            return false;
        }

        public int NumberOfNodes => (int) (reader.Size / (4 + strideAfterValue));
        public float4 Current => current;
        public LeafEnumerator GetEnumerator() => this;
    }
}