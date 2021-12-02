/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshFeatures : IDeserializable<MeshFeatures>, IMessage
    {
        [DataMember (Name = "map_uuid")] public string MapUuid;
        [DataMember (Name = "features")] public MeshMsgs.Feature[] Features;
    
        /// Constructor for empty message.
        public MeshFeatures()
        {
            MapUuid = string.Empty;
            Features = System.Array.Empty<MeshMsgs.Feature>();
        }
        
        /// Explicit constructor.
        public MeshFeatures(string MapUuid, MeshMsgs.Feature[] Features)
        {
            this.MapUuid = MapUuid;
            this.Features = Features;
        }
        
        /// Constructor with buffer.
        internal MeshFeatures(ref Buffer b)
        {
            MapUuid = b.DeserializeString();
            Features = b.DeserializeArray<MeshMsgs.Feature>();
            for (int i = 0; i < Features.Length; i++)
            {
                Features[i] = new MeshMsgs.Feature(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshFeatures(ref b);
        
        MeshFeatures IDeserializable<MeshFeatures>.RosDeserialize(ref Buffer b) => new MeshFeatures(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(MapUuid);
            b.SerializeArray(Features);
        }
        
        public void RosValidate()
        {
            if (MapUuid is null) throw new System.NullReferenceException(nameof(MapUuid));
            if (Features is null) throw new System.NullReferenceException(nameof(Features));
            for (int i = 0; i < Features.Length; i++)
            {
                if (Features[i] is null) throw new System.NullReferenceException($"{nameof(Features)}[{i}]");
                Features[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(MapUuid) + BuiltIns.GetArraySize(Features);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFeatures";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ea0bfd1049bc24f2cd76d68461f1f987";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrXQwQqCYAwH8PueYtADBBodgq51CoK6RcjQqYP8Jt8mZE+fYnjIa+303xjsx8yjhAob" +
                "arOukwIatjprrLL1gcm7yLc7llMygP2PC06X4w4XN6FibdhjP03PKsHxoTm5aADz4rP9UPI0GYQFWx6l" +
                "dY3/MlZLEazwWothrsFJgqHXjK2ajErUEmnoRrkELCMzWks5Qzmqtxt8zqmf0+tf/O+fTRfTBAtygje3" +
                "LIB6BQIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
