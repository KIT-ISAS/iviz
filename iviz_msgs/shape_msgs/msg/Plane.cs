using System.Runtime.Serialization;

namespace Iviz.Msgs.shape_msgs
{
    public sealed class Plane : IMessage
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        
        public double[/*4*/] coef;
    
        /// <summary> Constructor for empty message. </summary>
        public Plane()
        {
            coef = new double[4];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out coef, ref ptr, end, 4);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(coef, ref ptr, end, 4);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 32;
    
        public IMessage Create() => new Plane();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "shape_msgs/Plane";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3LQQqDMBQE0P0/xYDLdqGtdCF4CbeSxTf+VEESNRGspzdWyGbgDTMZGplX8WIDh9FZ" +
                "OAPGPLGVJzY/2i/CIHcBWbZ7xDse6H4x9BGjR42cKIvPqoZ2YtpcRXaJxUWd+LrYJ74VkZkch0/Zlurf" +
                "EZ3An1dFmgAAAA==";
                
    }
}
