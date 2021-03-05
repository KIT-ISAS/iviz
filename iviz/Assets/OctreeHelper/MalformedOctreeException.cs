using System;

namespace Iviz.Octree
{
    public class MalformedOctreeException : Exception
    {
        public MalformedOctreeException()
            : base("Depth reached a value greater than 16. " +
                   "Either the tree is malformed, or there is an error in the implementation.")
        {
        }
    }
}