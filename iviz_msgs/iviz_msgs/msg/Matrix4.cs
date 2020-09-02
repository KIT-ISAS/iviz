/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Matrix4")]
    public sealed class Matrix4 : IMessage
    {
        [DataMember (Name = "m")] public float[/*16*/] M { get; set; } // row major 
    
        /// <summary> Constructor for empty message. </summary>
        public Matrix4()
        {
            M = new float[16];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Matrix4(float[] M)
        {
            this.M = M;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Matrix4(Buffer b)
        {
            M = b.DeserializeStructArray<float>(16);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Matrix4(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(M, 16);
        }
        
        public void RosValidate()
        {
            if (M is null) throw new System.NullReferenceException(nameof(M));
            if (M.Length != 16) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 64;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Matrix4";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a50ec1c8c2829b160e4e35c9b42372f7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTaKNjSLVchVUFYoyi9XyE3Myi9S4OICAE9KojAcAAAA";
                
    }
}
