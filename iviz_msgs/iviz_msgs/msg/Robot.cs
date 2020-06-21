using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Robot")]
    public sealed class Robot : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "links")] public Link[] Links { get; set; }
        [DataMember (Name = "joints")] public Joint[] Joints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Robot()
        {
            Name = "";
            Links = System.Array.Empty<Link>();
            Joints = System.Array.Empty<Joint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Robot(string Name, Link[] Links, Joint[] Joints)
        {
            this.Name = Name;
            this.Links = Links;
            this.Joints = Joints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Robot(Buffer b)
        {
            Name = b.DeserializeString();
            Links = b.DeserializeArray<Link>();
            for (int i = 0; i < this.Links.Length; i++)
            {
                Links[i] = new Link(b);
            }
            Joints = b.DeserializeArray<Joint>();
            for (int i = 0; i < this.Joints.Length; i++)
            {
                Joints[i] = new Joint(b);
            }
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Robot(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            b.SerializeArray(Links, 0);
            b.SerializeArray(Joints, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (Links is null) throw new System.NullReferenceException();
            for (int i = 0; i < Links.Length; i++)
            {
                if (Links[i] is null) throw new System.NullReferenceException();
                Links[i].RosValidate();
            }
            if (Joints is null) throw new System.NullReferenceException();
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i] is null) throw new System.NullReferenceException();
                Joints[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(Name);
                for (int i = 0; i < Links.Length; i++)
                {
                    size += Links[i].RosMessageLength;
                }
                for (int i = 0; i < Joints.Length; i++)
                {
                    size += Joints[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Robot";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7a8c1e30ccd9284818a0a6d649d02500";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71Ty2rDMBC871foDwrOJRR66iEQklOhl1LM1lHkbfUwWiVY/vrKQVGel0BdX2Z2vdKO" +
                "tCMOnqwSFo2EFdmfj0+hEzAsHdmQou8RGV7++IP12+JZ0J6G2rDip7E38JmYd9kE52fCeVJk6z4O1ynf" +
                "xZLaE+9Q13eKL/+Ma3IXJZ2RwceaW+xO/Uq6Q4+Gb6qN5BZenXZeGAzSU9q8GcPJrygrhK12GGaV6AuL" +
                "hQ0wuYzD2WGXXDEXR1QZvzLi9DIO/rxwTOYhdoWnGcpTVdOS3jxoLOyJc1Cll2Eo/NOcq7tzhl9Goq5E" +
                "sAMAAA==";
                
    }
}
