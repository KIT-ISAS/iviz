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
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new TestFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestFeedback Feedback)
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
                "H4sIAAAAAAAACr1WTXPbNhC9c8b/ATM6xO7UTpv0I/WMDqqkOMo4icdWe/WAxIpEC4IqAErWv+9bUKSo" +
                "WprokFojW1/A24e3bxf7gaQiJ4r4ksgs6MoanT6WPvevbyppHoIMtRc+viRz8uE9kUpl9rdYbN+cJcNv" +
                "/DhLPj3cXCOoaoh8iPTOkoEAHaukU6KkIJUMUiwq0Nd5Qe7S0IoMUy2XpET8NWyW5K+wcV5oL/DMyZKT" +
                "xmxE7bEoVCKryrK2OpOBRNAl7e3HTm2FFEvpgs5qIx3WV05py8sXTpbE6Hh6+qcmm5GYTa6xxnrK6qBB" +
                "aAOEzJH02ub4USS1tuHtG96QDObr6hIfKUcSuuAiFDIwWXpaOvLMU/prxPiuOdwVsKEOIYry4jx+94iP" +
                "/kIgCCjQssoKcQ7md5tQVBaAJFbSaZkaYuAMCgD1FW96ddFDZtrXwkpbtfAN4i7GKbC2w+UzXRbImeHT" +
                "+zqHgFi4dNVKKyxNNxEkM5psEHCek26T8K4mZDJ4zxpjEXbFjOBVel9lGglQYq1DkfjgGD1m41Gr5H8z" +
                "5NH6OEv4PZKb44UpcI7ftVXTfLibfp7MPt+I9jEUP+A/O5PiNlFILzYU2JMpsURZk/utRk1wpN2tUKoN" +
                "5mg8n/05FT3MH/cxOSm1cxAXPkyJZToJ+O5+Ov10N59OOuA3+8COMoK74UxkHQ7hb1AAPgi5CDCzDnx6" +
                "xzmip1gKNk92RJ8/BviDT6IKjedQmEtDjKCDb1FA9HxOrkQBGu4GgS62lB/+GI+n00mP8tt9ymsgy6zQ" +
                "6BIKVsxYhUXNreCQEMfCjH7/cr/ThcP8dCBMWsWjqzo6c8f9YCRV01elYVf4CpWwkNrUjo7Ru59+nI57" +
                "/Ibi5+f0HP1FGfM7SIdrqqrDf+3y/dc5ppRJtNWI2QWr0SqDBFNuEmjW2q6k0erYAbbO6yplKH55Aed1" +
                "1rNViEW4M1+XvE7h8ej2dlfJQ/HrqQRTwm1FBxmeoi5y8jxb+6TtQruS7zW+Qbo0xNbMTEjtHaJvk3ff" +
                "4BCnycym2Cu/JgDfHEc8cfvlYd6HGorfIuDItmJsLxAgCYWsMQg1IshOAka5agYBD4MbFXVLT6g9z9gV" +
                "q82SrjWOj8pBrP3WmQxGxlTrOJLwQpQC3lS7+wpktncV15jozVe8RVFa5znLuF0U6CkkL3qbzSY8ZLEJ" +
                "mkFkq5MPnHE+UryZoeq60Jgw4q3c6yrRIKR4IprFASbOWAekwn6ybCEclDxrhEGHyiXSZQx2M6Zv8rcm" +
                "hO6gW/fBleS4q0RG/YFhyx8NZjtkeCxeSzS6fiLa2ZUNiR2YsmoTMFR6L3POMLLjl5Tphc7aeogMPBuI" +
                "0XniaxaAVFnHukCr01h11eYPq14ge6/7Q/lZ0gyX7fkQ/18RB/Tk4wsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
