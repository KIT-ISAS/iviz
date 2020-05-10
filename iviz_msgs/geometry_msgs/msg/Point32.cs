using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Point32 : IMessage
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Point32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Point32(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Point32(Buffer b)
        {
            this.x = b.Deserialize<float>();
            this.y = b.Deserialize<float>();
            this.z = b.Deserialize<float>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Point32(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.x);
            b.Serialize(this.y);
            b.Serialize(this.z);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 12;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/Point32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz1QO27DMAzdfYoHZGmBIkNyhE7dOvQCskXbRGVREOmk6elLKWkADRTJ9+MBXysrJskW" +
                "OCtsJRRRNpYMmRH8x9nAGXMlgpYw0cuVbcX5hJFN21apNLE65PU4HPDh6wpvybZRpAgT7Er47EzXlSpd" +
                "qDYZ5TGRc6tRiI2or5xPR8B5/HVzD6YcQ3flHScsVTaxBjaqUqiGkRPbrUP/kRuphoUaJJLyku9mLHwT" +
                "9oLk43ui5ipDXYPz4ugkj2DNjyIYJE/0hqDtEu1IU/BE/UDd83uSPTbtYU4SPAJ+ntXtWf0Of3eAjDBw" +
                "AQAA";
                
    }
}
