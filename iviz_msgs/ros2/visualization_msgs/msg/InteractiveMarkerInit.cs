/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerInit : IDeserializable<InteractiveMarkerInit>, IMessageRos2
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId;
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        [DataMember (Name = "seq_num")] public ulong SeqNum;
        // All markers.
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers;
    
        /// Constructor for empty message.
        public InteractiveMarkerInit()
        {
            ServerId = "";
            Markers = System.Array.Empty<InteractiveMarker>();
        }
        
        /// Explicit constructor.
        public InteractiveMarkerInit(string ServerId, ulong SeqNum, InteractiveMarker[] Markers)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerInit(ref ReadBuffer2 b)
        {
            b.DeserializeString(out ServerId);
            b.Deserialize(out SeqNum);
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
        }
        
        public InteractiveMarkerInit RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerInit(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (ServerId is null) BuiltIns.ThrowNullReference();
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, ServerId);
            WriteBuffer2.Advance(ref c, SeqNum);
            WriteBuffer2.Advance(ref c, Markers);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerInit";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
