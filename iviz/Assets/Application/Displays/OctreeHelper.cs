using System;
using System.Collections;
using System.Collections.Generic;
using Iviz.Core;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class OctreeHelper
    {
        const int TreeMaxVal = short.MaxValue + 1;
        const int TreeMaxDepth = 16;

        readonly float[] invLookupTable;
        readonly float[] sizeLookupTable;
        readonly float resolution;

        public float Resolution => resolution;

        static float Probability(float logOdds) => 1 - 1 / (1 - Mathf.Exp(logOdds));
        static float LogOdds(float probability) => Mathf.Log(1 / (1 - probability) - 1);

        public OctreeHelper(float resolution)
        {
            this.resolution = resolution;

            sizeLookupTable = new float[TreeMaxDepth + 1];
            invLookupTable = new float[TreeMaxDepth + 1];
            for (int i = 0; i < sizeLookupTable.Length; i++)
            {
                sizeLookupTable[i] = resolution * (1 << (TreeMaxDepth - i));
                invLookupTable[i] = 1.0f / (1 << (TreeMaxDepth - i));
            }
        }

        float3 KeyToCoord(in OcTreeKey key, int depth)
        {
            float il = invLookupTable[depth];
            float sl = sizeLookupTable[depth];

            float x = (Mathf.Floor((key.a - TreeMaxVal) * il) + 0.5f) * sl;
            float y = (Mathf.Floor((key.b - TreeMaxVal) * il) + 0.5f) * sl;
            float z = (Mathf.Floor((key.c - TreeMaxVal) * il) + 0.5f) * sl;
            return new float3(x, y, z);
        }

        float4 KeyToPosition(in OcTreeKey key, int depth) =>
            new float4(KeyToCoord(key, depth).Ros2Unity(), sizeLookupTable[depth]);

        public LeafIterator EnumerateLeaves(sbyte[] src, uint offset, uint valueStride, int maxDepth = 16)
        {
            return new LeafIterator(this, src, offset, valueStride, maxDepth);
        }

        public struct LeafIterator
        {
            const float MinProbForOccupied = 0.5f;
            static readonly float MinLogOdds = LogOdds(MinProbForOccupied);

            readonly OctreeHelper parent;
            readonly Stack<NodeIterator> stack;
            readonly int maxDepth;
            readonly uint strideAfterValue;
            Reader reader;
            float4 current;

            public LeafIterator(OctreeHelper parent, sbyte[] src, uint offset, uint valueStride, int maxDepth)
            {
                this.parent = parent;
                stack = new Stack<NodeIterator>(TreeMaxDepth);
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
                    if (currentIt.depth >= TreeMaxDepth)
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
            public LeafIterator GetEnumerator() => this;
        }


        public BinaryLeafIterator EnumerateLeavesBinary(sbyte[] src, uint offset, int maxDepth = 16)
        {
            return new BinaryLeafIterator(this, src, offset, maxDepth);
        }

        public struct BinaryLeafIterator
        {
            readonly OctreeHelper parent;
            readonly Stack<BinNodeIterator> stack;
            readonly int maxDepth;
            Reader reader;
            float4 current;

            public BinaryLeafIterator(OctreeHelper parent, sbyte[] src, uint offset, int maxDepth)
            {
                this.parent = parent;
                stack = new Stack<BinNodeIterator>(TreeMaxDepth);
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
                    if (currentIt.depth >= TreeMaxDepth)
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
            public BinaryLeafIterator GetEnumerator() => this;
        }

        struct Reader
        {
            readonly sbyte[] buffer;
            uint offset;

            public Reader(sbyte[] buffer, uint offset)
            {
                this.buffer = buffer;
                this.offset = offset;
            }

            public float ReadFloat()
            {
                byte b0 = (byte) buffer[offset++];
                byte b1 = (byte) buffer[offset++];
                byte b2 = (byte) buffer[offset++];
                byte b3 = (byte) buffer[offset++];

                int tmp = (b3 << 24) + (b2 << 16) + (b1 << 8) + b0;
                return Int32ToSingleBits(tmp);
            }

            static unsafe float Int32ToSingleBits(int bits) => *(float*) &bits;
            public int Size => buffer.Length;
            public void Skip(uint value) => offset += value;
            public sbyte ReadByte() => buffer[offset++];

            public ushort ReadUshort()
            {
                byte b0 = (byte) buffer[offset++];
                byte b1 = (byte) buffer[offset++];
                return (ushort) ((b0 << 0) + (b1 << 8));
            }
        }

        struct NodeIterator
        {
            public readonly OcTreeKey key;
            public readonly int depth;
            int position;
            readonly sbyte bitset;

            NodeIterator(int depth, int position, in OcTreeKey key, sbyte bitset)
            {
                this.depth = depth;
                this.position = position;
                this.key = key;
                this.bitset = bitset;
            }

            public static NodeIterator Start(sbyte bitset)
            {
                return new NodeIterator(0, -1, new OcTreeKey(TreeMaxVal), bitset);
            }

            public bool MoveNext()
            {
                if (position >= 7)
                {
                    return false;
                }

                position++;
                return true;
            }

            public bool HasChild => (bitset & (1 << position)) != 0;
            public bool IsLeaf => bitset == 0;

            public void CreateChild(sbyte childBitset, out NodeIterator childIt)
            {
                childIt = new NodeIterator(depth + 1, -1,
                    key.ComputeChildKey(position, TreeMaxVal >> (depth + 1)),
                    childBitset);
            }
        }

        struct BinNodeIterator
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
                return new BinNodeIterator(0, -2, new OcTreeKey(TreeMaxVal), bitset);
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

            public OcTreeKey ChildKey => key.ComputeChildKey(position / 2, TreeMaxVal >> (depth + 1));

            public void CreateChild(ushort childBitset, out BinNodeIterator childIt)
            {
                childIt = new BinNodeIterator(depth + 1, -2, ChildKey, childBitset);
            }
        }


        readonly struct OcTreeKey
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

    public class MalformedOctreeException : Exception
    {
        public MalformedOctreeException()
            : base("Depth reached a value greater than 16. " +
                   "Either the tree is malformed, or there is an error in the implementation.")
        {
        }
    }
}