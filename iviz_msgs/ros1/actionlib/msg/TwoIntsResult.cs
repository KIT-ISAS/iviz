/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsResult : IDeserializableRos1<TwoIntsResult>, IDeserializableRos2<TwoIntsResult>, IMessageRos1, IMessageRos2, IResult<TwoIntsActionResult>
    {
        [DataMember (Name = "sum")] public long Sum;
    
        /// Constructor for empty message.
        public TwoIntsResult()
        {
        }
        
        /// Explicit constructor.
        public TwoIntsResult(long Sum)
        {
            this.Sum = Sum;
        }
        
        /// Constructor with buffer.
        public TwoIntsResult(ref ReadBuffer b)
        {
            b.Deserialize(out Sum);
        }
        
        /// Constructor with buffer.
        public TwoIntsResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out Sum);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsResult(ref b);
        
        public TwoIntsResult RosDeserialize(ref ReadBuffer b) => new TwoIntsResult(ref b);
        
        public TwoIntsResult RosDeserialize(ref ReadBuffer2 b) => new TwoIntsResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Sum);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Sum);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Sum);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TwoIntsResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b88405221c77b1878a3cbbfff53428d7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzEzUSguzeUCAHohbPwKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
