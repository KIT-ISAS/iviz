/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestTimeArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE2NjlUoAVLFXAD3rdP6DQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
