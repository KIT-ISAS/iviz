/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/XRGazeState")]
    public sealed class XRGazeState : IDeserializable<XRGazeState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "transform")] public GeometryMsgs.Transform Transform;
    
        /// <summary> Constructor for empty message. </summary>
        public XRGazeState()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public XRGazeState(bool IsValid, in StdMsgs.Header Header, in GeometryMsgs.Transform Transform)
        {
            this.IsValid = IsValid;
            this.Header = Header;
            this.Transform = Transform;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal XRGazeState(ref Buffer b)
        {
            IsValid = b.Deserialize<bool>();
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Transform);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new XRGazeState(ref b);
        }
        
        XRGazeState IDeserializable<XRGazeState>.RosDeserialize(ref Buffer b)
        {
            return new XRGazeState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(Transform);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 57 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/XRGazeState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b062fdd884f10dff560e6f7d4400606b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7TQBC9W8o/jNRDW5QGqUUcInFDQA9IRa24RhPvxF6x3nV3x0nN1/PWTpwWiuAA" +
                "jSJ5bc97M/PmjdchOLJptWVnTfFJ2EikergUlYRGNParJlXp9V1knzYhNqSHU1HMinf/+DcrPt9+XFJS" +
                "M6YdS5oVJ3Sr7A1HQyiKDSsTaqDaVrXECydbcUBx04qh4a32raQFgHe1TeiRKvES2bmeuoQgDVSGpum8" +
                "LVmF1DbyBA+k9cTUclRbdo4j4kM01ufwTeRGMjv+Se478aXQ9fslYnySslOLgnowlFE4WV/hJRWd9Xp1" +
                "mQHFyd0uXOBWKig+JSetWXOx8tBGSblOTkvkeDU2twA31BFkMYnOhmcr3KZzQhKUIG0oazpD5Te91sGD" +
                "UGjL0fLaSSYuoQBYTzPo9PwRcy57SZ59ONCPjMccf0PrJ97c00WNmbncfeoqCIjANoatNQhd9wNJ6ax4" +
                "JWfXkWNfZNSYsjj5kDVGEFDDRHDllEJpMQBDO6t1kTRm9mEaKzj4vxnyN8swO/grSp4XOklDV9OO0Fp0" +
                "JwLBduEX/8CWHidBxy2XsFPxVUoN8WrEO1YbfPGlAyB6HCkGHZ+9UJ/7cp7rkmk7vPyphbwP14ODg4f/" +
                "G2EMF6s2IQE0NgKKNhZglSjQSeZklUyAJD4oOBr+BkqBnTKa2xZk/FiW/BiQM1lUizntakg8RGU7DMs7" +
                "rLstKdrKmuNAJjDTvrs56eYSdnJurHlMhimC5CD4+YKuN9SHjna5IRzi/isTMOGprmEbNIR5/sTsKZ4q" +
                "ehOw85AlJa6wOD4pPnAY/MYF1rdv6GE69dPp+wtN+2i0ZwfuKcS8q6OCT8ae7+6PNs06/6mn6bSDmX8A" +
                "6SYEZYcGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
