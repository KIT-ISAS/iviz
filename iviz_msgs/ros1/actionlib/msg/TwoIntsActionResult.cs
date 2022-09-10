/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsActionResult : IHasSerializer<TwoIntsActionResult>, IMessage, IActionResult<TwoIntsResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TwoIntsResult Result { get; set; }
    
        public TwoIntsActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TwoIntsResult();
        }
        
        public TwoIntsActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public TwoIntsActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        public TwoIntsActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        public TwoIntsActionResult RosDeserialize(ref ReadBuffer b) => new TwoIntsActionResult(ref b);
        
        public TwoIntsActionResult RosDeserialize(ref ReadBuffer2 b) => new TwoIntsActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 8; // Result
            return size;
        }
    
        public const string MessageType = "actionlib/TwoIntsActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3ba7dea8b8cddcae4528ade4ef74b6e7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVppPpqmntFBlVVHHSfxyGovnY4HJFYkWhBUAVCy/n3fghRF" +
                "OVajQxKNbNoS8Pbh7dvFviOpyIkiPhKZBV1Zo9P70uf++XUlzV2QofbCx0ey2FQzG/ycfG2CcPGRjL7w" +
                "K3l/d32JiKph8a7hNhCgYpV0SpQUpJJBimUF6jovyF0YWpNhmuWKlIjfhu2K/BAbF4X2Au+cLDlpzFbU" +
                "HotCJbKqLGurMxlIBF3SwX7s1FZIsZIu6Kw20mF95ZS2vHzpZEmMjrenf2uyGYnZ1SXWWE9ZHTQIbYGQ" +
                "OZJe2xxfiqTWNrx6yRvEQPw5r/yLv5IBVL3A55QjEx0LEQoZmDU9rCA0E5b+EsG+a045RBCoRAinvDiL" +
                "n93jX38uEA1caFVlhTjDEW63oagsAEmspdMyNcTAGaQA6jPe9Oy8h2wjtJW22sE3iPsYp8DaDpfPdFEg" +
                "eYZl8HUOJbFw5aq1VliabiNIZjTZIGA/J9024V1NyGTwK4uNRdgVU4On9L7KNDKhxEaHIvHBMXpMy71W" +
                "yVey5dESSfhPpDjHg+Nzpt/u6qb553b64Wr24VrsXiPxA36zPyluE4X0YkuBnZkS65M1iW8FamIj526N" +
                "gmgwx5PF7I+p6GG+OMTkjNTOQVm4MSXW6CTg2/l0+v52Mb3qgF8eAjvKCB6HLZFy2IM/QRn4IOQywMk6" +
                "8OkdJ4geYkHYPBH/8xrgByaJKjSGQ3muDDGCDn6HAqJnC3IlytBwTwh03lK++30ymU6vepRfHVLeAFlm" +
                "hSam7euMVVjW3BCeEuJYmPEvH+d7XTjM6yfCpFU8uqqjLffcn4ykavqsNOwKX6EMllKb2tExevPpb9NJ" +
                "j99I/PgpPUd/UxaOOCAWVFWHx3b5/vMcU8okmmvE7ILVaJhBgil3CLRsbdfSaHXsAK3zukoZiTffwHmd" +
                "9WwVYhHuzdclr1N4Mr652VfySPx0KsGUcGfRkwxPURc5+TRbh6TtUruSbze+PkK/C0QmpA4O0bfJ2y9w" +
                "iNNkZlMclF8TgK+NI564+Xi36EONxM8RcGx3YrS3B5CEQtYYhBoRZCcBowybccDD4EZF3dITas8zdsVq" +
                "s6QbjeOjcqR91DqTwdiYahMHE16IUnBct91lBTLtRcU1JnoTFm9RlNZ5zjK2iwI9hOQbXmWzq6RxQDOC" +
                "tCL5wOnm88Q7GZJuCo3ZIt7HvZYS3UGKh6JZHF3q9o55rBP2k2X/4JTkWSCMOFSukCtjsJsxfZO8DSF0" +
                "B72zHixJjltKZNQfFVr+6C7teIFWDHrbwywsiVQqs3/YjdjRDLKYK72XOTWp8SvK9FJnu2KIDPywReeh" +
                "r1kAUmUdiwJ9TmPVcJc8HkK+duqeH0zkCWrlzWuUVpn8B4mJ4hXZCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwoIntsActionResult> CreateSerializer() => new Serializer();
        public Deserializer<TwoIntsActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwoIntsActionResult>
        {
            public override void RosSerialize(TwoIntsActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwoIntsActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwoIntsActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TwoIntsActionResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TwoIntsActionResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TwoIntsActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwoIntsActionResult msg) => msg = new TwoIntsActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwoIntsActionResult msg) => msg = new TwoIntsActionResult(ref b);
        }
    }
}
