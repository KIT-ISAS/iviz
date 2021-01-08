/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestActionFeedback")]
    public sealed class TestRequestActionFeedback : IDeserializable<TestRequestActionFeedback>, IActionFeedback<TestRequestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestRequestFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TestRequestFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionFeedback(ref b);
        }
        
        TestRequestActionFeedback IDeserializable<TestRequestActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionFeedback(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXttEk/UgM6qLLiqHASw1Z7NbjkaJctl7sluZL17/uGq9VH" +
                "LKE6JBFsrySTbx7fvBnOe5KavCjTI5MqmtpZkz9WoQivbmppH6KMbRAhPbIZhXhP/7Z4vCPSuVT/iPn6" +
                "zSAbfuHXIPvwcHOF2Lrj8z6xHGQvBFg5Lb0WFUWpZZRiXuMUpijJX1hakGXGVUNapP/GVUPhknfOShME" +
                "fgpy5KW1K9EGrIq1UHVVtc4oGUlEU9EeAG81TkjRSB+Naq302FB7bRyvn3tZUcLn38AKOUVien2FVS6Q" +
                "aqMBqRUwlCcZjCvwTyxujYtvXvMObJwt6wt8pgIZ2TAQsZSRGdNT4ykwWRmuOMx33RkvAQ+RCIF0EGfp" +
                "u0d8DOcCccCCmlqV4gz071axrB0QSSykNzK3xMgKOgD2JW96eb4LzdSvhJOu7vE7yG2QU3DdFpiPdVEi" +
                "eZYlCG0BHbGy8fXCaKzNVwlFWUMuCjjRS78aZLytCwqQdyw2lmFfyg2eMoRaGWRCi6WJ5SAL0XOAlJdH" +
                "owfZV3Pn0ZoZZPweWS7wSBw42W/XpdR/upt8vJ5+vBH9ayh+wF/2KaWNopRBrCiyQ3NioVRngrVSXXik" +
                "3y+4NDrQ0Xg2/WsidkB/3Afl5LTeQ2N4MieW6jTku/vJ5MPdbHK9QX69j+xJEawOkyL9sAp/k/qFkPMI" +
                "X5vIAnjOFD2lunDFINtSff56gV8YJgnRuQ+V2lhiCBNDDwOqZzPyFQrScn+IdN6TfvhzPJ5MrndIv9kn" +
                "vQS0VKVB49AwpWIh5i03h0NaHI0z+v3T/VYajvPTgTh5nU6v2+TQLfuDoXRL/68OeyPUqIm5NLb1dJTg" +
                "/eSPyXiH4VD8/Jygp79JMcODhLi86jZ+bprvT2CZk5Jotgl0E61F/4wSXLlnoIcbt5DW6KNHWBtwUzJD" +
                "8cu3MODGga6OqRy3HtxkcKvyeHR7uy3qofj1VIo54R6jgxxPUhiJeZ6yfdpubnzFNx5fK5tUpG7NVEjv" +
                "H2PXLG+/wDFOlJqtsVeIXQS+To454/bTw2wXayh+S4gj1+uxvlUAJTRSxyjU6SA3KjDKZTclBBjd6iRd" +
                "fkoVBgavWXGWdWmgAEoIwT7rpLjCRtbWyzSz8FIUBd7U21sMfNYXGJeb2JnDeIumvC2KpOV6VaSnyLjf" +
                "8pKbXnfj1Ppe7tUKkTPPp0p3NrRdlgbjR7qud3pMMgrpNDNN03yT5rADggGAHHsJZ6XAOmEOoqpB1qzl" +
                "7YwaujwuCcE34L0P4U/y3GQSp/1poj8EWk7TDSFo0eCI3rebkH7IZXPyFkxirY0YP0OQBScbaQoNKTM3" +
                "qq+OxCKwmRg+DYbdCjCr2lQmaH8Gy6DCOpPdqPL18/jqwCTPcf8D39VmmBAMAAA=";
                
    }
}
