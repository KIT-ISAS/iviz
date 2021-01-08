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
        [DataMember (Name = "stamp")] public time Stamp { get; set; }
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        [DataMember (Name = "id")] public string Id { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GoalID()
        {
            Id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalID(time Stamp, string Id)
        {
            this.Stamp = Stamp;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GoalID(ref Buffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalID";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACj2QwW7EIAxE75HyDyPtff+j9/6AA06wGiDFZqP9+5psVHFi8Mw888B3YqhRPqCp9j36" +
                "pTaGuWySGWQ4k4Tkiii2SjtOUjT+7azG8TlPD3wZ/LErRyxvUAEFk1qg3F7cPIALxGBNWGEVR2POh4H2" +
                "fdhHqutpdLGX/4dj4XXQEAI3IykX0zxdZBf1PI2AsYREj60viV5BzvgeRaRag5AxVua4UPhxujgsjbXv" +
                "hsyqtDFOsQQ9OMgq4bPmTaHPO3647gkny13N8dCL+Jj/gvp2ZbvmxvkD9T6TLFoBAAA=";
                
    }
}
