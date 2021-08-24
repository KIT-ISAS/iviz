using System;
using System.Collections;

namespace Iviz.Octree
{
    public sealed class OctreeHelper
    {
        public const int TreeMaxVal = short.MaxValue + 1;
        public const int TreeMaxDepth = 16;

        static readonly float[] InvLookupTable;
        readonly float[] sizeLookupTable;

        static OctreeHelper()
        {
            InvLookupTable = new float[TreeMaxDepth + 1];
            for (int i = 0; i < InvLookupTable.Length; i++)
            {
                InvLookupTable[i] = 1.0f / (1 << (TreeMaxDepth - i));
            }
        }
        
        public float Resolution { get; }

        public static float Probability(float logOdds) => 1 - 1 / (1 - Exp(logOdds));
        public static float LogOdds(float probability) => Log(1 / (1 - probability) - 1);

        public OctreeHelper(float resolution)
        {
            Resolution = resolution;

            sizeLookupTable = new float[TreeMaxDepth + 1];
            for (int i = 0; i < sizeLookupTable.Length; i++)
            {
                sizeLookupTable[i] = resolution * (1 << (TreeMaxDepth - i));
            }
        }

        static void KeyToCoord(in OcTreeKey key, int depth, float size, out Float4 f)
        {
            /*
            f.x = (Floor((key.a - TreeMaxVal) * il) + 0.5f) * sl;
            f.y = (Floor((key.b - TreeMaxVal) * il) + 0.5f) * sl;
            f.z = (Floor((key.c - TreeMaxVal) * il) + 0.5f) * sl;
            */
            /*
            f.x = (((key.a - TreeMaxVal) >> shift) + 0.5f) * size;
            f.y = (((key.b - TreeMaxVal) >> shift) + 0.5f) * size;
            f.z = (((key.c - TreeMaxVal) >> shift) + 0.5f) * size;
            */
            /*
            f.x = (Floor((key.a - TreeMaxVal) >> shift) + 0.5f) * size;
            f.y = (Floor((key.b - TreeMaxVal) >> shift) + 0.5f) * size;
            f.z = (Floor((key.c - TreeMaxVal) >> shift) + 0.5f) * size;
            */
            float il = InvLookupTable[depth];
            f.x = (Floor((key.a - TreeMaxVal) * il) + 0.5f) * size;
            f.y = (Floor((key.b - TreeMaxVal) * il) + 0.5f) * size;
            f.z = (Floor((key.c - TreeMaxVal) * il) + 0.5f) * size;
            f.w = size;
        }

        internal void KeyToPosition(in OcTreeKey key, int depth, out Float4 position) =>
            KeyToCoord(key, depth, sizeLookupTable[depth], out position);

        /// <summary>
        /// Creates an iterator that returns the childless nodes before maxDepth
        /// and the nodes at exactly maxDepth (whether they have children or not). 
        /// Usable only on non-binary-encoded octomaps.
        /// </summary>
        /// <param name="src">Array that contains the octomap</param>
        /// <param name="offset">Offset at which to start reading the array</param>
        /// <param name="valueStride">
        /// Number of bytes to read between nodes.
        /// For "OcTree" it's 4 (a float), for "ColorOcTree" it's 7 (a float + 3 bytes).
        /// </param>
        /// <param name="maxDepth">Maximal depth</param>
        /// <returns>
        /// An <see cref="IEnumerable"/> of type <see cref="Float4"/> that traverses the octomap and returns the leaves.
        /// Each node is represented by four values: cube center (x, y, z) in ROS coords and cube size.
        /// </returns>
        public LeafEnumerator EnumerateLeaves(sbyte[] src, uint offset, uint valueStride, int maxDepth = 16)
        {
            return new LeafEnumerator(this, src, offset, valueStride, maxDepth);
        }


        /// <summary>
        /// Creates an iterator that returns the childless fully-occupied nodes before maxDepth
        /// and the nodes at exactly maxDepth (whether they have children or not).
        /// Usable only on binary-encoded octomaps.
        /// </summary>
        /// <param name="src">Array that contains the octomap</param>
        /// <param name="offset">Offset at which to start reading the array</param>
        /// <param name="maxDepth">Maximal depth</param>
        /// <returns>
        /// An <see cref="IEnumerable"/> of type <see cref="Float4"/> that traverses the octomap and returns the leaves.
        /// Each node is represented by four values: cube center (x, y, z) in ROS coords and cube size.
        /// </returns>
        public BinaryLeafEnumerator EnumerateLeavesBinary(sbyte[] src, uint offset, int maxDepth = 16)
        {
            return new BinaryLeafEnumerator(this, src, offset, maxDepth);
        }

        static float Exp(float f)
        {
#if NETSTANDARD2_1_OR_GREATER
            return MathF.Exp(f);
#else
            return (float) Math.Exp(f);
#endif
        }
        
        static float Log(float f)
        {
#if NETSTANDARD2_1_OR_GREATER
            return MathF.Log(f);
#else
            return (float) Math.Log(f);
#endif
        }
        
        static float Floor(float f)
        {
#if NETSTANDARD2_1_OR_GREATER
            return MathF.Floor(f);
#else
            return (float) Math.Floor(f);
#endif
        }        
    }
}