/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestActionResult : IDeserializable<TestRequestActionResult>, IMessage, IActionResult<TestRequestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestRequestResult Result { get; set; }
    
        public TestRequestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestRequestResult();
        }
        
        public TestRequestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public TestRequestActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestRequestResult(ref b);
        }
        
        public TestRequestActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestRequestResult(ref b);
        }
        
        public TestRequestActionResult RosDeserialize(ref ReadBuffer b) => new TestRequestActionResult(ref b);
        
        public TestRequestActionResult RosDeserialize(ref ReadBuffer2 b) => new TestRequestActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength => 5 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 5; /* Result */
            return c;
        }
    
        public const string MessageType = "actionlib/TestRequestActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "0476d1fdf437a3a6e7d6d0e9f5561298";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXMaRxC976+YKg6WUhGO7Xw4quJAgMikZFuFSC6pFDW72+xOsjtD5gPEv8/r2WUB" +
                "ScQcbFNIK8HM6zevX/f0O5I5WVHGRyIzr4yuVLqoXeFe3hhZ3XvpgxMuPpI5OT+jf0N8uFB5YeMjGXzm" +
                "V/L+/uYaUfOGybuGX0+Ajs6lzUVNXubSS7E0oK+KkuxVRWuqmGq9olzEb/12Ra6PjfNSOYF3QZqsrKqt" +
                "CA6LvBGZqeugVSY9Ca9qOtqPnUoLKVbSepWFSlqsNzZXmpcvrayJ0fF2LIvOSEzH11ijHWXBKxDaAiGz" +
                "JJ3SBb4USVDav3nNG0RP/Dkz7tVfSW++MVf4nApko2MhfCk9s6aHFYRmwtJdI9g3zSn7CAKVCOFyJy7i" +
                "Zwv86y4FooELrUxWigsc4W7rS6MBSGItrZJpRQycQQqgvuBNLy4PkHWE1lKbHXyDuI9xDqzucPlMVyWS" +
                "V7EMLhRQEgtX1qxVjqXpNoJklSLtBSxopd0mvKsJmfR+ZbGxCLtiavCUzplMIRO52ChfJs5bRo9pWag8" +
                "+UK2PFkmCf+JFBd4cHzO9Ntd7TT/3E0+jKcfbsTuNRDf4Tf7k+I2UUontuTZmSmxPlmT+FagJjZybtco" +
                "iAZzOJpP/5iIA8xXx5ickWAtlIUbU2KNzgK+m00m7+/mk3EH/PoY2FJG8DhsiZTDHvxJ7A5CLj2crDyf" +
                "3nKC6CEWhC4S8T+vHn5gkqhCYziU56oiRlDe7VBA9GJOtkYZVtwTPF22lO9/H40mk/EB5TfHlDdAllmp" +
                "iGm7kLEKy8AN4TkhToUZ/vJxtteFw3z/TJjUxKPnIdpyz/3ZSHmgT0rDrnAGZbCUqgqWTtGbTX6bjA74" +
                "DcQPT+lZ+psyf8IBsaBM8I/t8u2nOaaUSTTXiNkFC2iYXoIpdwi0bKXXslL5qQO0zusqZSB+/ArO66yn" +
                "jY9FuDdfl7xO4dHw9nZfyQPx07kEU8KdRc8yPEdd5ORpto5J66WyNd9ufH34wy4QmVB+dIhDm7z9DIc4" +
                "T2Y2xVH5NQH42jjhiduP9/NDqIH4OQIO9U6M9vYAksiRNQahRgTZScAo/WYccDB4lUfd0jNqzzG2YbVZ" +
                "0o3C8VE5Uj9qnUlvWFVmEwcTXohSsFy33WUFMu1FxTUmDqYs3pJTGoqCZWwXeXrwyVe8yqbjpHFAM4K0" +
                "IjnP6ebzxDsZkm5Khdki3scHLSW6g3IeiqZxdAntHfNYJ+wnzf7BKcmxQBhxqF4hV1WF3YzpmuRtCKE7" +
                "6J31YEmy3FIio8NRoeWP7tKOF2jFoLc9zsKSKE9l9g+7ETuaQRZzpXOyoCY1bkWZWqpsVwyRgeu36Dz0" +
                "NQtAqg6xKNDnFFb1d8njIeRLp+7lk6k8aSZMZGvRzuepMez+hVPc0BatVf8DrrSnIv4LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
