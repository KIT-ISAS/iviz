/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionFeedback")]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
        }
        
        TwoIntsActionFeedback IDeserializable<TwoIntsActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA71Wy3IbNxC871egSgdLqYhO7DwcVfHAkLTMlGyrJCZXFXYx3EWCxTJ4kOLfpwe7XJKy" +
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
                
    }
}
