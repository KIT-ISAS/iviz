/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class ModelCoefficients : IDeserializable<ModelCoefficients>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "values")] public float[] Values;
    
        public ModelCoefficients()
        {
            Values = System.Array.Empty<float>();
        }
        
        public ModelCoefficients(in StdMsgs.Header Header, float[] Values)
        {
            this.Header = Header;
            this.Values = Values;
        }
        
        public ModelCoefficients(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Values);
        }
        
        public ModelCoefficients(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Values);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ModelCoefficients(ref b);
        
        public ModelCoefficients RosDeserialize(ref ReadBuffer b) => new ModelCoefficients(ref b);
        
        public ModelCoefficients RosDeserialize(ref ReadBuffer2 b) => new ModelCoefficients(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Values);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Values);
        }
        
        public void RosValidate()
        {
            if (Values is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 4 * Values.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Values);
        }
    
        public const string MessageType = "pcl_msgs/ModelCoefficients";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ca27dea75e72cb894cd36f9e5005e93e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RTWvcMBC961cM7CFJYROS3hZ6K216KIQktxCWWWnWHpAlVyNv4n/fJy/5uuUQIxCW" +
                "3pfeXAsHKdQvm9vHzPX71cMjHThOYs65H1/8ub93vzdkNWwH6+zi+mi8orvKKXAJNEjlwJVpn5FLu17K" +
                "OspBIkg8jBJoua3zKHYO4n2vRlidJCkc40yTAVQz+TwMU1LPVajqIB/4YGoippFLVT9FLsDnEjQ1+L7w" +
                "IE0dy+TfJMkL/fm5ASaZ+KkqAs1Q8EXYNHW4JDdpQnuNQCt6uM12+ehW9095jXPpUPNrCqo915Zansci" +
                "1gKzbWD27fjKc5igJYFdMDpdzrb4tTOCG7LImH1Pp3jCzVz7nCAoGFpR3kVpwh5VQPWkkU7O3imnRTpx" +
                "yi/yR8U3j8/Iplfd9qZ1j+HFVoNNHZoEcCz5oAHQ3byI+KiSKkXdFS6za6yjpVv9amUDBNYyGuxslr1i" +
                "EoGetPbOamnqy1i2Gpz7DzMLnaW4AgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
