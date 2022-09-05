/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Pose2D : IDeserializable<Pose2D>, IHasSerializer<Pose2D>, IMessage
    {
        // Deprecated
        // Please use the full 3D pose.
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        // This expresses a position and orientation on a 2D manifold.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "theta")] public double Theta;
    
        public Pose2D()
        {
        }
        
        public Pose2D(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
        
        public Pose2D(ref ReadBuffer b)
        {
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out Theta);
        }
        
        public Pose2D(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out Theta);
        }
        
        public Pose2D RosDeserialize(ref ReadBuffer b) => new Pose2D(ref b);
        
        public Pose2D RosDeserialize(ref ReadBuffer2 b) => new Pose2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 24;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Pose2D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "938fa65709584ad8e77d238529be13b8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1VSvY7bMAze8xQEbshSZLgW3Qtkue2G9gEYm47ZypIgUcn57ftRygXtYICm+PP98IXO" +
                "kotMbDIfXug9CFehhs9WoaWFQF/PlFOV0wHvb5GuEqVwoNQKoTFtm8SZTVMkrWSpN/Oztfj4KtFGSVpI" +
                "blJ2WzVeieNMSyr0eqaaZdJFJ+Kcg069utLGfwYSZEvKRYGTEP2WaRRoxEYvyIGj9GH400ITh6mFx5hL" +
                "M0rZdOMQdroroHVU5TamA6dG9G4D5NyKo8OeSWpFOLgvdBdaGT2ZoUCQQFPKKtVZgQJUYNsz/jtF+vXW" +
                "CSZsKJQ1S1BA7NshGbVMUWT2TeDQ584NwoKSlIWx2vM5JPtHshP9QOue2n9z7morRRRepMt6CfLpBKhZ" +
                "SqF+6uy5CR53kLBscQH2Y0E2bTmICRS6cdD5C3H1Tcd5gMPAIuo1cNxIbayddVmk9EzMzerg3MzjE73Z" +
                "sXZkkMzUHSmYv3KZn+TcgNdxYwSMDqPZ8GXnO0kpyD6MpXev2uAKX6VvcnER+ReK8Lw/6Ppb0Evh4gbZ" +
                "ygbasNavRYca4AZj4exPz8iHn4QLw45F+yV0LpjwPF+kHOzGUZcUZvQvIbF9/0Yfz2h/RgBnfDj8Bc9+" +
                "JaJlAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Pose2D> CreateSerializer() => new Serializer();
        public Deserializer<Pose2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Pose2D>
        {
            public override void RosSerialize(Pose2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Pose2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Pose2D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Pose2D msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Pose2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Pose2D msg) => msg = new Pose2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Pose2D msg) => msg = new Pose2D(ref b);
        }
    }
}
