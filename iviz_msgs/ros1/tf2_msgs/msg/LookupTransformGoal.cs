/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformGoal : IHasSerializer<LookupTransformGoal>, IMessage, IGoal<LookupTransformActionGoal>
    {
        //Simple API
        [DataMember (Name = "target_frame")] public string TargetFrame;
        [DataMember (Name = "source_frame")] public string SourceFrame;
        [DataMember (Name = "source_time")] public time SourceTime;
        [DataMember (Name = "timeout")] public duration Timeout;
        //Advanced API
        [DataMember (Name = "target_time")] public time TargetTime;
        [DataMember (Name = "fixed_frame")] public string FixedFrame;
        //Whether or not to use the advanced API
        [DataMember (Name = "advanced")] public bool Advanced;
    
        public LookupTransformGoal()
        {
            TargetFrame = "";
            SourceFrame = "";
            FixedFrame = "";
        }
        
        public LookupTransformGoal(ref ReadBuffer b)
        {
            TargetFrame = b.DeserializeString();
            SourceFrame = b.DeserializeString();
            b.Deserialize(out SourceTime);
            b.Deserialize(out Timeout);
            b.Deserialize(out TargetTime);
            FixedFrame = b.DeserializeString();
            b.Deserialize(out Advanced);
        }
        
        public LookupTransformGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            TargetFrame = b.DeserializeString();
            b.Align4();
            SourceFrame = b.DeserializeString();
            b.Align4();
            b.Deserialize(out SourceTime);
            b.Deserialize(out Timeout);
            b.Deserialize(out TargetTime);
            FixedFrame = b.DeserializeString();
            b.Deserialize(out Advanced);
        }
        
        public LookupTransformGoal RosDeserialize(ref ReadBuffer b) => new LookupTransformGoal(ref b);
        
        public LookupTransformGoal RosDeserialize(ref ReadBuffer2 b) => new LookupTransformGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TargetFrame);
            b.Serialize(SourceFrame);
            b.Serialize(SourceTime);
            b.Serialize(Timeout);
            b.Serialize(TargetTime);
            b.Serialize(FixedFrame);
            b.Serialize(Advanced);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(TargetFrame);
            b.Serialize(SourceFrame);
            b.Serialize(SourceTime);
            b.Serialize(Timeout);
            b.Serialize(TargetTime);
            b.Serialize(FixedFrame);
            b.Serialize(Advanced);
        }
        
        public void RosValidate()
        {
            if (TargetFrame is null) BuiltIns.ThrowNullReference();
            if (SourceFrame is null) BuiltIns.ThrowNullReference();
            if (FixedFrame is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 37;
                size += WriteBuffer.GetStringSize(TargetFrame);
                size += WriteBuffer.GetStringSize(SourceFrame);
                size += WriteBuffer.GetStringSize(FixedFrame);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, TargetFrame);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SourceFrame);
            size = WriteBuffer2.Align4(size);
            size += 8; // SourceTime
            size += 8; // Timeout
            size += 8; // TargetTime
            size = WriteBuffer2.AddLength(size, FixedFrame);
            size += 1; // Advanced
            return size;
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "35e3720468131d675a18bb6f3e5f22f8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEzWMQQqAMAwE7/uK/kyiTUvANpCm4vOlGm+7wzAYbtJrcrLKvhWjxgg2dNrBwVwa/2Rt" +
                "5Gnkoj2tp9OBz4nS60SoyM05OsCueibKF/WDM/AACrcOXIIAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LookupTransformGoal> CreateSerializer() => new Serializer();
        public Deserializer<LookupTransformGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LookupTransformGoal>
        {
            public override void RosSerialize(LookupTransformGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(LookupTransformGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(LookupTransformGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(LookupTransformGoal msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(LookupTransformGoal msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<LookupTransformGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out LookupTransformGoal msg) => msg = new LookupTransformGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out LookupTransformGoal msg) => msg = new LookupTransformGoal(ref b);
        }
    }
}
