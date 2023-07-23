/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class SetParametersResult : IHasSerializer<SetParametersResult>, IMessage
    {
        // A true value of the same index indicates that the parameter was set
        // successfully. A false value indicates the change was rejected.
        [DataMember (Name = "successful")] public bool Successful;
        // Reason why the setting was either successful or a failure. This should only be
        // used for logging and user interfaces.
        [DataMember (Name = "reason")] public string Reason;
    
        public SetParametersResult()
        {
            Reason = "";
        }
        
        public SetParametersResult(bool Successful, string Reason)
        {
            this.Successful = Successful;
            this.Reason = Reason;
        }
        
        public SetParametersResult(ref ReadBuffer b)
        {
            b.Deserialize(out Successful);
            Reason = b.DeserializeString();
        }
        
        public SetParametersResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out Successful);
            b.Align4();
            Reason = b.DeserializeString();
        }
        
        public SetParametersResult RosDeserialize(ref ReadBuffer b) => new SetParametersResult(ref b);
        
        public SetParametersResult RosDeserialize(ref ReadBuffer2 b) => new SetParametersResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Successful);
            b.Serialize(Reason);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Successful);
            b.Align4();
            b.Serialize(Reason);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Reason);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Successful
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Reason);
            return size;
        }
    
        public const string MessageType = "rcl_interfaces/SetParametersResult";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f25d8aed319fb6d8decc390bac218615";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE02PwY3DMAwE/6qCQP7uIS0crgFGWlkKFCkgqeTc/dEOAvizDy45u7zQlUwm6MXNdWSy" +
                "AlJ+gGpP+Nu1RjaoG2yH+2Rx3yD0ZiWFhQvpjBGqeba2Lc7M3PQLPSNAsXBfcZwK7oiGtITbGO3ECE78" +
                "Aevo9C7bpxLMal+PO1SfyGmfhhB7Zm1TsNBvqd6rjNkSjd42usGBU5Eo+2Yb67qjuKd9KF7Qn8nssCWo" +
                "ye7JkR7CP3f1+n8gAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<SetParametersResult> CreateSerializer() => new Serializer();
        public Deserializer<SetParametersResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<SetParametersResult>
        {
            public override void RosSerialize(SetParametersResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(SetParametersResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(SetParametersResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(SetParametersResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(SetParametersResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<SetParametersResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out SetParametersResult msg) => msg = new SetParametersResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out SetParametersResult msg) => msg = new SetParametersResult(ref b);
        }
    }
}
