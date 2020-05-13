using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Point32 : IMessage
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        
        [DataMember] public float x { get; }
        [DataMember] public float y { get; }
        [DataMember] public float z { get; }
    
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
            this = b.Deserialize<Point32>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Point32(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 12;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Point32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz1QO27DMAzdfYoHZGmBIkNyhE7dOvQCskXbRGVREOmk6elLKWkADRTJ9+MBXysrJskW" +
                "OCtsJRRRNpYMmRH8x9nAGXMlgpYw0cuVbcX5hJFN21apNLE65PU4HPDh6wpvybZRpAgT7Er47EzXlSpd" +
                "qDYZ5TGRc6tRiI2or5xPR8B5/HVzD6YcQ3flHScsVTaxBjaqUqiGkRPbrUP/kRuphoUaJJLyku9mLHwT" +
                "9oLk43ui5ipDXYPz4ugkj2DNjyIYJE/0hqDtEu1IU/BE/UDd83uSPTbtYU4SPAJ+ntXtWf0Of3eAjDBw" +
                "AQAA";
                
    }
}
