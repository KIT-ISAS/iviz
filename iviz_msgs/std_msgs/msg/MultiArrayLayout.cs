
namespace Iviz.Msgs.std_msgs
{
    public sealed class MultiArrayLayout : IMessage
    {
        // The multiarray declares a generic multi-dimensional array of a
        // particular data type.  Dimensions are ordered from outer most
        // to inner most.
        
        public MultiArrayDimension[] dim; // Array of dimension properties
        public uint data_offset; // padding elements at front of data
        
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/MultiArrayLayout";
    
        public IMessage Create() => new MultiArrayLayout();
    
        public int GetLength()
        {
            int size = 8;
            for (int i = 0; i < dim.Length; i++)
            {
                size += dim[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiArrayLayout()
        {
            dim = System.Array.Empty<MultiArrayDimension>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out dim, ref ptr, end, 0);
            BuiltIns.Deserialize(out data_offset, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(dim, ref ptr, end, 0);
            BuiltIns.Serialize(data_offset, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "0fed2a11c13e11c5571b4e2a995a91a3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq2US4/aMBSF95H4D1ewAQqUx2jUGYkFUqVuOqupVI0QQp74JjEkNrKdodNf3+MkhNDp" +
                "shESiX0f3znxzYB+ZExFmXslrBXvJDnOhWVHglLWbFVc706lKlg7ZbTIqQ41CYloQCdhvYpLZJEUXpB/" +
                "P/GM6OslHqUsk7GSLUtKrCnIlJ4tFcZ55HtDSuvmeRZFT6HdJnRoS2x3hPY0oM2lcUtDJ2tODAJ2Uam0" +
                "Xy0rir1JEseemitQSql0SpwzMj2gfGDRvqqGjAgomzhm54x15DJT5pI2339uXp7plelslfesgUpgL9wt" +
                "hPNWSUYFoSW5E8cqURBb6ZwGXZ3YRNmgc0D4XY0fqslhchzRuoLZdjV8Csn7usV2sRur25XlbnzAynEX" +
                "DYIEsABCWDmh1TTOBKzN6f5u/uvuy5xUIVJoUT6DELDlLN7AGZvcWGqCHaqcK/WQfdUi3GPVAJ23890s" +
                "F6+oC9x+xirNfP+65dRvhudrQsfOakWL1dUYNONAs6aH5eJ+PicaauO5iWzMJOXoUMK5qhzcrthHTcFF" +
                "l+CspM/6150WAI06qzcA+F88LC/by265xof+da8tuOqsteUqWz6+ScsJ4yTheHuMWLDcmvOEDriB32Wh" +
                "J9VpOYbnuuMs6kXr/3z1oqfnb4/wVO4Ll7rP/5iuXhS0YDYaCzAt9R1MT9UbDn17eC8j1hiCyMvb+SuQ" +
                "hmFQ8CWgUivvRm1i7VpIrO8+pvaiP1dZMV6VBAAA";
                
    }
}
