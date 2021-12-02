/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapActionFeedback : IDeserializable<GetMapActionFeedback>, IActionFeedback<GetMapFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public GetMapFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public GetMapActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = GetMapFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public GetMapActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal GetMapActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapActionFeedback(ref b);
        
        GetMapActionFeedback IDeserializable<GetMapActionFeedback>.RosDeserialize(ref Buffer b) => new GetMapActionFeedback(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Wy3IbNxC871egSgdLqUhJ7DwcVfHAkLRMl2SrJCZXFXYx3EWCxW4ALCn+fXqwD5IR" +
                "WebBNosSX0BPo6dnMO9JKnKiiC+JzIKurNHpU+lz/8NNJc1jkKHxwseX5IbCnazfEalUZv+IZfcmGX3h" +
                "R3L3eHONoKol8r6ldybAxirplCgpSCWDFMsK7HVekLs0tCLDTMualIi/hk1N/gobF4X2As+cLDlpzEY0" +
                "HotCJbKqLBurMxlIBF3S3n7s1FZIUUsXdNYY6bC+ckpbXr50siRGx9PTvw3ZjMR8eo011lPWBA1CGyBk" +
                "jqTXNsePImm0DW9e84bkbLGuLvGRcuRgCC5CIQOTpefakWee0l8jxnft4a6ADXEIUZQX5/G7J3z0FwJB" +
                "QIHqKivEOZjfb0JRWQCSWEmnZWqIgTMoANRXvOnVxQ4y074WVtqqh28RtzFOgbUDLp/pskDODJ/eNzkE" +
                "xMLaVSutsDTdRJDMaLJBwHhOuk3Cu9qQydk71hiLsCtmBK/S+yrTSIASax2KxAfH6DEbT1olX8mNR4sj" +
                "4bfIbI4Xjs8JfttXTPvhfvZxOv94I/rHSPyI/2xLittEIb3YUGBDpsT6ZG3iO4Ha2Mi5W6EOWszxZDH/" +
                "ayZ2MH/ax+SMNM5BWZgwJdboJOD7h9ns7n4xmw7Ar/eBHWUEa8OWSDnswd/A/T4IuQxwsg58escJoudY" +
                "BzZPtkRfPs7wB5NEFVrDoSprQ4ygg+9RQPR8Qa5E9RluBYEuOsqPf04ms9l0h/KbfcprIMus0GgRCj7M" +
                "WIVlw33gkBDHwoz/+PSw1YXD/HwgTFrFo6sm2nLL/WAk1dBnpWFX+AplsJTaNI6O0XuYfZhNdviNxC8v" +
                "6Tn6mzLmd5AOF1TVhP/b5fvPc0wpk+ipEXMI1qBPBgmm3CHQqbVdSaPVsQN0zhsqZSR+/QbOG6xnqxCL" +
                "cGu+IXmDwpPx7e22kkfit1MJpoSrig4yPEVd5ORltvZJ26V2JV9qfH0MaYh9mZmQ2jvErk3efoFDnCYz" +
                "m2Kv/NoAfG0c8cTtp8fFLtRI/B4Bx7YXo7s9gCQUssYg1IogBwkY5aqdAjwMblTULT2h9jxjV6w2S7rW" +
                "OD4qB7H2W2dyNjamWsd5hBeiFPCm2l5WINNdVFxjYme24i2K0ibPWcZuUaDnkHzDq2w+jVNSd+/2IvnA" +
                "6ebzxDsZkq4Ljdki3sc7LSW6gxTPQvM4usTp6oBO2E+W/YNTkmeBMOJQWSNXxmA3Y/o2eWtC6AG6tx4s" +
                "SY5bSmS0OyokLX90l268QCsGPXS53Sz0Iyu7ETswXzUmYJz0XuacXqTG15Tppc76YogMPLuH0bGpWwBS" +
                "ZROLAn1OY9VVnzweQr5S6qxcdUnbG8WT5D8njnFfygsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
