/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Matrix4 : IDeserializableCommon<Matrix4>, IMessageCommon
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
            b.DeserializeStructArray(16, out M);
        }
        
        public Matrix4(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(16, out M);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Matrix4(ref b);
        
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
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, M, 16);
        }
    
        public const string MessageType = "iviz_msgs/Matrix4";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
