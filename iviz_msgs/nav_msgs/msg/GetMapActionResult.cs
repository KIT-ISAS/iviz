/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapActionResult : IDeserializable<GetMapActionResult>, IActionResult<GetMapResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public GetMapResult Result { get; set; }
    
        /// Constructor for empty message.
        public GetMapActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new GetMapResult();
        }
        
        /// Explicit constructor.
        public GetMapActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal GetMapActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapActionResult(ref b);
        
        GetMapActionResult IDeserializable<GetMapActionResult>.RosDeserialize(ref Buffer b) => new GetMapActionResult(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ac66e5b9a79bb4bbd33dab245236c892";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XTVPcOBC9+1eoikNga2ZCPjabpYoDC7OELUhYIHuhKEq2e8ZKbGkiyQyzv35fS/4Y" +
                "B9hwCEwN2GNL3U+vX7daH0jmZEURLonMvDK6VOl15ebu5aGR5bmXvnbChUtySP5ELs7I1aUXNlyS3Z/8" +
                "SU7OD3fgMI8gPkRoGwJIdC5tLiryMpdeipkBcjUvyI5LuqGSUVYLykV461cLchNMvCiUE/jOSZOVZbkS" +
                "tcMgb0RmqqrWKpOehFcVDeZjptJCioW0XmV1KS3GG5srzcNnVlbE1vF19K0mnZE4OtjBGO0oq70CoBUs" +
                "ZJakU3qOlyKplfZvXvOEZONiacb4SXPw3zkXvpCewdLtAvwyTul24OOXuLgJbIMcgpfcic3w7Bo/3ZaA" +
                "E0CghckKsQnkpytfGA2DJG6kVTItiQ1nYABWX/CkF1trlhn2jtBSm9Z8tNj7eIxZ3dnlNY0LxKzk1bt6" +
                "DgIxcGHNjcoxNF0FI1mpSHsB0VlpVwnPii6TjT+ZYwzCrBARXKVzJlMIQC6WyheJ85ath2hcqzx5IjU+" +
                "mBgJ3yKyc1zYPwf4fZst8cfp9OPB0cdD0X52xTb+sywpTBOFdGJFngWZEvOTxcA3BEXfiLm9QR5Em3v7" +
                "F0f/TMWazVdDmxyR2lowCxGmxBw9yvDp2XR6cnoxPegMvx4atpQRpA1ZIuSQBz+B+p0XcuahZOV59ZYD" +
                "RLchD/Q86YHe/WzgDyIJLETBISsXJbEF5V1rBUA3L8hWyL6SS4GnrQby+ef9/en0YA3ymyHkJSzLrFAo" +
                "ETl0mDELs5rrwH1EPORm749PZz0v7ObtPW5SE5ae10GWPfZ7PeU1/ZAaVoUzSIOZVGVt6SF4Z9O/pvtr" +
                "+HbFr3fhWfpCGeO7Fw4nlKn993IZ/RhjSplETQ02O2c16qSXQMoVApVa6RtZqvyhBTTK6zJlV7x7BuV1" +
                "0tPGhyTsxdcFr2N4f+/4uM/kXfHbYwGmhK2K7kX4GHYRk7vRGoLWM2Ur3tR4++jCEOoyI6F8sIh1mbz/" +
                "CYt4HM0sikH6RQe8bTygieNP5xfrpnbF78Hgnm7JaHYPWBI5osZGKJIgOwrYyiR2AQ4CL/PAW/qI3HNs" +
                "2zDbTOlSYfnIHPgals5kY68szTL0IzwQqYAb029WANNsVJxjYq2v4ik5pfV8zjQ2gzzd+uQZt7Kjg9Al" +
                "NftuS5LzHG5eT9iTQemyUOgtwn68VlKCOijnXugotC6hu7qHJ8wnzfrBKskxQWhxqFogVmWJ2WzTxeAt" +
                "Ca470630IEmyXFICovVWocGP6tK0FyjFgLcaRmFGlKcy+8pqxIzYv6KddE7OKYbGLShTM5W1yRAQuElj" +
                "nXu9OACgqjokBeqcwqhJGzxuQp4odFreNEFba8OT7umnLKsXSKnVoQURlVw8NYyBw7bRtsSNKxKHQ/B6" +
                "jNaoQTPidjpKiLAdI5ZluT6alYbopTJVpfIrYWYwaVoXkyQZnFUEYn6Cg8BBexDg6bxmENM9V3pmWm3g" +
                "XegiAwxrluNKfsE0NPRkR7GkdQm6uT3a3poI0S0QNnpoLF7JlTB2wVZqqOdye/Rqe/sKkz7rr9osIXQn" +
                "xq8mCReyy6vg+sl1sbb2NhyFQSqnOIFkgQ1byZCRaFSaXSUrpEWaklUQOkgPD4eRbRj8vgxETrkIlAYh" +
                "yWNC4tk1/74OOdpzjzCbMjZDl9VLDv5VMsNAPg/17zABi0AUcoThkke5q/bUFB42AwrCuc9/PyI+bZwa" +
                "q1BU2xUxhMtqJPC1Muc4tcfCEEOS5XhpLLha4PjTTIKhINIghzbcMDRJ5oSWzNtVpP00TAnunijCd/0B" +
                "216fPTGoQB3QA+kMlRW1SmY0Ci0NHufNexUFgA0ZkNu5E5GcGpDYDUj+rlEyrQ52+3HJMy0QUFoFo7nh" +
                "mt8UiBY/1oKjeYA8WG4SNPXurbjt7lbd3b/PA7+n7r6iOOBzCJ5/fet553RF3fv/FbV3yyT5D95gcQXN" +
                "EQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
