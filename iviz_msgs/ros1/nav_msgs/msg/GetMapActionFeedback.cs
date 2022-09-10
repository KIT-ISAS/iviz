/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionFeedback : IHasSerializer<GetMapActionFeedback>, IMessage, IActionFeedback<GetMapFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public GetMapFeedback Feedback { get; set; }
    
        public GetMapActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = GetMapFeedback.Singleton;
        }
        
        public GetMapActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public GetMapActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        public GetMapActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        public GetMapActionFeedback RosDeserialize(ref ReadBuffer b) => new GetMapActionFeedback(ref b);
        
        public GetMapActionFeedback RosDeserialize(ref ReadBuffer2 b) => new GetMapActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
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
            size = Feedback.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "nav_msgs/GetMapActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVptkn6kntFBlRRHHTvx2GovnY4HJFYkWhBUAVCy/n3fghRF" +
                "OVajQxKNbH0Bbx/evl3sO5KKnCjiSyKzoCtrdPpQ+tx/d1VJcx9kqL3w8SW5onAjV2+JVCqzf8SyfZOM" +
                "PvMjubm/ukRQ1RB519AbCLCxSjolSgpSySDFsgJ7nRfkLgytyTDTckVKxF/DdkV+iI2LQnuBZ06WnDRm" +
                "K2qPRaESWVWWtdWZDCSCLulgP3ZqK6RYSRd0VhvpsL5ySltevnSyJEbH09O/NdmMxHx6iTXWU1YHDUJb" +
                "IGSOpNc2x48iqbUNr1/xBjEQf95V/uVfyWCxqS7wPeVIRsdChEIGZk2PK0eeCUt/iWDfNKccIghUIoRT" +
                "XpzF7x7w0Z8LRAMXWlVZIc5whNttKCoLQBJr6bRMDTFwBimA+oI3vTjvIdsIbaWtdvAN4j7GKbC2w+Uz" +
                "XRRInmEZfJ1DSSxcuWqtFZam2wiSGU02CDjQSbdNeFcTMhm8ZbGxCLtiavAqva8yjUwosdGhSHxwjB7T" +
                "8qBV8oVsebRKEn6LFOd44fic6Te70mk+3M7eT+fvr8TuMRLf4z/7k+I2UUgvthTYmSmxPlmT+FagJjZy" +
                "7tYoiAZzPFnM/5iJHubLQ0zOSO0clIUbU2KNTgK+vZvNbm4Xs2kH/OoQ2FFG8DhsiZTDHvwNysAHIZcB" +
                "TtaBT+84QfQYC8LmififxwB/MElUoTEcynNliBF08DsUED1bkCtRhoZ7QqDzlvL975PJbDbtUX59SHkD" +
                "ZJkVmpi2rzNWYVlzQ3hOiGNhxr9+uNvrwmF+eCZMWsWjqzracs/92Uiqpk9Kw67wFcpgKbWpHR2jdzf7" +
                "bTbp8RuJHz+m5+hvysIRB8SCqurw1C7ffppjSplEc42YXbAaDTNIMOUOgZat7VoarY4doHVeVykj8dNX" +
                "cF5nPVuFWIR783XJ6xSejK+v95U8Ej+fSjAl3Fn0LMNT1EVOPs7WIWm71K7k242vj9DvApEJqYND9G3y" +
                "5jMc4jSZ2RQH5dcE4GvjiCeuP9wv+lAj8UsEHNudGO3tASShkDUGoUYE2UnAKMNmHPAwuFFRt/SE2vOM" +
                "XbHaLOlG4/ioHGmftM5kMDam2sTBhBeiFBzXbXdZgUx7UXGNid6QxVsUpXWes4ztokCPIfmKV9l8mjQO" +
                "aEaQViQfON18nngnQ9JNoTFbxPu411KiO0jxUDSPo0vd3jFPdcJ+suwfnJI8C4QRh8oVcmUMdjOmb5K3" +
                "IYTuoHfWgyXJcUuJjPqjQssf3aUdL9CKQW97mIXd7MpuxA7MV7UJmCu9lzk1qfEryvRSZ7tiiAz8sEXn" +
                "oa9ZAFJlHYsCfU5j1XCXPB5CvlDqrFy3STuYyZPkP5sw3+vTCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GetMapActionFeedback> CreateSerializer() => new Serializer();
        public Deserializer<GetMapActionFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapActionFeedback>
        {
            public override void RosSerialize(GetMapActionFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GetMapActionFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GetMapActionFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GetMapActionFeedback msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(GetMapActionFeedback msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<GetMapActionFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GetMapActionFeedback msg) => msg = new GetMapActionFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GetMapActionFeedback msg) => msg = new GetMapActionFeedback(ref b);
        }
    }
}
