/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterEvent : IDeserializable<ParameterEvent>, IMessage
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
    
        public ParameterEvent()
        {
            Node = "";
            NewParameters = System.Array.Empty<Parameter>();
            ChangedParameters = System.Array.Empty<Parameter>();
            DeletedParameters = System.Array.Empty<Parameter>();
        }
        
        public ParameterEvent(ref ReadBuffer b)
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
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ParameterEvent(ref b);
        
        public ParameterEvent RosDeserialize(ref ReadBuffer b) => new ParameterEvent(ref b);
        
        public ParameterEvent RosDeserialize(ref ReadBuffer2 b) => new ParameterEvent(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Stamp);
            b.Serialize(Node);
            b.SerializeArray(NewParameters);
            b.SerializeArray(ChangedParameters);
            b.SerializeArray(DeletedParameters);
        }
        
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
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += WriteBuffer.GetStringSize(Node);
                size += WriteBuffer.GetArraySize(NewParameters);
                size += WriteBuffer.GetArraySize(ChangedParameters);
                size += WriteBuffer.GetArraySize(DeletedParameters);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Stamp);
            WriteBuffer2.AddLength(ref c, Node);
            WriteBuffer2.AddLength(ref c, NewParameters);
            WriteBuffer2.AddLength(ref c, ChangedParameters);
            WriteBuffer2.AddLength(ref c, DeletedParameters);
        }
    
        public const string MessageType = "rcl_interfaces/ParameterEvent";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "502643898464d39f99483f2f85733689";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/jNhC9+1cMNod+QFEX+XJaoIcs4BY57CZI3L0UhZeWRhYBilRJKq776/uG+rAS" +
                "bHZ7KJpDZJEzb2bevBmd0LrWgRoOQe2YCmej0jaQolZ51XBkT/zENuaLE3rHheoCU6z55TXtFZwsqega" +
                "XVDXlipyBpjQcqErHB0dLJ5AK2DurDnQlknLTyZXJexYe2YKHEO+gOFajnSDk6ialvY1W5gg65c5uKLo" +
                "vOcyX2w7baK2G21xW6mCww/rCUEwf+kMIv/ZKYPkuKSHu0fAxZqiI+tKzhchem136UUcPvD+GC8gvopU" +
                "qydG9kgHuVLlfJ9W738/Gv/+B1neb47OAnf/KlRRK7tDRmWX4ifEoQNzyMHsX8OWbHD+VdjB7Bnsz//x" +
                "3+L9468/kS/MvD1TEqnhSE6HpIVRmehL4Zqms7qAsub6zOk2irXIqZVeRN8VEKRGN3HGtmugL8AKXMmh" +
                "8LqN0iuHthkW01oXNT0p03ECKqJ+4kl6FaSSNDvK8xh5EokoejCfbr8JA2QPL3KH0geZB3DvlaF4aDlk" +
                "eJWJ+DSR8FEc8ybsPqGEcnaxhn06n7UtGfex/u9mpdDI/LcAaYHQUg4bbceiB8Ys/xXp+01PB+bNlGDZ" +
                "pxHP56pN1d3fPNy8X61XD5sPd+vN42qN7pWp64Oon68fWTzWRcEC1Le6op2Lke13hB6jm1CM1VFj0P+W" +
                "zXBCKec+vmgDbZC5wAb6LMvj/sFJX81872Rjbx3WTmidRV1gQRJUbetd67WINVWM5hu3zxcdeLxOeIL9" +
                "5qOCjY1vINuDkQXlvMj9pdT69uZ0JwtTbnouj4Ehw0HiKdU+5F5DumkHwFxLkVhSjYra2VTZO+cMQ5cJ" +
                "LBslypgcRMQYsXBYKRMwDVvYkvzbDEo7oVsoYjfmRh4LSYah8q6h0x+zs7Pz7Hx5lr09v8quLy+y5fIy" +
                "u36L0h1cX7te5gvwc3VBusc+Bruh0nVbUNR6fFECSqDKONSCkK2D+ZBG5Qx4ltPb1WpFy8uLfJEMAdoj" +
                "zDEjlImPwDipsjOsQwjZAQXODdsdzoxudJzGvX/MYPDd814dpGvbQ5SB7mQg5ItgnT0dgzyjXwyxcOWx" +
                "Sd6v4M1bFPo2iJs04gtuVxenWx1HFifvxC3cR3a/jvA5jsPEqHwxek6/gDSwNrr2r/AcaHzm+Q9rUpXd" +
                "kAgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
