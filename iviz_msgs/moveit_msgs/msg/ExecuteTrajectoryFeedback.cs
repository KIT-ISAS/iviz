/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryFeedback")]
    public sealed class ExecuteTrajectoryFeedback : IDeserializable<ExecuteTrajectoryFeedback>, IFeedback<ExecuteTrajectoryActionFeedback>
    {
        // The internal state that the move group action currently is in
        [DataMember (Name = "state")] public string State { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryFeedback()
        {
            State = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryFeedback(string State)
        {
            this.State = State;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryFeedback(ref Buffer b)
        {
            State = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryFeedback(ref b);
        }
        
        ExecuteTrajectoryFeedback IDeserializable<ExecuteTrajectoryFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(State);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (State is null) throw new System.NullReferenceException(nameof(State));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(State);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuPiKi4pysxLVyguSSxJ5eUCAAJzrWgQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
