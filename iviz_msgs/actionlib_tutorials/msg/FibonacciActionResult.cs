/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionResult")]
    public sealed class FibonacciActionResult : IDeserializable<FibonacciActionResult>, IActionResult<FibonacciResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public FibonacciResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new FibonacciResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionResult(ref b);
        }
        
        FibonacciActionResult IDeserializable<FibonacciActionResult>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionResult(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bee73a9fe29ae25e966e105f5553dd03";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXW/bNhR9F6D/QKAPTYol3dp9dAH84Dlu6iFtg8TbyzAEFHktcZMol6Ts+N/3XMqS" +
                "7cTG/NDFiCM7Ic89PPfcy/uBpCYnivhIpAqmtqXJ7iuf+9dXtSzvggyNFz4+kvcmq61UytySb8ogXHyk" +
                "yeAbv9Lk493VBaLqlsmHyC9NXgjwsVo6LSoKUssgxawGf5MX5M5KWlDJXKs5aRH/G1Zz8ue8c1oYL/CT" +
                "kyUny3IlGo9VoRaqrqrGGiUDiWAq2gHgrcYKKebSBaOaUjpsqJ02ltfPnKwo4vPb05eGrCIxubzAKutJ" +
                "NcGA1AoYypH0xub4JxY3xoa3b3gHNk6X9Rm+U45c9AxEKGRgxvQwh9BMVvoLDvOqPeM54CESIZD24iT+" +
                "7R5f/alAHLCgea0KcQL6N6tQ1BaIJBbSGZmVxMgKOgD2JW96eboNzdQvhJW27vBbyE2QY3DtBpiPdVYg" +
                "eSVL4JscOmLl3NULo7E2W0UUVRqyQcCDTrpVmvC2NihA3rPYWIZ9MTd4Su9rZZAJLZYmFGnig+MAMS/3" +
                "RqfJ/+bOg9WSJvwZWc7xiBw42e/WRdR9uxl/upx8uhLdayC+x2/2KcWNopBerCiwQzNioVRrgrVSbXik" +
                "3y24NFrQ4Wg6+XMstkB/2AXl5DTOQWN4MiOW6jjkm9vx+OPNdHzZI7/ZRXakCFaHSZF+WIX/gmrwQchZ" +
                "gK9NYAEcZ4oeYl3YPE02VJ++XuANw0QhWvehUuclMYQJvoMB1ZMpuQoFWXJ/CHTakb77YzQajy+3SL/d" +
                "Jb0EtFSFQePQMKViIWYNN4d9WhyMM/zt8+1GGo7z4544WR1Pr5vo0A37vaF0Q/+tDnvD16iJmTRl4+gg" +
                "wdvx7+PRFsOB+OkpQUf/kGKGewlxedVNeGya745gmZGSaLYRtI/WoH8GCa7cM9DDjV3I0uiDR1gbsC+Z" +
                "gfj5OQzYO9DWIZbjxoN9Bjcqj4bX15uiHohfjqWYEe4x2svxKIWRmKcp26VtZ8ZVfOPxtdKnInZrpkJ6" +
                "9xjbZnn3DY5xpNRsjZ1CbCPwdXLIGdef76bbWAPxa0Qc2k6P9a0CKKGROkahVgfZq8Ao5+2U4GH0Ukfp" +
                "smOq0DN4zYqzrEsDBVBCCPaok+IKG5ZlvYwzCy9FUeBDvbnFwGd9gXG5ia0JjLdoypo8j1quVwV6wPj1" +
                "vJfc5LIdp9b3cqeWD5x5PlW8s6HtsjAYP+J1vdVjolFIx5lpEuebOIftEQwAZNlLOCt51glzEFVzZK0s" +
                "eTuj+jaPS0LwHrzzIfxJjptM5LQ7TXSHQMtZDyFo0eC42k3IjEhnUv3L5uQt7ciL8dN7mXOykSY/J2Vm" +
                "RnXVEVl4NhPDx8GwXQFmVRPLBO3PYBlUWGeyHVWeIY+hQaIMhHv9aJpPkyTOpH/93c+xafIV7hO4syEM" +
                "AAA=";
                
    }
}
