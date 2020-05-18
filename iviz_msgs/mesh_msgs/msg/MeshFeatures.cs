using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshFeatures")]
    public sealed class MeshFeatures : IMessage
    {
        [DataMember (Name = "map_uuid")] public string MapUuid { get; set; }
        [DataMember (Name = "features")] public MeshMsgs.Feature[] Features { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFeatures()
        {
            MapUuid = "";
            Features = System.Array.Empty<MeshMsgs.Feature>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFeatures(string MapUuid, MeshMsgs.Feature[] Features)
        {
            this.MapUuid = MapUuid;
            this.Features = Features;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshFeatures(Buffer b)
        {
            MapUuid = b.DeserializeString();
            Features = b.DeserializeArray<MeshMsgs.Feature>();
            for (int i = 0; i < this.Features.Length; i++)
            {
                Features[i] = new MeshMsgs.Feature(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshFeatures(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.MapUuid);
            b.SerializeArray(Features, 0);
        }
        
        public void Validate()
        {
            if (MapUuid is null) throw new System.NullReferenceException();
            if (Features is null) throw new System.NullReferenceException();
            for (int i = 0; i < Features.Length; i++)
            {
                if (Features[i] is null) throw new System.NullReferenceException();
                Features[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(MapUuid);
                for (int i = 0; i < Features.Length; i++)
                {
                    size += Features[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFeatures";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ea0bfd1049bc24f2cd76d68461f1f987";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQwQqCYAwH8PueYtADBBYdgq51CoK6RcjQqYP8Jt8mZE+fYniojrnTf2OwHzOPEkqs" +
                "qUnbVnKo2aq0ttKWeyZvI19vWIzJAHZ/LjieD1v8ugkla80eu3F6UgmOd83IRQOY5+/tu5Kvkl6Ys2VR" +
                "Gtc4l/GHCBZ4qcQw0+AkwdArxkZNBiVqgdR3g1wCFpEZraGMoRjUmzU+ptRN6TkX//Nn48VVgjk5wQu3" +
                "LIB6BQIAAA==";
                
    }
}
