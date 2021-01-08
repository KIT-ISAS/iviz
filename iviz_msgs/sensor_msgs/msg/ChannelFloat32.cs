/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/ChannelFloat32")]
    public sealed class ChannelFloat32 : IDeserializable<ChannelFloat32>, IMessage
    {
        // This message is used by the PointCloud message to hold optional data
        // associated with each point in the cloud. The length of the values
        // array should be the same as the length of the points array in the
        // PointCloud, and each value should be associated with the corresponding
        // point.
        // Channel names in existing practice include:
        //   "u", "v" - row and column (respectively) in the left stereo image.
        //              This is opposite to usual conventions but remains for
        //              historical reasons. The newer PointCloud2 message has no
        //              such problem.
        //   "rgb" - For point clouds produced by color stereo cameras. uint8
        //           (R,G,B) values packed into the least significant 24 bits,
        //           in order.
        //   "intensity" - laser or pixel intensity.
        //   "distance"
        // The channel name should give semantics of the channel (e.g.
        // "intensity" instead of "value").
        [DataMember (Name = "name")] public string Name { get; set; }
        // The values array should be 1-1 with the elements of the associated
        // PointCloud.
        [DataMember (Name = "values")] public float[] Values { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ChannelFloat32()
        {
            Name = "";
            Values = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ChannelFloat32(string Name, float[] Values)
        {
            this.Name = Name;
            this.Values = Values;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ChannelFloat32(ref Buffer b)
        {
            Name = b.DeserializeString();
            Values = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ChannelFloat32(ref b);
        }
        
        ChannelFloat32 IDeserializable<ChannelFloat32>.RosDeserialize(ref Buffer b)
        {
            return new ChannelFloat32(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Values, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Values is null) throw new System.NullReferenceException(nameof(Values));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += 4 * Values.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/ChannelFloat32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d40139cdd33dfedcb71ffeeeb42ae7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmVTTYvbMBC9B/IfBu8lgWxg0x5Kj11or6X0VnpQ5IktKmuMRko2/75PsvO1a3wQaOa9" +
                "N++Nnuh375QGVjUdE45ZuaX9mVLP9FNcSK9ecnutSEK9+JZkTE6C8dSaZJaLJzKqYp1J6D651BMb29NY" +
                "AMiFimYL0BaETJ5Dhxo51Iuj8Zm1gsRozqS9ZFDsud6qGRjo9fzYV9F1bppICshN9YZMaCclleMO+b3c" +
                "KlBiZB0ltC50BakSbJeLcn7tTQjsKUCOFjZ+c5pQSGM0NjkL94L1ueWvpZyoyc2GmmNDzxTlVJVY8XkI" +
                "tCosjJ4j+/P6Yo/nQyJNHFnIDfB6O+HcfTUr/DKOoi7VNLJmpGAlHDmUSJT2OVHkwTicDxI/oAAkSXQW" +
                "bZGNomUKJfCJ4517u2voPewP8gFIc4k4yt7zMIttYrcvE3+XOIdfU9dS1mY7rRZswPU8qoWf0UBCRvWX" +
                "R47Vr82Pzbf1vCA0GvsPCKiT2TGjsMx1wR0wDsh2n2nvkm4eYWCwxJbjRSMAOMC/c1HqjWLqIte9Id7r" +
                "3aW4hVsmWG6mNShG2btVuKxUhzBJ4TpCsEho2tBL5Yq3XQW850Y+iQ2e0gFrUiZs1qjRFMtSFewb42zA" +
                "++fx8vxy215GCliBK/dtwx8fBTgOXkz6tPvz9/r0lov/uGi5CwoEAAA=";
                
    }
}
