/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupActionFeedback : IDeserializable<PickupActionFeedback>, IActionFeedback<PickupFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public PickupFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public PickupActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new PickupFeedback();
        }
        
        /// Explicit constructor.
        public PickupActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal PickupActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new PickupFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PickupActionFeedback(ref b);
        
        PickupActionFeedback IDeserializable<PickupActionFeedback>.RosDeserialize(ref Buffer b) => new PickupActionFeedback(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgRofYndhtkzZNPaODKimOOk6isdVePSCwItGQoAqAkvXv+xakKKqW" +
                "Jjok0cjWF/D24e3bxb4nqcmJPL4kUgVT2cKkj6XP/I+3lSweggy1Fz6+JHOjPterd0Q6leqzWLZvkuFX" +
                "fiQfHm5vEFQ3RN439AYCbKyWTouSgtQySLGswN5kObmrgtZUMNNyRVrEX8N2Rf4aGxe58QLPjCw5WRRb" +
                "UXssCpVQVVnW1igZSART0sF+7DRWSLGSLhhVF9JhfeW0sbx86WRJjI6np39rsorEbHKDNdaTqoMBoS0Q" +
                "lCPpjc3wo0hqY8PrV7whGSw21RU+UoYcdMFFyGVgsvS0cuSZp/Q3iPFDc7hrYEMcQhTtxUX87hEf/aVA" +
                "EFCgVaVycQHm823IKwtAEmvpjEwLYmAFBYD6gje9uOwhM+0bYaWtdvAN4j7GObC2w+UzXeXIWcGn93UG" +
                "AbFw5aq10ViabiOIKgzZIGA8J9024V1NyGTwjjXGIuyKGcGr9L5SBgnQYmNCnvjgGD1m49Ho5Bu58WRx" +
                "JPwWmc3wwvE5wW93FdN8mE8/TmYfb8XuMRQ/4T/bkuI2kUsvthTYkCmxPqpJfCtQExs5d2vUQYM5Gi9m" +
                "f09FD/PnQ0zOSO0clIUJU2KNzgKe30+nH+aL6aQDfnUI7EgRrA1bIuWwB38D9/sg5DLAySbw6R0niJ5i" +
                "Hdgs2RN9/hjgDyaJKjSGQ1WuCmIEE/wOBUQvFuRKVF/BrSDQZUv54a/xeDqd9Ci/PqS8AbJUuUGL0PCh" +
                "YhWWNfeBY0KcCjP649P9XhcO88uRMGkVj67raMs996ORdE1flIZd4SuUwVKaonZ0it799M/puMdvKH59" +
                "Ts/RP6SY31E6XFBVHf5vl5df5piSkuipEbMLVqNPBgmm3CHQqY1dy8LoUwdonddVylC8+Q7O66xnqxCL" +
                "cG++LnmdwuPR3d2+kofit3MJpoSrio4yPEdd5OR5tg5J26VxJV9qfH10aYh9mZmQPjhE3yZvv8IhzpOZ" +
                "TXFQfk0AvjZOeOLu08OiDzUUv0fAkd2J0d4eQBIaWWMQakSQnQSMct1MAR4GL3TULT2j9jxjV6w2S7ox" +
                "OD4qB7EOW2cyGBVFtYnzCC9EKeBNtb+sQKa9qLjGRG+24i2a0jrLWMZ2UaCnkHzHq2w2iVNSe+/uRPKB" +
                "083niXcyJN3kBrNFvI97LSW6gzTPQrM4usTp6ohO2E+W/YNTkmeBMOJQuUKuigK7GdM3ydsQQnfQO+vB" +
                "kuS4pURG/VGh5Y/u0o4XaMWghy7Xz8JuZGU3Ygfmq7oIGCe9lxmnF6nxK1JmadSuGCIDz+5hdJ71mgUg" +
                "VdaxKNDnDFZd75LHQ8g3Sl0JK5rQ5O1wGkfINjyPHpT8BzV586PbCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
