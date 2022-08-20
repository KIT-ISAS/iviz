/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class IntegerRange : IDeserializable<IntegerRange>, IMessage
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
    
        public IntegerRange()
        {
        }
        
        public IntegerRange(long FromValue, long ToValue, ulong Step)
        {
            this.FromValue = FromValue;
            this.ToValue = ToValue;
            this.Step = Step;
        }
        
        public IntegerRange(ref ReadBuffer b)
        {
            b.Deserialize(out FromValue);
            b.Deserialize(out ToValue);
            b.Deserialize(out Step);
        }
        
        public IntegerRange(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out FromValue);
            b.Deserialize(out ToValue);
            b.Deserialize(out Step);
        }
        
        public IntegerRange RosDeserialize(ref ReadBuffer b) => new IntegerRange(ref b);
        
        public IntegerRange RosDeserialize(ref ReadBuffer2 b) => new IntegerRange(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FromValue);
            b.Serialize(ToValue);
            b.Serialize(Step);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(FromValue);
            b.Serialize(ToValue);
            b.Serialize(Step);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 24;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "rcl_interfaces/IntegerRange";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a7c4f01534dc15eed16bcb5b61ffc96f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE42TsW7jMBBEe33FIm4FATHiFO6uuCLt5QMOtLlyCNCkQi4tOEH+PUPSluQUd64sUbsz" +
                "O2/pFf3hIXBkJ5F2PjkdSTlNiqLwQCdlE1PvAw7JOOEDB5LzwJoGFdSRhUPXNCt6FRVkUY4no+t7bNG5" +
                "tymaE3cNRJ6fqA/++Ld8zc2/nb6/Vfzc+Go+mHx/ackTIwTLyOxI3rjYlDjia7iuWaHt1zIc2j84eDLH" +
                "wRpGetp7J8YlnyIF5Q5XB4zT0YtmZe25LfJZBXKjT1bDlyzHiA9AhRj8npTNxrlSmyjK7flmuoq7JRVp" +
                "ZGvzr3LQ4xMKjsmKGWwx/6/CLgk5NjjCogJTgLkJfIn7UhXULnqbhOfc1wxkIlkVym7/NT20lu4y+mkC" +
                "vC9HotEgEJDkI+/s+WarHXF36Mj0EMwFlTKG0Nwbh7sFEp/zFdnSYzutfUvrtgy9pc3XbFvkIVcNJvvH" +
                "sv01ONAMYoo8resuwDdFJkLPecF1+bmpLL8A8iD+oerUqZQd1TlfU7QuoLR58ZXKfUzWSyabK5N1ZXIB" +
                "u4Q+MUHjU1u4bLom1T9VucnNN4eXWvYNBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
