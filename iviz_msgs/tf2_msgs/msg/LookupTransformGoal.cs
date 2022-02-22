/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformGoal : IDeserializable<LookupTransformGoal>, IGoal<LookupTransformActionGoal>
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
    
        /// Constructor for empty message.
        public LookupTransformGoal()
        {
            TargetFrame = "";
            SourceFrame = "";
            FixedFrame = "";
        }
        
        /// Explicit constructor.
        public LookupTransformGoal(string TargetFrame, string SourceFrame, time SourceTime, duration Timeout, time TargetTime, string FixedFrame, bool Advanced)
        {
            this.TargetFrame = TargetFrame;
            this.SourceFrame = SourceFrame;
            this.SourceTime = SourceTime;
            this.Timeout = Timeout;
            this.TargetTime = TargetTime;
            this.FixedFrame = FixedFrame;
            this.Advanced = Advanced;
        }
        
        /// Constructor with buffer.
        public LookupTransformGoal(ref ReadBuffer b)
        {
            TargetFrame = b.DeserializeString();
            SourceFrame = b.DeserializeString();
            SourceTime = b.Deserialize<time>();
            Timeout = b.Deserialize<duration>();
            TargetTime = b.Deserialize<time>();
            FixedFrame = b.DeserializeString();
            Advanced = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LookupTransformGoal(ref b);
        
        public LookupTransformGoal RosDeserialize(ref ReadBuffer b) => new LookupTransformGoal(ref b);
    
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
        
        public void RosValidate()
        {
            if (TargetFrame is null) BuiltIns.ThrowNullReference(nameof(TargetFrame));
            if (SourceFrame is null) BuiltIns.ThrowNullReference(nameof(SourceFrame));
            if (FixedFrame is null) BuiltIns.ThrowNullReference(nameof(FixedFrame));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 37;
                size += BuiltIns.GetStringSize(TargetFrame);
                size += BuiltIns.GetStringSize(SourceFrame);
                size += BuiltIns.GetStringSize(FixedFrame);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "35e3720468131d675a18bb6f3e5f22f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEzWMQQqAMAwE7/uK/kyiTUvANpCm4vOlGm+7wzAYbtJrcrLKvhWjxgg2dNrBwVwa/2Rt" +
                "5Gnkoj2tp9OBz4nS60SoyM05OsCueibKF/WDM/AACrcOXIIAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
