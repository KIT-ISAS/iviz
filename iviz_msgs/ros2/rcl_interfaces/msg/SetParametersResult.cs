/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class SetParametersResult : IDeserializable<SetParametersResult>, IMessage
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
            b.DeserializeString(out Reason);
        }
        
        public SetParametersResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out Successful);
            b.DeserializeString(out Reason);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new SetParametersResult(ref b);
        
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
            b.Serialize(Reason);
        }
        
        public void RosValidate()
        {
            if (Reason is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + WriteBuffer.GetStringSize(Reason);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Successful);
            WriteBuffer2.AddLength(ref c, Reason);
        }
    
        public const string MessageType = "rcl_interfaces/SetParametersResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f25d8aed319fb6d8decc390bac218615";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE02PwY3DMAwE/6qCQP7uIS0crgFGWlkKFCkgqeTc/dEOAvizDy45u7zQlUwm6MXNdWSy" +
                "AlJ+gGpP+Nu1RjaoG2yH+2Rx3yD0ZiWFhQvpjBGqeba2Lc7M3PQLPSNAsXBfcZwK7oiGtITbGO3ECE78" +
                "Aevo9C7bpxLMal+PO1SfyGmfhhB7Zm1TsNBvqd6rjNkSjd42usGBU5Eo+2Yb67qjuKd9KF7Qn8nssCWo" +
                "ye7JkR7CP3f1+n8gAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
