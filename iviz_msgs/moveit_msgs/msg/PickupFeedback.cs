/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupFeedback")]
    public sealed class PickupFeedback : IDeserializable<PickupFeedback>, IFeedback<PickupActionFeedback>
    {
        // The internal state that the pickup action currently is in
        [DataMember (Name = "state")] public string State { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupFeedback()
        {
            State = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupFeedback(string State)
        {
            this.State = State;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupFeedback(ref Buffer b)
        {
            State = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupFeedback(ref b);
        }
        
        PickupFeedback IDeserializable<PickupFeedback>.RosDeserialize(ref Buffer b)
        {
            return new PickupFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiKi4pysxLVyguSSxJ5QIAXR/O4Q8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
