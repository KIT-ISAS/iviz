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
        public LookupTransformActionFeedback(ref Buffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA71WTXPbNhC981dgRofYnVhtnTZNPaODKimOOk7isdVePSC4ItGQoIoPyfr3fQtSFOVY" +
                "jQ5JNLL1Bbx9ePt2se9IZmRFEV8SqbyuTanTh8rl7sfrWpb3XvrghIsvyU1dfwqrhZXGLWtbvSXKUqk+" +
                "iWX7Jhl95Ufy/v76CtGzhtG7hudAgJbJpM1ERV5m0ksBPqLQeUH2oqQ1lUy5WlEm4q9+uyI3xMZFoZ3A" +
                "MydDVpblVgSHRb4Wqq6qYLSSnoTXFR3sx05thBQrab1WoZQW62ubacPLl1ZWxOh4Ovo3kFEk5tMrrDGO" +
                "VPAahLZAUJak0ybHjyIJ2vhXl7whGSw29QU+Uo5kdMGFL6RnsvS4suSYp3RXiPFDc7ghsCEOIUrmxFn8" +
                "7gEf3blAEFCgVa0KcQbmt1tf1AaAJNbSapmWxMAKCgD1BW96cd5DNhHaSFPv4BvEfYxTYE2Hy2e6KJCz" +
                "kk/vQg4BsXBl67XOsDTdRhBVajJewIFW2m3Cu5qQyeAta4xF2BUzglfpXK00EpCJjfZF4rxl9JiNB50l" +
                "38iNR6sk4bfIbI4Xjs8JfrMrnebD7ezDdP7hWuweI/ET/rMtKW4ThXRiS54NmRLro5rEtwI1sZFzu0Yd" +
                "NJjjyWL+90z0MH8+xOSMBGuhLEyYEmt0EvDt3Wz2/nYxm3bAl4fAlhTB2rAlUg578Ddwv/NCLj2crD2f" +
                "3nKC6DHWgckT8T+PAf5gkqhCYzhU5aokRtDe7VBA9GxBtkL1ldwKPJ23lO//mkxms2mP8qtDyhsgS1Vo" +
                "YtouKFZhGbgPPCfEsTDjPz7e7XXhML88Eyat49GzEG255/5spCzQF6VhV7gaZbCUugyWjtG7m/05m/T4" +
                "jcSvn9Oz9A8pf8QBsaDq4J/a5eWXOaakJHpqxOyCBfRJL8GUOwQ6tTZrWers2AFa53WVMhKvv4PzOuuZ" +
                "2sci3JuvS16n8GR8c7Ov5JH47VSCKeGqomcZnqIucvJ5tg5Jm6W2FV9qfH34fheITCg7OETfJm++wiFO" +
                "k5lNcVB+TQC+No544ubj/aIPNRK/R8Cx2YnR3h5AEhmyxiDUiCA7CRhl2EwBDgYvs6hbekLtOcauWW2W" +
                "dKNxfFSONE9aZzIYl2W9ifMIL0QpWK7b7rICmfai4hoTvSGLt2SUhjxnGdtFnh598h2vsvk0aRzQjCCt" +
                "SM5zuvk88U6GpJtCY7aI93GvpUR3UMaz0DyOLqG9Y57qhP1k2D84JTkWCCMOVSvkqiyxmzFdk7wNIXQH" +
                "vbMeLEmWW0pk1B8VWv7oLu14gVYMetvDLOxGVnYjdmC+CqXHOOmczKlJjVuR0kutdsUQGbhhi86zXrMA" +
                "pKoQiwJ9TmPVcJc8HkK+Uer88rJJ2pGZPEn+A7ZdGW3cCwAA";
                
    }
}
