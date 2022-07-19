/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class PointIndices : IDeserializableRos1<PointIndices>, IDeserializableRos2<PointIndices>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "indices")] public int[] Indices;
    
        /// Constructor for empty message.
        public PointIndices()
        {
            Indices = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public PointIndices(in StdMsgs.Header Header, int[] Indices)
        {
            this.Header = Header;
            this.Indices = Indices;
        }
        
        /// Constructor with buffer.
        public PointIndices(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Indices);
        }
        
        /// Constructor with buffer.
        public PointIndices(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Indices);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PointIndices(ref b);
        
        public PointIndices RosDeserialize(ref ReadBuffer b) => new PointIndices(ref b);
        
        public PointIndices RosDeserialize(ref ReadBuffer2 b) => new PointIndices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Indices);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Indices);
        }
        
        public void RosValidate()
        {
            if (Indices is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 4 * Indices.Length;
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Indices);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "pcl_msgs/PointIndices";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "458c7998b7eaf99908256472e273b3d4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwK7W2ht9Kmh0AguZWyaG1lRuCxp5Zm0/n3fZ6laXvroYPB" +
                "ePze92TpTjhJo3HbghZ//+7rN9KSNIqFED785y/cP34+kHk6TjbY27tL7o4enUvilmgS58TO9FxRlg6j" +
                "tH2Ws2SYeJol0Xbr6yx2C+PTqEZYgxRpnPNKi0HklWKdpqVoZBdyneQvP5xaiGnm5hqXzA362pKWLn9u" +
                "PEmnY5l8X6REoS8fD9AUk7i4oqAVhNiETcuASwrL1r5uCLunl7rHUQY09zWcfGTvxcqPuYn1OtkOyHhz" +
                "edwt2GiOICUZXW//jjjaDSEEJchc40jXqPxh9bEWAIXO3JRPWTo4ogOgXnXT1c0f5LKhC5f6C38h/s74" +
                "F2x55fY37UfMLPfX2zKggRDOrZ41QXpaN0jMKsUp66lxW0N3XSLD7lPvMURwbRPBzmY1KgaQ6EV9DOat" +
                "07dpHDWF8BORtB2+rgIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
