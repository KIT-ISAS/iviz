/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsResult")]
    public sealed class TwoIntsResult : IDeserializable<TwoIntsResult>, IResult<TwoIntsActionResult>
    {
        [DataMember (Name = "sum")] public long Sum;
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsResult(long Sum)
        {
            this.Sum = Sum;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsResult(ref Buffer b)
        {
            Sum = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsResult(ref b);
        }
        
        TwoIntsResult IDeserializable<TwoIntsResult>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Sum);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b88405221c77b1878a3cbbfff53428d7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUSguzeUCAHohbPwKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
