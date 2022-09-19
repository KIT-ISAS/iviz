/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Matrix4 : IHasSerializer<Matrix4>, IMessage
    {
        /// <summary> Row major </summary>
        [DataMember (Name = "m")] public float[/*16*/] M;
    
        public Matrix4()
        {
            M = new float[16];
        }
        
        public Matrix4(float[] M)
        {
            this.M = M;
        }
        
        public Matrix4(ref ReadBuffer b)
        {
            {
                var array = new float[16];
                b.DeserializeStructArray(ref Unsafe.As<float, byte>(ref array[0]), 16 * 4);
                M = array;
            }
        }
        
        public Matrix4(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                var array = new float[16];
                b.DeserializeStructArray(ref Unsafe.As<float, byte>(ref array[0]), 16 * 4);
                M = array;
            }
        }
        
        public Matrix4 RosDeserialize(ref ReadBuffer b) => new Matrix4(ref b);
        
        public Matrix4 RosDeserialize(ref ReadBuffer2 b) => new Matrix4(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(M, 16);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(M, 16);
        }
        
        public void RosValidate()
        {
            if (M is null) BuiltIns.ThrowNullReference();
            if (M.Length != 16) BuiltIns.ThrowInvalidSizeForFixedArray(M.Length, 16);
        }
    
        public const int RosFixedMessageLength = 64;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 64;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "iviz_msgs/Matrix4";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Matrix4> CreateSerializer() => new Serializer();
        public Deserializer<Matrix4> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Matrix4>
        {
            public override void RosSerialize(Matrix4 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Matrix4 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Matrix4 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Matrix4 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Matrix4>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Matrix4 msg) => msg = new Matrix4(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Matrix4 msg) => msg = new Matrix4(ref b);
        }
    }
}
