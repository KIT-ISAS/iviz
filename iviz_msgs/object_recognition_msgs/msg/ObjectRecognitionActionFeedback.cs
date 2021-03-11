/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionActionFeedback")]
    public sealed class ObjectRecognitionActionFeedback : IDeserializable<ObjectRecognitionActionFeedback>, IActionFeedback<ObjectRecognitionFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ObjectRecognitionFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionFeedback(ref b);
        }
        
        ObjectRecognitionActionFeedback IDeserializable<ObjectRecognitionActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionFeedback(ref b);
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
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L+D/QECH2EXttkk/UgM6qJLiKHBiw1Z7NbjkaJftLqmSXMn6933DlVZS" +
                "LSE6JBFk64t88zjvzXDek9TkRZleMqmicbYy+VMdivDDjZPVY5SxCSKkl+wu/5tUfCDlCmt47TsinUv1" +
                "j5it35xl/S/8OMs+Pt5cg4FuWb1PXM+yngA3q6XXoqYotYxSzBzOYoqS/GVFC6qYdz0nLdKvcTWncIWN" +
                "09IEgWdBlrysqpVoAhZFJ5Sr68YaJSOJaGra24+dxgop5tJHo5pKeqx3XhvLy2de1sToeAb6tyGrSExG" +
                "11hjA6kmGhBaAUF5ksHYAj+KrDE2vnnNG7LedOku8ZEKKNIFF7GUkcnS89xTYJ4yXCPGd+3hroCN7EAS" +
                "q4M4T9894WO4EAgCCjR3qhTnYH6/iqWzACSxkN7IvCIGVsgAUF/xplcXO8hM+1pYad0GvkXcxjgF1na4" +
                "fKbLEppVfPrQFEggFs69WxiNpfkqgajKkI0CNvTSrzLe1YbMeu84x1iEXUkRvMoQnDIQQIuliWUWomf0" +
                "pMaT0dlXM+TRYjnL+D3ELfDCFFjjt5sSaj/cjz+NJp9uxObRFz/iPzuT0jZRyiBWFNmTOXGKVKv9Okdt" +
                "cMjuF6jbFnMwnE7+GosdzJ/2MVmUxnskFz7MidN0EvD9w3j88X46HnXAr/eBPSmCu+FMqA6H8DcogBCF" +
                "nEWY2UQ+vWeN6DmVgi2yLdGXjx7+4JOUhdZzKMx5RYxgYtiggOj5lHyNAqy4G0S6WFN+/HM4HI9HO5Tf" +
                "7FNeAlmq0qBLaFhRcRZmDbeCQ4k4Fmbwx93DNi8c5ucDYXKXjq6b5Mwt94ORdEOfTQ27IjhUwkyaqvF0" +
                "jN7D+MN4uMOvL355Sc8T9/QjDkg15Zr4f7t8/3mOOSmJtpowu2ANWmWUYMpNAs3a2IWsjD52gLXzukrp" +
                "i1+/gfM661kXUxFuzdeJ12V4OLi93VZyX/x2KsGccFvRQYanZBeavFRrn7SdGV/zvcY3SCdDas3MhPTe" +
                "IXZt8vYLHOK0NLMp9sqvDcA3xxFP3N49Tneh+uL3BDiwm2SsLxAgCQ3VGITaJMguBYxy1Q4CAQavdMpb" +
                "fkLtBcZ2nG1O6dLg+KgcxNpvnVlvUFVumUYSXohSwBu3va9AZn1XcY2JnWGLt2jKm6LgNK4XRXqO2Te9" +
                "zSYjHrLYBO0gss5TiKw4HyndzMjqsjSYMNKtvNNVkkFI80Q0SQNMmrEOpAr7ybKFcFAKnCMMOlTPIVdV" +
                "YTdjhla/JSF0B71xH1xJnrtKYrQ7MKz5o8Gshwx0Y9BDo9sVYjO7siGxA1NWU0UMlSHIghWGOmFOysyM" +
                "2tRDYhDYQIzOE1+7AKTqJtUFWp3BqquNflj19dRzaS5/Qp1uBvNWxqPz+hm4/AfTbARC/AsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
