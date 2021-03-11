/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionFeedback")]
    public sealed class FibonacciActionFeedback : IDeserializable<FibonacciActionFeedback>, IActionFeedback<FibonacciFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public FibonacciFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new FibonacciFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionFeedback(ref b);
        }
        
        FibonacciActionFeedback IDeserializable<FibonacciActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "73b8497a9f629a31c0020900e4148f07";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QECH2EXttEk/UgM6qJLsqHASw1Z7KQqDS4522e5yFZIrWf++b7ir" +
                "lVRLiA6pBdn6It88vnkznPckNTmRx5dEqmAqW5j0sfSZf31TyeIhyFB74eNLcm3SykqlzDWRTqX6R8zb" +
                "N71k8JUfveTDw80VIuuGzfvIsZf0BThZLZ0WJQWpZZBiXuEMJsvJXRS0pIL5lgvSIv4a1gvyl9g4y40X" +
                "eGZkycmiWIvaY1GohKrKsrZGyUAimJL29mOnsUKKhXTBqLqQDusrp43l5XMnS2J0PD19rskqEtPxFdZY" +
                "T6oOBoTWQFCOpDc2w48iqY0Nb9/whqQ/W1UX+EgZMtEFFyGXgcnS08KRZ57SXyHGN83hLoENdQhRtBdn" +
                "8btHfPTnAkFAgRaVysUZmN+tQ15ZAJJYSmdkWhADKygA1Fe86dX5DjLTvhJW2moD3yBuY5wCaztcPtNF" +
                "jpwVfHpfZxAQCxeuWhqNpek6gqjCkA0C9nPSrRPe1YRM+tesMRZhV8wIXqX3lTJIgBYrE/LEB8foMRuP" +
                "Rif/myGPFkkv4fdIboYXpsA5frcpnebD3eTjePrxRmweA/Ed/rMzKW4TufRiTYE9mRJLpJrctxo1wZF2" +
                "t0S9NpjD0Wz6x0TsYH6/j8lJqZ2DuPBhSizTScB395PJh7vZZNwBv9kHdqQI7oYzkXU4hL9BAfgg5DzA" +
                "zCbw6R3niJ5iKdgs2RJ9/ujjDz6JKjSeQ2EuCmIEE/wGBUTPZuRKFGDB3SDQeUv54ffRaDIZ71B+u095" +
                "BWSpcoMuoWFFxSrMa24Fh4Q4Fmb466f7rS4c5ocDYdIqHl3X0Zlb7gcj6Zq+KA27wleohLk0Re3oGL37" +
                "yW+T0Q6/gfjxOT1Hf5NifgfpcE1VdfivXb79MseUlERbjZhdsBqtMkgw5SaBZm3sUhZGHztA67yuUgbi" +
                "pxdwXmc9W4VYhFvzdcnrFB4Nb2+3lTwQP59KMCXcVnSQ4SnqIifPs7VP2s6NK/le4xukS0NszcyE9N4h" +
                "dm3y7isc4jSZ2RR75dcE4JvjiCduPz3MdqEG4pcIOLQbMdoLBEhCI2sMQo0IspOAUS6bQcDD4IWOuqUn" +
                "1J5n7IrVZklXBsdH5SDWfutM+sOiqFZxJOGFKAW8qbb3Fci0dxXXmNgZsniLprTOMpaxXRToKSQveptN" +
                "xzxksQmaQaTVyQfOOB8p3sxQdZUbTBjxVt7pKtEgpHkimsYBJs5YB6TCfrJsIRyUPGuEQYfKBdJVFNjN" +
                "mL7J34oQuoPeuA+uJMddJTLaHRha/mgw7ZCBbgx66/1EzDdDLAyJHZiy6iJgqPReZpxhZMcvSJm5UZt6" +
                "iAw8G4jReeJrFoBUWce6QKszWHW5yR9WvUT2Qo30GAj2+tmM3kuSOGz++Vc3n4LRvwWh1ED6CwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
