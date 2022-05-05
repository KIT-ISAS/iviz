/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class ExecuteTrajectoryFeedback : IDeserializable<ExecuteTrajectoryFeedback>, IFeedback<ExecuteTrajectoryActionFeedback>
    {
        // The internal state that the move group action currently is in
        [DataMember (Name = "state")] public string State;
    
        /// Constructor for empty message.
        public ExecuteTrajectoryFeedback()
        {
            State = "";
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryFeedback(string State)
        {
            this.State = State;
        }
        
        /// Constructor with buffer.
        public ExecuteTrajectoryFeedback(ref ReadBuffer b)
        {
            b.DeserializeString(out State);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteTrajectoryFeedback(ref b);
        
        public ExecuteTrajectoryFeedback RosDeserialize(ref ReadBuffer b) => new ExecuteTrajectoryFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(State);
        }
        
        public void RosValidate()
        {
            if (State is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(State);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/ExecuteTrajectoryFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PiKi4pysxLVyguSSxJ5QIAXR/O4Q8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
