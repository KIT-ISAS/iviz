/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Light")]
    public sealed class Light : IDeserializable<Light>, IMessage
    {
        public const byte POINT = 0;
        public const byte DIRECTIONAL = 1;
        public const byte SPOT = 2;
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "cast_shadows")] public bool CastShadows { get; set; }
        [DataMember (Name = "diffuse")] public Color32 Diffuse { get; set; }
        [DataMember (Name = "range")] public float Range { get; set; }
        [DataMember (Name = "position")] public Vector3f Position { get; set; }
        [DataMember (Name = "direction")] public Vector3f Direction { get; set; }
        [DataMember (Name = "inner_angle")] public float InnerAngle { get; set; }
        [DataMember (Name = "outer_angle")] public float OuterAngle { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Light()
        {
            Name = "";
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
        public Light(ref Buffer b)
        {
            Name = b.DeserializeString();
            Type = b.Deserialize<byte>();
            CastShadows = b.Deserialize<bool>();
            Diffuse = new Color32(ref b);
            Range = b.Deserialize<float>();
            Position = new Vector3f(ref b);
            Direction = new Vector3f(ref b);
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
            Diffuse.RosSerialize(ref b);
            b.Serialize(Range);
            Position.RosSerialize(ref b);
            Direction.RosSerialize(ref b);
            b.Serialize(InnerAngle);
            b.Serialize(OuterAngle);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 46;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Light";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c08cec0d4c9fe9b11d0596f99987e126";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72QTQrCMBCF94HcITfwbyNCF6IiBW2LitsSbVMH2kxJUrWe3lEa6wHEZPPy5c0w8zhr" +
                "QLupSOIwOohADHkHluFutTiEcTTfEB55vE/il23MGWfWGdCF0LLK/bdra9InxFKcpXWpvcgMb5azBZZo" +
                "JmORgVKNJY8qUToCRuqCnsf87MigRI0WHKD+QhkYkm/mq0Dr3KRUWn61wsb18HVZ8OPDtvv1TMAVHmll" +
                "CzvotuqWNz6FwouTF/IPw/i8Pnnc+2jaXj5olCeOnAp59gEAAA==";
                
    }
}
