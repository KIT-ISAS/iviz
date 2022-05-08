/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestGoal : IDeserializable<TestGoal>, IGoal<TestActionGoal>
    {
        [DataMember (Name = "goal")] public int Goal;
    
        /// Constructor for empty message.
        public TestGoal()
        {
        }
        
        /// Explicit constructor.
        public TestGoal(int Goal)
        {
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        public TestGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Goal);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestGoal(ref b);
        
        public TestGoal RosDeserialize(ref ReadBuffer b) => new TestGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Goal);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "18df0149936b7aa95588e3862476ebde";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkjPT8zhAgAyerI5CwAAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
