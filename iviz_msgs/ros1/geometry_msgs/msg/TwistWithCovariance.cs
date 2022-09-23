/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class TwistWithCovariance : IHasSerializer<TwistWithCovariance>, IMessage
    {
        // This expresses velocity in free space with uncertainty.
        [DataMember (Name = "twist")] public Twist Twist;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        public TwistWithCovariance()
        {
            Covariance = new double[36];
        }
        
        public TwistWithCovariance(in Twist Twist, double[] Covariance)
        {
            this.Twist = Twist;
            this.Covariance = Covariance;
        }
        
        public TwistWithCovariance(ref ReadBuffer b)
        {
            b.Deserialize(out Twist);
            {
                var array = new double[36];
                b.DeserializeStructArray(array);
                Covariance = array;
            }
        }
        
        public TwistWithCovariance(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Twist);
            {
                var array = new double[36];
                b.DeserializeStructArray(array);
                Covariance = array;
            }
        }
        
        public TwistWithCovariance RosDeserialize(ref ReadBuffer b) => new TwistWithCovariance(ref b);
        
        public TwistWithCovariance RosDeserialize(ref ReadBuffer2 b) => new TwistWithCovariance(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Twist);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in Twist);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) BuiltIns.ThrowNullReference();
            if (Covariance.Length != 36) BuiltIns.ThrowInvalidSizeForFixedArray(Covariance.Length, 36);
        }
    
        public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 336;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/TwistWithCovariance";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71TTWvcQAy9+1c8yCUBx4Wm7CHQc8khUNql9INStLbsncQeGc04a+fXV+N1nCzNoYdS" +
                "g408o/c0enpzhu3eBfDYK4fAAQ/cSuniBOdRKzNCTyXj4OIegy9ZIzkfpyLLtgcXImL6ZtkZPsnhsqM7" +
                "USgnMvaRohMPqRH3jM24QSkPpI6MBh1FdaPhtrYn6tb0npQ6jqwBQ2AQajdydUmjHfOUuTD0jfFrxZrP" +
                "NV5gSfna9s/HHFOOxxwqSwHayRDxFYnxj+Vvry9/n5cvsroVipt3P642P180k2Xv//GT3X7+cI2GxbrR" +
                "6VcXmvBm1nsW7G8GtlO5Z2+LUeBiQOs8k4J8ZW8ztBabWjEU2Rcuo+gVlpTn/yXv/3S3VH3qb520TdJa" +
                "THunDRZI04+wXPHthI7JmxvlGWnAyqlBk1WSzZRrUc5NDlRiynlJcnZ0b5TszWuGpr43MkJU8qE9WmBW" +
                "EOdcNEWOw95UnbOcbyzRGBr2rK6EusZVR6QV6lYwYWnOTFq/tavUtsczH4uZcY3kyXAXBW5qTDLgkBqy" +
                "QFFRpES04/VctGvTeSWfL8mR4lTQj2KzN1lCoIZNuxCZKru2i4UxrtG0Ro/ZbyhtY0kQBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwistWithCovariance> CreateSerializer() => new Serializer();
        public Deserializer<TwistWithCovariance> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwistWithCovariance>
        {
            public override void RosSerialize(TwistWithCovariance msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwistWithCovariance msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwistWithCovariance _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TwistWithCovariance _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<TwistWithCovariance>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwistWithCovariance msg) => msg = new TwistWithCovariance(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwistWithCovariance msg) => msg = new TwistWithCovariance(ref b);
        }
    }
}
