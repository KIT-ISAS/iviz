/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public Light()
        {
            Name = string.Empty;
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Light(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Light(ref b);
        
        public Light RosDeserialize(ref ReadBuffer b) => new Light(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(CastShadows);
            b.Serialize(in Diffuse);
            b.Serialize(Range);
            b.Serialize(in Position);
            b.Serialize(in Direction);
            b.Serialize(InnerAngle);
            b.Serialize(OuterAngle);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 46 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Light";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7WQzwrCMAzG73mKvoE6LyLsICoyUDfc8Dqq62agNqPt/LOnt2JXX0Bz+b780oQm0KGy" +
                "M5alyb5gMRvDJ18lh/WySNL9YuvoxNM8S9+PIgBjNaqGKX4VvmafrYATkWRnbmxpLryiu4ElSdLTiFVY" +
                "150RUEvi1uWaq0bAUZytK9esJYMWSX1JhdrZNxpaUCmhS9cnv2Oos4G5iH8csMs3c4Y37MuraczIb+NX" +
                "1l4bryev/P8fGc4UDvEI7hlcD/AC0NcUX98BAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
