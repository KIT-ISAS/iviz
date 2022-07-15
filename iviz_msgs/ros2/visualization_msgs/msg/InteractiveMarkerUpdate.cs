/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerUpdate : IDeserializable<InteractiveMarkerUpdate>, IMessageRos2
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId;
        // Sequence number.
        // The client will use this to detect if it has missed an update.
        [DataMember (Name = "seq_num")] public ulong SeqNum;
        // Type holds the purpose of this message.  It must be one of UPDATE or KEEP_ALIVE.
        // UPDATE: Incremental update to previous state.
        //         The sequence number must be 1 higher than for
        //         the previous update.
        // KEEP_ALIVE: Indicates the that the server is still living.
        //             The sequence number does not increase.
        //             No payload data should be filled out (markers, poses, or erases).
        public const byte KEEP_ALIVE = 0;
        public const byte UPDATE = 1;
        [DataMember (Name = "type")] public byte Type;
        // Note: No guarantees on the order of processing.
        //       Contents must be kept consistent by sender.
        // Markers to be added or updated
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers;
        // Poses of markers that should be moved
        [DataMember (Name = "poses")] public InteractiveMarkerPose[] Poses;
        // Names of markers to be erased
        [DataMember (Name = "erases")] public string[] Erases;
    
        /// Constructor for empty message.
        public InteractiveMarkerUpdate()
        {
            ServerId = "";
            Markers = System.Array.Empty<InteractiveMarker>();
            Poses = System.Array.Empty<InteractiveMarkerPose>();
            Erases = System.Array.Empty<string>();
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerUpdate(ref ReadBuffer2 b)
        {
            b.DeserializeString(out ServerId);
            b.Deserialize(out SeqNum);
            b.Deserialize(out Type);
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
            b.DeserializeArray(out Poses);
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new InteractiveMarkerPose(ref b);
            }
            b.DeserializeStringArray(out Erases);
        }
        
        public InteractiveMarkerUpdate RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerUpdate(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Type);
            b.SerializeArray(Markers);
            b.SerializeArray(Poses);
            b.SerializeArray(Erases);
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
            if (Poses is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) BuiltIns.ThrowNullReference(nameof(Poses), i);
                Poses[i].RosValidate();
            }
            if (Erases is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Erases.Length; i++)
            {
                if (Erases[i] is null) BuiltIns.ThrowNullReference(nameof(Erases), i);
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, ServerId);
            WriteBuffer2.Advance(ref c, SeqNum);
            WriteBuffer2.Advance(ref c, Type);
            WriteBuffer2.Advance(ref c, Markers);
            WriteBuffer2.Advance(ref c, Poses);
            WriteBuffer2.Advance(ref c, Erases);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerUpdate";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
