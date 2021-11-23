/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryFeedback : IDeserializable<ExecuteTrajectoryFeedback>, IFeedback<ExecuteTrajectoryActionFeedback>
    {
        // The internal state that the move group action currently is in
        [DataMember (Name = "state")] public string State;
    
        /// Constructor for empty message.
        public ExecuteTrajectoryFeedback()
        {
            State = string.Empty;
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryFeedback(string State)
        {
            this.State = State;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryFeedback(ref Buffer b)
        {
            State = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryFeedback(ref b);
        
        ExecuteTrajectoryFeedback IDeserializable<ExecuteTrajectoryFeedback>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(State);
        }
        
        public void RosValidate()
        {
            if (State is null) throw new System.NullReferenceException(nameof(State));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(State);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiKi4pysxLVyguSSxJ5QIAXR/O4Q8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
