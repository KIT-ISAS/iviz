/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeActionFeedback : IDeserializable<ShapeActionFeedback>, IMessage, IActionFeedback<ShapeFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ShapeFeedback Feedback { get; set; }
    
        public ShapeActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ShapeActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public ShapeActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ShapeActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ShapeActionFeedback RosDeserialize(ref ReadBuffer b) => new ShapeActionFeedback(ref b);
        
        public ShapeActionFeedback RosDeserialize(ref ReadBuffer2 b) => new ShapeActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Status.AddRos2MessageLength(ref c);
            Feedback.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "turtle_actionlib/ShapeActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W23LbNhB951dgRg+xO7XSJL2kntGDKiuOMk7isdS+dDoekFyRaEGQxUWy/r5nQYqS" +
                "HKvRQxKNbN2Aswdnzy72LcmcrCjjSyIzr2qjVXpfucI9v66lnnvpgxMuviTzUjb0hihPZfaPWHZvktEX" +
                "fiTv59eXiJm3PN627AYCZEwubS4q8jKXXoplDfKqKMleaFqRZqJVQ7mIv/pNQ26IjYtSOYFnQYas1Hoj" +
                "gsMiX4usrqpgVCY9Ca8qOtiPncoIKRppvcqClhbra5srw8uXVlbE6Hg6+jeQyUjMri6xxjjKglcgtAFC" +
                "Zkk6ZQr8KJKgjH/1kjeIgfjzrnYv/koGi3V9ge+pQC56FsKX0jNremgsOSYs3SWCfdeecoggUIkQLnfi" +
                "LH53j4/uXCAauFBTZ6U4wxFuN76sDQBJrKRVMtXEwBmkAOoz3vTsfA/ZRGgjTb2FbxF3MU6BNT0un+mi" +
                "RPI0y+BCASWxsLH1SuVYmm4iSKYVGS9gQCvtJuFdbchk8IbFxiLsiqnBq3SuzhQykYu18mXivGX0mJZ7" +
                "lSdfyZZHiyTht0hxgReOz5l+va2c9sPt9MPV7MO12D5G4gf8Z39S3CZK6cSGPDszJdYnaxPfCdTGRs7t" +
                "CgXRYo4ni9kfU7GH+eIQkzMSrIWycGNKrNFJwLd30+n728X0qgd+eQhsKSN4HLZEymEP/gZl4LyQSw8n" +
                "K8+nt5wgeogFYYpE/M9jgD+YJKrQGg7l2WhiBOXdFgVEzxZkK5Sh5p7g6byjPP99MplOr/YovzqkvAay" +
                "zEpFTNuFjFVYBm4ITwlxLMz4t493O104zI9PhEnrePQ8RFvuuD8ZKQ/0WWnYFa5GGSyl0sHSMXp303fT" +
                "yR6/kfjpU3qW/qbMH3FALKg6+Md2+f7zHFPKJJprxOyDBTRML8GUOwRatjIrqVV+7ACd8/pKGYmfv4Hz" +
                "euuZ2sci3JmvT16v8GR8c7Or5JH45VSCKeHOoicZnqIucvJptg5Jm6WyFd9ufH34/S4QmVB+cIh9m7z+" +
                "Aoc4TWY2xUH5tQH42jjiiZuP88U+1Ej8GgHHZitGd3sASeTIGoNQK4LsJWCUYTsOOBhc51G39ITac4xd" +
                "s9os6Vrh+KgcaR61zmQw1rpex8GEF6IULNdtf1mBTHdRcY2JvRmLt+SUhqJgGbtFnh588g2vstlV0jqg" +
                "HUE6kZzndPN54p0MSdelwmwR7+O9lhLdQTkPRbM4uoTujnmsE/aTYf/glORYIIw4VDXIldbYzZiuTd6a" +
                "ELqH3loPliTLLSUy2h8VOv7oLt14gVYMepvDLGxnV3YjdmC+CtpjrnROFtSmxjWUqaXKtsUQGbhhh85D" +
                "X7sApKoQiwJ9TmHVcJs8HkK+Uup8sF7TfZ/B5wejOcL+B64lZQ3aCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
