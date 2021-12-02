/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        [DataMember (Name = "name")] public string Name;
        // The values array should be 1-1 with the elements of the associated
        // PointCloud.
        [DataMember (Name = "values")] public float[] Values;
    
        /// Constructor for empty message.
        public ChannelFloat32()
        {
            Name = string.Empty;
            Values = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public ChannelFloat32(string Name, float[] Values)
        {
            this.Name = Name;
            this.Values = Values;
        }
        
        /// Constructor with buffer.
        internal ChannelFloat32(ref Buffer b)
        {
            Name = b.DeserializeString();
            Values = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ChannelFloat32(ref b);
        
        ChannelFloat32 IDeserializable<ChannelFloat32>.RosDeserialize(ref Buffer b) => new ChannelFloat32(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Values);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Values is null) throw new System.NullReferenceException(nameof(Values));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + 4 * Values.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/ChannelFloat32";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3d40139cdd33dfedcb71ffeeeb42ae7f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACl1Tu47bMBDs/RULXWMDPgPnpAhS5oCkDYJ0QQqKWklEaK7AJe3z32dIyU9BBQHuzszO" +
                "LF/o9+iUDqxqBiYcs3JH7ZnSyPRTXEjvXnJ3rUhCo/iOZEpOgvHUmWRWL2RUxTqT0HxyaSQ2dqSp9JML" +
                "FcwWnB34mDyHATXS14uj8Zm1YMRozqSjZBC0XC/VHBjg9fzYVsF1aZo5gHGTvCUTullHZbgDfhZb5UmM" +
                "rJOEzoUBQBV+t8LpfTQhsKcAKVqY+MNpQhVN0djkLHwL1ueOv6KaqMnNlppjQ68U5VRVWPH5EGhdGBgt" +
                "R/bnzcUYz30iTRxZyB1g8q7C3H01I/wyTaIu1RSyZrhvJRw5lCiU2pwo8sE4nHuJzyDASBKdRVdko+iY" +
                "wwh84njn2/6a9QjfgzzjaC7JRmk9H2alTRzaMu13iUvkNWstVV228z7BAlwvY1pYGQ0EZFR/eWBY/9r+" +
                "2H7bLFtBk7H/AIAyWcwyCrfcEFyPWcC1/0ytS7p9QIG1EjuOi0C0c4Bz5yLTG8XARav7QKzXu6W2g08m" +
                "WG5K9sUge5f/ZYkGREgKs+G9RTDzSl4q17wbCto9L1JJbPBweuxGma3Z7FaaYtmjgnxhW+Z+fgtvr2+3" +
                "XWVYj9SvvLd9fngBu1XvxaRP+z9/L49s9R9cgvy88QMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
