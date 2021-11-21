/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestActionFeedback")]
    public sealed class TestRequestActionFeedback : IDeserializable<TestRequestActionFeedback>, IActionFeedback<TestRequestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestRequestFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TestRequestFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestRequestActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionFeedback(ref b);
        }
        
        TestRequestActionFeedback IDeserializable<TestRequestActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c8b/ATM6xO7UTpv0I/WMDqqkOMo4icdWe/WAxIpEC4IqAErWv+9bUKSo" +
                "WprokFpjm5IMvH14+3axH0gqcqKIj0RmQVfW6PSx9Ll/fVNJ8xBkqL3w8ZHMyYd7+qfG4z2RSmX2t1hs" +
                "35wlw2/8Oks+PdxcI7Zq+HyILM+SgQArq6RToqQglQxSLCqcQucFuUtDKzLMuFySEvG/YbMkf4WN80J7" +
                "gZ+cLDlpzEbUHotCJbKqLGurMxlIBF3S3n7s1FZIsZQu6Kw20mF95ZS2vHzhZEmMjh/P6tiMxGxyjTXW" +
                "U1YHDUIbIGSOpNc2xz9FUmsb3r7hDclgvq4u8ZFy5KILLkIhA5Olp6Ujzzylv0aM75rDXQEb6hCiKC/O" +
                "43eP+OgvBIKAAi2rrBDnYH63CUVlAUhiJZ2WqSEGzqAAUF/xplcXPWSmfS2stFUL3yDuYpwCaztcPtNl" +
                "gZwZPr2vcwiIhUtXrbTC0nQTQTKjyQYBAzrpNgnvakImg/esMRZhV8wIntL7KtNIgBJrHYrEB8foMRuP" +
                "WiX/myGPlslZwu+R3BwPpsA5ftcWT/Phbvp5Mvt8I9rXUPyAv+xMittEIb3YUGBPpsQSZU3utxo1wZF2" +
                "t0LFNpij8Xz251T0MH/cx+Sk1M5BXPgwJZbpJOC7++n00918OumA3+wDO8oI7oYzkXU4hL+J7UHIRYCZ" +
                "deDTO84RPcVSsHmyI/r8NcAvfBJVaDyHwlwaYgQdfIsCoudzciUK0HA3CHSxpfzwx3g8nU56lN/uU14D" +
                "WWaFRpdQsGLGKixqbgWHhDgWZvT7l/udLhzmpwNh0ioeXdXRmTvuByOpmr4qDbvCV6iEhdSmdnSM3v30" +
                "43Tc4zcUPz+n5+gvypjfQTpcU1Ud/muX77/OMaVMoq1GzC5YjVYZJJhyk0Cz1nYljVbHDrB1XlcpQ/HL" +
                "Czivs56tQizCnfm65HUKj0e3t7tKHopfTyWYEm4rOsjwFHWRk+fZ2idtF9qVfK/xDdKlIbZmZkJq7xB9" +
                "m7z7Boc4TWY2xV75NQH45jjiidsvD/M+1FD8FgFHthVje4EASShkjUGoEUF2EjDKVTMIeBjcqKhbekLt" +
                "ecauWG2WdK1xfFQOYu23zmQwMqZax5GEF6IU8Kba3Vcgs72ruMZEb8ziLYrSOs9Zxu2iQE8hedHbbDbh" +
                "IYtN0AwiW5184IzzkeLNDFXXhcaEEW/lXleJBiHFE9EsDjBxxjogFfaTZQvhoORZIww6VC6RLmOwmzF9" +
                "k781IXQH3boPriTHXSUy6g8MW/5atUMGujHoodH1E9HOrmxI7MCUVZuAodJ7mXOGkR2/pEwvdNbWQ2Tg" +
                "2UCMzhNfswCkyjrWBVqdxqqrNn9Y9QLZe31gNj9D3H8ByIilBOILAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
