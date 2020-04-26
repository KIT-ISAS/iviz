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
        
        public float x;
        public float y;
        public float z;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out x, ref ptr, end);
            BuiltIns.Deserialize(out y, ref ptr, end);
            BuiltIns.Deserialize(out z, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(x, ref ptr, end);
            BuiltIns.Serialize(y, ref ptr, end);
            BuiltIns.Serialize(z, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 12;
    
        public IMessage Create() => new Point32();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "geometry_msgs/Point32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz1QO27DMAzdfYoHZGmBIkNyhE7dOvQCskXbRGVREOmk6elLKWkADRTJ9+MBXysrJskW" +
                "OCtsJRRRNpYMmRH8x9nAGXMlgpYw0cuVbcX5hJFN21apNLE65PU4HPDh6wpvybZRpAgT7Er47EzXlSpd" +
                "qDYZ5TGRc6tRiI2or5xPR8B5/HVzD6YcQ3flHScsVTaxBjaqUqiGkRPbrUP/kRuphoUaJJLyku9mLHwT" +
                "9oLk43ui5ipDXYPz4ugkj2DNjyIYJE/0hqDtEu1IU/BE/UDd83uSPTbtYU4SPAJ+ntXtWf0Of3eAjDBw" +
                "AQAA";
                
    }
}
