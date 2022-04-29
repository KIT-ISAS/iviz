/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshVertexCostsStamped : IDeserializable<MeshVertexCostsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "mesh_vertex_costs")] public MeshMsgs.MeshVertexCosts MeshVertexCosts;
    
        /// Constructor for empty message.
        public MeshVertexCostsStamped()
        {
            Uuid = "";
            Type = "";
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts();
        }
        
        /// Explicit constructor.
        public MeshVertexCostsStamped(in StdMsgs.Header Header, string Uuid, string Type, MeshMsgs.MeshVertexCosts MeshVertexCosts)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Type = Type;
            this.MeshVertexCosts = MeshVertexCosts;
        }
        
        /// Constructor with buffer.
        public MeshVertexCostsStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            b.DeserializeString(out Type);
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexCostsStamped(ref b);
        
        public MeshVertexCostsStamped RosDeserialize(ref ReadBuffer b) => new MeshVertexCostsStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Type is null) BuiltIns.ThrowNullReference();
            if (MeshVertexCosts is null) BuiltIns.ThrowNullReference();
            MeshVertexCosts.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += BuiltIns.GetStringSize(Type);
                size += MeshVertexCosts.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTWsbMRC961cM+JCk4ATam6GH0tAmh0AhoZdSzFgarwRaaSuNnOy/79O6TkMh0EO7" +
                "CLQavffmUyu6k+rpg2oJu6bSj5UHMVXddqxDvboRdlLILxvMJaSBWgvu9K/zJGaEyhHf9b5KUXn6mKtW" +
                "Wm4Oi2Fru8WY9//4M3f3nzf0R8RmRffKyXFxiEHZsTLtMzIJg5eyjnKQCBKPkzhabnsm9RLEBx8qYQ2S" +
                "pHCMM7UKkGayeRxbCpZRKg3I7SUfzJCIaeKiwbbIBfhcXEgdvi88SlfHqvKjSbJCt9cbYFIV2zQgoBkK" +
                "tgjXXtnbazItJH33thPM6uExr3GUAf14dk7qWXuw8jQVNA/BcN3Ax5tjcpfQRnEEXlyl88W2xbFeEJwg" +
                "BJmy9XSOyL/M6nOCoNCBS+BdlC5sUQGonnXS2cUL5bRIJ075JH9U/O3jb2TTs27Pae3Rs9izr21AAQGc" +
                "Sj4EB+huXkRsDJKUYtgVLrPprKNLs/rUawwQWEtHsHOt2QY0wNFjUH+a2qUbW0zxf5rGVx8E8nzlye1j" +
                "ZvT623f69U5+AlNTzwmdAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
