/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class Plane : IDeserializable<Plane>, IMessage
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        [DataMember (Name = "coef")] public double[/*4*/] Coef;
    
        public Plane()
        {
            Coef = new double[4];
        }
        
        public Plane(double[] Coef)
        {
            this.Coef = Coef;
        }
        
        public Plane(ref ReadBuffer b)
        {
            b.DeserializeStructArray(4, out Coef);
        }
        
        public Plane(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(4, out Coef);
        }
        
        public Plane RosDeserialize(ref ReadBuffer b) => new Plane(ref b);
        
        public Plane RosDeserialize(ref ReadBuffer2 b) => new Plane(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Coef, 4);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Coef, 4);
        }
        
        public void RosValidate()
        {
            if (Coef is null) BuiltIns.ThrowNullReference();
            if (Coef.Length != 4) BuiltIns.ThrowInvalidSizeForFixedArray(Coef.Length, 4);
        }
    
        public const int RosFixedMessageLength = 32;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 32;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.Align8(c);
            c += 4 * 8;
            return c;
        }
    
        public const string MessageType = "shape_msgs/Plane";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz3LQQqDMBQE0P0/xYDLdqGtdCF4CbeSxTf+VEESNRGspzdWyGbgDTMZGplX8WIDh9FZ" +
                "OAPGPLGVJzY/2i/CIHcBWbZ7xDse6H4x9BGjR42cKIvPqoZ2YtpcRXaJxUWd+LrYJ74VkZkch0/Zlurf" +
                "EZ3An1dFmgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
