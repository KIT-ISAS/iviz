/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Matrix4 : IDeserializableRos1<Matrix4>, IMessageRos1
    {
        /// <summary> Row major </summary>
        [DataMember (Name = "m")] public float[/*16*/] M;
    
        /// Constructor for empty message.
        public Matrix4()
        {
            M = new float[16];
        }
        
        /// Explicit constructor.
        public Matrix4(float[] M)
        {
            this.M = M;
        }
        
        /// Constructor with buffer.
        public Matrix4(ref ReadBuffer b)
        {
            b.DeserializeStructArray(16, out M);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Matrix4(ref b);
        
        public Matrix4 RosDeserialize(ref ReadBuffer b) => new Matrix4(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(M, 16);
        }
        
        public void RosValidate()
        {
            if (M is null) BuiltIns.ThrowNullReference();
            if (M.Length != 16) BuiltIns.ThrowInvalidSizeForFixedArray(M.Length, 16);
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 64;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/Matrix4";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
