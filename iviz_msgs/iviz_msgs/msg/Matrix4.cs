/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Matrix4 : IDeserializable<Matrix4>, IMessage
    {
        [DataMember (Name = "m")] public float[/*16*/] M; // row major 
    
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
        internal Matrix4(ref Buffer b)
        {
            M = b.DeserializeStructArray<float>(16);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Matrix4(ref b);
        
        Matrix4 IDeserializable<Matrix4>.RosDeserialize(ref Buffer b) => new Matrix4(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(M, 16);
        }
        
        public void RosValidate()
        {
            if (M is null) throw new System.NullReferenceException(nameof(M));
            if (M.Length != 16) throw new RosInvalidSizeForFixedArrayException(nameof(M), M.Length, 16);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 64;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Matrix4";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
