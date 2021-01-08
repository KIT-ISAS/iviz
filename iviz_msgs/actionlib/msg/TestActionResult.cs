/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestActionResult")]
    public sealed class TestActionResult : IDeserializable<TestActionResult>, IActionResult<TestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestActionResult(ref b);
        }
        
        TestActionResult IDeserializable<TestActionResult>.RosDeserialize(ref Buffer b)
        {
            return new TestActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
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
        [Preserve] public const string RosMessageType = "actionlib/TestActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d669e3a63aa986c667ea7b0f46ce85e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXttEk/UgM6qLLqKHASw1Z7NbjkaJctl6uSXMn6933D1Wql" +
                "WEJ1SCJYXskm3zy+eTOcdyQ1eVGmRyZVNLWzJn+sQhFe3dTSPkQZmyBCemQzCvGeQmOj8OkxyIZf+DXI" +
                "PjzcXCGgbkm8S9QG2QsBKk5Lr0VFUWoZpZjXoG6KkvyFpSVZplktSIv037heULjknbPSBIGfghx5ae1a" +
                "NAGrYi1UXVWNM0pGEtFUtAfAW40TUiykj0Y1VnpsqL02jtfPvawo4fM70L8NOUVien2FVS6QaqIBqTUw" +
                "lCcZjCvwTyxujItvXvMObJyt6gt8pwJp2DIQsZSRGdPTAkIzWRmuOMx37RkvAQ+RCIF0EGfpb4/4Gs4F" +
                "4oAFLWpVijPQv1vHsnZAJLGU3sjcEiMr6ADYl7zp5fkuNFO/Ek66usNvIfsgp+C6HpiPdVEieZYlCE0B" +
                "HbFy4eul0VibrxOKsoZcFLCfl349yHhbGxQgf7DYWIZ9KTd4yhBqZZAJLVYmloMsRM8BUl4ejR5kX82d" +
                "RwtlkPFnZLnAI3HgZL/d1E/37W7y8Xr68UZ0r6H4Ab/Zp5Q2ilIGsabIDs2JhVKtCTZKteGRfr/k0mhB" +
                "R+PZ9K+J2AH9cR+Uk9N4D43hyZxYqtOQ7+4nkw93s8n1Fvn1PrInRbA6TIr0wyr8F1RDiELOI3xtIgvg" +
                "OVP0lOrCFYOsp/r89QJvGCYJ0boPlbqwxBAmhg4GVM9m5CsUpOX+EOm8I/3w53g8mVzvkH6zT3oFaKlK" +
                "g8ahYUrFQswbbg6HtDgaZ/T7p/teGo7z04E4eZ1Or5vk0J79wVC6of9Xh70RatTEXBrbeDpK8H7yfjLe" +
                "YTgUPz8n6OlvUszwICEur7qJn5vm+xNY5qQkmm0C3UZr0D+jBFfuGejhxi2lNfroETYG3JbMUPzyLQy4" +
                "daCrYyrH3oPbDPYqj0e3t31RD8Wvp1LMCfcYHeR4ksJIzPOU7dN2c+MrvvH4WtmmInVrpkJ6/xi7Znn7" +
                "BY5xotRsjb1CbCPwdXLMGbefHma7WEPxW0IcuU6Pza0CKKGROkahVge5VYFRLtspIcDoVifp8lOqMDB4" +
                "zYqzrCsDBVBCCPZZJ8UVNrK2XqWZhZeiKPCh7m8x8NlcYFxuYmf44i2a8qYokpabVZGeMH5920tuet2O" +
                "U5t7uVMrRM48nyrd2dB2VRqMH+m63ukxySik08w0TfNNmsMOCAYAcuwlnJUC64Q5iKoFsmYtb2fU0OZx" +
                "RQi+Be98CH+S5yaTOO1PE90h0HI2Q0jA+pVE79tNyJxI51L9w+bkLe3Ii/EzBFlwspGmsCBl5kZ11ZFY" +
                "BDYTw6fBsF0BZlWTygTtz2AZVNhksh1Vvn4eX/Xj+yBrZ9Buiv8PctexkQgMAAA=";
                
    }
}
