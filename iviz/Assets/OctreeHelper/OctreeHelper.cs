using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Octree
{
    public sealed class OctreeHelper
    {
        public const int TreeMaxVal = short.MaxValue + 1;
        public const int TreeMaxDepth = 16;

        readonly float[] invLookupTable;
        readonly float[] sizeLookupTable;
        readonly float resolution;

        public float Resolution => resolution;

        public static float Probability(float logOdds) => 1 - 1 / (1 - Mathf.Exp(logOdds));
        public static float LogOdds(float probability) => Mathf.Log(1 / (1 - probability) - 1);

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

        internal void KeyToPosition(in OcTreeKey key, int depth, out float4 position) =>
            position = new float4(KeyToCoord(key, depth), sizeLookupTable[depth]);

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
        /// An <see cref="IEnumerable"/> of type <see cref="float4"/> that traverses the octomap and returns the leaves.
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
        /// An <see cref="IEnumerable"/> of type <see cref="float4"/> that traverses the octomap and returns the leaves.
        /// Each node is represented by four values: cube center (x, y, z) in ROS coords and cube size.
        /// </returns>
        public BinaryLeafEnumerator EnumerateLeavesBinary(sbyte[] src, uint offset, int maxDepth = 16)
        {
            return new BinaryLeafEnumerator(this, src, offset, maxDepth);
        }
    }
}