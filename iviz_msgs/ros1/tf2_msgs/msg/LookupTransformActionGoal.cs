/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformActionGoal : IHasSerializer<LookupTransformActionGoal>, IMessage, IActionGoal<LookupTransformGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public LookupTransformGoal Goal { get; set; }
    
        public LookupTransformActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new LookupTransformGoal();
        }
        
        public LookupTransformActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, LookupTransformGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public LookupTransformActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        public LookupTransformActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        public LookupTransformActionGoal RosDeserialize(ref ReadBuffer b) => new LookupTransformActionGoal(ref b);
        
        public LookupTransformActionGoal RosDeserialize(ref ReadBuffer2 b) => new LookupTransformActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            GoalId.RosValidate();
            Goal.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = GoalId.AddRos2MessageLength(size);
            size = Goal.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformActionGoal";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f2e7bcdb75c847978d0351a13e699da5";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwWobMRC971cM+JCkEJemt0BvoUmghdLkVooZS+NdkV1pq5Hs7N/3Sbt2U8ihh8Ys" +
                "CEkzb97Me/KdsJVIXV0aNskF37vtZtBW398G7u9vqMWycbb5EsJTHh8je92FOJTbetd8+s+/5uvD7TVp" +
                "sjONu5ncih4Se8vR0iCJLScm8KDOtZ3Ey1720iOJh1Es1ds0jaJrJD52TglfK14i9/1EWRGUApkwDNk7" +
                "w0kouUH+ykem88Q0ckzO5J4j4kO0zpfwXeRBCjo+lV9ZvBG6v7lGjFcxOTkQmoBgorA63+KSmux8+nhV" +
                "EmhFP74H/fCzWT0ewiXOpYUUJxaUOk6FtTyPUbQQZr1GsXdzl2sUwZQE5azSeT3bYKsXhGrgImMwHZ2j" +
                "hW9T6oIHoNCeo+NtLwXYYBRAPStJZxcvkH2F9uzDEX5G/FPjX2D9Cbf0dNlBvL6MQXOLSSJwjGHvLEK3" +
                "UwUxvROfCP6LHKemZM0lm9XnMmwEIatKg5VVg3FQwtLBpa7RFAt6laXY9Y1s+eobqR5byJJ2IfcWmxCl" +
                "9lUbgZaHzkGQ2kR5N3RgpVico2iiOOm+6l29iZGwX4pB5LiHNQ6deHKJ0KhocS98IcOYCANHdsHU2TUH" +
                "QekTNG1lV7gwGYmJoVxh9HK+C39nj5pgvKA3lSKnOdNOxG7ZPIGZRQZMmfuEx6jKrVQRSEcxbufM3ODC" +
                "QNcLenkpcwBIDVkTmBGeH6LWR/2Kcm8kXdpdzaK98k/WHOsnjq2kTbXR8UxDjkaWs3ls80mdo82Rq0xl" +
                "F3IC/xqzINWYoznds9gFp2m2IfTEds94W+j6NxX+YlOMBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LookupTransformActionGoal> CreateSerializer() => new Serializer();
        public Deserializer<LookupTransformActionGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LookupTransformActionGoal>
        {
            public override void RosSerialize(LookupTransformActionGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(LookupTransformActionGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(LookupTransformActionGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(LookupTransformActionGoal msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(LookupTransformActionGoal msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<LookupTransformActionGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out LookupTransformActionGoal msg) => msg = new LookupTransformActionGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out LookupTransformActionGoal msg) => msg = new LookupTransformActionGoal(ref b);
        }
    }
}
