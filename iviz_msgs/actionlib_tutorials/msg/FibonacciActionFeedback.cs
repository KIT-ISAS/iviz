/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciActionFeedback : IDeserializable<FibonacciActionFeedback>, IActionFeedback<FibonacciFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public FibonacciFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new FibonacciFeedback();
        }
        
        /// Explicit constructor.
        public FibonacciActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal FibonacciActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FibonacciActionFeedback(ref b);
        
        FibonacciActionFeedback IDeserializable<FibonacciActionFeedback>.RosDeserialize(ref Buffer b) => new FibonacciActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "73b8497a9f629a31c0020900e4148f07";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9768goEPsonbapE1TAzqokuyocBLDVnspCoNLjnbZ7nJVkitZ/z5vuB+S" +
                "agnRIYkgW1/km8c3b4bzjqQmJ/L4kkgVTGULkz6WPvMvbypZPAQZai98fEmuTVpZqZS5JtKpVP+KRfsm" +
                "GX7hR/L+4eYKcXXD5V3DcCBAyGrptCgpSC2DFIsKBzBZTu6ioBUVTLZckhbx17BZkr/ExnluvMAzI0tO" +
                "FsVG1B6LQiVUVZa1NUoGEsGUtLcfO40VUiylC0bVhXRYXzltLC9fOFkSo+Pp6b+arCIxm1xhjfWk6mBA" +
                "aAME5Uh6YzP8KJLa2PD6FW9IBvN1dYGPlCENfXARchmYLD0tHXnmKf0VYnzXHO4S2BCHEEV7cRa/e8RH" +
                "fy4QBBRoWalcnIH53SbklQUgiZV0RqYFMbCCAkB9wZtenO8gM+0rYaWtOvgGcRvjFFjb4/KZLnLkrODT" +
                "+zqDgFi4dNXKaCxNNxFEFYZsEPCek26T8K4mZDK4Zo2xCLtiRvAqva+UQQK0WJuQJz44Ro/ZeDQ6+Upu" +
                "PFofCb9FZjO8cHxO8NuuaJoPd9MPk9mHG9E9huIH/GdbUtwmcunFhgIbMiXWRzWJbwVqYiPnboU6aDBH" +
                "4/nsz6nYwfxxH5MzUjsHZWHClFijk4Dv7qfT93fz6aQHfrUP7EgRrA1bIuWwB38D9/sg5CLAySbw6R0n" +
                "iJ5iHdgs2RJ9/hjgDyaJKjSGQ1UuC2IEE3yHAqJnc3Ilqq/gVhDovKX88Md4PJ1Odii/3qe8BrJUuUGL" +
                "0PChYhUWNfeBQ0IcCzP67eP9VhcO89OBMGkVj67raMst94ORdE2flYZd4SuUwUKaonZ0jN799PfpeIff" +
                "UPz8nJ6jf0gxv4N0uKCqOvzfLt9/nmNKSqKnRsw+WI0+GSSYcodApzZ2JQujjx2gdV5fKUPx5hs4r7ee" +
                "rUIswq35+uT1Co9Ht7fbSh6KX04lmBKuKjrI8BR1kZPn2donbRfGlXyp8fXRpyH2ZWZCeu8QuzZ5+wUO" +
                "cZrMbIq98msC8LVxxBO3Hx/mu1BD8WsEHNlOjPb2AJLQyBqDUCOC7CVglMtmCvAweKGjbukJtecZu2K1" +
                "WdK1wfFROYi13zqTwagoqnWcR3ghSgFvqu1lBTLtRcU1JnbGK96iKa2zjGVsFwV6Csk3vMpmkzgltfdu" +
                "J5IPnG4+T7yTIek6N5gt4n2801KiO0jzLDSLo0ucrg7ohP1k2T84JXkWCCMOlUvkqiiwmzF9k7w1IXQP" +
                "3VkPliTHLSUy2h0VWv7oLu14gVYMepv9LCy62RVuxA7MV3URME56LzNOL1Ljl6TMwqiuGCIDz+5hdJ71" +
                "mgUgVdaxKNDnDFZddsnjIeSrpy7USI6BXC+fDeZJEmfMv/7ux9Ik+QQWWshy7QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
