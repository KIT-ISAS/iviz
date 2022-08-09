/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class PointIndices : IDeserializable<PointIndices>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "indices")] public int[] Indices;
    
        public PointIndices()
        {
            Indices = System.Array.Empty<int>();
        }
        
        public PointIndices(in StdMsgs.Header Header, int[] Indices)
        {
            this.Header = Header;
            this.Indices = Indices;
        }
        
        public PointIndices(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Indices);
        }
        
        public PointIndices(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Indices);
        }
        
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4;  // Indices length
            c += 4 * Indices.Length;
            return c;
        }
    
        public const string MessageType = "pcl_msgs/PointIndices";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "458c7998b7eaf99908256472e273b3d4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RS0sDMRC+51cM9FArVFFvBW+i9SCIeitSpsm4O5BN1sxsdf+9ky2+bh5cAmGT75Vv" +
                "1oSBCrTT5jjpxfnmGTgF9iTOuct//tzd480KRMO2k0ZO1wffGTwqpoAlQEeKARXhJVssbloqy0h7ikbC" +
                "rqcA062OPcmJEZ9aFrDVUKKCMY4wiIE0g89dNyT2qATKHf3iG5MTIPRYlP0QsRg+l8Cpwl8KdlTVbQm9" +
                "DpQ8we3VyjBJyA/KFmg0BV8IhVNjl+CGqb5KgBlsHrKcPbvZ01te2jk11vJXCtAWtaam976Q1MAoKzM7" +
                "PrzyxEysJTK7IHA0nW3tVxZgbpaF+uxbOLIn3I/a5mSCBHssjLtIVdhbFaY6r6T54odymqQTpvwpf1D8" +
                "9viLbPrSrW9atja8WGuQobEmDdiXvOdg0N04ifjIlBQi7wqW0VXWwdLNrmvZBjLWNBrbUSR7tkkEeGNt" +
                "nWip6tNYthyc+wCoH7bctwIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
