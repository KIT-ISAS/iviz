/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveItErrorCodes")]
    public sealed class MoveItErrorCodes : IDeserializable<MoveItErrorCodes>, IMessage
    {
        [DataMember (Name = "val")] public int Val { get; set; }
        // overall behavior
        public const int SUCCESS = 1;
        public const int FAILURE = 99999;
        public const int PLANNING_FAILED = -1;
        public const int INVALID_MOTION_PLAN = -2;
        public const int MOTION_PLAN_INVALIDATED_BY_ENVIRONMENT_CHANGE = -3;
        public const int CONTROL_FAILED = -4;
        public const int UNABLE_TO_AQUIRE_SENSOR_DATA = -5;
        public const int TIMED_OUT = -6;
        public const int PREEMPTED = -7;
        // planning & kinematics request errors
        public const int START_STATE_IN_COLLISION = -10;
        public const int START_STATE_VIOLATES_PATH_CONSTRAINTS = -11;
        public const int GOAL_IN_COLLISION = -12;
        public const int GOAL_VIOLATES_PATH_CONSTRAINTS = -13;
        public const int GOAL_CONSTRAINTS_VIOLATED = -14;
        public const int INVALID_GROUP_NAME = -15;
        public const int INVALID_GOAL_CONSTRAINTS = -16;
        public const int INVALID_ROBOT_STATE = -17;
        public const int INVALID_LINK_NAME = -18;
        public const int INVALID_OBJECT_NAME = -19;
        // system errors
        public const int FRAME_TRANSFORM_FAILURE = -21;
        public const int COLLISION_CHECKING_UNAVAILABLE = -22;
        public const int ROBOT_STATE_STALE = -23;
        public const int SENSOR_INFO_STALE = -24;
        // kinematics errors
        public const int NO_IK_SOLUTION = -31;
    
        /// <summary> Constructor for empty message. </summary>
        public MoveItErrorCodes()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveItErrorCodes(int Val)
        {
            this.Val = Val;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveItErrorCodes(ref Buffer b)
        {
            Val = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveItErrorCodes(ref b);
        }
        
        MoveItErrorCodes IDeserializable<MoveItErrorCodes>.RosDeserialize(ref Buffer b)
        {
            return new MoveItErrorCodes(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Val);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveItErrorCodes";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aa336b18d80531f66439810112c0a43e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE31STY/aMBC9I/EfIlXaW6TysbvdQw4mGNaLM0NtB6mnUVpFbbRsaBMWqf++Y8AB0qo5" +
                "ONK8N19vXlXvJ+PoUGyHg+HgQ7Q7lE2x3UZfyx/Fodo1w0F1JNg8TaW1ySgEFkLp3MjkyX8+9xReawGg" +
                "YEkel/Mk7hIUbIRWc8rQKQTyxCQeB/QqSmemcHJOsy8kYaMMQibBUfosYCmTeBLyUgRnUHftpgHIQcy0" +
                "JIckPufKSLISLBrisiKJ7wPNqYy7YO6S+KHbwUiZrZ0v93hS5ee2qOuq/h7dRa9VXb4V++pbGzXlr/ey" +
                "3Udl0+yatlPKCeOIXyd5E0pRa2V5N5bi4784G4Wa/5bWwj0zHawzQoGznDC6CLtEofv1xjfg/wpNbphX" +
                "WMjyl5peuoVjLQ3mawKRseaj+7/QXjHmPPQ5Bmd4XpXhxz6sFaxC/U99EGcvMnUBfjrdov3d7su3nugL" +
                "wxziMcAu0GQU3BmPRxennIVjE8l05T3KHtkw0RuFmZ2aVyP79wh2Ap5tpGCBHTg9TXZljdvpAEmtyKLO" +
                "3fFwEx5q8Ad7+QFJeQMAAA==";
                
    }
}
