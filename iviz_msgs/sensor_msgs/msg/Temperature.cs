
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Temperature : IMessage
    {
        // Single temperature reading.
        
        public std_msgs.Header header; // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        
        public double temperature; // Measurement of the Temperature in Degrees Celsius
        
        public double variance; // 0 is interpreted as variance unknown
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Temperature";
    
        public IMessage Create() => new Temperature();
    
        public int GetLength()
        {
            int size = 16;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Temperature()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out temperature, ref ptr, end);
            BuiltIns.Deserialize(out variance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(temperature, ref ptr, end);
            BuiltIns.Serialize(variance, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq2TT4vbMBDF74J8h4EcdreQtLSlh0BPXfrnsFDY3MPEntiisuRqxkn97ftkN03CXnqo" +
                "CFiRZ34zem9MS3r2sQlCJl0vmW3IQlm4xunaOfqKrWRq58dlLcl8J2rc9eSVrJXpYN5coU6s1Akr9rW7" +
                "yr9dSzpk7mTn6zMspIrNp0jp8IL5pz10dwiJ7cP7m7cz72mu2Um0M2J7FeQjPUqTRZQ+SVA/6BXtyNlz" +
                "rGbURHtT2vLRJPdZTGrCrf5GDfFHTKe4cB//81q4p+cvG1Krd502+nr2YuHgmXGsOdeQ1rhmYzokmOSb" +
                "VvIqyFECTdag0+mtjb3oGonbtlxEqZEIKUIYaVAEWaIqdd0QPVSfnbzJRyYUY+o5m6+GwBnxKcOFEj55" +
                "V+j4qfwcpKjy7XGDmKhSDebR0AhCBecUzuEluQF6vntbEtxye0qrIm+DGbvMlbVspVn5BdW19Mm6QY1X" +
                "8+XWYEMdQZVa6X462+GvPhCKVELSp6qle3T+fbQWs1SmYLJtj4EHuIICoN6VpLuHK3Jpe0ORYzrjZ+Kl" +
                "xr9gC2XmljutWngWyu11aCAgAvucjr5G6H6cIFXwZV6D32fOo5s+qKmkW34uGiMIWZMjeLJqqjwMqOnk" +
                "rXVqudDPX5JbuN/IQO/63gMAAA==";
                
    }
}
