/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryResult")]
    public sealed class ExecuteTrajectoryResult : IDeserializable<ExecuteTrajectoryResult>, IResult<ExecuteTrajectoryActionResult>
    {
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryResult()
        {
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryResult(MoveItErrorCodes ErrorCode)
        {
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryResult(ref b);
        }
        
        ExecuteTrajectoryResult IDeserializable<ExecuteTrajectoryResult>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1f7ab918f5d0c5312f25263d3d688122";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Sy47TMBTd+ysiIbGLoI+ZYZCycFM3Y+r4FtupxOoqDNEQ0QckoRJ/z/VgV5kIscIL" +
                "x/E5vo9zD2Pl+dLIQXTducvPX5o+afwRH+nMWPafFytt8T45Usp2wGP/1L+ZpmftaVjMk0t9YOxVQmBX" +
                "Hw7J5+ZrfWnPXUBtlefC2mwW/jdcqsqI7N4vFi53imstdYEeFessjWyp91zJNZbgJGj0vCydB3B0iYHI" +
                "nVjj6hMKvZcGdCm0w/yB60Jk6SI8y0E7A+qaaxnuK81XSqAD5B8raQRaoS0YpKA8S28Cy8mSUkDlsvQ2" +
                "Vm+EKHfOx7rzSnw/1KdTe3pKXiff2lNzrIf2sU+65sfPph/+zCxqZx03Dml3glrAHJSSlpoiBd7+hbKX" +
                "oOhrccfdA7G1dYZL7SzxZ1HMAriaBpuPsX9FWYyJIyg+8rNZssl0CgPVDjUvSeXZzRScRCLK7YRiYAWh" +
                "RULvJqiSehuDv5tgsPogchfRe69+/6sfmuNLmTeGCEgFaLsBU2I0YTqfXU0RxCK7iHzrvUh+2BPPm4KI" +
                "UcFRrX5/xqJowTBSb+CKLX1NIxu8qEsDyi1aUJV7ntOChvgbNkfH6+cDAAA=";
                
    }
}
