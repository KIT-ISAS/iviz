/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformActionFeedback : IDeserializable<LookupTransformActionFeedback>, IMessage, IActionFeedback<LookupTransformFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public LookupTransformFeedback Feedback { get; set; }
    
        public LookupTransformActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public LookupTransformActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public LookupTransformActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public LookupTransformActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public LookupTransformActionFeedback RosDeserialize(ref ReadBuffer b) => new LookupTransformActionFeedback(ref b);
        
        public LookupTransformActionFeedback RosDeserialize(ref ReadBuffer2 b) => new LookupTransformActionFeedback(ref b);
    
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
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = Feedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVptnDZJPaODKiuOMk7isdVeOh0PSK5INCTA4kOy/n3fghQl" +
                "OVajQxKNbH0Bbx/evl3sW5I5WVHGl0RmXhldqfS+doX76crI6s5LH5xw8SW5NuZTaOZWarcwtn5DlKcy" +
                "+yQW3Ztk9JUfyfu7qwtEz1tGb1ueAwFaOpc2FzV5mUsvBfiIUhUl2bOKllQx5bqhXMRf/bohN8TGeamc" +
                "wLMgTVZW1VoEh0XeiMzUddAqk56EVzXt7cdOpYUUjbReZaGSFuuNzZXm5Qsra2J0PB39G0hnJGaXF1ij" +
                "HWXBKxBaAyGzJJ3SBX4USVDavzjnDWIg/ro17vnfyWC+Mmf4ngpkpWchfCk9s6aHxpJjwtJdINgP7SmH" +
                "CAKVCOFyJ07id/f46E4FooELNSYrxQmOcLP2pdEAJLGUVsm0IgbOIAVQn/GmZ6c7yDpCa6nNBr5F3MY4" +
                "Blb3uHymsxLJq1gGFwooiYWNNUuVY2m6jiBZpUh7AStaadcJ72pDJoM3LDYWYVdMDV6lcyZTyEQuVsqX" +
                "ifOW0WNa7lWefCNbHiyXhN8ixQVeOD5n+vWmhtoPN9MPl7MPV2LzGImf8Z/9SXGbKKUTa/LszJRYn6xN" +
                "fCdQGxs5t0sURIs5nsxnf07FDubzfUzOSLAWysKNKbFGRwHf3E6n72/m08se+Hwf2FJG8DhsiZTDHvwN" +
                "ysB5IRceTlaeT285QfQQC0IXififxwB/MElUoTUcyrOpiBGUdxsUED2Zk61RhhX3BE+nHeW7PyaT6fRy" +
                "h/KLfcorIMusVMS0XchYhUXghvCUEIfCjH//eLvVhcP88kSY1MSj5yHacsv9yUh5oC9Kw65wBmWwkKoK" +
                "lg7Ru52+m052+I3Er5/Ts/QPZf6AA2JBmeAf2+XHL3NMKZNorhGzDxbQML0EU+4QaNlKL2Wl8kMH6JzX" +
                "V8pIvPwOzuutp42PRbg1X5+8XuHJ+Pp6W8kj8epYginhzqInGR6jLnLyebb2SeuFsjXfbnx9+N0uEJlQ" +
                "vneIXZu8/gqHOE5mNsVe+bUB+No44Inrj3fzXaiR+C0CjvVGjO72AJLIkTUGoVYE2UvAKMN2HHAweJVH" +
                "3dIjas8xtmG1WdKVwvFROVI/ap3JYFxVZhUHE16IUrBct/1lBTLdRcU1JnamLd6SUxqKgmXsFnl68Ml3" +
                "vMpml0nrgHYE6URyntPN54l3MiRdlQqzRbyPd1pKdAflPBTN4ugSujvmsU7YT5r9g1OSY4Ew4lDdIFdV" +
                "hd2M6drkrQihe+iN9WBJstxSIqPdUaHjj+7SjRdoxaC33s/CZnZlN2IH5qtQecyVzsmC2tS4hjK1UNmm" +
                "GCIDN+zQeehrF4BUHWJRoM8prBpuksdDyDdKnV+ct0k7MJwnyX97lDpR5QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
