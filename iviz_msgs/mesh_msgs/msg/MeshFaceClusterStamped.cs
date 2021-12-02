/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshFaceClusterStamped : IDeserializable<MeshFaceClusterStamped>, IMessage
    {
        // header
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // mesh uuid
        [DataMember (Name = "uuid")] public string Uuid;
        // Cluster
        [DataMember (Name = "cluster")] public MeshFaceCluster Cluster;
        // overwrite existing labeled faces
        [DataMember (Name = "override")] public bool @override;
    
        /// Constructor for empty message.
        public MeshFaceClusterStamped()
        {
            Uuid = string.Empty;
            Cluster = new MeshFaceCluster();
        }
        
        /// Explicit constructor.
        public MeshFaceClusterStamped(in StdMsgs.Header Header, string Uuid, MeshFaceCluster Cluster, bool @override)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Cluster = Cluster;
            this.@override = @override;
        }
        
        /// Constructor with buffer.
        internal MeshFaceClusterStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            Cluster = new MeshFaceCluster(ref b);
            @override = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshFaceClusterStamped(ref b);
        
        MeshFaceClusterStamped IDeserializable<MeshFaceClusterStamped>.RosDeserialize(ref Buffer b) => new MeshFaceClusterStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            Cluster.RosSerialize(ref b);
            b.Serialize(@override);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Cluster is null) throw new System.NullReferenceException(nameof(Cluster));
            Cluster.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += Cluster.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFaceClusterStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e9b5993e06e78f5ff36e4050fa2e88c6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC961cM+JCksCm0t4WeGtLmECgkt1KWsTRrD8iSK8m78b/vkx2nIacemsWw" +
                "lvzemzdfDfXCTpLJxR2G3OWP35fzdm0aGiT3NE3qgEkauvUdH776KRdg7gG4ZSvPZ7LP94DEk6Rz0iIk" +
                "T5pLZXtuxYujIxjZtDH6BZXUiTFf/vPP3D9829Ob3ODroXBwnBySK+y4MB0jctaul7TzchIPEg8jfC5f" +
                "yzxKvgbxsddMeDoJktj7maYMUIlk4zBMQS0j2aIo2ms+mBqIaeRU1E6eUaUYk9NQ4cfEg1R1PFl+TxKs" +
                "0N3NHpiQxU5FYWiGgk3CuRbx7obMpKF8/lQJpnk8xx2O0qH8L8Gp9FyqWXkak+Tqk/MeMT6syV1DG8UR" +
                "RHGZLpe7A475ihAEFmSMtqdLOP8xlz4GCAqdOCm3XqqwRQWgelFJF1evlKvtPQUOcZNfFf/G+BfZ8KJb" +
                "c9r16Jmv2eepQwEBHFM8YW4ctfMiYr1KKOS1TZxmU1lrSNPc1hoDBNbSEfxzztEqGuDorKXfxnvpxqGO" +
                "+PtMY92ndRzfLI5ptpe1tT9/LUty0OC07koTx6IxsN+cLqtkzB8K1Y1/xAMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
