
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Pose2D : IMessage 
    {
        // Deprecated
        // Please use the full 3D pose.
        
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        
        
        // This expresses a position and orientation on a 2D manifold.
        
        public double x;
        public double y;
        public double theta;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Pose2D";

        public IMessage Create() => new Pose2D();

        public int GetLength() => 24;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out x, ref ptr, end);
            BuiltIns.Deserialize(out y, ref ptr, end);
            BuiltIns.Deserialize(out theta, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(x, ref ptr, end);
            BuiltIns.Serialize(y, ref ptr, end);
            BuiltIns.Serialize(theta, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "938fa65709584ad8e77d238529be13b8";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE1VSvY7bMAze8xQEbshSZLgW3Qtkue2G9gEYm47ZypIgUcn57ftRygXtYICm+PP98IXO" +
            "kotMbDIfXug9CFehhs9WoaWFQF/PlFOV0wHvb5GuEqVwoNQKoTFtm8SZTVMkrWSpN/Oztfj4KtFGSVpI" +
            "blJ2WzVeieNMSyr0eqaaZdJFJ+Kcg069utLGfwYSZEvKRYGTEP2WaRRoxEYvyIGj9GH400ITh6mFx5hL" +
            "M0rZdOMQdroroHVU5TamA6dG9G4D5NyKo8OeSWpFOLgvdBdaGT2ZoUCQQFPKKtVZgQJUYNsz/jtF+vXW" +
            "CSZsKJQ1S1BA7NshGbVMUWT2TeDQ584NwoKSlIWx2vM5JPtHshP9QOue2n9z7morRRRepMt6CfLpBKhZ" +
            "SqF+6uy5CR53kLBscQH2Y0E2bTmICRS6cdD5C3H1Tcd5gMPAIuo1cNxIbayddVmk9EzMzerg3MzjE73Z" +
            "sXZkkMzUHSmYv3KZn+TcgNdxYwSMDqPZ8GXnO0kpyD6MpXev2uAKX6VvcnER+ReK8Lw/6Ppb0Evh4gbZ" +
            "ygbasNavRYca4AZj4exPz8iHn4QLw45F+yV0LpjwPF+kHOzGUZcUZvQvIbF9/0Yfz2h/RgBnfDj8Bc9+" +
            "JaJlAwAA";

    }
}
