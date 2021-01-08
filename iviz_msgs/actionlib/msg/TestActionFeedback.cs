/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestActionFeedback")]
    public sealed class TestActionFeedback : IDeserializable<TestActionFeedback>, IActionFeedback<TestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new TestFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new TestFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestActionFeedback(ref b);
        }
        
        TestActionFeedback IDeserializable<TestActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TestActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d3d0bf7fb3dda24779c010a9f3eb7cb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W224bNxB9X0D/QCAPsYvaaZNeUgN6UGXFUeEkhq321eCSo122XK5KciXr73uGu6tL" +
                "LKF6SCJY1o08c3jmzHDek9TkRZleMqmiqZ01+WMVivDqppb2IcrYBBHSSzajEN8R6Vyqf8S8ezPIhl/4" +
                "Mcg+PNxcIahuibxP9AbZCwE6TkuvRUVRahmlmNegb4qS/IWlJVmmWi1Ii/RrXC8oXPLOWWmCwF9Bjry0" +
                "di2agFWxFqquqsYZJSOJaCraA+CtxgkpFtJHoxorPTbUXhvH6+deVpTw+Rno34acIjG9vsIqF0g10YDU" +
                "GhjKkwzGFfgRixvj4pvXvAMbZ6v6Ap+pQCo2DEQsZWTG9LTwFJisDFcc5rv2jJeAh0iEQDqIs/TdIz6G" +
                "c4E4YEGLWpXiDPTv1rGsHRBJLKU3MrfEyAo6APYlb3p5vgvN1K+Ek67u8VvIbZBTcN0WmI91USJ5liUI" +
                "TQEdsXLh66XRWJuvE4qyhlwUsKCXfj3IeFsbFCDvWGwsw76UG7zKEGplkAktViaWgyxEzwFSXh6NHmRf" +
                "zZ1Hi2WQ8XtkucBL4sDJftvVUP/pbvLxevrxRvSPofgB/9mnlDaKUgaxpsgOzYmFUq0JOqXa8Ei/X3Jp" +
                "tKCj8Wz610TsgP64D8rJabyHxvBkTizVach395PJh7vZ5HqD/Hof2ZMiWB0mRfphFf4G1RCikPMIX5vI" +
                "AnjOFD2lunDFINtSff54gScMk4Ro3YdKXVhiCBNDDwOqZzPyFQrScn+IdN6TfvhzPJ5MrndIv9knvQK0" +
                "VKVB49AwpWIh5g03h0NaHI0z+v3T/VYajvPTgTh5nU6vm+TQLfuDoXRD/68OeyPUqIm5NLbxdJTg/eSP" +
                "yXiH4VD8/Jygp79JMcODhLi86iZ+bprvT2CZk5Jotgl0E61B/4wSXLlnoIcbt5TW6KNH6Ay4KZmh+OVb" +
                "GHDjQFfHVI5bD24yuFV5PLq93Rb1UPx6KsWccI/RQY4nKYzEPE/ZPm03N77iG4+vlU0qUrdmKqT3j7Fr" +
                "lrdf4BgnSs3W2CvENgJfJ8eccfvpYbaLNRS/JcSR6/XobhVACY3UMQq1OsiNCoxy2U4JAUa3OkmXn1KF" +
                "gcFrVpxlXRkogBJCsM86Ka6wkbX1Ks0svBRFgTf19hYDn+4C43ITOwMYb9GUN0WRtOxWRXqKjPstL7np" +
                "dTtOdfdyr1aInHk+Vbqzoe2qNBg/0nW902OSUUinmWma5ps0hx0QDADk2Es4KwXWCXMQVQtkzVrezqih" +
                "zeOKEHwD3vsQ/iTPTSZx2p8m+kOg5XRDSMD6lUTv201IP+SyOXkLJrHGRoyfIciCk400hQUpMzeqr47E" +
                "IrCZGD4Nhu0KMKuaVCZofwbLoEKXyXZU+fp5fLU7wg+ydgrtD4n4/wHIWSueEQwAAA==";
                
    }
}
