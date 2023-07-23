/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformResult : IHasSerializer<LookupTransformResult>, IMessage, IResult<LookupTransformActionResult>
    {
        [DataMember (Name = "transform")] public GeometryMsgs.TransformStamped Transform;
        [DataMember (Name = "error")] public Tf2Msgs.TF2Error Error;
    
        public LookupTransformResult()
        {
            Transform = new GeometryMsgs.TransformStamped();
            Error = new Tf2Msgs.TF2Error();
        }
        
        public LookupTransformResult(GeometryMsgs.TransformStamped Transform, Tf2Msgs.TF2Error Error)
        {
            this.Transform = Transform;
            this.Error = Error;
        }
        
        public LookupTransformResult(ref ReadBuffer b)
        {
            Transform = new GeometryMsgs.TransformStamped(ref b);
            Error = new Tf2Msgs.TF2Error(ref b);
        }
        
        public LookupTransformResult(ref ReadBuffer2 b)
        {
            Transform = new GeometryMsgs.TransformStamped(ref b);
            Error = new Tf2Msgs.TF2Error(ref b);
        }
        
        public LookupTransformResult RosDeserialize(ref ReadBuffer b) => new LookupTransformResult(ref b);
        
        public LookupTransformResult RosDeserialize(ref ReadBuffer2 b) => new LookupTransformResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Transform.RosSerialize(ref b);
            Error.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Transform.RosSerialize(ref b);
            Error.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Transform.RosValidate();
            Error.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Transform.RosMessageLength;
                size += Error.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Transform.AddRos2MessageLength(size);
            size = Error.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformResult";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3fe5db6a19ca9cfb675418c5ad875c36";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WXW/bNhR916+4aB6aDIm8Jl0xBE0Bo3E6oYmdOU6wYRgMRrqWiEikSlJxvV+/Q+rD" +
                "blNse1gTGLF4eb/PuVfOWVfszGZZ2dyOFkYou9KmunGiqjkj1wsitzrudC6OJ8ZoQ+z/R2f/8190dfPh" +
                "lPJ/TCvao0UhLfHn2rC1bElsM6WV0RWlWptMKuEYZ1ExFSwyNnE4LKV34TS5gp9qpoUss+VWsY9WIZTI" +
                "mfyjtq7cUGPRovtNcAOtt4IKw6uzF4Vz9elotJYPMjbaxtrkI7d68c6t3o7EO6pF+gBHsbe5YTh0ljKd" +
                "NhUrJ5zUilAHYhhcKV9SEMZR9EuooSslss5IlX+VLu2FbNpKcNSrtkiv1EqjoZs76H4nGK3LWgTbzH29" +
                "TqhMmAzddCITToRaC5kXbI5KfuQSRi33wq3b1GzjHgJ8clZsRNl3HyCmuqoaJVOPoJNAadcellKBHrUw" +
                "TqZNKcwTwL13fCx/alilTMn5KXSU5bRxEglt4CE1LKzvdnJOUSOVOzn2Buj2H3NtX/0Z7S3W+ghyzgHQ" +
                "kAV6LxztMDUjYU8R7Ie2yhhB0CVGuMzSfpAtcbQHhGjIhWudFrSPEq43rgAzPJiPwkhxXwYmpmgFvL70" +
                "Ri8Pdjyr4FoJpXv3rcdtjP/iVg1+fU1HBcArfRtsk6OTUKyNfpTZdgzSUoLFVMp7I8wm8lZtyGjvInDS" +
                "eRwDNPgW1upUAomM1tIVPaWH2Xve7dKzzLAHC2XYUNJ2s9yzWzOjW2v9hEXW82xlMM4W8w1SRXecOm1O" +
                "WvsyzHD0awMDo/yMG90O+/MU2SXzjRIFPYa7r/L3I5EE7mqFEahYAFZM22AJw0wamPrdBK+M1YeVdYh1" +
                "hm2Gfijt4KMSD3DJIJK3FnUNZ2K3J14Mk32O8/iQ1gX6G7Q8EcL8homXKRmZy5030mAsqCvukPCSApHK" +
                "ss25DQYI4aTv9kFMyYo2uqG1LwgPpls0GvAOeYU5cFof+i3Tufiyodca0759JyjrsOKA+qrUwr15TZ+H" +
                "p83w9NezQL3l2LfQVqSNHF40X2DuT5+2BPVN/teC+qf196Lxk98dYf3+TNPZcjKfz+Z0Rj92osvZ7OPt" +
                "9SB+1Ynfz6bTyftFcpcsfh8uj7vLyW+L+fh6djleJLPpcHvS3SbTu/Flcr4czz/cXk2mi0HhdaewSK4m" +
                "s9ut/KdePh9Pby5m86vh5k3UXbU/m7pNFw7L9hBFfwMDb9ZIjQkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LookupTransformResult> CreateSerializer() => new Serializer();
        public Deserializer<LookupTransformResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LookupTransformResult>
        {
            public override void RosSerialize(LookupTransformResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(LookupTransformResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(LookupTransformResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(LookupTransformResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(LookupTransformResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<LookupTransformResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out LookupTransformResult msg) => msg = new LookupTransformResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out LookupTransformResult msg) => msg = new LookupTransformResult(ref b);
        }
    }
}
