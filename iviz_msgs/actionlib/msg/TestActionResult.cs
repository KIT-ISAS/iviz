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
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestResult Result)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACr1WTXPbNhC9c0b/ATM6xO7ETpv0I/WMDqqsOuo4icdWe/WAxIpEC4IqAErWv+9bUKSo" +
                "WJrokFojm5INvH14+3axH0gqcqKIj0RmQVfW6PSx9Ll/c1NJ8xBkqL3w8ZHMyYd78rUJwsXHIBl949cg" +
                "+fhwc4WAqiHxIVIbJEMBKlZJp0RJQSoZpFhUoK7zgtyFoRUZplkuSYn437BZkr/ExnmhvcA7J0tOGrMR" +
                "tceiUImsKsva6kwGEkGXtLcfO7UVUiylCzqrjXRYXzmlLS9fOFkSo+Pt6d+abEZidn2FNdZTVgcNQhsg" +
                "ZI6k1zbHP0VSaxveveUNyXC+ri7wlXIkoAsuQiEDk6WnJSRmntJfIcZ3zeEugQ11CFGUF2fxb4/46s8F" +
                "goACLausEGdgfrcJRWUBSGIlnZapIQbOoABQX/GmV+c9ZKZ9Jay0VQvfIO5inAJrO1w+00WBnBk+va9z" +
                "CIiFS1ettMLSdBNBMqPJBgHXOek2Ce9qQibD31ljLMKumBE8pfdVppEAJdY6FIkPjtFjNh61Sv43Qx6t" +
                "jUHCn5HcHA+mwDl+31ZM8+Vu+ul69ulGtK+R+B6/2ZkUt4lCerGhwJ5MiSXKmtxvNWqCI+1uhTJtMMeT" +
                "+eyvqehh/rCPyUmpnYO48GFKLNNJwHf30+nHu/n0ugN+uw/sKCO4G85E1uEQ/gsKwAchFwFm1oFP7zhH" +
                "9BRLwebJjujz1xA/8ElUofEcCnNpiBF08C0KiJ7NyZUoQMPdIND5lvLDn5PJdHrdo/xun/IayDIrNLqE" +
                "ghUzVmFRcys4JMSxMOPfPt/vdOEwPx4Ik1bx6KqOztxxPxhJ1fRVadgVvkIlLKQ2taNj9O6nf0wnPX4j" +
                "8dNzeo7+poz5HaTDNVXV4Uu7vP46x5QyibYaMbtgNVplkGDKTQLNWtuVNFodO8DWeV2ljMTPL+C8znq2" +
                "CrEId+brktcpPBnf3u4qeSR+OZVgSrit6CDDU9RFTp5na5+0XWhX8r3GN0iXhtiamQmpvUP0bfL+Gxzi" +
                "NJnZFHvl1wTgm+OIJ24/P8z7UCPxawQc21aM7QUCJKGQNQahRgTZScAol80g4GFwo6Ju6Qm15xm7YrVZ" +
                "0rXG8VE5iLXfOpPh2JhqHUcSXohSwIdqd1+BzPau4hoTvdmKtyhK6zxnGbeLAj2F5EVvs9k1D1lsgmYQ" +
                "2erkA2ecjxRvZqi6LjQmjHgr97pKNAgpnohmcYCJM9YBqbCfLFsIByXPGmHQoXKJdBmD3Yzpm/ytCaE7" +
                "6NZ9cCU57iqRUX9g2PLXqh0y0I1BD42un4gFkUpl9g8bEjuaQRZDpfcy5wwjO35JmV7orK2HyMCzgRid" +
                "J75mAUiVdawLtDqNVZdt/rDqBbL3ZjeQD5JmtGzn8v8AdyZqmNoLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
