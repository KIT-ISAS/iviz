/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class MarkerArray : IDeserializable<MarkerArray>, IMessageRos2
    {
        [DataMember (Name = "markers")] public Marker[] Markers;
    
        /// Constructor for empty message.
        public MarkerArray()
        {
            Markers = System.Array.Empty<Marker>();
        }
        
        /// Explicit constructor.
        public MarkerArray(Marker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        public MarkerArray(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
        }
        
        public MarkerArray RosDeserialize(ref ReadBuffer2 b) => new MarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Markers);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/MarkerArray";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
