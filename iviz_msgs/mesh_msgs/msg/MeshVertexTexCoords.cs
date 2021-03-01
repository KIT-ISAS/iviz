/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshVertexTexCoords")]
    public sealed class MeshVertexTexCoords : IDeserializable<MeshVertexTexCoords>, IMessage
    {
        // Mesh Attribute Type
        [DataMember (Name = "u")] public float U { get; set; }
        [DataMember (Name = "v")] public float V { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexTexCoords()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexTexCoords(float U, float V)
        {
            this.U = U;
            this.V = V;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshVertexTexCoords(ref Buffer b)
        {
            U = b.Deserialize<float>();
            V = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshVertexTexCoords(ref b);
        }
        
        MeshVertexTexCoords IDeserializable<MeshVertexTexCoords>.RosDeserialize(ref Buffer b)
        {
            return new MeshVertexTexCoords(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexTexCoords";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
    }
}
