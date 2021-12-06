/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsResult : IDeserializable<TwoIntsResult>, IResult<TwoIntsActionResult>
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
        internal TwoIntsResult(ref ReadBuffer b)
        {
            Sum = b.Deserialize<long>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsResult(ref b);
        
        public TwoIntsResult RosDeserialize(ref ReadBuffer b) => new TwoIntsResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Sum);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b88405221c77b1878a3cbbfff53428d7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUSguzeUCAHohbPwKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
