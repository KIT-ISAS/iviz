/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class ModelCoefficients : IDeserializable<ModelCoefficients>, IHasSerializer<ModelCoefficients>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "values")] public float[] Values;
    
        public ModelCoefficients()
        {
            Values = EmptyArray<float>.Value;
        }
        
        public ModelCoefficients(in StdMsgs.Header Header, float[] Values)
        {
            this.Header = Header;
            this.Values = Values;
        }
        
        public ModelCoefficients(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Values = array;
            }
        }
        
        public ModelCoefficients(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Values = array;
            }
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 4 * Values.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Values.Length
            size += 4 * Values.Length;
            return size;
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
    
        public Serializer<ModelCoefficients> CreateSerializer() => new Serializer();
        public Deserializer<ModelCoefficients> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelCoefficients>
        {
            public override void RosSerialize(ModelCoefficients msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelCoefficients msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelCoefficients msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelCoefficients msg) => msg.Ros2MessageLength;
            public override void RosValidate(ModelCoefficients msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<ModelCoefficients>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelCoefficients msg) => msg = new ModelCoefficients(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelCoefficients msg) => msg = new ModelCoefficients(ref b);
        }
    }
}
