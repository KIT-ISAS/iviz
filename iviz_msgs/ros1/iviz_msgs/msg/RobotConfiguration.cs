/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class RobotConfiguration : IDeserializable<RobotConfiguration>, IHasSerializer<RobotConfiguration>, IMessage
    {
        [DataMember (Name = "source_parameter")] public string SourceParameter;
        [DataMember (Name = "saved_robot_name")] public string SavedRobotName;
        [DataMember (Name = "frame_prefix")] public string FramePrefix;
        [DataMember (Name = "frame_suffix")] public string FrameSuffix;
        [DataMember (Name = "attached_to_tf")] public bool AttachedToTf;
        [DataMember (Name = "render_as_occlusion_only")] public bool RenderAsOcclusionOnly;
        [DataMember (Name = "tint")] public StdMsgs.ColorRGBA Tint;
        [DataMember (Name = "metallic")] public float Metallic;
        [DataMember (Name = "smoothness")] public float Smoothness;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "visible")] public bool Visible;
    
        public RobotConfiguration()
        {
            SourceParameter = "";
            SavedRobotName = "";
            FramePrefix = "";
            FrameSuffix = "";
            Id = "";
        }
        
        public RobotConfiguration(ref ReadBuffer b)
        {
            b.DeserializeString(out SourceParameter);
            b.DeserializeString(out SavedRobotName);
            b.DeserializeString(out FramePrefix);
            b.DeserializeString(out FrameSuffix);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.DeserializeString(out Id);
            b.Deserialize(out Visible);
        }
        
        public RobotConfiguration(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out SourceParameter);
            b.Align4();
            b.DeserializeString(out SavedRobotName);
            b.Align4();
            b.DeserializeString(out FramePrefix);
            b.Align4();
            b.DeserializeString(out FrameSuffix);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
            b.Align4();
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.DeserializeString(out Id);
            b.Deserialize(out Visible);
        }
        
        public RobotConfiguration RosDeserialize(ref ReadBuffer b) => new RobotConfiguration(ref b);
        
        public RobotConfiguration RosDeserialize(ref ReadBuffer2 b) => new RobotConfiguration(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(SourceParameter);
            b.Serialize(SavedRobotName);
            b.Serialize(FramePrefix);
            b.Serialize(FrameSuffix);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
            b.Serialize(in Tint);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(Id);
            b.Serialize(Visible);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(SourceParameter);
            b.Serialize(SavedRobotName);
            b.Serialize(FramePrefix);
            b.Serialize(FrameSuffix);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
            b.Serialize(in Tint);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(Id);
            b.Serialize(Visible);
        }
        
        public void RosValidate()
        {
            if (SourceParameter is null) BuiltIns.ThrowNullReference();
            if (SavedRobotName is null) BuiltIns.ThrowNullReference();
            if (FramePrefix is null) BuiltIns.ThrowNullReference();
            if (FrameSuffix is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 47;
                size += WriteBuffer.GetStringSize(SourceParameter);
                size += WriteBuffer.GetStringSize(SavedRobotName);
                size += WriteBuffer.GetStringSize(FramePrefix);
                size += WriteBuffer.GetStringSize(FrameSuffix);
                size += WriteBuffer.GetStringSize(Id);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SourceParameter);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SavedRobotName);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, FramePrefix);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, FrameSuffix);
            size += 1; // AttachedToTf
            size += 1; // RenderAsOcclusionOnly
            size = WriteBuffer2.Align4(size);
            size += 16; // Tint
            size += 4; // Metallic
            size += 4; // Smoothness
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Visible
            return size;
        }
    
        public const string MessageType = "iviz_msgs/RobotConfiguration";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3794bb74650c9f56ddc4ca7447266716";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE62OMQ7CMAxF95wiN0CCDYkBGDqxwAEsN3VKpDRGtlvB7SmCRkKsePp6tr6fmqTSe+VR" +
                "AsENBQcyEqcfjhN1INyyQZlXC4+vO7gJxXT/ZjrGF2uZs0czDNe5wBgsvplQ6UgAFTiEPGriAlzyY27p" +
                "YNBeV0fOLOfmsPeWirmYGW2z9rMX5pxCBTow27WQ6mKQuvePKWlqMznndn8ed7o0W/+rWqWkpr6mtiZ0" +
                "7gmdyuiccQEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<RobotConfiguration> CreateSerializer() => new Serializer();
        public Deserializer<RobotConfiguration> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<RobotConfiguration>
        {
            public override void RosSerialize(RobotConfiguration msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(RobotConfiguration msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(RobotConfiguration msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(RobotConfiguration msg) => msg.Ros2MessageLength;
            public override void RosValidate(RobotConfiguration msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<RobotConfiguration>
        {
            public override void RosDeserialize(ref ReadBuffer b, out RobotConfiguration msg) => msg = new RobotConfiguration(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out RobotConfiguration msg) => msg = new RobotConfiguration(ref b);
        }
    }
}
