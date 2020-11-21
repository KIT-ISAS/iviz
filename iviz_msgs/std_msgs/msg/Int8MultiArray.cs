/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int8MultiArray")]
    public sealed class Int8MultiArray : IDeserializable<Int8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public sbyte[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int8MultiArray(MultiArrayLayout Layout, sbyte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
        }
        
        Int8MultiArray IDeserializable<Int8MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9G/QfBuuSuI7rjxCSgA+GQC8pFFIoxZiw0Y6stSWt2V3FTX99364+LCc9" +
                "li4ykmd2Zt57O7MxfctZWKZc6z0JRy5j+lrlTq2MEW+P4k1Xjgq2VmyZJKeqVE7pklJtokFMUidVwaUT" +
                "wYhH5DkVPl74eDuJBtHgQz7Km3e9YrIHTlSqkiZNSlI40eyKBqp0t+tNu9uv4O5WTKFYG+dLRoPlP16g" +
                "8fTlnqyTz4Xd2s/vSXk1vkO8E3nIleTCsCVBWy7ZqKT2XkkFzSyoivwEHbhjOgjjVFIhrObo3g48IXpo" +
                "A5DLMGkj2bCk1OiCUJsNFdoGCE6TKsvG8E79LgvEBATotup0a110MPrAAME2GlRQfjEPSJ51mlruHdlB" +
                "SKnKLXHOvgEAzHk8pesfAyokCZpHG0s201UuafX4Y/XziV6YjkY5xyXwEhgU9hyHdUZJ9ilEKdsGAedA" +
                "98qz621OlQlsY/K/0xFcqPFuvL+kZUC07hP55MOf6yrr2Wakzi3zzWgHy36DhIEHAAGIMHJMi6skExA5" +
                "p5vr6a/r2ympwo/HUbkMbIAPM/UKrInOtaFmM/SM6Rg0APkTIWHvmxoov55uJrl4QWpgHmastpkb9nxW" +
                "/WbIvyRU7ZsDaJgXI0AaeUhLupvPbqZTootSO252NrKSsrSrIGHIB90Dgcs246wP4qiky4Y9V4cBpfrm" +
                "Mwx4z+7mnX/ez9gIMuw5u5yLvrHL2Aj08WANp4zeQtf7i8vrb/RxTDt8QPyqKMehffb+f1118n8vh4e2" +
                "R6OBp4OBaXTACNVfUH+rXjEHXTt3g9eo4i/I5pze7aQLPzy4I6jCvWxxgG1kLZ2PrL/+VuUPNujUl/0F" +
                "AAA=";
                
    }
}
