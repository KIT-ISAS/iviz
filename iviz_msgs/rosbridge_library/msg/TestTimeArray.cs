/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/TestTimeArray")]
    public sealed class TestTimeArray : IDeserializable<TestTimeArray>, IMessage
    {
        [DataMember (Name = "times")] public time[] Times;
    
        /// <summary> Constructor for empty message. </summary>
        public TestTimeArray()
        {
            Times = System.Array.Empty<time>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestTimeArray(time[] Times)
        {
            this.Times = Times;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestTimeArray(ref Buffer b)
        {
            Times = b.DeserializeStructArray<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestTimeArray(ref b);
        }
        
        TestTimeArray IDeserializable<TestTimeArray>.RosDeserialize(ref Buffer b)
        {
            return new TestTimeArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Times, 0);
        }
        
        public void RosValidate()
        {
            if (Times is null) throw new System.NullReferenceException(nameof(Times));
        }
    
        public int RosMessageLength => 4 + 8 * Times.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestTimeArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivJzE2NjlUoAVLFvFwARiS1Xg4AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
