/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciActionFeedback : IHasSerializer<FibonacciActionFeedback>, IMessage, IActionFeedback<FibonacciFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public FibonacciFeedback Feedback { get; set; }
    
        public FibonacciActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new FibonacciFeedback();
        }
        
        public FibonacciActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public FibonacciActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        public FibonacciActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        public FibonacciActionFeedback RosDeserialize(ref ReadBuffer b) => new FibonacciActionFeedback(ref b);
        
        public FibonacciActionFeedback RosDeserialize(ref ReadBuffer2 b) => new FibonacciActionFeedback(ref b);
    
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
            BuiltIns.ThrowIfNull(Status, nameof(Status));
            Status.RosValidate();
            BuiltIns.ThrowIfNull(Feedback, nameof(Feedback));
            Feedback.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = Feedback.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "actionlib_tutorials/FibonacciActionFeedback";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "73b8497a9f629a31c0020900e4148f07";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXMaRxC976+YKg6WUhGO7cR2VMWBAJJxybZKIrm4XKrZ2WZ3kt1ZMh8g/n1ezy4L" +
                "SCLmYJtC4mvm9ZvXr3v6HcmMrCjiSyKV17UpdXpXudw9v6xleeulD064+JJc6LQ2Uil9QZSlUv0j5u2b" +
                "ZPCNH8mH28tzxM0aLu8ahj0BQiaTNhMVeZlJL8W8xgF0XpA9K2lJJZOtFpSJ+KtfL8j1sXFWaCfwzMmQ" +
                "lWW5FsFhka+FqqsqGK2kJ+F1RXv7sVMbIcVCWq9VKKXF+tpm2vDyuZUVMTqejv4NZBSJ6fgca4wjFbwG" +
                "oTUQlCXptMnxo0iCNv7VS94geuLzTe1efEl6s1V9hu8pRz46FsIX0jNrul9YckxYunME+6k5ZR9BoBIh" +
                "XObESfzuDh/dqUA0cKFFrQpxgiNcr31RGwCSWEqrZVoSAytIAdRnvOnZ6Q6yidBGmnoD3yBuYxwDazpc" +
                "PtNZgeSVLIMLOZTEwoWtlzrD0nQdQVSpyXgBE1pp1wnvakImvQsWG4uwK6YGr9K5WmlkIhMr7YvEecvo" +
                "MS13Oku+ky0PFkrCb5HiHC8cnzP9dlM9zYfrycfx9OOl2DwG4hf8Z39S3CYK6cSaPDszJdZHNYlvBWpi" +
                "I+d2iYJoMIej2fSvidjBfLGPyRkJ1kJZuDEl1ugo4OubyeTD9Wwy7oBf7gNbUgSPw5ZIOezB36AMnBdy" +
                "7uFk7fn0lhNE97EgTJ6I/3n08AeTRBUaw6E8FyUxgvZugwKiJzOyFcqw5J7g6bSlfPvnaDSZjHcov9qn" +
                "vAKyVIUmpu2CYhXmgRvCU0IcCjP849PNVhcO8+sTYdI6Hj0L0ZZb7k9GygJ9VRp2hatRBnOpy2DpEL2b" +
                "yfvJaIffQPz2mJ6lv0n5Aw6IBVUH/9AuP3+dY0pKorlGzC5YQMP0Eky5Q6Bla7OUpc4OHaB1XlcpA/H6" +
                "Bzivs56pfSzCrfm65HUKj4ZXV9tKHog3xxJMCXcWPcnwGHWRk8fZ2idt5tpWfLvx9eF3u0BkQtneIXZt" +
                "8vYbHOI4mdkUe+XXBOBr44Anrj7dznahBuL3CDg0GzHa2wNIIkPWGIQaEWQnAaP0m3HAweBlFnVLj6g9" +
                "x9g1q82SrjSOj8qR5kHrTHrDsqxXcTDhhSgFy3XbXVYg015UXGNiZ87iLRmlIc9ZxnaRp3uf/MCrbDpO" +
                "Ggc0I0grkvOcbj5PvJMh6arQmC3ifbzTUqI7KOOhaBpHl9DeMQ91wn4y7B+ckhwLhBGHqgVyVZbYzZiu" +
                "Sd6KELqD3lgPliTLLSUy2h0VWv7oLu14gVYMeuv9LGxmV3YjdmC+CqXHXOmczKlJjVuQ0nOtNsUQGbh+" +
                "i85DX7MApKoQiwJ9TmNVf5M8HkK+e+p8QHI05Hr+aEJPkjhsfv7SzadJ8h+n8o+d9gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciActionFeedback> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciActionFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciActionFeedback>
        {
            public override void RosSerialize(FibonacciActionFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciActionFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciActionFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciActionFeedback msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FibonacciActionFeedback msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FibonacciActionFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciActionFeedback msg) => msg = new FibonacciActionFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciActionFeedback msg) => msg = new FibonacciActionFeedback(ref b);
        }
    }
}
