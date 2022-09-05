/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformActionResult : IDeserializable<LookupTransformActionResult>, IHasSerializer<LookupTransformActionResult>, IMessage, IActionResult<LookupTransformResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public LookupTransformResult Result { get; set; }
    
        public LookupTransformActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new LookupTransformResult();
        }
        
        public LookupTransformActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public LookupTransformActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new LookupTransformResult(ref b);
        }
        
        public LookupTransformActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new LookupTransformResult(ref b);
        }
        
        public LookupTransformActionResult RosDeserialize(ref ReadBuffer b) => new LookupTransformActionResult(ref b);
        
        public LookupTransformActionResult RosDeserialize(ref ReadBuffer2 b) => new LookupTransformActionResult(ref b);
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = Result.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ac26ce75a41384fa8bb4dc10f491ab90";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XbVPbRhD+rl9xUz4EOsQ0kKQpEzLjgkPcgk2NybTT6TBnaSVfkXTO3Qnj/vo+eyfJ" +
                "Ni8JHwIesKW73b1n3/c+kUzIiKn/iWTslC5zNbksbGZ3jrXMz510lRXW/0QnWl9Vs7GRpU21KUZkq9wJ" +
                "43+ig+/8iU7Pj/dxchLQfAoYNwQglYk0iSjIyUQ6KYBFTFU2JfMyp2vKGW4xo0T4XbeYke2AcTxVVuAv" +
                "o5KMzPOFqCyInBaxLoqqVLF0JJwqaI0fnKoUUsykcSqucmlAr02iSiZPjSyIpePP0peKyphE/2gfNKWl" +
                "uHIKgBaQEBuSVpUZNkVUqdLt7TKD2BB/j7R99U+0MZ7rl1inDB5pUQg3lY5R080MhmbA0u7jsB+Dlh0c" +
                "AisRjkus2PRrl3i1WwKnAQvNdDwVm1DhbOGmuoRAEtfSKDnJiQXHMAWkvmCmF1srkksvupSlbsQHicsz" +
                "HiO2bOWyTi+ncF7OZrBVBkuCcGb0tUpAOll4IXGuqHQCYWikWUTMFY6MNj6ysUEELu8a/EprdazgiUTM" +
                "lZtG1hmW7t1yqZLoicLywVSJ+BEuzvDD57On3zX5E17OeoOj/uBYNJ8D8RO+OT7Js4mptGJBjiNzQmyf" +
                "ODi+NlA4Gz4310iIILN7OO5/7okVma/WZbJHKmNgWUTjhNhGjxJ8Nur1Ts/GvaNW8O66YEMxIcYRlnA5" +
                "woNXkAbWCZk6RLJyrL1hB9GNT4gyi8RXPhv4R5B4K4SAQ3rOcmIJytlGCoBujskUSMOca4KjrRry+cXh" +
                "Ya93tAJ5bx3yHJJlPFXEsG0VsxXSigvCfYZ46Jjur8PR0i58zOt7jplor3pS+bBcYr/3pKSib5qGo8Jq" +
                "pEEqVV4ZegjeqPdb73AF34F4cxeeoX8pdg9EgE8oXbnb4bL9bYwTiiWKq5fZHlahYDoJpFwhULJVeS1z" +
                "lTykQB15baYciLfPEHlt6JXa+SRcBl/rvNbCh92Tk2UmH4ifHwtwQuhZdC/Cx1gXPrnrrXXQZapMwd2N" +
                "24dbrQIeCSVrSqyGybvvoMTjzMxBsZZ+4QBuGw/ExMnwfLwq6kD84gV2y8YYdfeAJJHAayyEghFkawKW" +
                "0gnjgEWA54m32+QRuWdZtmZrs0nnCuojc2R5q3RGG90813M/mDAhUsFw3rbNCmDqRsU5JlYmLWZJaFJl" +
                "GZuxJnJ046JnbGX9oyhEQBhBaiNZx+5mfXxPhknnU4XZwvfjlZLio4MSHor6fnSp6h5z207gp5LjB1qS" +
                "ZQNhxKFiBl/lObhZpg3OmxOObkU3oYeQJMMlxSNaHRVq/Kgu9XiBUgx4i3UvpETJRMZXHI3gCIMs5kpr" +
                "ZUbBNXZGsUpV3CSDR2A7tXQe+gIBQBWVTwrUOQWqTuM8HkKeyHUu3Q1Ou3cwjzJCn3BmEWja3fN6unXN" +
                "QtTKGX/c7RmDECT+fiLUX4fVDOrNvMuOa5FirtPFnfG7vr902qFvg93sZ8nblOj6eXK5JGxOa3zOj9q6" +
                "5m5Qt0VQvZdiaig9+GHq3Gx/Z2eurlTHaNvRJttx6Q8fXPp+R37ALSG+gqAO85wT+Ykl0XFVoHJIH/uc" +
                "4YUvnCWr5Bc7UfRp7SpWx846XNQgRhM0watOg5JMFFaj1por3n1eNzYWNcT+g9Y2lIwW2ITcnJD3bq7v" +
                "+MfyVStFEUBayRg5FH3GfKLNXuDPvbGiPyowmJKNaXSw6vMoWYO5R0Uprv3eLfyirYGosAtEmSz9WN9y" +
                "gjFBF/FV0RcV4yvbNlfFRJPv6JBRyCvi5sMjFQrYbAZhctUmvAyWTepkne1QVz0VB5G/wvpLL8qYUZla" +
                "Sf2WWYpauW2uKqh9eR4wh8PgQi6QtbW3OqKfioWuUFKhAx5Mfdf2N5YGlx/0nNbbop4FPY5Vg55ptPVl" +
                "8pUo7hKNI0pzLd3b1+KmfVq0T/89i6uXMXaft0vMr6rN6DWf89uXZYCykb+pUPM0f/JG0RT4ep4aDC97" +
                "o9FwxHfQdsQa/n5x1i6/qpcPh4NBjy+Z/fFf7eZuvdn7czzqng1PuuP+cNDu7tW7/cHn7kn/6LI7Or44" +
                "7Q3GLcHrmmDcP+0NL5brb5r1UXdw/nE4Om133kb1VuhPdZX0L5fhJYr+B/bf0GHUEgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LookupTransformActionResult> CreateSerializer() => new Serializer();
        public Deserializer<LookupTransformActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LookupTransformActionResult>
        {
            public override void RosSerialize(LookupTransformActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(LookupTransformActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(LookupTransformActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(LookupTransformActionResult msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<LookupTransformActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out LookupTransformActionResult msg) => msg = new LookupTransformActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out LookupTransformActionResult msg) => msg = new LookupTransformActionResult(ref b);
        }
    }
}
