/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Matrix4 : IDeserializable<Matrix4>, IMessage
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
        [Preserve] public const int RosFixedMessageLength = 64;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Matrix4";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
