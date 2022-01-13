/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryResult : IDeserializable<ExecuteTrajectoryResult>, IResult<ExecuteTrajectoryActionResult>
    {
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public ExecuteTrajectoryResult()
        {
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryResult(MoveItErrorCodes ErrorCode)
        {
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        public ExecuteTrajectoryResult(ref ReadBuffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteTrajectoryResult(ref b);
        
        public ExecuteTrajectoryResult RosDeserialize(ref ReadBuffer b) => new ExecuteTrajectoryResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1f7ab918f5d0c5312f25263d3d688122";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Sy47TMBTd+ysiIbGLoI+ZYZCycFM3Y+r4FtupxOoqDNEQ0QckoRJ/z/VgV5kIscIL" +
                "x/E5vo9zD2Pl+dLIQXTducvPX5o+afwRH+nMWPafFytt8T45Usp2wGP/1L+ZpmftaVjMk0t9YOxVQmBX" +
                "Hw7J5+ZrfWnPXUBtlefC2mwW/jdcqsqI7N4vFi53imstdYEeFessjWyp91zJNZbgJGj0vCydB3B0iYHI" +
                "nVjj6hMKvZcGdCm0w/yB60Jk6SI8y0E7A+qaaxnuK81XSqAD5B8raQRaoS0YpKA8S28Cy8mSUkDlsvQ2" +
                "Vm+EKHfOx7rzSnw/1KdTe3pKXiff2lNzrIf2sU+65sfPph/+zCxqZx03Dml3glrAHJSSlpoiBd7+hbKX" +
                "oOhrccfdA7G1dYZL7SzxZ1HMAriaBpuPsX9FWYyJIyg+8rNZssl0CgPVDjUvSeXZzRScRCLK7YRiYAWh" +
                "RULvJqiSehuDv5tgsPogchfRe69+/6sfmuNLmTeGCEgFaLsBU2I0YTqfXU0RxCK7iHzrvUh+2BPPm4KI" +
                "UcFRrX5/xqJowTBSb+CKLX1NIxu8qEsDyi1aUJV7ntOChvgbNkfH6+cDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
