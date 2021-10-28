/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceFeedback")]
    public sealed class PlaceFeedback : IDeserializable<PlaceFeedback>, IFeedback<PlaceActionFeedback>
    {
        // The internal state that the place action currently is in
        [DataMember (Name = "state")] public string State;
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceFeedback()
        {
            State = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceFeedback(string State)
        {
            this.State = State;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceFeedback(ref Buffer b)
        {
            State = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceFeedback(ref b);
        }
        
        PlaceFeedback IDeserializable<PlaceFeedback>.RosDeserialize(ref Buffer b)
        {
            return new PlaceFeedback(ref b);
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
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(State);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "af6d3a99f0fbeb66d3248fa4b3e675fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiKi4pysxLVyguSSxJ5QIAXR/O4Q8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
