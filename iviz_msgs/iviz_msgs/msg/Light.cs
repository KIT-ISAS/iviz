/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Light")]
    public sealed class Light : IDeserializable<Light>, IMessage
    {
        public const byte POINT = 0;
        public const byte DIRECTIONAL = 1;
        public const byte SPOT = 2;
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "cast_shadows")] public bool CastShadows;
        [DataMember (Name = "diffuse")] public Color32 Diffuse;
        [DataMember (Name = "range")] public float Range;
        [DataMember (Name = "position")] public Vector3f Position;
        [DataMember (Name = "direction")] public Vector3f Direction;
        [DataMember (Name = "inner_angle")] public float InnerAngle;
        [DataMember (Name = "outer_angle")] public float OuterAngle;
    
        /// <summary> Constructor for empty message. </summary>
        public Light()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Light(string Name, byte Type, bool CastShadows, in Color32 Diffuse, float Range, in Vector3f Position, in Vector3f Direction, float InnerAngle, float OuterAngle)
        {
            this.Name = Name;
            this.Type = Type;
            this.CastShadows = CastShadows;
            this.Diffuse = Diffuse;
            this.Range = Range;
            this.Position = Position;
            this.Direction = Direction;
            this.InnerAngle = InnerAngle;
            this.OuterAngle = OuterAngle;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Light(ref Buffer b)
        {
            Name = b.DeserializeString();
            Type = b.Deserialize<byte>();
            CastShadows = b.Deserialize<bool>();
            b.Deserialize(out Diffuse);
            Range = b.Deserialize<float>();
            b.Deserialize(out Position);
            b.Deserialize(out Direction);
            InnerAngle = b.Deserialize<float>();
            OuterAngle = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Light(ref b);
        }
        
        Light IDeserializable<Light>.RosDeserialize(ref Buffer b)
        {
            return new Light(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(CastShadows);
            b.Serialize(Diffuse);
            b.Serialize(Range);
            b.Serialize(Position);
            b.Serialize(Direction);
            b.Serialize(InnerAngle);
            b.Serialize(OuterAngle);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 46 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Light";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2QzwrCMAzG74G9Q9/AfxcRdpApMtBtOPE6OtfOwGyk7dT59HZY5wuIvXxffmlCEmhR" +
                "2TnL0jg5sJCN4R2v4v06OsRpstw6OvE0z9L+0xTAWI2qZopfhM/Z7iqgJGrYiRtbmDOv6G4goob0bMoq" +
                "lLI1AmRD3LpYc1ULOIqTdWnJrmTQIqkvqVA726NPCSoldOHqmm8bau3AAAIIf/wC2OWbBcMbPouLqc3I" +
                "7xP4rbXX2mvplf9jls+tguEcj8F1g3u6UV5yTNi35gEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
