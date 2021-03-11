/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshVertexColorsStamped")]
    public sealed class MeshVertexColorsStamped : IDeserializable<MeshVertexColorsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexColorsStamped()
        {
            Uuid = string.Empty;
            MeshVertexColors = new MeshMsgs.MeshVertexColors();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexColorsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshVertexColors MeshVertexColors)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshVertexColors = MeshVertexColors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshVertexColorsStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColorsStamped(ref b);
        }
        
        MeshVertexColorsStamped IDeserializable<MeshVertexColorsStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColorsStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshVertexColors is null) throw new System.NullReferenceException(nameof(MeshVertexColors));
            MeshVertexColors.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += MeshVertexColors.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexColorsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e3527729bbf26fabb162c58762b6739";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Ty2rcMBTdC+YfLswiD5gU2t1AF2lC0ywCpQndlDJcS3dsgSy5ekziv++RhrihUNpF" +
                "W2OwJJ9z7rkPrelO0kCXOUfblSx1m7gXlbLZjalPrz4IG4k0tA+Oo/U9lWKNGsE8YqrGZ4lZnq6CCzFR" +
                "+3VoJzvdjtRKvf3Lz0rd3d9s6SenK7Wm+8zecDTwkdlwZtoHpGD7QeLGyUEcWDxOYqj9zfMk6QLEh8Em" +
                "wtuLl8jOzVQSQDmQDuNYvNWMGmWL/F7ywbSemCaO2eriOAIforG+wveRR6nqeJN8K+K10O31FhifRJds" +
                "YWiGgo7CqZb39ppUsT6/eV0Jav3wGDbYSo9GLMEpD5yrWXmaIroGM5y2iHF+TO4C2qiOIIpJdNrOdtim" +
                "M0IQWJAp6IFO4fzjnIfgISh04Gi5c1KFNSoA1ZNKOjl7oVxtb8mzD8/yR8UfMf5E1i+6NafNgJ65mn0q" +
                "PQoI4BTDwRpAu7mJaGfFZ3K2ixxnVVnHkGr9vtYYILBaR/DllIK2aIChR5uH59Ft3dhhfP/ZQI6/vBd1" +
                "Nn933xry0827yy9f6X/foCX2Su1d4Dp/cVn1y6pbVgxT3wFcEOmYQwQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
