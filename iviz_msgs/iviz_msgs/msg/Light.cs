/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Light")]
    public sealed class Light : IMessage, IDeserializable<Light>
    {
        public const byte POINT = 0;
        public const byte DIRECTIONAL = 1;
        public const byte SPOT = 2;
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "cast_shadows")] public bool CastShadows { get; set; }
        [DataMember (Name = "diffuse")] public Color Diffuse { get; set; }
        [DataMember (Name = "range")] public float Range { get; set; }
        [DataMember (Name = "position")] public Vector3 Position { get; set; }
        [DataMember (Name = "direction")] public Vector3 Direction { get; set; }
        [DataMember (Name = "inner_angle")] public float InnerAngle { get; set; }
        [DataMember (Name = "outer_angle")] public float OuterAngle { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Light()
        {
            Name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Light(string Name, byte Type, bool CastShadows, in Color Diffuse, float Range, in Vector3 Position, in Vector3 Direction, float InnerAngle, float OuterAngle)
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
            Diffuse = new Color(ref b);
            Range = b.Deserialize<float>();
            Position = new Vector3(ref b);
            Direction = new Vector3(ref b);
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
                "H4sIAAAAAAAAE7WQSw6CMBCG93OKuYGvjTFhYdQYEgUixC2pUnAS6JC2+Dq9qLVeQGfz//3mkc5AR8pO" +
                "MYnDKMMAh/B+L8PdapGFcTTf9HTkaJrEz6IxgLGaVIVKNNLl7K2VcGCu8SiMzc1JFHwxsOCaNRZUlp2R" +
                "UNYs7GSMWqhKwl4eLesJtmzIEisPCtK9e5JPAykldd531d8h3FnP+gh+HLBN1zOkM93zxlRm8NrELfvR" +
                "yunBqfj/N9yN/BWu3t28uwM8ACr8e23YAQAA";
                
    }
}
