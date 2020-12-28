/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract (Name = "pcl_msgs/PointIndices")]
    public sealed class PointIndices : IDeserializable<PointIndices>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "indices")] public int[] Indices { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointIndices()
        {
            Header = new StdMsgs.Header();
            Indices = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointIndices(StdMsgs.Header Header, int[] Indices)
        {
            this.Header = Header;
            this.Indices = Indices;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PointIndices(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Indices = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointIndices(ref b);
        }
        
        PointIndices IDeserializable<PointIndices>.RosDeserialize(ref Buffer b)
        {
            return new PointIndices(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Indices, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Indices is null) throw new System.NullReferenceException(nameof(Indices));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 4 * Indices.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "pcl_msgs/PointIndices";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "458c7998b7eaf99908256472e273b3d4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwK7W2ht9Kmh0AguZWyaG1lRuCxp5Zm0/n3fZ6laXvroYPB" +
                "ePze92TpTjhJo3HbghZ//+7rN9KSNIqFED785y/cP34+kHk6TjbY27tL7o4enUvilmgS58TO9FxRlg6j" +
                "tH2Ws2SYeJol0Xbr6yx2C+PTqEZYgxRpnPNKi0HklWKdpqVoZBdyneQvP5xaiGnm5hqXzA362pKWLn9u" +
                "PEmnY5l8X6REoS8fD9AUk7i4oqAVhNiETcuASwrL1r5uCLunl7rHUQY09zWcfGTvxcqPuYn1OtkOyHhz" +
                "edwt2GiOICUZXW//jjjaDSEEJchc40jXqPxh9bEWAIXO3JRPWTo4ogOgXnXT1c0f5LKhC5f6C38h/s74" +
                "F2x55fY37UfMLPfX2zKggRDOrZ41QXpaN0jMKsUp66lxW0N3XSLD7lPvMURwbRPBzmY1KgaQ6EV9DOat" +
                "07dpHDWF8BORtB2+rgIAAA==";
                
    }
}
