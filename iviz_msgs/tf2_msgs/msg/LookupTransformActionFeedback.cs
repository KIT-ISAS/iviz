/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionFeedback")]
    public sealed class LookupTransformActionFeedback : IDeserializable<LookupTransformActionFeedback>, IActionFeedback<LookupTransformFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public LookupTransformFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LookupTransformActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionFeedback(ref b);
        }
        
        LookupTransformActionFeedback IDeserializable<LookupTransformActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c0b/ATM5xO7Ebuu0TeoZHVRJcdRxEo+t9uoBiRWJhgRYAJSsf9+3IKmP" +
                "WJrqkERjWV/A24e3bxf7nqQiJ4r4ksgsaGtKnT5WPvc/3lhZPgQZGi98fElurf3c1HMnjV9YV70jUqnM" +
                "PotF92aQDL/yY5B8eLi5RnzVcnofmQ6SFwLMjJJOiYqCVDJIAUqi0HlB7qKkJZXMuqpJifhrWNfkL3nn" +
                "vNBe4C8nQ06W5Vo0HquCFZmtqsboTAYSQVe0B8BbtRFS1NIFnTWldNhgndKG1y+crCji89PTvw2ZjMRs" +
                "co1VxlPWBA1Sa2BkjqTXJsePWNxoE15f8Q5snK/sBT5TjqxsGIhQyMCM6al25Jms9Ncc5of2jJeAh0iE" +
                "QMqLs/jdIz76c4E4YEG1zQpxBvp361BYA0QSS+m0TEti5Aw6APYlb3p5vgvN1K+Fkcb2+C3kNsgpuGYL" +
                "zMe6KJC8kiXwTQ4dsbJ2dqkV1qbriJKVmkwQcKOTbj1IeFsbFCDvWGwsw76YG7xK722mkQklVjoUg8QH" +
                "xwFiXh61GiTfzJ1H62aQ8HtkOcdL5MDJftuVU//pbvpxMvt4I/rHUPyE/+xTihtFIb1YU2CHpsRCZa0J" +
                "OqXa8Ei/W3JptKCj8Xz291TsgP68D8rJaZyDxvBkSizVach399Pph7v5dLJBvtpHdpQRrA6TIv2wCn+D" +
                "avBByEWAr3VgARxnip5iXZh8kGypPn+8wBOGiUK07kOl1iUxhA6+hwHVszm5CgVZcn8IdN6TfvhrPJ5O" +
                "JzukX++TXgFaZoVG41AwZcZCLBpuDoe0OBpn9Men+600HOeXA3FSG0+vmujQLfuDoVRD/68Oe8Nb1MRC" +
                "6rJxdJTg/fTP6XiH4VD8+pygo38oY4YHCXF52SZ8aZpXJ7BMKZNothF0E61B/wwSXLlnoIdrs5SlVkeP" +
                "0BlwUzJD8dv3MODGgcaGWI5bD24yuFV5PLq93Rb1ULw5lWJKuMfoIMeTFEZinqdsn7ZZaFfxjcfXyiYV" +
                "sVszFVL7x9g1y9uvcIwTpWZr7BViG4Gvk2POuP30MN/FGorfI+LI9Hp0twqghELqGIVaHeRGBUa5bKcE" +
                "D6OXKkqXnlKFnsEtK86yrjQUQAkh2BedFFfYqCztKs4svBRFgTd2e4uBT3eBcbmJnVmMtyhKmzyPWnar" +
                "Aj0Fxv2el9xs0o5T3b3cq+UDZ55PFe9saLsqNMaPeF3v9JhoFFJxZprF+SbOYQcEAwAZ9hLOSp51whxE" +
                "VY2slSVvZ1Tf5nFFCL4B730If5LjJhM57U8T/SHQcrohxGP9SqL37SakH3LZnLwFk1hTBoyf3suck400" +
                "+ZoyvdBZXx2RhWczMXwcDNsVYFY1sUzQ/jSWQYUuk+2o8s3yGBZXbQaPDPMc+j9gvVcwFwwAAA==";
                
    }
}
