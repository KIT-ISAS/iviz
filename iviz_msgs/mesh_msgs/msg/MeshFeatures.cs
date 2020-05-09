using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshFeatures : IMessage
    {
        public string map_uuid { get; set; }
        public mesh_msgs.Feature[] features { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFeatures()
        {
            map_uuid = "";
            features = System.Array.Empty<mesh_msgs.Feature>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFeatures(string map_uuid, mesh_msgs.Feature[] features)
        {
            this.map_uuid = map_uuid ?? throw new System.ArgumentNullException(nameof(map_uuid));
            this.features = features ?? throw new System.ArgumentNullException(nameof(features));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshFeatures(Buffer b)
        {
            this.map_uuid = BuiltIns.DeserializeString(b);
            this.features = BuiltIns.DeserializeArray<mesh_msgs.Feature>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshFeatures(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.map_uuid, b);
            BuiltIns.SerializeArray(this.features, b, 0);
        }
        
        public void Validate()
        {
            if (map_uuid is null) throw new System.NullReferenceException();
            if (features is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(map_uuid);
                for (int i = 0; i < features.Length; i++)
                {
                    size += features[i].RosMessageLength;
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshFeatures";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ea0bfd1049bc24f2cd76d68461f1f987";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQwQqCYAwH8PueYtADBBYdgq51CoK6RcjQqYP8Jt8mZE+fYniojrnTf2OwHzOPEkqs" +
                "qUnbVnKo2aq0ttKWeyZvI19vWIzJAHZ/LjieD1v8ugkla80eu3F6UgmOd83IRQOY5+/tu5Kvkl6Ys2VR" +
                "Gtc4l/GHCBZ4qcQw0+AkwdArxkZNBiVqgdR3g1wCFpEZraGMoRjUmzU+ptRN6TkX//Nn48VVgjk5wQu3" +
                "LIB6BQIAAA==";
                
    }
}
