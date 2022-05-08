/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public TwoIntsActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public TwoIntsActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsActionFeedback(ref b);
        
        public TwoIntsActionFeedback RosDeserialize(ref ReadBuffer b) => new TwoIntsActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TwoIntsActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Wy3IbNxC871egSgdLqYhO7DwcVfHAkLTMlGyrJCZXFXYx3EWCxTJ4kOLfpwe7XJKy" +
                "GPNgm0WJL6Cn0dMzmHckFTlRpZdMFkE31uj8ofalf3ndSHMfZIhe+PSSzdfNzAb/lkjlsvhHLLo32fAL" +
                "P7L399dXiKpaJu9afmcCdKySTomaglQySLFoQF+XFblLQysyTLVekhLp17BZkh9g47zSXuBZkiUnjdmI" +
                "6LEoNKJo6jpaXchAIuiaDvZjp7ZCiqV0QRfRSIf1jVPa8vKFkzUxOp6e/o1kCxKzyRXWWE9FDBqENkAo" +
                "HEmvbYkfRRa1Da9f8YbsDIJe4iOVSEIfXIRKBiZLj0tHnnlKf4UY37WHGwAb4hCiKC/O03cP+OgvBIKA" +
                "Ai2bohLnYH67CVVjAUhiJZ2WuSEGLqAAUF/wphcXe8g2QVtpmy18i7iLcQqs7XH5TJcVcmb49D6WEBAL" +
                "l65ZaYWl+SaBFEaTDQLOc9JtMt7VhszO3rLGWIRdKSN4ld43hUYClFjrUGU+OEZP2XjQKvtKbjxaHRm/" +
                "RWZLvHB8TvCbbcm0H26nHyazD9di+xiKH/CfbUlpm6ikFxsKbMicWJ+iTXwnUBsbOXcr1EGLORrPZ39N" +
                "xR7mj4eYnJHoHJSFCXNijU4Cvr2bTt/fzqeTHvjVIbCjgmBt2BIphz34G7jfByEXAU7WgU/vOEH0mOrA" +
                "lpn4n8cZ/mCSpEJrOFTl0hAj6OC3KCB6PidXo/oMt4JAFx3l+z/H4+l0skf59SHlNZBlUWli2j4WrMIi" +
                "ch94TohjYUa/f7zb6cJhfnomTN6ko6uYbLnj/mwkFemz0rArfIMyWEhtoqNj9O6mf0zHe/yG4udP6Tn6" +
                "m4pwxAGpoJoYntrl+89zzKmQ6KkJsw8W0SeDBFPuEOjU2q6k0erYATrn9ZUyFL98A+f11rNNSEW4M1+f" +
                "vF7h8ejmZlfJQ/HrqQRzwlVFzzI8RV3k5NNsHZK2C+1qvtT4+gj7XSAxIXVwiH2bvPkChzhNZjbFQfm1" +
                "AfjaOOKJm4/3832oofgtAY7sVozu9gCSUMgag1ArguwlYJRBOwV4GNyopFt+Qu15xm5YbZZ0rXF8VI60" +
                "T1pndjYyplmneYQXohQc121/WYFMd1FxjYm94Yq3KMpjWbKM3aJAjyH7hlfZbJK1DmhHkE4kHzjdfJ50" +
                "J0PSdaUxW6T7eK+lJHeQ4llolkaX2N0xT3XCfrLsH5ySPAuEEYfqJXJlDHYzpm+TtyaE7qG31oMlyXFL" +
                "SYz2R4WOP7pLN16gFYPe5jAL25GV3YgdmK+iCRgnvZcltanxSyr0QhfbYkgM/KBD51mvXQBSdUxFgT6n" +
                "sWqwTR4PIV87dS+fDONZ9h+WbStTzQsAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
