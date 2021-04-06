/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = "shape_msgs/Plane")]
    public sealed class Plane : IDeserializable<Plane>, IMessage
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        [DataMember (Name = "coef")] public double[/*4*/] Coef { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Plane()
        {
            Coef = new double[4];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Plane(double[] Coef)
        {
            this.Coef = Coef;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Plane(ref Buffer b)
        {
            Coef = b.DeserializeStructArray<double>(4);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Plane(ref b);
        }
        
        Plane IDeserializable<Plane>.RosDeserialize(ref Buffer b)
        {
            return new Plane(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Coef, 4);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Coef is null) throw new System.NullReferenceException(nameof(Coef));
            if (Coef.Length != 4) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 32;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/Plane";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3LQQqDMBQE0P0/xYDLdqGtdCF4CbeSxTf+VEESNRGspzdWyGbgDTMZGplX8WIDh9FZ" +
                "OAPGPLGVJzY/2i/CIHcBWbZ7xDse6H4x9BGjR42cKIvPqoZ2YtpcRXaJxUWd+LrYJ74VkZkch0/Zlurf" +
                "EZ3An1dFmgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
