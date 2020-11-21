/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt16MultiArray")]
    public sealed class UInt16MultiArray : IDeserializable<UInt16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public ushort[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ushort>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt16MultiArray(MultiArrayLayout Layout, ushort[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt16MultiArray(ref b);
        }
        
        UInt16MultiArray IDeserializable<UInt16MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt16MultiArray(ref b);
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
                size += 2 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9D+Q/XJKXNkuzfJSyFvIQKOylg0EHZYRQVOs6VmJbQZKbdb9+R7LsOO0e" +
                "x4yDlav7cc7RvRrS95yFZcq13pNw5DKmb1Xu1MoY8fYg3nTlqGBrxZZJcqpK5ZQuKdWm3xuS1ElVcOlE" +
                "MOIVeU6Fjxc+3k76vX7vQz7K47d+hmQPnKhUJTFNSlI4Eb36vUqVbnaz3jT+/gkObXwo1sT5kv3e8h8/" +
                "oPH49Y6sk8+F3drP70l5NX5AvBN5yJXkwrAlQVsu2aik3r2SCppZUBX5CTpwD+kgjFNJhbCaoXs78ITo" +
                "vglALsOkjWTDklKjC0JtNlRoGyA4Taoso+Gd+m0WSAkI0G3V6tZs0cHoAwME21r4xTwgedZparlzZAch" +
                "pSq3xDn7BgAw5/GUrnsMqJAkaB5tLNlMV7mk1cPT6ucjvTAdjXKOS+AlMCjsOQ7rjJLsU4hSNg0CzoHu" +
                "lWfXcU6VCWyH5H+nI7hQ4914f0nLgGjdJfLJhz/XVdazzUidW+ab0Q6W/QYJAw8AAhBh5JgWV0kmIHJO" +
                "N9fTX9dfpqQKPx5H5TKwAT7M1CuwJjrXhqIz9BzSMWgA8idCwt7FGii/nm4muXhBamAeZKy2mRt09qz6" +
                "zZB/SajaNQfQMC9GgDTykJZ0O5/dTKdEF6V2HD2jrKQs7SpIGPJB90Dgssk464I4KumyQWerxYBSXfMZ" +
                "Bnxnt/N2f97NGAUZdDbbnIuusc0YBfp4sIZTRm+h6/3F5fU3+jimHRYQvyrKcWifvf9fV53838vhvunR" +
                "fs/TwcBEHTBC9Qrqb9Ur5qBt53bwoir+gozn9M6TLvzw4I6gCveyxQE2kbV0PrJe/a3KH3KMNrj9BQAA";
                
    }
}
