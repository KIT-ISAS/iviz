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
        
        public double[/*4*/] coef { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Plane()
        {
            coef = new double[4];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Plane(double[] coef)
        {
            BuiltIns.AssertSize(coef, 4);
            this.coef = coef;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Plane(Buffer b)
        {
            this.coef = BuiltIns.DeserializeStructArray<double>(b, 4);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Plane(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.coef, b, 4);
        }
        
        public void Validate()
        {
            BuiltIns.AssertSize(coef, 4);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 32;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "shape_msgs/Plane";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3LQQqDMBQE0P0/xYDLdqGtdCF4CbeSxTf+VEESNRGspzdWyGbgDTMZGplX8WIDh9FZ" +
                "OAPGPLGVJzY/2i/CIHcBWbZ7xDse6H4x9BGjR42cKIvPqoZ2YtpcRXaJxUWd+LrYJ74VkZkch0/Zlurf" +
                "EZ3An1dFmgAAAA==";
                
    }
}
