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
            Name = string.Empty;
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACl1TTYvbMBC9B/ofBu8lC9nApj2UPXahvZbSW+lBlse2qKIxGinZ/Ps+yc6n8UGgmffe" +
                "vDd6ot+jU9qzqhmYcMzKHbUnSiPTT3EhvXvJ3aUiCY3iO5IpOQnGU2eSWT2RURXrTELz0aWR2NiRptJP" +
                "LlQwW3C24GPyHAbUSF8vDsZn1oIRozmRjpJB0HK9VLNngNfzfVsF16Vp5gDGVfKGTOhmHZXhBvhRbJUn" +
                "MbJOEjoXBgBV+O0Kp/fRhMCeAqRoYeIPpwlVNEVjk7PwLVifO35DNVGTmw01h4ZeKMqxqrDi8z7QujAw" +
                "Wg7sT89nYzz3iTRxZCG3h8nbCnPz1YzwyzSJulRTyJrhvpVw4FCiUGpzosh743DuJT6CACNJdBZdkY2i" +
                "Yw4j8JHjjW+7S9YjfA/yiKO5JBul9byflTZxaMu03yUukdestVR12c77BAtwvYxpYWU0EJBR/fWOYf1r" +
                "82Pz7XnZCpqM/QcAlMlillG45YbgeswCrt0Xal3SzR0KrJXYcVwEop0DnDsVmd4oBi5a3QdivdwttR18" +
                "MsFyU7IvBtmb/M9LNCBCUpgN7y2CmVfyXLnm7VDQbnmRSmKDh9NjN8pszfN2pSmWPSrIZ7Zl7se38Pry" +
                "et1VhvVI/cJ73ee7F7Bd9V5M+rz78/f8yD6t/gMJcfcf8gMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
