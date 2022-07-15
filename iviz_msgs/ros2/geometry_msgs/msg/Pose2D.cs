/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class Pose2D : IDeserializable<Pose2D>, IMessageRos2
    {
        // Deprecated as of Foxy and will potentially be removed in any following release.
        // Please use the full 3D pose.
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.# This expresses a position and orientation on a 2D manifold.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "theta")] public double Theta;
    
        /// Constructor for empty message.
        public Pose2D()
        {
        }
        
        /// Explicit constructor.
        public Pose2D(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
        
        /// Constructor with buffer.
        public Pose2D(ref ReadBuffer2 b)
        {
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out Theta);
        }
        
        public Pose2D RosDeserialize(ref ReadBuffer2 b) => new Pose2D(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, X);
            WriteBuffer2.Advance(ref c, Y);
            WriteBuffer2.Advance(ref c, Theta);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Pose2D";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
