/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class Plane : IDeserializable<Plane>, IMessageRos1
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        [DataMember (Name = "coef")] public double[/*4*/] Coef;
    
        /// Constructor for empty message.
        public Plane()
        {
            Coef = new double[4];
        }
        
        /// Explicit constructor.
        public Plane(double[] Coef)
        {
            this.Coef = Coef;
        }
        
        /// Constructor with buffer.
        public Plane(ref ReadBuffer b)
        {
            b.DeserializeStructArray(4, out Coef);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Plane(ref b);
        
        public Plane RosDeserialize(ref ReadBuffer b) => new Plane(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Coef, 4);
        }
        
        public void RosValidate()
        {
            if (Coef is null) BuiltIns.ThrowNullReference();
            if (Coef.Length != 4) BuiltIns.ThrowInvalidSizeForFixedArray(Coef.Length, 4);
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 32;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "shape_msgs/Plane";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz3LQQqDMBQE0P0/xYDLdqGtdCF4CbeSxTf+VEESNRGspzdWyGbgDTMZGplX8WIDh9FZ" +
                "OAPGPLGVJzY/2i/CIHcBWbZ7xDse6H4x9BGjR42cKIvPqoZ2YtpcRXaJxUWd+LrYJ74VkZkch0/Zlurf" +
                "EZ3An1dFmgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
