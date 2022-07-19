/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class IntegerRange : IDeserializableRos2<IntegerRange>, IMessageRos2
    {
        // Represents bounds and a step value for an integer typed parameter.
        // Start value for valid values, inclusive.
        [DataMember (Name = "from_value")] public long FromValue;
        // End value for valid values, inclusive.
        [DataMember (Name = "to_value")] public long ToValue;
        // Size of valid steps between the from and to bound.
        //
        // A step value of zero implies a continuous range of values. Ideally, the step
        // would be less than or equal to the distance between the bounds, as well as an
        // even multiple of the distance between the bounds, but neither are required.
        //
        // If the absolute value of the step is larger than or equal to the distance
        // between the two bounds, then the bounds will be the only valid values. e.g. if
        // the range is defined as {from_value: 1, to_value: 2, step: 5} then the valid
        // values will be 1 and 2.
        // 
        // If the step is less than the distance between the bounds, but the distance is
        // not a multiple of the step, then the "to" bound will always be a valid value,
        // e.g. if the range is defined as {from_value: 2, to_value: 5, step: 2} then
        // the valid values will be 2, 4, and 5.
        [DataMember (Name = "step")] public ulong Step;
    
        /// Constructor for empty message.
        public IntegerRange()
        {
        }
        
        /// Explicit constructor.
        public IntegerRange(long FromValue, long ToValue, ulong Step)
        {
            this.FromValue = FromValue;
            this.ToValue = ToValue;
            this.Step = Step;
        }
        
        /// Constructor with buffer.
        public IntegerRange(ref ReadBuffer2 b)
        {
            b.Deserialize(out FromValue);
            b.Deserialize(out ToValue);
            b.Deserialize(out Step);
        }
        
        public IntegerRange RosDeserialize(ref ReadBuffer2 b) => new IntegerRange(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(FromValue);
            b.Serialize(ToValue);
            b.Serialize(Step);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, FromValue);
            WriteBuffer2.AddLength(ref c, ToValue);
            WriteBuffer2.AddLength(ref c, Step);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/IntegerRange";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
