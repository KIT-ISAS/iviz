/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestActionResult")]
    public sealed class TestRequestActionResult : IDeserializable<TestRequestActionResult>, IActionResult<TestRequestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestRequestResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestRequestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestRequestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionResult(ref b);
        }
        
        TestRequestActionResult IDeserializable<TestRequestActionResult>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionResult(ref b);
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
                int size = 5;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0476d1fdf437a3a6e7d6d0e9f5561298";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXttEk/UgM6qJLqKHASw1Z7FbjkaJftLqmSXMn6933D1eoj" +
                "llAdkgiWV7LJN49v3gznHUlNXpTpkUkVjbOVyWd1KMKrWyerxyhjE0RIj2xKIT7Qv016hKaKwqdHL+t/" +
                "4Vcv+/B4e4O4uuXyLjHsZS8EGFktvRY1RalllGLucAJTlOSvKlpSxWzrBWmR/hvXCwrXvHNamiDwU5Al" +
                "L6tqLZqAVdEJ5eq6sUbJSCKamg4AeKuxQoqF9NGoppIeG5zXxvL6uZc1JXx+B1bHKhKT0Q1W2UCqiQak" +
                "1sBQnmQwtsA/sbgxNr55zTuwcbpyV/hOBbKxZSBiKSMzpqcFhGayMtxwmO/aM14DHiIRAukgLtLfZvga" +
                "LgXigAUtnCrFBejfr2PpLBBJLKU3Mq+IkRV0AOxL3vTych+aqd8IK63r8FvIXZBzcO0OmI91VSJ5FUsQ" +
                "mgI6YuXCu6XRWJuvE4qqDNko4EIv/bqX8bY2KED+YLGxDPtSbvCUIThlkAktViaWvSxEzwFSXmZG97Kv" +
                "5s6T9dLL+DOyXOCROHCy327KqPt2P/44mny8Fd2rL37Ab/YppY2ilEGsKbJDc2KhVGuCjVJteKTfL7k0" +
                "WtDBcDr5ayz2QH88BOXkNN5DY3gyJ5bqPOT7h/H4w/10PNoivz5E9qQIVodJkX5Yhf+SeoWQ8whfm8gC" +
                "eM4UPaW6sEUv21F9/nqBNwyThGjdh0pdVMQQJoYOBlQvpuRrFGTF/SHSZUf68c/hcDwe7ZF+c0h6BWip" +
                "SoPGoWFKxULMG24Ox7Q4GWfw+6eHnTQc56cjcXKXTq+b5NAd+6OhdEP/rw57IzjUxFyaqvF0kuDD+P14" +
                "uMewL35+TtDT36SY4VFCXF6uiZ+b5vszWOakJJptAt1Ga9A/owRX7hno4cYuZWX0ySNsDLgtmb745VsY" +
                "cOtA62Iqx50HtxncqTwc3N3tirovfj2XYk64x+gox7MURmKep+yQtp0bX/ONx9fKNhWpWzMV0ofH2DfL" +
                "2y9wjDOlZmscFGIbga+TU864+/Q43cfqi98S4sB2emxuFUAJjdQxCrU6yK0KjHLdTgkBRq90ki4/pwoD" +
                "gztWnGVdGSiAEkKwzzoprrBBVblVmll4KYoCH9zuFgOfzQXG5Sb2ZjDeoilviiJpuVkV6Qnj17e95Caj" +
                "dpza3MudWiFy5vlU6c6GtqvSYPxI1/Vej0lGIZ1mpkmab9IcdkQwAJBlL+GsFFgnzEFUL5C1quLtjBra" +
                "PK4IwbfgnQ/hT/LcZBKnw2miOwRazmYIQYsGx/VhQuZEOpfqHzYnb2lHXoyfIciCk400hQUpMzeqq47E" +
                "IrCZGD4Nhu0KMKubVCZofwbLoMImk+2o8vXz+OrZFN/L2lEUqZu1p8ty57gmZsFwr8O41/r3P6R7Jn4w" +
                "DAAA";
                
    }
}
