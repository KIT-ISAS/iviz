/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class Odometry : IHasSerializer<Odometry>, IMessage
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose;
        [DataMember (Name = "twist")] public GeometryMsgs.TwistWithCovariance Twist;
    
        public Odometry()
        {
            ChildFrameId = "";
            Pose = new GeometryMsgs.PoseWithCovariance();
            Twist = new GeometryMsgs.TwistWithCovariance();
        }
        
        public Odometry(in StdMsgs.Header Header, string ChildFrameId, GeometryMsgs.PoseWithCovariance Pose, GeometryMsgs.TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Pose = Pose;
            this.Twist = Twist;
        }
        
        public Odometry(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            ChildFrameId = b.DeserializeString();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        public Odometry(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            ChildFrameId = b.DeserializeString();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        public Odometry RosDeserialize(ref ReadBuffer b) => new Odometry(ref b);
        
        public Odometry RosDeserialize(ref ReadBuffer2 b) => new Odometry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            Pose.RosValidate();
            Twist.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 684;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(ChildFrameId);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ChildFrameId);
            size = WriteBuffer2.Align8(size);
            size += 344; // Pose
            size += 336; // Twist
            return size;
        }
    
        public const string MessageType = "nav_msgs/Odometry";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+1WTW/bRhC981cMoEPsQmLRuPDBQA9Fg6Q+FEgTI2kbBMaKHJLbkLvM7lIS8+vzZvkh" +
                "WVabHFKdaggwuZzPN29mZ0F3lfbkuHXs2QRPyhD7oBsVmGxBilrrddDW4EtOG65tpkNP2lDhmMm3KuOU" +
                "KFnAEoswy7cgVhv2XpWQqWxX57QWac50oTkfZJgya12ujTgrnGqYSr1hQ+ueKlY5uzSe3us8HR2Erfbh" +
                "23iIXytd5/eTk+TX6HT0nfjgtCmPZUq2DQfX3ze+9N+/RMJvdah+sRvltDLZgMGR1J1EfSQWM0mSn77x" +
                "X/Lb6xc35EM+eB4yAnivA+qnXA7MgspVUFRYZKrLit2qZhQWSqppgVz8GvqW/YA6gMavZMNO1XVPnYdQ" +
                "sIC2aTqjM8EWjOEH+tBEBUAf5YLOulq5R6UQ6/h5/tixIHL77AYyxnPWBRSpFpZljpWXKtw+o6TTJlw9" +
                "FQVa0LtX1v/wPlncbe0K51yicHMUKK4KEjXvhNkSsPI3cPbdkGUKJ0CJ4S73dBHP7vHqLwneEAu3Nqvo" +
                "Aim87ENlBy7F2q1rFsMZoIDVJ6L05PLAsommjTJ2Mj9Y3Pv4GrNmtis5rSoUrxYYfFcCSQi2zm50DtGJ" +
                "ybVG/1Kt1065PhGtwWWyeB55Hxsmlgb/lffoY1Qipy1oOVF9Jvl/RMsv9c5Et8N5NM+U/byJMVMHBRcU" +
                "at+nSSLGhtaDkVd2u2rU3yD4bEnFGYaJJmBd767BtLkVMeyc3o0Txjo9i4O8ACSw80J6xFLoHecrtTuM" +
                "MYoKl29h36HbltHHga5yLNy72C2pX9KnJTk7OlBr2wX6g8Tio+M/Tx//FY8vk6K2Klz/+O7q+v1BMucr" +
                "HTL6+QS+j8u1lEkhx/n4fX+fHICdEmqIYs4Cye8dCOpMtLuXO1eCCGWiI9pYeOaHuk7xj7ejhPwg3akw" +
                "tJuf+vnp03nC30N3qqUe4HnUWnj7uMcdd0SD5vr3jKan7XlyO3GZTklO097/w6JyanBEc9Nt/P/oOE/5" +
                "vrpga2c/sHAU+4YGd3EJMi5AmR3KlHGtkA0Dm8obzoJ1VzSK7N9HufNkN3o9eZFt4rfj1VmqH1cVa7Dx" +
                "NKwwT5DsrAnFXDuoxhkJmjlGV2Ko6kC5BXLGCpyN+gCTjL1BtFXbwhi2OKeMrwcKRATpgtMyXdK2AqpR" +
                "Su79uK7FBU9n5HSp80FT2n9WVjQmB5IWT9FKdT3EPDgDcWFkItxlSrcF9bajrSSEBzfulVbW9CmuuPYE" +
                "a5exSQYTJwbxvOhjCAdstF8cSZ8B99pLQ9gMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Odometry> CreateSerializer() => new Serializer();
        public Deserializer<Odometry> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Odometry>
        {
            public override void RosSerialize(Odometry msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Odometry msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Odometry msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Odometry msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Odometry msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Odometry>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Odometry msg) => msg = new Odometry(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Odometry msg) => msg = new Odometry(ref b);
        }
    }
}
