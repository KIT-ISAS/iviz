/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsActionResult : IDeserializable<TwoIntsActionResult>, IActionResult<TwoIntsResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TwoIntsResult Result { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TwoIntsResult();
        }
        
        /// Explicit constructor.
        public TwoIntsActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal TwoIntsActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwoIntsActionResult(ref b);
        
        TwoIntsActionResult IDeserializable<TwoIntsActionResult>.RosDeserialize(ref Buffer b) => new TwoIntsActionResult(ref b);
    
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
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3ba7dea8b8cddcae4528ade4ef74b6e7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwXLbNhC98ysw40PsTu20SZqmntFBlVRHGSfx2GqvHhBYkWhBUAVAyfr7vgVFSqql" +
                "iQ5JNLIp2cDbh7dvF/uepCYvyvTIpIqmdtbkj1UowsubWtqHKGMTREiPbLaqpy6GewqNjcKnRzb4yq/s" +
                "48PNNSLqlsX7ltuZABWnpdeioii1jFLMa1A3RUn+0tKSLNOsFqRF+m9cLyhcYeOsNEHgXZAjL61diyZg" +
                "UayFqquqcUbJSCKaivb2Y6dxQoqF9NGoxkqP9bXXxvHyuZcVMTregf5tyCkS0/E11rhAqokGhNZAUJ5k" +
                "MK7AP0XWGBdfv+IN2RnEvMRXKpCAPriIpYxMlp4W0Jd5ynCNGD+0h7sCNsQhRNFBnKe/PeJruBAIAgq0" +
                "qFUpzsH8bh3L2gGQxFJ6I3NLDKygAFBf8KYXFzvITPtaOOnqDr5F3MY4Bdb1uHymyxI5s3z60BQQEAsX" +
                "vl4ajaX5OoEoa8hFAdd56dcZ72pDZmd/sMZYhF0pI3jKEGplkAAtViaWWYie0VM2Ho3OvpEbj1ZGxh+R" +
                "2QIPjs8JfteVS/vlbvJpPP10I7rXQPyE32xLSttEKYNYU2RD5sT6qDbxG4Ha2Mi5X6IOWszhaDb9ayJ2" +
                "MH/ex+SMNN5DWZgwJ9boJOC7+8nk491sMu6BX+0De1IEa8OWSDnswX+B+0MUch7hZBP59J4TRE+pDlyR" +
                "bYk+f53hByZJKrSGQ1UuLDGCiaFDAdHzGfkK1We5FUS62FB++HM0mkzGO5Rf71NeAVmq0qBFaPhQsQrz" +
                "hvvAISGOhRn+/vl+qwuHeXMgTF6no+sm2XLL/WAk3dAXpWFXhBplMJfGNp6O0buffJiMdvgNxC/P6Xn6" +
                "mxTzO0iHC6pu4v/t8uOXOeakJHpqwuyDNeiTUYIpdwh0auOW0hp97AAb5/WVMhBvv4Pzeuu5OqYi3Jqv" +
                "T16v8Gh4e7ut5IH49VSCOeGqooMMT1EXOXmerX3Sbm58xZcaXx99GlJfZiak9w6xa5N3X+EQp8nMptgr" +
                "vzYAXxtHPHH7+WG2CzUQvyXAoevE2NweQBIaWWMQakWQvQSMctVOAQEGtzrplp9Qe4Gxa1abJV0ZHB+V" +
                "g1j7rTM7G1pbr9I8wgtRCvhQby8rkNlcVFxjYmew4i2a8qYoWMbNokhPMfuOV9l0nKakzb3biRQip5vP" +
                "k+5kSLoqDWaLdB/vtJTkDtI8C03T6JKmqwM6YT859g9OSYEFwohD1QK5sha7GTO0yVsRQvfQnfVgSfLc" +
                "UhKj3VFhw9/obrxAKwY9dLndLMyJdC7VP+xG7GjnV4yTIciC04vUhAUpMzeqK4bEILB7GJ1nvXYBSFVN" +
                "Kgr0OYNVV13yeAj51ql7uTeIZ6iVt29QWlX2HwMXfOXQCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
