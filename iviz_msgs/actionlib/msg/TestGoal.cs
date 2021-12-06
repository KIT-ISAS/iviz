/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal TestGoal(ref ReadBuffer b)
        {
            Goal = b.Deserialize<int>();
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
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "18df0149936b7aa95588e3862476ebde";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkjPT8zhAgAyerI5CwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
