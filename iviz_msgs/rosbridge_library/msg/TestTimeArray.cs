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
        internal TestTimeArray(ref Buffer b)
        {
            Times = b.DeserializeStructArray<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestTimeArray(ref b);
        
        TestTimeArray IDeserializable<TestTimeArray>.RosDeserialize(ref Buffer b) => new TestTimeArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Times);
        }
        
        public void RosValidate()
        {
            if (Times is null) throw new System.NullReferenceException(nameof(Times));
        }
    
        public int RosMessageLength => 4 + 8 * Times.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_library/TestTimeArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivJzE2NjlUoAVLFXAD3rdP6DQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
