/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Feedback : IHasSerializer<Feedback>, IMessage
    {
        public const byte TYPE_EXPIRED = 0;
        public const byte TYPE_BUTTON_CLICK = 1;
        public const byte TYPE_MENUENTRY_CLICK = 2;
        public const byte TYPE_POSITION_CHANGED = 3;
        public const byte TYPE_ORIENTATION_CHANGED = 4;
        public const byte TYPE_SCALE_CHANGED = 5;
        public const byte TYPE_TRAJECTORY_CHANGED = 6;
        public const byte TYPE_COLLIDER_ENTERED = 7;
        public const byte TYPE_COLLIDER_EXITED = 8;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "viz_id")] public string VizId;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "entry_id")] public int EntryId;
        [DataMember (Name = "angle")] public double Angle;
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "trajectory")] public Trajectory Trajectory;
        [DataMember (Name = "collider_id")] public string ColliderId;
    
        public Feedback()
        {
            VizId = "";
            Id = "";
            Trajectory = new Trajectory();
            ColliderId = "";
        }
        
        public Feedback(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out VizId);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out EntryId);
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new Trajectory(ref b);
            b.DeserializeString(out ColliderId);
        }
        
        public Feedback(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out VizId);
            b.Align4();
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Align4();
            b.Deserialize(out EntryId);
            b.Align8();
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new Trajectory(ref b);
            b.Align4();
            b.DeserializeString(out ColliderId);
        }
        
        public Feedback RosDeserialize(ref ReadBuffer b) => new Feedback(ref b);
        
        public Feedback RosDeserialize(ref ReadBuffer2 b) => new Feedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(EntryId);
            b.Serialize(Angle);
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
            Trajectory.RosSerialize(ref b);
            b.Serialize(ColliderId);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(EntryId);
            b.Serialize(Angle);
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
            Trajectory.RosSerialize(ref b);
            b.Serialize(ColliderId);
        }
        
        public void RosValidate()
        {
            if (VizId is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Trajectory is null) BuiltIns.ThrowNullReference();
            Trajectory.RosValidate();
            if (ColliderId is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 105;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(VizId);
                size += WriteBuffer.GetStringSize(Id);
                size += Trajectory.RosMessageLength;
                size += WriteBuffer.GetStringSize(ColliderId);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, VizId);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Type
            size = WriteBuffer2.Align4(size);
            size += 4; // EntryId
            size = WriteBuffer2.Align8(size);
            size += 8; // Angle
            size += 24; // Position
            size += 32; // Orientation
            size += 24; // Scale
            size = Trajectory.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ColliderId);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Feedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "96a1314c47cca615e02cf92c54cca017";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V34/TOBB+z18x0j6we+oW2OUArcRD6fYg3LLtdQMCIVR5k2niu8QOttNS/vr77LRp" +
                "u/TEPbBUkWp7Zj7Pj2/GjVTuOSUfJ6PZ6MMkno4u6QU9iprt8ct3STK+ng2v4uGfkD3elb0dXb8bXSfT" +
                "j534bFc8Gd/ESeyNXw+uXwXo8135eBrDenBH5cmuys1wcDXaEf6+K0ymgzejYTL293caT3c1huOrq/hy" +
                "NJ3hnlEb3LPD8g9xEsTPo8i6bFbZ3D58zSJjQ0X4w7GRKqeF/DaT2WaHVQvnVjVHWJ2fEStnVl5nXmrh" +
                "nj4hofKSo5x1xV4SsCcaylRrK53U6o7wr0Y4NgoC0kYCTxxQes+p0+acbCqAnhjxdzhYkeuWGy9TXZYS" +
                "MXinoujFT/5Fb29eXdCdrEVHdOOEyoTJCE6LTDhBc41syrxgc1rygksYiarmjILUp9D2YZgU0hK+nBUb" +
                "UZYraiyUnEYgVdUomSI95GTFe/awlIoE1cI4mTalMNDXJpPKq8+NqNij47P8pWGVMsWXF9BRltPGSTi0" +
                "AkJqWFiftfiSQnFRUhjQEX2aavv4c3SULPUpzjkHOTovyBXCea/5a23YeoeFvcBlv7VR9nEJssS4LrN0" +
                "HM5m2NoTwm3whWudFnSMECYrV6D0rmBaCCPFbckeGHUugfrAGz042UFWAVoJpTfwLeL2jv8DqzpcH9Np" +
                "geKVPg22yZFJKNZGL8CijG5XASQtPTWplLdGgGveqr0yOvrDJxtKsAqlwb+wVqcSlchoKV2xoWYoS+Dl" +
                "/dDyQNdtGIZUOSGVDcFsOpH03FModCdyNjeMoGqRctfNX7vVqlt9+zXub+fCJgbDnmwoAxK8Nyz2nfe7" +
                "L9uhgj6s+tEPItqslr8mtvU4OxQYLYJsP6S+b+Q4dJxWaNyKBUqGGdFZwjCTBqYIuQ9UNozAuUfSUabZ" +
                "ktKeC5X4B5AM+ntrUdcAE36IKlu2qcQxTI65n/d7tCxYtVqevmHqhDklUzIyl1lr6TPcGQtaB9cjNz8D" +
                "/cuy9bm9DPQDiNFt4U76FM9ppRta+oCwMOvxqOmWO79C9zqte342riEOvTCYTlbkngDWYTD/sOr3U2rp" +
                "X83g1fad+u5BtPzps+9DtmGWYNMNV3tvz9b3TiCRgy2JxGYoeMf2Gdjzz5E/ztbydoBgbu42Inh6563/" +
                "j9c9+hepgP3rjAkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Feedback> CreateSerializer() => new Serializer();
        public Deserializer<Feedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Feedback>
        {
            public override void RosSerialize(Feedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Feedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Feedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Feedback msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Feedback msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Feedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Feedback msg) => msg = new Feedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Feedback msg) => msg = new Feedback(ref b);
        }
    }
}
