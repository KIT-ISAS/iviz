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
            b.SerializeArray(Dim);
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
                "H4sIAAAAAAAACq2US4/aMBSF9/kVV7ABCpTHaNQZiQVSpW46q6lUVShCnvgmMSQ2sp2h01/f4ySEMJ1l" +
                "IyQS+z6+c+KbIf3Imcqq8EpYK95IclIIy44EZazZqqTZnUlVsnbKaFFQE2pSEtGQTsJ6lVTIIim8IP92" +
                "4jnR10s8SlkmYyVblpRaU5KpPFsqjfPI94aU1u3zPIqeQrtt6NCV2MWE9jSk7aVxR0Mna04MAnZRpbRf" +
                "r2qKvUlTx57aK1BKqXRGXDAyPaB8YNG+roaMCCjbJGHnjHXkclMVkrbff25/PdML09kq71kDlcBeulsI" +
                "562SjApCS3InTlSqILbWOQu6erGpskHnkPC7Gj9S08P0OKZNDbPra/gUkvdNi90ynqjblVU8OWDlGEfD" +
                "IAEsgBBWTmk9S3IBawu6v1v8vvuyIFWKDFqUzyEEbAWLV3AmpjCW2mCHKudaPWRftQj3WDdA590inhfi" +
                "BXWBO8hZZbkfXLec+sPwfEPo2FutabG6noBmEmg29LBa3i8WRCNtPLeRrZmkHB0qOFeXg9s1+7gtuOwT" +
                "nJX0+eC60wGgUW/1BgD/y4fVZXvVL9f6MLjudQXXvbWuXG3Lv2/Scso4STjeHiMWLLfmPKUDbuB3Vepp" +
                "fVqO4bnpOI+izX++oqfnb49wVO5Ll7nPH8xWFIRgMFr9GJXmDo5n6hUnvju5l/lq3UDk5dW8C6RRmBJ8" +
                "BqjSyrtxl9hYFhKbuw96/AVWkePpkQQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
