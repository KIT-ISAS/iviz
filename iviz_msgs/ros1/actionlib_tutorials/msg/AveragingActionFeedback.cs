/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingActionFeedback : IDeserializable<AveragingActionFeedback>, IHasSerializer<AveragingActionFeedback>, IMessage, IActionFeedback<AveragingFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public AveragingFeedback Feedback { get; set; }
    
        public AveragingActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = AveragingFeedback.Singleton;
        }
        
        public AveragingActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public AveragingActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = AveragingFeedback.Singleton;
        }
        
        public AveragingActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = AveragingFeedback.Singleton;
        }
        
        public AveragingActionFeedback RosDeserialize(ref ReadBuffer b) => new AveragingActionFeedback(ref b);
        
        public AveragingActionFeedback RosDeserialize(ref ReadBuffer2 b) => new AveragingActionFeedback(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = Feedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "actionlib_tutorials/AveragingActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W224bNxB9368goIfYRa00SZukBvSgyoqjwEkMW+1LURjc5WiX7S5X5UWy/r5nuBdJ" +
                "ttXoIYkgWzfyzOGZM8N5T1KRFUV8SWTmdW1Knd5VLnfPL2tZ3nrpgxMuviTjFVmZa5O/I1KpzP4Ri/ZN" +
                "MvrKj+Tj7eU54qqGy/uG4UCAkFHSKlGRl0p6KRY1DqDzguxZSSsqmWy1JCXir36zJDfExnmhncAzJ4ND" +
                "lOVGBIdFvhZZXVXB6Ex6El5XtLcfO7URUiyl9ToLpbRYX1ulDS9fWFkRo+Pp6N9AJiMxuzjHGuMoC16D" +
                "0AYImSXpIBx+FEnQxr96yRvEQPx5U7sXfyWD+bo+w/eUIx89C+EL6Zk13S8tOSYs3TmC/dCccoggUIkQ" +
                "TjlxEr+7w0d3KhANXGhZZ4U4wRGuN76oDQBJrKTVMi2JgTNIAdRnvOnZ6Q6yidBGmrqDbxC3MY6BNT0u" +
                "n+msQPJKlsGFHEpi4dLWK62wNN1EkKzUZLyACa20m4R3NSGTwTsWG4uwK6YGr9K5OtPIhBJr7YvEecvo" +
                "MS13WiXfyJYHCyXht0hxjheOz5l+21VP8+F6+uli9ulSdI+R+An/2Z8Ut4lCOrEhz85MifXJmsS3AjWx" +
                "kXOLYmwxx5P57I+p2MF8sY/JGQnWQlm4MSXW6Cjg65vp9OP1fHrRA7/cB7aUETwOWyLlsAd/gzJwXsiF" +
                "h5O159NbThDdx4IweSL+5zHAH0wSVWgMh/JclsQI2rsOBURP5mQrlGHJPcHTaUv59vfJZDq92KH8ap/y" +
                "GsgyKzQxbRcyVmERuCE8JcShMOPfPt9sdeEwPz8RJq3j0VWIttxyfzKSCvRFadgVrkYZLKQug6VD9G6m" +
                "H6aTHX4j8ctjepb+pswfcEAsqDr4h3b58cscU8okmmvE7IMFNEwvwZQ7BFq2NitZanXoAK3z+koZidff" +
                "wXm99UztYxFuzdcnr1d4Mr662lbySLw5lmBKuLPoSYbHqIucPM7WPmmz0Lbi242vD7/bBSITUnuH2LXJ" +
                "269wiONkZlPslV8TgK+NA564+nw734UaiV8j4Nh0YrS3B5CEQtYYhBoRZC8BowybccDB4KWKuqVH1J5j" +
                "7JrVZknXGsdH5UjzoHUmg3FZ1us4mPBClILluu0vK5BpLyquMbEzZ/EWRWnIecjqbjNP9z75jlfZ7CJp" +
                "HNCMIK1IznO6+TzxToak60Jjtoj38U5Lie4gxUPRLI4uob1jHuqE/WTYPzglORYIIw5VS+SqLLGbMV2T" +
                "vDUhdA/dWQ+WJMstJTLaHRVa/ugu7XiBVgx6m/0sdLMruxE7MF+F0mOudE7m1KTGLSnTC511xRAZuGGL" +
                "zkNfswCkqhCLAn1OY9WwSx4PId88dT4gORpyPX80oSP6fzs6A+7lCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingActionFeedback> CreateSerializer() => new Serializer();
        public Deserializer<AveragingActionFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingActionFeedback>
        {
            public override void RosSerialize(AveragingActionFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AveragingActionFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AveragingActionFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(AveragingActionFeedback msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<AveragingActionFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AveragingActionFeedback msg) => msg = new AveragingActionFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AveragingActionFeedback msg) => msg = new AveragingActionFeedback(ref b);
        }
    }
}
