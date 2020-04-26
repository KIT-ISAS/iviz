using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistWithCovariance : IMessage
    {
        // This expresses velocity in free space with uncertainty.
        
        public Twist twist;
        
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        public double[/*36*/] covariance;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovariance()
        {
            twist = new Twist();
            covariance = new double[36];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            twist.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out covariance, ref ptr, end, 36);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            twist.Serialize(ref ptr, end);
            BuiltIns.Serialize(covariance, ref ptr, end, 36);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 336;
    
        public IMessage Create() => new TwistWithCovariance();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "geometry_msgs/TwistWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TTWvcQAy9+1c8yCUBx4Wm7CHQc8khUNql9INStLbsncQeGc04a+fXV+N1nCzNoYdS" +
                "g408o/c0enpzhu3eBfDYK4fAAQ/cSuniBOdRKzNCTyXj4OIegy9ZIzkfpyLLtgcXImL6ZtkZPsnhsqM7" +
                "USgnMvaRohMPqRH3jM24QSkPpI6MBh1FdaPhtrYn6tb0npQ6jqwBQ2AQajdydUmjHfOUuTD0jfFrxZrP" +
                "NV5gSfna9s/HHFOOxxwqSwHayRDxFYnxj+Vvry9/n5cvsroVipt3P642P180k2Xv//GT3X7+cI2GxbrR" +
                "6VcXmvBm1nsW7G8GtlO5Z2+LUeBiQOs8k4J8ZW8ztBabWjEU2Rcuo+gVlpTn/yXv/3S3VH3qb520TdJa" +
                "THunDRZI04+wXPHthI7JmxvlGWnAyqlBk1WSzZRrUc5NDlRiynlJcnZ0b5TszWuGpr43MkJU8qE9WmBW" +
                "EOdcNEWOw95UnbOcbyzRGBr2rK6EusZVR6QV6lYwYWnOTFq/tavUtsczH4uZcY3kyXAXBW5qTDLgkBqy" +
                "QFFRpES04/VctGvTeSWfL8mR4lTQj2KzN1lCoIZNuxCZKru2i4UxrtG0Ro/ZbyhtY0kQBAAA";
                
    }
}
