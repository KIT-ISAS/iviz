/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class ParameterEvent : IDeserializable<ParameterEvent>, IMessageRos2
    {
        // This message contains a parameter event.
        // Because the parameter event was an atomic update, a specific parameter name
        // can only be in one of the three sets.
        // The time stamp when this parameter event occurred.
        [DataMember (Name = "stamp")] public time Stamp;
        // Fully qualified ROS path to node.
        [DataMember (Name = "node")] public string Node;
        // New parameters that have been set for this node.
        [DataMember (Name = "new_parameters")] public Parameter[] NewParameters;
        // Parameters that have been changed during this event.
        [DataMember (Name = "changed_parameters")] public Parameter[] ChangedParameters;
        // Parameters that have been deleted during this event.
        [DataMember (Name = "deleted_parameters")] public Parameter[] DeletedParameters;
    
        /// Constructor for empty message.
        public ParameterEvent()
        {
            Node = "";
            NewParameters = System.Array.Empty<Parameter>();
            ChangedParameters = System.Array.Empty<Parameter>();
            DeletedParameters = System.Array.Empty<Parameter>();
        }
        
        /// Constructor with buffer.
        public ParameterEvent(ref ReadBuffer2 b)
        {
            b.Deserialize(out Stamp);
            b.DeserializeString(out Node);
            b.DeserializeArray(out NewParameters);
            for (int i = 0; i < NewParameters.Length; i++)
            {
                NewParameters[i] = new Parameter(ref b);
            }
            b.DeserializeArray(out ChangedParameters);
            for (int i = 0; i < ChangedParameters.Length; i++)
            {
                ChangedParameters[i] = new Parameter(ref b);
            }
            b.DeserializeArray(out DeletedParameters);
            for (int i = 0; i < DeletedParameters.Length; i++)
            {
                DeletedParameters[i] = new Parameter(ref b);
            }
        }
        
        public ParameterEvent RosDeserialize(ref ReadBuffer2 b) => new ParameterEvent(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Stamp);
            b.Serialize(Node);
            b.SerializeArray(NewParameters);
            b.SerializeArray(ChangedParameters);
            b.SerializeArray(DeletedParameters);
        }
        
        public void RosValidate()
        {
            if (Node is null) BuiltIns.ThrowNullReference();
            if (NewParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < NewParameters.Length; i++)
            {
                if (NewParameters[i] is null) BuiltIns.ThrowNullReference(nameof(NewParameters), i);
                NewParameters[i].RosValidate();
            }
            if (ChangedParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ChangedParameters.Length; i++)
            {
                if (ChangedParameters[i] is null) BuiltIns.ThrowNullReference(nameof(ChangedParameters), i);
                ChangedParameters[i].RosValidate();
            }
            if (DeletedParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < DeletedParameters.Length; i++)
            {
                if (DeletedParameters[i] is null) BuiltIns.ThrowNullReference(nameof(DeletedParameters), i);
                DeletedParameters[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Stamp);
            WriteBuffer2.Advance(ref c, Node);
            WriteBuffer2.Advance(ref c, NewParameters);
            WriteBuffer2.Advance(ref c, ChangedParameters);
            WriteBuffer2.Advance(ref c, DeletedParameters);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/ParameterEvent";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
