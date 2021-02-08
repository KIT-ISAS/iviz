/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestGoal")]
    public sealed class TestGoal : IDeserializable<TestGoal>, IGoal<TestActionGoal>
    {
        [DataMember (Name = "goal")] public int Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestGoal()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestGoal(int Goal)
        {
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestGoal(ref Buffer b)
        {
            Goal = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestGoal(ref b);
        }
        
        TestGoal IDeserializable<TestGoal>.RosDeserialize(ref Buffer b)
        {
            return new TestGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Goal);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "18df0149936b7aa95588e3862476ebde";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA8vMKzE2UkjPT8zhAgAyerI5CwAAAA==";
                
    }
}
