/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestActionResult : IHasSerializer<TestActionResult>, IMessage, IActionResult<TestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestResult Result { get; set; }
    
        public TestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestResult();
        }
        
        public TestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public TestActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestResult(ref b);
        }
        
        public TestActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestResult(ref b);
        }
        
        public TestActionResult RosDeserialize(ref ReadBuffer b) => new TestActionResult(ref b);
        
        public TestActionResult RosDeserialize(ref ReadBuffer2 b) => new TestActionResult(ref b);
    
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
            if (Status is null) BuiltIns.ThrowNullReference(nameof(Status));
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Result
            return size;
        }
    
        public const string MessageType = "actionlib/TestActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3d669e3a63aa986c667ea7b0f46ce85e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WwXLbNhC98yswo0PsTq00SZumntFBlVVHHSfxyGovnY4HBFYkWhJUAVCy/j5vQYqi" +
                "HKvRIYlGNm0JePvw9u1i35LU5EQeH4lUwVS2MOl96TP//LqSxV2QofbCx0eyIB/m5OsiCBcfyegLv5J3" +
                "d9eXCKcbCm8bYgMBHlZLp0VJQWoZpFhW4G2ynNxFQWsqmGO5Ii3it2G7Ij/ExkVuvMA7I0tOFsVW1B6L" +
                "QiVUVZa1NUoGEsGUdLAfO40VUqykC0bVhXRYXzltLC9fOlkSo+Pt6b+arCIxu7rEGutJ1cGA0BYIypH0" +
                "xmb4UiS1seHVS94gBuKveeVf/J0MFpvqAp9ThjR0LETIZWDW9LCC0ExY+ksE+6455RBBoBIhnPbiLH52" +
                "j3/9uUA0cKFVpXJxhiPcbkNeWQCSWEtnZFoQAytIAdRnvOnZeQ/ZRmgrbbWDbxD3MU6BtR0un+kiR/IK" +
                "lsHXGZTEwpWr1kZjabqNIKowZIOA95x024R3NSGTwW8sNhZhV0wNntL7ShlkQouNCXnig2P0mJZ7o5Ov" +
                "ZMuj9ZHwn0hxhgfH50y/2RVN88/t9P3V7P212L1G4gf8Zn9S3CZy6cWWAjszJdZHNYlvBWpiI+dujYJo" +
                "MMeTxezPqehhvjjE5IzUzkFZuDEl1ugk4Nv5dPrudjG96oBfHgI7UgSPw5ZIOezBn6AMfBByGeBkE/j0" +
                "jhNED7EgbJaI/3kN8AOTRBUaw6E8VwUxggl+hwKiZwtyJcqw4J4Q6LylfPfHZDKdXvUovzqkvAGyVLkh" +
                "pu1rxSosa24ITwlxLMz41w/zvS4c5scnwqRVPLquoy333J+MpGv6rDTsCl+hDJbSFLWjY/Tm09+nkx6/" +
                "kfjpU3qO/iEVjjggFlRVh8d2+f7zHFNSEs01YnbBajTMIMGUOwRatrFrWRh97ACt87pKGYnX38B5nfVs" +
                "FWIR7s3XJa9TeDK+udlX8kj8fCrBlHBn0ZMMT1EXOfk0W4ek7dK4km83vj5CvwtEJqQPDtG3yZsvcIjT" +
                "ZGZTHJRfE4CvjSOeuPlwt+hDjcQvEXBsd2K0tweQhEbWGIQaEWQnAaMMm3HAw+CFjrqlJ9SeZ+yK1WZJ" +
                "NwbHR+VI+6h1JoNxUVSbOJjwQpSC47rtLiuQaS8qrjHRG694i6a0zjKWsV0U6CEk3/Aqm10ljQOaEaQV" +
                "yQdON58n3smQdJMbzBbxPu61lOgO0jwUzeLoUrd3zGOdsJ8s+wenJM8CYcShcoVcFQV2M6ZvkrchhO6g" +
                "d9aDJclxS4mM+qNCyx/dpR0v0IpBb3uYhSWRTqX6l92IHc0gi7nSe5lRkxq/ImWWRu2KITLwwxadh75m" +
                "AUiVdSwK9DmDVcNd8ngI+dqpe74fx5NmtGyH8o8ZIPpM1gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestActionResult> CreateSerializer() => new Serializer();
        public Deserializer<TestActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestActionResult>
        {
            public override void RosSerialize(TestActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TestActionResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TestActionResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TestActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestActionResult msg) => msg = new TestActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestActionResult msg) => msg = new TestActionResult(ref b);
        }
    }
}
