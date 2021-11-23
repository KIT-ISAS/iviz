/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiArrayLayout : IDeserializable<MultiArrayLayout>, IMessage
    {
        // The multiarray declares a generic multi-dimensional array of a
        // particular data type.  Dimensions are ordered from outer most
        // to inner most.
        [DataMember (Name = "dim")] public MultiArrayDimension[] Dim; // Array of dimension properties
        [DataMember (Name = "data_offset")] public uint DataOffset; // padding elements at front of data
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
    
        /// Constructor for empty message.
        public MultiArrayLayout()
        {
            Dim = System.Array.Empty<MultiArrayDimension>();
        }
        
        /// Explicit constructor.
        public MultiArrayLayout(MultiArrayDimension[] Dim, uint DataOffset)
        {
            this.Dim = Dim;
            this.DataOffset = DataOffset;
        }
        
        /// Constructor with buffer.
        internal MultiArrayLayout(ref Buffer b)
        {
            Dim = b.DeserializeArray<MultiArrayDimension>();
            for (int i = 0; i < Dim.Length; i++)
            {
                Dim[i] = new MultiArrayDimension(ref b);
            }
            DataOffset = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiArrayLayout(ref b);
        
        MultiArrayLayout IDeserializable<MultiArrayLayout>.RosDeserialize(ref Buffer b) => new MultiArrayLayout(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Dim, 0);
            b.Serialize(DataOffset);
        }
        
        public void RosValidate()
        {
            if (Dim is null) throw new System.NullReferenceException(nameof(Dim));
            for (int i = 0; i < Dim.Length; i++)
            {
                if (Dim[i] is null) throw new System.NullReferenceException($"{nameof(Dim)}[{i}]");
                Dim[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetArraySize(Dim);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayLayout";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "0fed2a11c13e11c5571b4e2a995a91a3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
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
    }
}
