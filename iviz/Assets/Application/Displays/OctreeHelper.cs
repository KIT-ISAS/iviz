using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Iviz.Msgs;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class OctreeHelper
    {
        const string BinaryFileHeader = "# Octomap OcTree binary file";
        const string FileHeader = "# Octomap OcTree file";
        const int TreeMaxVal = short.MaxValue + 1;
        const int TreeMaxDepth = 16;

        readonly float[] invLookupTable;
        readonly float[] sizeLookupTable;
        readonly float resolution;

        OctreeHelper(float resolution)
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

        Vector3 KeyToCoord(in OcTreeKey key, int depth)
        {
            float il = invLookupTable[depth];
            float sl = sizeLookupTable[depth];
            
            float x = (Mathf.Floor((key.a - TreeMaxVal) * il) + 0.5f) * sl;
            float y = (Mathf.Floor((key.b - TreeMaxVal) * il) + 0.5f) * sl;
            float z = (Mathf.Floor((key.c - TreeMaxVal) * il) + 0.5f) * sl;
            return new Vector3(x, y, z);
        }

        public IEnumerable<OctreeLeaf> GetLeaves(byte[] src, int startOffset = 0, int maxDepth = 16)
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (startOffset < 0)
            {
                throw new IndexOutOfRangeException(nameof(startOffset));
            }

            if (maxDepth > TreeMaxDepth)
            {
                throw new IndexOutOfRangeException(nameof(maxDepth));
            }

            var stack = new Stack<NodeIterator>(16);
            Reader reader = new Reader(src, startOffset);

            float startValue = reader.ReadFloat();
            NodeIterator startIt = NodeIterator.Start(reader.ReadByte());
            if (maxDepth == 0)
            {
                yield return new OctreeLeaf(default, startValue, resolution * (1 << TreeMaxDepth));
                yield break;
            }

            stack.Push(startIt);

            while (stack.Count != 0)
            {
                NodeIterator prevIt = stack.Pop();
                if (!prevIt.CanMoveNext())
                {
                    continue;
                }

                NodeIterator nextIt = prevIt.MoveNext();
                stack.Push(nextIt);

                if (!nextIt.HasChild)
                {
                    continue;
                }

                float value = reader.ReadFloat();
                byte bitset = reader.ReadByte();

                NodeIterator childIt = nextIt.CreateChild(bitset);
                if (!childIt.IsLeaf)
                {
                    stack.Push(childIt);
                }

                if (childIt.IsLeaf && childIt.depth <= maxDepth || childIt.depth == maxDepth)
                {
                    yield return
                        new OctreeLeaf(KeyToCoord(childIt.key, childIt.depth), value, sizeLookupTable[childIt.depth]);
                }
            }
        }

        public static IEnumerable<OctreeLeaf> ReadFromFile(string filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            byte[] chars = File.ReadAllBytes(filename);
            if (chars.Length < FileHeader.Length + 1)
            {
                throw new InvalidOperationException();
            }

            byte magicEnd = chars[FileHeader.Length];
            if (magicEnd != '\n')
            {
                throw new InvalidOperationException();
            }

            string magic = BuiltIns.UTF8.GetString(chars, 0, FileHeader.Length);
            if (magic != FileHeader)
            {
                throw new InvalidOperationException();
            }

            int dataStart = -1;
            for (int i = FileHeader.Length; i < chars.Length; i++)
            {
                if (chars[i] == '\n'
                    && chars[i + 1] == 'd'
                    && chars[i + 2] == 'a'
                    && chars[i + 3] == 't'
                    && chars[i + 4] == 'a'
                    && chars[i + 5] == '\n')
                {
                    dataStart = i + 6;
                    break;
                }
            }

            if (dataStart == -1)
            {
                throw new InvalidOperationException();
            }

            string header = BuiltIns.UTF8.GetString(chars, FileHeader.Length + 1, dataStart - 6 - FileHeader.Length);
            string[] lines = header.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            string id = null;
            float resolution = -1;
            foreach (string line in lines)
            {
                if (line[0] == '#')
                {
                    continue;
                }

                string[] words = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 2)
                {
                    continue;
                }

                switch (words[0])
                {
                    case "id":
                        id = words[0];
                        break;
                    case "res":
                        resolution = float.TryParse(words[1], NumberStyles.Any, BuiltIns.Culture,
                            out float tmpResolution)
                            ? tmpResolution
                            : -1;
                        break;
                }
            }

            if (id == null || resolution < 0)
            {
                throw new InvalidOperationException();
            }

            return new OctreeHelper(resolution).GetLeaves(chars, dataStart);
        }

        struct Reader
        {
            readonly byte[] buffer;
            int offset;

            public Reader(byte[] buffer, int startOffset = 0)
            {
                this.buffer = buffer;
                offset = startOffset;
            }

            public float ReadFloat()
            {
                int tmp = (buffer[offset + 3] << 24)
                          + (buffer[offset + 2] << 16)
                          + (buffer[offset + 1] << 8)
                          + buffer[offset];
                offset += 4;
                return Int32ToSingleBits(tmp);
            }

            static unsafe float Int32ToSingleBits(int bits) => *(float*) &bits;

            public byte ReadByte()
            {
                return buffer[offset++];
            }
        }

        readonly struct NodeIterator
        {
            public readonly OcTreeKey key;
            public readonly byte depth;
            readonly sbyte position;
            readonly byte bitset;

            NodeIterator(byte depth, sbyte position, in OcTreeKey key, byte bitset)
            {
                this.depth = depth;
                this.position = position;
                this.key = key;
                this.bitset = bitset;
            }

            public static NodeIterator Start(byte bitset)
            {
                return new NodeIterator(0, -1, new OcTreeKey(TreeMaxVal), bitset);
            }

            public bool CanMoveNext()
            {
                return position < 7;
            }

            public NodeIterator MoveNext()
            {
                return new NodeIterator(depth, (sbyte) (position + 1), key, bitset);
            }

            public bool HasChild => (bitset & (1 << position)) != 0;
            public bool IsLeaf => bitset == 0;

            public NodeIterator CreateChild(byte childBitset)
            {
                return new NodeIterator((byte) (depth + 1), -1,
                    key.ComputeChildKey(position, TreeMaxVal >> (depth + 1)),
                    childBitset);
            }
        }

        readonly struct OcTreeKey
        {
            public readonly int a;
            public readonly int b;
            public readonly int c;

            OcTreeKey(int a, int b, int c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public OcTreeKey(int val)
            {
                a = val;
                b = val;
                c = val;
            }

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

            public override string ToString()
            {
                return $"[{a.ToString()} {b.ToString()} {c.ToString()}]";
            }
        }
    }

    public readonly struct OctreeLeaf
    {
        public readonly Vector3 Position;
        public readonly float LogOdds;
        public readonly float Size;

        public double Occupancy => Probability(LogOdds);

        static double Probability(double logodds)
        {
            return 1.0 - 1.0 / (1.0 + Math.Exp(logodds));
        }

        public OctreeLeaf(in Vector3 position, float logOdds, float size)
        {
            Position = position;
            LogOdds = logOdds;
            Size = size;
        }
    }
}