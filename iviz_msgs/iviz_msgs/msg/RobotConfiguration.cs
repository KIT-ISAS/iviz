/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/RobotConfiguration")]
    public sealed class RobotConfiguration : IDeserializable<RobotConfiguration>, IMessage
    {
        [DataMember (Name = "source_parameter")] public string SourceParameter { get; set; }
        [DataMember (Name = "saved_robot_name")] public string SavedRobotName { get; set; }
        [DataMember (Name = "frame_prefix")] public string FramePrefix { get; set; }
        [DataMember (Name = "frame_suffix")] public string FrameSuffix { get; set; }
        [DataMember (Name = "attached_to_tf")] public bool AttachedToTf { get; set; }
        [DataMember (Name = "render_as_occlusion_only")] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember (Name = "tint")] public StdMsgs.ColorRGBA Tint { get; set; }
        [DataMember (Name = "metallic")] public float Metallic { get; set; }
        [DataMember (Name = "smoothness")] public float Smoothness { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "visible")] public bool Visible { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RobotConfiguration()
        {
            SourceParameter = string.Empty;
            SavedRobotName = string.Empty;
            FramePrefix = string.Empty;
            FrameSuffix = string.Empty;
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public RobotConfiguration(string SourceParameter, string SavedRobotName, string FramePrefix, string FrameSuffix, bool AttachedToTf, bool RenderAsOcclusionOnly, in StdMsgs.ColorRGBA Tint, float Metallic, float Smoothness, string Id, bool Visible)
        {
            this.SourceParameter = SourceParameter;
            this.SavedRobotName = SavedRobotName;
            this.FramePrefix = FramePrefix;
            this.FrameSuffix = FrameSuffix;
            this.AttachedToTf = AttachedToTf;
            this.RenderAsOcclusionOnly = RenderAsOcclusionOnly;
            this.Tint = Tint;
            this.Metallic = Metallic;
            this.Smoothness = Smoothness;
            this.Id = Id;
            this.Visible = Visible;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RobotConfiguration(ref Buffer b)
        {
            SourceParameter = b.DeserializeString();
            SavedRobotName = b.DeserializeString();
            FramePrefix = b.DeserializeString();
            FrameSuffix = b.DeserializeString();
            AttachedToTf = b.Deserialize<bool>();
            RenderAsOcclusionOnly = b.Deserialize<bool>();
            Tint = new StdMsgs.ColorRGBA(ref b);
            Metallic = b.Deserialize<float>();
            Smoothness = b.Deserialize<float>();
            Id = b.DeserializeString();
            Visible = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RobotConfiguration(ref b);
        }
        
        RobotConfiguration IDeserializable<RobotConfiguration>.RosDeserialize(ref Buffer b)
        {
            return new RobotConfiguration(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(SourceParameter);
            b.Serialize(SavedRobotName);
            b.Serialize(FramePrefix);
            b.Serialize(FrameSuffix);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
            Tint.RosSerialize(ref b);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(Id);
            b.Serialize(Visible);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (SourceParameter is null) throw new System.NullReferenceException(nameof(SourceParameter));
            if (SavedRobotName is null) throw new System.NullReferenceException(nameof(SavedRobotName));
            if (FramePrefix is null) throw new System.NullReferenceException(nameof(FramePrefix));
            if (FrameSuffix is null) throw new System.NullReferenceException(nameof(FrameSuffix));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 47;
                size += BuiltIns.UTF8.GetByteCount(SourceParameter);
                size += BuiltIns.UTF8.GetByteCount(SavedRobotName);
                size += BuiltIns.UTF8.GetByteCount(FramePrefix);
                size += BuiltIns.UTF8.GetByteCount(FrameSuffix);
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/RobotConfiguration";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3794bb74650c9f56ddc4ca7447266716";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62OMQ7CMAxF95wiN0CCDYkBGDqxwAEsN3VKpDRGtlvB7SmCRkKsePp6tr6fmqTSe+VR" +
                "AsENBQcyEqcfjhN1INyyQZlXC4+vO7gJxXT/ZjrGF2uZs0czDNe5wBgsvplQ6UgAFTiEPGriAlzyY27p" +
                "YNBeV0fOLOfmsPeWirmYGW2z9rMX5pxCBTow27WQ6mKQuvePKWlqMznndn8ed7o0W/+rWqWkpr6mtiZ0" +
                "7gmdyuiccQEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
