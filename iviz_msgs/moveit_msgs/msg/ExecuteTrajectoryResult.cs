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
            return new ExecuteTrajectoryResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAAE61Sy47TMBTdW+o/REJiF0EfM8MgZeGmbsbUsYvtVGJ1FYZoiGhTSEIl/p7rSZy2mREr" +
                "snAsn3Nf5x5C0uOp4C2r62MdH78VTVC4KzzinZDoP38kNcnH4IAlyxYOzVPzblyelFU7nwWnfD8hE/Im" +
                "QLjO9/vga/E9P5XHetITTBbHzJho6h/WlItMs+jefS62e94KKiWXCTicraJwCOByRwVfQaosVxIcMQpn" +
                "Hr14hZ5JLVvB8gswueNayZRJC/EDlQmLwrmPi5W0Womh3MIDmaRLwcAqoJ8zrhkYJo3SgGlpFN54muUp" +
                "VlGZjcLbYQbNWLq1Lt1dp8rPfV5VZfUUvA1+lFVxyNvysQnq4tfvomm7DTaDUpZqC3hahpNArITgBmdD" +
                "Kd6/xtlxJfBvYEvtA9KlsZpyaQ0GTM/CJoqKcb7ZFfivRPMr5gXmo9ymFudqflmJVtkWJE1R8+nNC3SU" +
                "DDm3Y45WS9WPivDdGBZcbnz+D2NQLT+x2Hr4vttF86dpi8NI9LVGDmAb0qyVTsG7M5xNz07phUMTsXjj" +
                "PIoe2SHRGQWZg5oXLbvzGRwE7G3E5VoN4KLr7MIa191JBXwDRonMPi9ujk2Rv3XM8A4LBAAA";
                
    }
}
