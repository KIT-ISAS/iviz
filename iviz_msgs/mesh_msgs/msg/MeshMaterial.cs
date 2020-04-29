using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshMaterial : IMessage
    {
        public uint texture_index;
        public std_msgs.ColorRGBA color;
        public bool has_texture;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out texture_index, ref ptr, end);
            color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out has_texture, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(texture_index, ref ptr, end);
            color.Serialize(ref ptr, end);
            BuiltIns.Serialize(has_texture, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 21;
    
        public IMessage Create() => new MeshMaterial();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshMaterial";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
    }
}
