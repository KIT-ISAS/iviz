/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestActionResult : IDeserializable<TestActionResult>, IActionResult<TestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestResult Result { get; set; }
    
        /// Constructor for empty message.
        public TestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestResult();
        }
        
        /// Explicit constructor.
        public TestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal TestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestActionResult(ref b);
        
        TestActionResult IDeserializable<TestActionResult>.RosDeserialize(ref Buffer b) => new TestActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3d669e3a63aa986c667ea7b0f46ce85e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwXLbNhC98ysw40PsTu20SZukntFBlVRHGSfx2GqvHhBYkWhBUAVAyfr7vgVFSqql" +
                "iQ5JNLIp2cDbh7dvF/uepCYvyvTIpIqmdtbkj1UowsubWtqHKGMTREiPbEYh3lNobBQ+PbLBV35lHx9u" +
                "rhFOtxTet8TOBHg4Lb0WFUWpZZRiXoO3KUryl5aWZJljtSAt0n/jekHhChtnpQkC74IceWntWjQBi2It" +
                "VF1VjTNKRhLRVLS3HzuNE1IspI9GNVZ6rK+9No6Xz72siNHxDvRvQ06RmI6vscYFUk00ILQGgvIkg3EF" +
                "/imyxrj4+hVvyM5mq/oSX6mA+n1wEUsZmSw9LaAv85ThGjF+aA93BWyIQ4iigzhPf3vE13AhEAQUaFGr" +
                "UpyD+d06lrUDIIml9EbmlhhYQQGgvuBNLy52kJn2tXDS1R18i7iNcQqs63H5TJclcmb59KEpICAWLny9" +
                "NBpL83UCUdaQiwKW89KvM97VhszO/mCNsQi7UkbwlCHUyiABWqxMLLMQPaOnbDwanX0jNx4ti4w/IrMF" +
                "HhyfE/yuq5X2y93k03j66UZ0r4H4Cb/ZlpS2iVIGsabIhsyJ9VFt4jcCtbGRc79EHbSYw9Fs+tdE7GD+" +
                "vI/JGWm8h7IwYU6s0UnAd/eTyce72WTcA7/aB/akCNaGLZFy2IP/AveHKOQ8wskm8uk9J4ieUh24ItsS" +
                "ff46ww9MklRoDYeqXFhiBBNDhwKi5zPyFarPciuIdLGh/PDnaDSZjHcov96nvAKyVKVBi9DwoWIV5g33" +
                "gUNCHAsz/P3z/VYXDvPLgTB5nY6um2TLLfeDkXRDX5SGXRFqlMFcGtt4OkbvfvJhMtrhNxC/Pqfn6W9S" +
                "zO8gHS6ouon/t8uPX+aYk5LoqQmzD9agT0YJptwh0KmNW0pr9LEDbJzXV8pAvPkOzuut5+qYinBrvj55" +
                "vcKj4e3ttpIH4u2pBHPCVUUHGZ6iLnLyPFv7pN3c+IovNb4++jSkvsxMSO8dYtcm777CIU6TmU2xV35t" +
                "AL42jnji9vPDbBdqIH5LgEPXibG5PYAkNLLGINSKIHsJGOWqnQICDG510i0/ofYCY9esNku6Mjg+Kgex" +
                "9ltndja0tl6leYQXohTwod5eViCzuai4xsTOVMVbNOVNUbCMm0WRnmL2Ha+y6ThNSZt7txMpRE43nyfd" +
                "yZB0VRrMFuk+3mkpyR2keRaaptElTVcHdMJ+cuwfnJICC4QRh6oFcmUtdjNmaJO3IoTuoTvrwZLkuaUk" +
                "Rrujwoa/0d14gVYMeuhyu1mYE+lcqn/YjdjRzq8YJ0OQBacXqQkLUmZuVFcMiUFg9zA6z3rtApCqmlQU" +
                "6HMGq6665PEQ8q1T93I7hWftRLmZxf8DT73QLM0LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
