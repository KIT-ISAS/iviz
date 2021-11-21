/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionResult")]
    public sealed class GetMapActionResult : IDeserializable<GetMapActionResult>, IActionResult<GetMapResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public GetMapResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new GetMapResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMapActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionResult(ref b);
        }
        
        GetMapActionResult IDeserializable<GetMapActionResult>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionResult(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac66e5b9a79bb4bbd33dab245236c892";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XTVPbSBC9q4r/MFUcAlu2Qz42m6WKAwtewhYkLJC9UBQ1ktrWJJJGmRlhvL9+X8/o" +
                "wwpmwyHgMtiWZrpf93vd0/pAMiUjMv8RycQpXeYqvins3L480jK/cNLVVlj/ER2RO5XVOdk6d8L4j41o" +
                "7ye/NqLTi6NduEwDjA8e3Ea0KQCmTKVJRUFOptJJMdMAr+YZmXFOt5Qz0KKiVPi7blmRnWDjZaaswHtO" +
                "JRmZ50tRWyxyWiS6KOpSJdKRcKqgwX7sVKWQopLGqaTOpcF6bVJV8vKZkQWxdbwtfaupTEgcH+5iTWkp" +
                "qZ0CoCUsJIakVeUcN0VUq9K9ec0bos3LhR7jJ81BQedcuEw6Bkt3FVLMOKXdhY9fQnAT2EZ2CF5SK7b8" +
                "tRv8tNsCTgCBKp1kYgvIz5Yu0yUMkriVRsk4JzacIAOw+oI3vdhescywd0UpS92aDxZ7H48xW3Z2OaZx" +
                "Bs5yjt7WcyQQCyujb1WKpfHSG0lyRaUT0J2RZhnxruAy2vyTc4xF2OUZwae0VicKBKRioVwWWWfYumfj" +
                "RqXRkwnywerYiPg7yJ3jgyEwx+/bmgk/zqYfD48/Hon2tSd28J+VSX6byKQVS3KsyZg4RUngvslRcA7a" +
                "zS0KNdjcP7g8/mcqVmy+GtpkUmpjkFzoMCZO06MMn51Pp6dnl9PDzvDroWFDCUHdUCZYh0L4CgrAOiFn" +
                "DmJWjqM3zBHd+VIo51EP9P5rE3/Qic9C0BwKs8qJLShnWysAunVJpkAB5twNHG03kC8+HxxMp4crkN8M" +
                "IS9gWSaZQpdIIcWEszCruRWsS8RDbvb/+HTe54XdvF3jJtY+9LT2yuyxr/WU1vTD1LAqrEYlzKTKa0MP" +
                "wTuf/jU9WMG3J369D8/QF0oY31o4XFO6dt/LZfRjjDElEm3V2+yc1WiVTgIpNwk0a1XeylylDwXQKK+r" +
                "lD3x7hmU10mv1M4XYS++jrwuwwf7Jyd9Je+J3x4LMCacVrQW4WOyC07uszUEXc6UKfhc4xOko8G3ZkZC" +
                "6SCIVZm8/wlBPC7NLIpB+QUHfHI8oImTTxeXq6b2xO/e4H7ZJqM5QGBJpGCNjVBIguxSwFYmYRCwEHie" +
                "+rzFj6g9y7Y1Z5tTulAIH5UDX8PWGW3u57le+JGEF6IU8EX35xXANGcV15hYma54S0pxPZ9zGptFju5c" +
                "9Kyn2fEhD1ksgjCINHmyjhnnkPzJjKwuMoUJw5/KK13FC4RSnoiO/QDjZ6w1qcJ+KllCCJQs5wiDDhUV" +
                "6Mpz7GabNvC3ILjuTLfqgyrJcFfxiFYHhgY/GkwzZKAbA95ySMSMKI1l8pUFiR1hkMVQaa2cU2DHVpSo" +
                "mUraevAI7KSxzhNfWABQRe3rAq1OYdWk5Q+rno69Ut42vK1M5BtRd/lTktQVCmt5ZJCLQlZPj2TgMggJ" +
                "GjDEQywqiIl4PcaM1AAa8WgdhEQ4l8Fonq+uZr2Bw1jGKlduKfQMJnXrYxJFg0cXAeZP8VBw2D4U8HZ4" +
                "iZCc7roqZ7pVCO75idLDMHoxLuQXbMNwT2YUeltXqVs7o53tiRBdhLDRQ2MJS26JYSI2soSGrnZGr3Z2" +
                "rrHpc/m11AvI3Yrxq0nEHe3q2rt+BnWsRN8xkmnUdIwHksQnxBTSlyaGluaESTJpUK9kFBSPvPuLA3bb" +
                "JH7fD0JauRvkGqykoTJx7YZ/3/hi7dMPpnUeBqOr4iXzfx3NsJAfj/p72IAoQEQKJq54lb1uH6L8xWZB" +
                "RngMdN+vCFcbp9ooNNg2IoZwVYwE3kamTFX7lOhpJJmPF9ogVxWehppNMOR16hXRMg5Dk2hOGM+cWYa8" +
                "n/kt3t2TkXzfI1O839dQ4BXAfQAAO0OXRd+SCY38hIPLaXNfBQ3gfAbqdu9ERGcaeewWRH/XaJ+m9Hb7" +
                "ddGzxQgwnY4x7vAR0HSKNgSEg+d1j3oQceSV9e6tuOu+Lbtv/z5XBH3+1jbIQVaH+PnXtz77XLfogf8f" +
                "VPttgfD+A6QHRtjpEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
