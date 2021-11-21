/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionFeedback")]
    public sealed class LookupTransformActionFeedback : IDeserializable<LookupTransformActionFeedback>, IActionFeedback<LookupTransformFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public LookupTransformFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LookupTransformActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionFeedback(ref b);
        }
        
        LookupTransformActionFeedback IDeserializable<LookupTransformActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionFeedback(ref b);
        }
    
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c0b/ATM6xO7Ubuv0I/WMDqqsOOo4icdWe/WAxIpEQwIqAErWv+9bUKSo" +
                "WprokEQjW1/A24e3bxf7jqQiJ4r4ksgsaGtKnT5VPvc/3FpZPgYZai98fEnurP1UL+dOGr+wrnpLpFKZ" +
                "fRKL7ZtBMvrCj0Hy/vH2GvFVw+ldZDpIhgLMjJJOiYqCVDJIAUqi0HlB7qKkFZXMulqSEvHXsFmSv8TG" +
                "eaG9wDMnQ06W5UbUHouCFZmtqtroTAYSQVe0tx87tRFSLKULOqtL6bDeOqUNL184WRGj4+np35pMRmJ2" +
                "c401xlNWBw1CGyBkjqTXJsePIqm1Ca+veEMynK/tBT5Sjnx0wUUoZGCy9Lx05Jmn9NeI8V1zuEtgQx1C" +
                "FOXFWfzuCR/9uUAQUKClzQpxBub3m1BYA0ASK+m0TEti4AwKAPUVb3p13kNm2tfCSGNb+AZxF+MUWNPh" +
                "8pkuCuSs5NP7OoeAWLh0dqUVlqabCJKVmkwQMKGTbpPwriZkMnzLGmMRdsWM4FV6bzONBCix1qFIfHCM" +
                "HrPxpFXy1Qx5tFQGCb9HcnO8MAXO8Zu2gJoP99MPN7MPt6J9jMSP+M/OpLhNFNKLDQX2ZEosUdbkfqtR" +
                "ExxpdytUbYM5nsxnf09FD/OnfUxOSu0cxIUPU2KZTgK+f5hO39/Ppzcd8NU+sKOM4G44E1mHQ/gbFIAP" +
                "Qi4CzKwDn95xjug5loLJkx3Rl48h/uCTqELjORTmsiRG0MG3KCB6NidXoQBL7gaBzreUH/+aTKbTmx7l" +
                "1/uU10CWWaHRJRSsmLEKi5pbwSEhjoUZ//HxYacLh/n5QJjUxqOrOjpzx/1gJFXTZ6VhV3iLSlhIXdaO" +
                "jtF7mP45nfT4jcQvL+k5+ocy5neQDteUrcP/7fL95zmmlEm01YjZBavRKoMEU24SaNbarGSp1bEDbJ3X" +
                "VcpI/PoNnNdZz9gQi3Bnvi55ncKT8d3drpJH4rdTCaaE24oOMjxFXeTkZbb2SZuFdhXfa3yDdGmIrZmZ" +
                "kNo7RN8mb77AIU6TmU2xV35NAL45jnji7uPjvA81Er9HwLFpxdheIEASClljEGpEkJ0EjHLZDAIeBi9V" +
                "1C09ofY8Y1tWmyVdaxwflYNY+60zGY7L0q7jSMILUQp4Y3f3Fchs7yquMdEbtXiLorTOc5ZxuyjQc0i+" +
                "6W02u+Ehi03QDCJbnXzgjPOR4s0MVdeFxoQRb+VeV4kGIcUT0SwOMHHGOiAV9pNhC+Gg5FkjDDpULZGu" +
                "ssRuxvRN/taE0B106z64khx3lcioPzBs+WvVDhnoxqCHRtdPRDu7siGxA1NWXQYMld7LnDOM7PglZXqh" +
                "s7YeIgPPBmJ0nviaBSBV1bEu0Oo0Vl22+cOqr5e9sLhq8nZkPB8g9H/rmhfZ6QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
