/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class SceneLight : IHasSerializer<SceneLight>, IMessage
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
    
        public SceneLight()
        {
            Name = "";
        }
        
        public SceneLight(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out CastShadows);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Range);
            b.Deserialize(out Position);
            b.Deserialize(out Direction);
            b.Deserialize(out InnerAngle);
            b.Deserialize(out OuterAngle);
        }
        
        public SceneLight(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out CastShadows);
            b.Deserialize(out Diffuse);
            b.Align4();
            b.Deserialize(out Range);
            b.Deserialize(out Position);
            b.Deserialize(out Direction);
            b.Deserialize(out InnerAngle);
            b.Deserialize(out OuterAngle);
        }
        
        public SceneLight RosDeserialize(ref ReadBuffer b) => new SceneLight(ref b);
        
        public SceneLight RosDeserialize(ref ReadBuffer2 b) => new SceneLight(ref b);
    
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
            b.Align4();
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(CastShadows);
            b.Serialize(in Diffuse);
            b.Align4();
            b.Serialize(Range);
            b.Serialize(in Position);
            b.Serialize(in Direction);
            b.Serialize(InnerAngle);
            b.Serialize(OuterAngle);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Name, nameof(Name));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 46;
                size += WriteBuffer.GetStringSize(Name);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size += 1; // Type
            size += 1; // CastShadows
            size += 4; // Diffuse
            size = WriteBuffer2.Align4(size);
            size += 4; // Range
            size += 12; // Position
            size += 12; // Direction
            size += 4; // InnerAngle
            size += 4; // OuterAngle
            return size;
        }
    
        public const string MessageType = "iviz_msgs/SceneLight";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VSTW/bMAy961cQ2GUFhm5rL8OAHoZsGAK0TbDmHsgWbROzRUOkm6W/fpSteJf1tulC" +
                "8evx8UluoqifYL/bPh7gDj64xf+6/fFtc9juHr/cW/RjiT7td7noxjnRRLGF6AcsOT2P6CrmHmovepTO" +
                "Bz6J23DP6fYGAjXNJOianr2an3xs0bXIA2o6Hwdp5f2eDclyIwspcXwlHShhPecvYBQjpqMh9n8G8KRr" +
                "zM7dPz7u4en7Z6BnelnIlT2LGKnYttiqWP+/iPxVKfcGDh0J1BzVUxTQDldxgRvw5lml6QdNQgQZfY1v" +
                "T6QdmIIVqeSq0eQmsZara0PcWrmAhXgYMGAAZbCHhXkmnDpM+IwpjxGqejRsUfQhAxVa1wCGcyFXkGLw" +
                "MyuLGOCYeGDNzfaIPGLyFfWk57n10jmgiG8xtwQUauNCRv1PhGmE3tLLRplVBLEZ9metu+eyWOYj4BU4" +
                "1vgOvGQlskj2hXERaOa86XkKefb6u36tt/N6e3G/AfygDAlNAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<SceneLight> CreateSerializer() => new Serializer();
        public Deserializer<SceneLight> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<SceneLight>
        {
            public override void RosSerialize(SceneLight msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(SceneLight msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(SceneLight msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(SceneLight msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(SceneLight msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<SceneLight>
        {
            public override void RosDeserialize(ref ReadBuffer b, out SceneLight msg) => msg = new SceneLight(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out SceneLight msg) => msg = new SceneLight(ref b);
        }
    }
}
