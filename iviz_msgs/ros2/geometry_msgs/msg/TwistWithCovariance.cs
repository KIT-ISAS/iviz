/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class TwistWithCovariance : IDeserializable<TwistWithCovariance>, IMessageRos2
    {
        // This expresses velocity in free space with uncertainty.
        [DataMember (Name = "twist")] public Twist Twist;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public TwistWithCovariance()
        {
            Twist = new Twist();
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public TwistWithCovariance(Twist Twist, double[] Covariance)
        {
            this.Twist = Twist;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        public TwistWithCovariance(ref ReadBuffer2 b)
        {
            Twist = new Twist(ref b);
            b.DeserializeStructArray(36, out Covariance);
        }
        
        public TwistWithCovariance RosDeserialize(ref ReadBuffer2 b) => new TwistWithCovariance(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Twist.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Twist is null) BuiltIns.ThrowNullReference();
            Twist.RosValidate();
            if (Covariance is null) BuiltIns.ThrowNullReference();
            if (Covariance.Length != 36) BuiltIns.ThrowInvalidSizeForFixedArray(Covariance.Length, 36);
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 336;
        
        public void GetRosMessageLength(ref int c)
        {
            Twist.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Covariance, 36);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistWithCovariance";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
