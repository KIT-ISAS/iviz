/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceFeedback : IDeserializable<PlaceFeedback>, IFeedback<PlaceActionFeedback>
    {
        // The internal state that the place action currently is in
        [DataMember (Name = "state")] public string State;
    
        /// Constructor for empty message.
        public PlaceFeedback()
        {
            State = string.Empty;
        }
        
        /// Explicit constructor.
        public PlaceFeedback(string State)
        {
            this.State = State;
        }
        
        /// Constructor with buffer.
        public PlaceFeedback(ref ReadBuffer b)
        {
            State = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlaceFeedback(ref b);
        
        public PlaceFeedback RosDeserialize(ref ReadBuffer b) => new PlaceFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiKi4pysxLVyguSSxJ5QIAXR/O4Q8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
