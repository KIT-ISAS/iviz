/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Light")]
    public sealed class Light : IMessage
    {
        public const byte POINT = 0;
        public const byte DIRECTIONAL = 1;
        public const byte SPOT = 2;
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "cast_shadows")] public bool CastShadows { get; set; }
        [DataMember (Name = "diffuse")] public Color Diffuse { get; set; }
        [DataMember (Name = "range")] public float Range { get; set; }
        [DataMember (Name = "direction")] public Vector3 Direction { get; set; }
        [DataMember (Name = "inner_angle")] public float InnerAngle { get; set; }
        [DataMember (Name = "outer_angle")] public float OuterAngle { get; set; }
        [DataMember (Name = "pose")] public Matrix4 Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Light()
        {
            Name = "";
            Pose = new Matrix4();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Light(string Name, byte Type, bool CastShadows, in Color Diffuse, float Range, in Vector3 Direction, float InnerAngle, float OuterAngle, Matrix4 Pose)
        {
            this.Name = Name;
            this.Type = Type;
            this.CastShadows = CastShadows;
            this.Diffuse = Diffuse;
            this.Range = Range;
            this.Direction = Direction;
            this.InnerAngle = InnerAngle;
            this.OuterAngle = OuterAngle;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Light(Buffer b)
        {
            Name = b.DeserializeString();
            Type = b.Deserialize<byte>();
            CastShadows = b.Deserialize<bool>();
            Diffuse = new Color(b);
            Range = b.Deserialize<float>();
            Direction = new Vector3(b);
            InnerAngle = b.Deserialize<float>();
            OuterAngle = b.Deserialize<float>();
            Pose = new Matrix4(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Light(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(CastShadows);
            Diffuse.RosSerialize(b);
            b.Serialize(Range);
            Direction.RosSerialize(b);
            b.Serialize(InnerAngle);
            b.Serialize(OuterAngle);
            Pose.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 98;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Light";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5160c230ff0932d45a06d2998b92b7aa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VRSwrCMBDdzykGPIBfRAQXoiIFbUXFjUhJNa2RNlOS1N/pjRrjBXQ27+W9SeYTqIQ0" +
                "PVxEQbjGATbgfR4Hy8loHUThcGbVplNXi+iZ1ALQRgmZoWQFd565lRwSohz3TJtYH9mBLhpGlJPCg0jT" +
                "SnNIc2Km3ULFZMZhw/eGVNu6yjJB0vtCSq5im5R/71BlvDZntv61gyXZR20MfhwwX037KM7iHhc60/XX" +
                "FG7QD2YOE4fs/224hfmVXD27eXb/fxtu+5+S22Z3hwXWUNEFC3ay/w3wAP2elUxYAgAA";
                
    }
}
