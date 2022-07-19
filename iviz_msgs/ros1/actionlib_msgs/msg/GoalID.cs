/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [DataContract]
    public sealed class GoalID : IDeserializableRos1<GoalID>, IMessageRos1
    {
        // The stamp should store the time at which this goal was requested.
        // It is used by an action server when it tries to preempt all
        // goals that were requested before a certain time
        [DataMember (Name = "stamp")] public time Stamp;
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        [DataMember (Name = "id")] public string Id;
    
        /// Constructor for empty message.
        public GoalID()
        {
            Id = "";
        }
        
        /// Explicit constructor.
        public GoalID(time Stamp, string Id)
        {
            this.Stamp = Stamp;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public GoalID(ref ReadBuffer b)
        {
            b.Deserialize(out Stamp);
            b.DeserializeString(out Id);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GoalID(ref b);
        
        public GoalID RosDeserialize(ref ReadBuffer b) => new GoalID(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Stamp);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + WriteBuffer.GetStringSize(Id);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_msgs/GoalID";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz2PS5LDIAxE95yiq7L3PbKfC8ggG1VscJCIK7cf4XHNkqY/Tw/8ZIYa7Qc0174lf9TG" +
                "MJdNdgYZziwxuyKKtdKGkxSN353VOE3hgafB/7pywvwFFVA0qQXK7cPN81wgBmvCCqs4GvN+GGjbPD06" +
                "Xc5jiX36vxozL4OFELkZSbmIwoV1IQdPD35JXlk/kryeHO87Rki1RiFjLMxppvhysuSJxto3w86qtDJO" +
                "sQw9OMoi8e/Am0Cnu91Dt8Gh9q7mZOhF3DUF9bPKOlwh/AJcvpWYTwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public bool Equals(GoalID? other) => ReferenceEquals(this, other) || (other != null && Stamp == other.Stamp && Id == other.Id);
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is GoalID other && Equals(other);
        public override int GetHashCode() => (Stamp, Id).GetHashCode();
        public static bool operator ==(GoalID? left, GoalID? right) => ReferenceEquals(left, right) || !ReferenceEquals(left, null) && left.Equals(right);
        public static bool operator !=(GoalID? left, GoalID? right) => !(left == right);
    }
}
