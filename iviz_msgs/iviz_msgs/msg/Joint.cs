using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Joint")]
    public sealed class Joint : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "type")] public string Type { get; set; }
        [DataMember (Name = "parent")] public string Parent { get; set; }
        [DataMember (Name = "child")] public string Child { get; set; }
        [DataMember (Name = "origin_xyz")] public Vector3 OriginXyz { get; set; }
        [DataMember (Name = "origin_rpy")] public Vector3 OriginRpy { get; set; }
        [DataMember (Name = "axis")] public Vector3 Axis { get; set; }
        [DataMember (Name = "limit")] public Vector2 Limit { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Joint()
        {
            Name = "";
            Type = "";
            Parent = "";
            Child = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Joint(string Name, string Type, string Parent, string Child, in Vector3 OriginXyz, in Vector3 OriginRpy, in Vector3 Axis, in Vector2 Limit)
        {
            this.Name = Name;
            this.Type = Type;
            this.Parent = Parent;
            this.Child = Child;
            this.OriginXyz = OriginXyz;
            this.OriginRpy = OriginRpy;
            this.Axis = Axis;
            this.Limit = Limit;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Joint(Buffer b)
        {
            Name = b.DeserializeString();
            Type = b.DeserializeString();
            Parent = b.DeserializeString();
            Child = b.DeserializeString();
            OriginXyz = new Vector3(b);
            OriginRpy = new Vector3(b);
            Axis = new Vector3(b);
            Limit = new Vector2(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Joint(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(Parent);
            b.Serialize(Child);
            OriginXyz.RosSerialize(b);
            OriginRpy.RosSerialize(b);
            Axis.RosSerialize(b);
            Limit.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (Type is null) throw new System.NullReferenceException();
            if (Parent is null) throw new System.NullReferenceException();
            if (Child is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Type);
                size += BuiltIns.UTF8.GetByteCount(Parent);
                size += BuiltIns.UTF8.GetByteCount(Child);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Joint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0c5d3c4c67df7c7d8e75bb485c08c657";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC+DsgsSi1LwSGC85IzMnhSssNbkkv8hYIb8oMz0zL76i" +
                "sgpdqKigEi6UWJFZDOUYKeRk5maWcNlSGXD5BrtbKWSWZVbF5xanF+tDreZKy8lPLDE2UqiAsyrhrCou" +
                "OjnDCKszuABe6YBheAEAAA==";
                
    }
}
