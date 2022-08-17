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
        [DataMember (Name = "position")] public GeometryMsgs.Point32 Position;
        [DataMember (Name = "direction")] public GeometryMsgs.Point32 Direction;
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Name);
            c += 1; // Type
            c += 1; // CastShadows
            c += 4; // Diffuse
            c = WriteBuffer2.Align4(c);
            c += 4; // Range
            c += 12; // Position
            c += 12; // Direction
            c += 4; // InnerAngle
            c += 4; // OuterAngle
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Light";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VSTW/bMAy961cQ2GUFhm5rL8OAHoZsGAK0TbDmHsgWbROzRUOkm6W/fpSteJf1tulC" +
                "8evx8UluoqifYL/bPh7gDj64xf+6/fFtc9juHr/cW/RjiT7td7noxjnRRLGF6AcsOT2P6CrmHmovepTO" +
                "Bz6J23DP6fYGAjXNJOianr2an3xs0bXIA2o6Hwdp5f2eDclyIwspcXwlHShhPecvYBQjpqMh9n8G8KRr" +
                "zM7dPz7u4en7Z6BnelnIlT2LGKnYttiqWP+/iPxVKfcGDh0J1BzVUxTQDldxgRvw5lml6QdNQgQZfY1v" +
                "T6QdmIIVqeSq0eQmsZara0PcWrmAhXgYMGAAZbCHhXkmnDpM+IwpjxGqejRsUfQhAxVa1wCGcyFXkGLw" +
                "MyuLGOCYeGDNzfaIPGLyFfWk57n10jmgiG8xtwQUauNCRv1PhGmE3tLLRplVBLEZ9metu+eyWOYj4BU4" +
                "1vgOvGQlskj2hXERaOa86XkKefb6u36tt/N6e3G/AfygDAlNAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
