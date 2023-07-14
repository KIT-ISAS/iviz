/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class PointIndices : IHasSerializer<PointIndices>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "indices")] public int[] Indices;
    
        public PointIndices()
        {
            Indices = EmptyArray<int>.Value;
        }
        
        public PointIndices(in StdMsgs.Header Header, int[] Indices)
        {
            this.Header = Header;
            this.Indices = Indices;
        }
        
        public PointIndices(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Indices = array;
            }
        }
        
        public PointIndices(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Indices = array;
            }
        }
        
        public PointIndices RosDeserialize(ref ReadBuffer b) => new PointIndices(ref b);
        
        public PointIndices RosDeserialize(ref ReadBuffer2 b) => new PointIndices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Indices.Length);
            b.SerializeStructArray(Indices);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Indices.Length);
            b.SerializeStructArray(Indices);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Indices, nameof(Indices));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 4 * Indices.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Indices.Length
            size += 4 * Indices.Length;
            return size;
        }
    
        public const string MessageType = "pcl_msgs/PointIndices";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "458c7998b7eaf99908256472e273b3d4";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RS0sDMRC+51cM9FArVFFvBW+i9SCIeitSpsm4O5BN1sxsdf+9ky2+bh5cAmGT75Vv" +
                "1oSBCrTT5jjpxfnmGTgF9iTOuct//tzd480KRMO2k0ZO1wffGTwqpoAlQEeKARXhJVssbloqy0h7ikbC" +
                "rqcA062OPcmJEZ9aFrDVUKKCMY4wiIE0g89dNyT2qATKHf3iG5MTIPRYlP0QsRg+l8Cpwl8KdlTVbQm9" +
                "DpQ8we3VyjBJyA/KFmg0BV8IhVNjl+CGqb5KgBlsHrKcPbvZ01te2jk11vJXCtAWtaam976Q1MAoKzM7" +
                "PrzyxEysJTK7IHA0nW3tVxZgbpaF+uxbOLIn3I/a5mSCBHssjLtIVdhbFaY6r6T54odymqQTpvwpf1D8" +
                "9viLbPrSrW9atja8WGuQobEmDdiXvOdg0N04ifjIlBQi7wqW0VXWwdLNrmvZBjLWNBrbUSR7tkkEeGNt" +
                "nWip6tNYthyc+wCoH7bctwIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PointIndices> CreateSerializer() => new Serializer();
        public Deserializer<PointIndices> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PointIndices>
        {
            public override void RosSerialize(PointIndices msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PointIndices msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PointIndices msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PointIndices msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(PointIndices msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<PointIndices>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PointIndices msg) => msg = new PointIndices(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PointIndices msg) => msg = new PointIndices(ref b);
        }
    }
}
