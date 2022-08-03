/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
    
        public Light()
        {
            Name = "";
        }
        
        public Light(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Type);
            b.Deserialize(out CastShadows);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Range);
            b.Deserialize(out Position);
            b.Deserialize(out Direction);
            b.Deserialize(out InnerAngle);
            b.Deserialize(out OuterAngle);
        }
        
        public Light(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Type);
            b.Deserialize(out CastShadows);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Range);
            b.Deserialize(out Position);
            b.Deserialize(out Direction);
            b.Deserialize(out InnerAngle);
            b.Deserialize(out OuterAngle);
        }
        
        public Light RosDeserialize(ref ReadBuffer b) => new Light(ref b);
        
        public Light RosDeserialize(ref ReadBuffer2 b) => new Light(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 46 + WriteBuffer.GetStringSize(Name);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, CastShadows);
            WriteBuffer2.AddLength(ref c, Diffuse);
            WriteBuffer2.AddLength(ref c, Range);
            WriteBuffer2.AddLength(ref c, Position);
            WriteBuffer2.AddLength(ref c, Direction);
            WriteBuffer2.AddLength(ref c, InnerAngle);
            WriteBuffer2.AddLength(ref c, OuterAngle);
        }
    
        public const string MessageType = "iviz_msgs/Light";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7WQzwrCMAzG73mKvoE6LyLsICoyUDfc8Dqq62agNqPt/LOnt2JXX0Bz+b780oQm0KGy" +
                "M5alyb5gMRvDJ18lh/WySNL9YuvoxNM8S9+PIgBjNaqGKX4VvmafrYATkWRnbmxpLryiu4ElSdLTiFVY" +
                "150RUEvi1uWaq0bAUZytK9esJYMWSX1JhdrZNxpaUCmhS9cnv2Oos4G5iH8csMs3c4Y37MuraczIb+NX" +
                "1l4bryev/P8fGc4UDvEI7hlcD/AC0NcUX98BAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
