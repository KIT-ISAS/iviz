/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionFeedback : IDeserializable<ObjectRecognitionActionFeedback>, IActionFeedback<ObjectRecognitionFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ObjectRecognitionFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionActionFeedback(ref b);
        
        ObjectRecognitionActionFeedback IDeserializable<ObjectRecognitionActionFeedback>.RosDeserialize(ref Buffer b) => new ObjectRecognitionActionFeedback(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndptk36kntFBlRRHGSf22GqvHhBYkWhJQAVAyfr3fQuKlFRL" +
                "Ex2SaGTrC3j7sO/tYt+T1ORFmV4yqaJxtjL5Ux2K8MONk9VjlLEJIqSX7C7/m1R8IOUKa3jtOyKdS/WP" +
                "mG/eZIMv/Mg+Pt5cI75uOb1vmZ4JELNaei1qilLLKMXc4SCmKMlfVrSkiknXC9Ii/RrXCwpX2DgrTRB4" +
                "FmTJy6paiyZgUXRCubpurFEykoimpr392GmskGIhfTSqqaTHeue1sbx87mVNjI5noH8bsorEdHyNNTaQ" +
                "aqIBoTUQlCcZjC3wo8gaY+Ob17whO5ut3CU+UgE5+uAiljIyWXpeeArMU4ZrxPiuPdwVsJEc6GF1EOfp" +
                "uyd8DBcCQUCBFk6V4hzM79exdBaAJJbSG5lXxMAKGQDqK9706mIHmWlfCyut6+BbxG2MU2Btj8tnuiyh" +
                "WcWnD02BBGLhwrul0ViarxOIqgzZKOBBL/06411tyOzsHecYi7ArKYJXGYJTBgJosTKxzEL0jJ7UeDI6" +
                "+0puPFonGb+FsgVeOD4L/LYrnvbD/eTTePrpRnSPgfgR/9mWlLaJUgaxpsiGzInzo1rhNwlqY0Nzv0Qd" +
                "tJjD0Wz610TsYP60j8mKNN4jszBhTpyjk4DvHyaTj/ezybgHfr0P7EkRrA1bQnLYg7+B+0MUch7hZBP5" +
                "9J4FoudUB7bItkRfPs7wB5OkLLSGQ1UuKmIEE0OHAqLnM/I1qq/iVhDpYkP58c/RaDIZ71B+s095BWSp" +
                "SoMWoeFDxVmYN9wHDiXiWJjhH3cP27xwmJ8PhMldOrpuki233A9G0g19NjXsiuBQBnNpqsbTMXoPkw+T" +
                "0Q6/gfjlJT1P3M2POCAVlGvi/+3y/ec55qQkemrC7IM16JNRgil3CHRqY5eyMvrYATbO6ytlIH79Bs7r" +
                "rWddTEW4NV8vXp/h0fD2dlvJA/HbqQRzwlVFBxmekl1o8lKtfdJ2bnzNlxpfH70MqS8zE9J7h9i1ydsv" +
                "cIjT0sym2Cu/NgBfG0c8cXv3ONuFGojfE+DQdsnY3B5AEhqqMQi1SZB9Chjlqp0CAgxe6ZS3/ITaC4zt" +
                "ONuc0pXB8VE5iLXfOrOzYVW5VZpHeCFKAW/c9rICmc1FxTUmdsYs3qIpb4qC07hZFOk5Zt/wKpuO05S0" +
                "uXe7JIXIcvN50p2MlK5Kg9ki3cc7LSW5gzTPQtM0uqTp6kCesJ8s+wenpMAJwohD9QJaVRV2M2ZoxVsR" +
                "QvfQnfVgSfLcUhKj3VFhwx/dZTNeoBWLlUSX21WhG1nZjdiB+aqpIsbJEGTB8kKasCBl5kZ1xZAYBHYP" +
                "o/Os1y4AqbpJRYE+Z7DqqhOPh5CvJJ1Lw/gTSrSbxlsNjw7pWfYfHh9+uu8LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
