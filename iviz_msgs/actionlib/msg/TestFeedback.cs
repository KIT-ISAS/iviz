/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestFeedback")]
    public sealed class TestFeedback : IDeserializable<TestFeedback>, IFeedback<TestActionFeedback>
    {
        [DataMember (Name = "feedback")] public int Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestFeedback()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestFeedback(int Feedback)
        {
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestFeedback(ref Buffer b)
        {
            Feedback = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestFeedback(ref b);
        }
        
        TestFeedback IDeserializable<TestFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TestFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Feedback);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "49ceb5b32ea3af22073ede4a0328249e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhLTU1JSkzO5uICAIj0iCsQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
