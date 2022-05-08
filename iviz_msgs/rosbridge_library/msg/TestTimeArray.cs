/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract]
    public sealed class TestTimeArray : IDeserializable<TestTimeArray>, IMessage
    {
        [DataMember (Name = "times")] public time[] Times;
    
        /// Constructor for empty message.
        public TestTimeArray()
        {
            Times = System.Array.Empty<time>();
        }
        
        /// Explicit constructor.
        public TestTimeArray(time[] Times)
        {
            this.Times = Times;
        }
        
        /// Constructor with buffer.
        public TestTimeArray(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Times);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestTimeArray(ref b);
        
        public TestTimeArray RosDeserialize(ref ReadBuffer b) => new TestTimeArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Times);
        }
        
        public void RosValidate()
        {
            if (Times is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 8 * Times.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestTimeArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvJzE2NjlUoAVLFXAD3rdP6DQAAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
