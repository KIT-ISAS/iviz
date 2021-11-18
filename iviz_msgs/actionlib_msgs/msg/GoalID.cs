/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [Preserve, DataContract (Name = "actionlib_msgs/GoalID")]
    public sealed class GoalID : IDeserializable<GoalID>, IMessage
    {
        // The stamp should store the time at which this goal was requested.
        // It is used by an action server when it tries to preempt all
        // goals that were requested before a certain time
        [DataMember (Name = "stamp")] public time Stamp;
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        [DataMember (Name = "id")] public string Id;
    
        /// <summary> Constructor for empty message. </summary>
        public GoalID()
        {
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalID(time Stamp, string Id)
        {
            this.Stamp = Stamp;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GoalID(ref Buffer b)
        {
            Stamp = b.Deserialize<time>();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GoalID(ref b);
        }
        
        GoalID IDeserializable<GoalID>.RosDeserialize(ref Buffer b)
        {
            return new GoalID(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Stamp);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Id);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalID";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACj2PTW7EIAyF90hzhyfNPveYfS/ggBOsJpBiM9HcviaNuuTxfj4/8ZUZarQf0Fz7lvxR" +
                "G8NcNtkZZDizxOyKKNZKG05SNP7prMZpCk+8DP7XlRPmD6iAokktUG5vbp7nAjFYE1ZYxdGY98NA2+bp" +
                "0elyHkvs0//VmHkZLITIzUjKRRQurAs5eHrwS/LK+pbk9eR4nzFCqjUKGWNhTjPFbydLnmisfTPsrEor" +
                "4xTL0IOjLBL/DrwJdLrbPXQbHGrvak6GXsRdU1A/q6zDFR7hFzUY0x9QAQAA";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public bool Equals(GoalID? other) => ReferenceEquals(this, other) || (other != null && Stamp == other.Stamp && Id == other.Id);
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is GoalID other && Equals(other);
        public override int GetHashCode() => (Stamp, Id).GetHashCode();
        public static bool operator ==(GoalID? left, GoalID? right) => ReferenceEquals(left, right) || !ReferenceEquals(left, null) && left.Equals(right);
        public static bool operator !=(GoalID? left, GoalID? right) => !(left == right);
    }
}
