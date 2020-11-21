/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/MultiArrayLayout")]
    public sealed class MultiArrayLayout : IDeserializable<MultiArrayLayout>, IMessage
    {
        // The multiarray declares a generic multi-dimensional array of a
        // particular data type.  Dimensions are ordered from outer most
        // to inner most.
        [DataMember (Name = "dim")] public MultiArrayDimension[] Dim { get; set; } // Array of dimension properties
        [DataMember (Name = "data_offset")] public uint DataOffset { get; set; } // padding elements at front of data
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
    
        /// <summary> Constructor for empty message. </summary>
        public MultiArrayLayout()
        {
            Dim = System.Array.Empty<MultiArrayDimension>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiArrayLayout(MultiArrayDimension[] Dim, uint DataOffset)
        {
            this.Dim = Dim;
            this.DataOffset = DataOffset;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MultiArrayLayout(ref Buffer b)
        {
            Dim = b.DeserializeArray<MultiArrayDimension>();
            for (int i = 0; i < Dim.Length; i++)
            {
                Dim[i] = new MultiArrayDimension(ref b);
            }
            DataOffset = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiArrayLayout(ref b);
        }
        
        MultiArrayLayout IDeserializable<MultiArrayLayout>.RosDeserialize(ref Buffer b)
        {
            return new MultiArrayLayout(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                foreach (var i in Dim)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayLayout";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0fed2a11c13e11c5571b4e2a995a91a3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UW2/aMBR+R+I/HMELZcC4VNVaiQekSn3ZnjppmhBCbnySGBIb2U5Z9+v32Qlp2Pq4" +
                "SIhw7PPd7MOQvudMZVV4JawVbyQ5KYRlR4Iy1mxVUq9OpSpZO2W0KKjealIS/d6QTsJ6lVRoIym8IP92" +
                "4hnR46UBWJbJWMmWJaXWlGQqz5ZK43wA8IaU1k1h1u/1e98C5SawtCjbHUECDWlzIW8V0cmaE0MEu36v" +
                "UtqvllHJ3qSpY0/NE5RKqXRGXDBaPYT5oEf7CIeOwA2GJGHnjHXkclMVkjZff2x+PtML09kq71lDL8FB" +
                "6a51OG+V5AAhtCR34kSlCp6j3Wlw19mcKhvdDil83o9gpCaHyfGG1lHRtmvkU2jf1yzbxW6srivL3fiA" +
                "ynEHwOgDgiBEWDmh1TTJBUIu6O52/uv2y5xUKTIYUj6HG+grWLxCa2IKY6nZjDyHdI4ZwPy7IeEeGg7Q" +
                "b+e7WSFeAA3Ng5xVlvtBZ82p34z41wTWbjmKRnk1hqRxkLSm++Xibj4nGmnjudnZxErK0aFChBEPuUcD" +
                "NxfERVfEWUmfDzpLrQZQdctXGvC9uF+268suYhPIoLPYYq66xRaxCejfg7WcMu4Wbr3H7IX8rTlP6IAX" +
                "hF+VehKvzzH8rlnjTKz/84Mpe356QLhyX7rMff5g5vq9YAcD0+SAEarfkH6mXjEH7XVuB69JBVsv5/TX" +
                "ThqF4cF/BFVaeYcDvHTW0YXO+u0jlj9CA/MksQQAAA==";
                
    }
}
