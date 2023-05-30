/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class MultiArrayLayout : IHasSerializer<MultiArrayLayout>, IMessage
    {
        // The multiarray declares a generic multi-dimensional array of a
        // particular data type.  Dimensions are ordered from outer most
        // to inner most.
        /// <summary> Array of dimension properties </summary>
        [DataMember (Name = "dim")] public MultiArrayDimension[] Dim;
        /// <summary> Padding elements at front of data </summary>
        [DataMember (Name = "data_offset")] public uint DataOffset;
        // Accessors should ALWAYS be written in terms of dimension stride
        // and specified outer-most dimension first.
        // 
        // multiarray(i,j,k) = data[data_offset + dim_stride[1]*i + dim_stride[2]*j + k]
        //
        // A standard, 3-channel 640x480 image with interleaved color channels
        // would be specified as:
        //
        // dim[0].label  = "height"
        // dim[0].size   = 480
        // dim[0].stride = 3*640*480 = 921600  (note dim[0] stride is just size of image)
        // dim[1].label  = "width"
        // dim[1].size   = 640
        // dim[1].stride = 3*640 = 1920
        // dim[2].label  = "channel"
        // dim[2].size   = 3
        // dim[2].stride = 3
        //
        // multiarray(i,j,k) refers to the ith row, jth column, and kth channel.
    
        public MultiArrayLayout()
        {
            Dim = EmptyArray<MultiArrayDimension>.Value;
        }
        
        public MultiArrayLayout(MultiArrayDimension[] Dim, uint DataOffset)
        {
            this.Dim = Dim;
            this.DataOffset = DataOffset;
        }
        
        public MultiArrayLayout(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                MultiArrayDimension[] array;
                if (n == 0) array = EmptyArray<MultiArrayDimension>.Value;
                else
                {
                    array = new MultiArrayDimension[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MultiArrayDimension(ref b);
                    }
                }
                Dim = array;
            }
            b.Deserialize(out DataOffset);
        }
        
        public MultiArrayLayout(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                MultiArrayDimension[] array;
                if (n == 0) array = EmptyArray<MultiArrayDimension>.Value;
                else
                {
                    array = new MultiArrayDimension[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MultiArrayDimension(ref b);
                    }
                }
                Dim = array;
            }
            b.Align4();
            b.Deserialize(out DataOffset);
        }
        
        public MultiArrayLayout RosDeserialize(ref ReadBuffer b) => new MultiArrayLayout(ref b);
        
        public MultiArrayLayout RosDeserialize(ref ReadBuffer2 b) => new MultiArrayLayout(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Dim.Length);
            foreach (var t in Dim)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(DataOffset);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Dim.Length);
            foreach (var t in Dim)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(DataOffset);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Dim, nameof(Dim));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                foreach (var msg in Dim) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Dim.Length
            foreach (var msg in Dim) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // DataOffset
            return size;
        }
    
        public const string MessageType = "std_msgs/MultiArrayLayout";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "0fed2a11c13e11c5571b4e2a995a91a3";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UTWvbQBC961cM9sV2bdcfITQBHwyFXppTCqUYYTbakbS2tGt2V3HTX9+3kizLaY4V" +
                "Aq3m872nGQ3pR85UVoVXwlrxRpKTQlh2JChjzVYljXcmVcnaKaNFQU2oSUlEQzoJ61VSIYuk8IL824nn" +
                "RF8v8ShlmYyVbFlSak1JpvJsqTTOI98bUlq37/MoegrttqFDV2IXE9rTkLaXxh0aOllzYiBgF1VK+/Wq" +
                "RrE3aerYU3sFlFIqnREXjEwPUD5g0b6uhowIULZJws4Z68jlpiokbb//3P56phems1XeswZUAvbS3YJw" +
                "3irJqCC0JHfiRKUKZGues8CrF5sqG3gOCfdV+JGaHqbHMW1qMLs+h08hed+02C3jibq1rOLJAZZjHA0D" +
                "BWABCGHllNazJBeQtqD7u8Xvuy8LUqXIwEX5HESArWDxCpyJKYylNtihyrlmD9pXLsI91g3QebeI54V4" +
                "QV3AHeSsstwPri6n/jAFFzr2rDVaWNcToJkENBt6WC3vFwuikTae28hWTFKODhWUq8tB7Rr7uC247CM4" +
                "K+nzwdXTAUCjnvUGAJ7Lh9XFveqXa3UYXH1dwXXP1pWrZfn3S1pOGZOE8fZYsSC5NecpHXCA3lWpp/W0" +
                "HMN70xHDv/nPV/T0/O0Risp96TL3+YPdigIRLEbLH6vSnKB4pl4x8d3kXvarVQORl0/zLpBGYUvwG6BK" +
                "K+/GXWIjWUhsTh/0+AtWkePpkQQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MultiArrayLayout> CreateSerializer() => new Serializer();
        public Deserializer<MultiArrayLayout> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiArrayLayout>
        {
            public override void RosSerialize(MultiArrayLayout msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiArrayLayout msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiArrayLayout msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiArrayLayout msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiArrayLayout msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiArrayLayout>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiArrayLayout msg) => msg = new MultiArrayLayout(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiArrayLayout msg) => msg = new MultiArrayLayout(ref b);
        }
    }
}
