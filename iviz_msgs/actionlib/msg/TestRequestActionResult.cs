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
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestRequestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestResult Result)
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
                "H4sIAAAAAAAACr1WXXMaNxR93xn+g2Z4iN2JnTbpR+oZHiimDh0n8di0rx6tdNlVuytRSQvm3/dcLSwQ" +
                "w4SH1Az2gi2de3TuuVf3A0lNXpTpkUkVjbOVyR/rUIQ3N05WD1HGJoiQHtmUQrynf5v0CE0VhU+PXjb4" +
                "xq9e9vHh5gpxdcvlQ2LYy/oCjKyWXouaotQySjFzOIEpSvIXFS2oYrb1nLRI/42rOYVLbJyWJgi8C7Lk" +
                "ZVWtRBOwKDqhXF031igZSURT095+7DRWSDGXPhrVVNJjvfPaWF4+87ImRsc7sDJWkZhcX2GNDaSaaEBo" +
                "BQTlSQZjC/xTZI2x8d1b3pD1p0t3ga9UIA9dcBFLGZksPc0hMfOU4QoxvmsPdwlsqEOIooM4S397xNdw" +
                "LhAEFGjuVCnOwPxuFUtnAUhiIb2ReUUMrKAAUF/xplfnO8hM+0pYad0GvkXcxjgF1na4fKaLEjmr+PSh" +
                "KSAgFs69WxiNpfkqgajKkI0C5vPSrzLe1YbM+r+zxliEXSkjeMoQnDJIgBZLE8ssRM/oKRuPRmf/myGP" +
                "lkgv489IboEHU+Acv98UTvvlbvzpevLpRmxeA/E9frMzKW0TpQxiRZE9mRNLpNrcrzVqgyPtfoFqbTGH" +
                "o+nkr7HYwfxhH5OT0ngPceHDnFimk4Dv7sfjj3fT8XUH/HYf2JMiuBvORNbhEP5Lag1CziLMbCKf3nOO" +
                "6CmVgi2yLdHnrz5+4JOkQus5FOa8IkYwMWxQQPRsSr5GAVbcDSKdryk//DkajcfXO5Tf7VNeAlmq0qBL" +
                "aFhRsQqzhlvBISGOhRn+9vl+qwuH+fFAmNylo+smOXPL/WAk3dBXpWFXBIdKmElTNZ6O0bsf/zEe7fAb" +
                "iJ+e0/P0Nynmd5AO15Rr4pd2ef11jjkpibaaMLtgDVpllGDKTQLN2tiFrIw+doC187pKGYifX8B5nfWs" +
                "i6kIt+brktcpPBre3m4reSB+OZVgTrit6CDDU9RFTp5na5+0nRlf873GN0iXhtSamQnpvUPs2uT9NzjE" +
                "aTKzKfbKrw3AN8cRT9x+fpjuQg3ErwlwaDdirC8QIAmNrDEItSLITgJGuWwHgQCDVzrplp9Qe4GxHavN" +
                "ki4Njo/KQaz91pn1h1Xllmkk4YUoBXxw2/sKZNZ3FdeY2BmxeIumvCkKlnG9KNJTzF70Nptc85DFJmgH" +
                "kbVOIXLG+UjpZoaqy9Jgwki38k5XSQYhzRPRJA0wacY6IBX2k2UL4aAUWCMMOlTPka6qwm7GDG3+loTQ" +
                "HfTGfXAlee4qidHuwLDmjwazHjLQjUFvtZ+IGZHOpfqHDYkd7SCLoTIEWXCGkZ0wJ2VmRm3qITEIbCBG" +
                "54mvXQBSdZPqAq3OYNXlJn9Y9QLZe/NsLu9l7YSJhD22J8ty57gGHoPhtoZBjrPQy/4Ddv0YCwIMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
