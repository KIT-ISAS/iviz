/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class BoundingBoxStamped : IDeserializable<BoundingBoxStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "boundary")] public BoundingBox Boundary;
    
        /// Constructor for empty message.
        public BoundingBoxStamped()
        {
            Boundary = new BoundingBox();
        }
        
        /// Explicit constructor.
        public BoundingBoxStamped(in StdMsgs.Header Header, BoundingBox Boundary)
        {
            this.Header = Header;
            this.Boundary = Boundary;
        }
        
        /// Constructor with buffer.
        public BoundingBoxStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Boundary = new BoundingBox(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BoundingBoxStamped(ref b);
        
        public BoundingBoxStamped RosDeserialize(ref ReadBuffer b) => new BoundingBoxStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Boundary.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Boundary is null) BuiltIns.ThrowNullReference();
            Boundary.RosValidate();
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/BoundingBoxStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "da26f418902c5d679a0e80380a951267";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VwU7cMBC95ytG4gBUSypB1QNSD0VVC4dKVKBe0aw9SawmdrCdXcLX99nZzbICiR4K" +
                "q0jrxDNv3sw8jy+FtXhq8l9x4Qarja0v3AMt05r9WBRf/vOv+Hnz45xC1HddqMPHyyn2Ad1EThE1dRJZ" +
                "c2SqHKiZuhF/0spKWjhx14umvBvHXkIJx9vGBMJTixXPbTvSEGAUHSnXdYM1iqNQNJ3s+cPTWGLq2Uej" +
                "hpY97J1HAZJ55bmThI4nyP0gVgldfTuHjQ2ihmhAaASC8sIBRcMmFYOx8ew0ORQHt2t3glepUeA5OMWG" +
                "YyIrD72XkHhyOEeMD1NyJbBRHEEUHegof7vDazgmBAEF6Z1q6AjMr8fYOAtAoRV7w8tWErBCBYB6mJwO" +
                "j58g2wxt2bot/IS4i/EvsHbGTTmdNOhZm7IPQ40CwrD3bmU0TJdjBlGtERupNUuf5JS8ppDFwfdUYxjB" +
                "K3cE/xyCUwYN0LQ2sSlC9Ak9d+PO6LdSo1mZx0mOT85AUYuDFv047VyjbqSQC9S6v/NbVHT+DPV8lLdi" +
                "+JwLWvCVvCQZgRRHg665ivpEE32svKDQPStZpHOQPuvNvsm26Bw5b7a+JRXXDnqdDYpfA/rgbcbd2b1X" +
                "gqCyPdtQa2RjQ9bTzB+54PBmynvpFlXrOH7+RA/zapxXj+9Df1e6bQ5zo6DxvXruk09v97u6YwJ2ZfFK" +
                "RtvV+n1y26j9pcRolff2UyrTCL3KQ89ZjMxOGC3DdJ494aiNh2uW4S3mvSBx6NZE0k4CWZe00PEfQAom" +
                "UPLmvgcYrgHPNrRTKfEZLkdS1uWC1o3YySpNkDzv8w1hFHlTGz15pgrPzkyb5BYUq1NMoLadOE/BID+A" +
                "eDc17rikq4pGN9A6JYSF31xMjpYy88oDNDq3SLfSBuIFraMsIXCdBBAirsRXu/4XZxTnBbsHAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
