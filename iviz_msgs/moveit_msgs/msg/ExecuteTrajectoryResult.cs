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
                "H4sIAAAAAAAACq1TTY/TMBC9W+p/iFSJWwRp9wOQfHBTN2vq2MV2KnEahSVaIvoBSajEv2e82FU2Qpzw" +
                "wXH8nmfGb54JKc+XRgy8685dfv7S9Enjl/CIazIj9D+PGSlt8T45YtJ2gGP/1L+eFjAj7WlYLpJLfSBk" +
                "niDa1YdD8rn5Wl/acxdQW+U5t5Zm4X/DhKwMp+/8IGFzJ5lSQhXgUb6maWQLtWdSrKHUTmgFnkfTRQBH" +
                "mxCIzPE1rD4BV3thtCq5cpA/MFVwmi7DsVwrZ7S85roJ+5ViK8nBaWAfK2E4WK6sNoBBGU1vA8uJElPo" +
                "ytH0LlZvOC93mJmm916J74f6dGpPT8mr5Ft7ao710D72Sdf8+Nn0w5+29VEdx4wDnB3HK0CupRQWL4UK" +
                "vPkLZS+0xK+FHXMPyFbWGSaUs8jPopiFZnIabDHG/hVlOSaOoHjI9+aGTLpTGF3tQLESVc5up+AkElLu" +
                "JhSjVzpckabZ/QSVQm1j8LcTTK8+8NxFFP00T/pf/dAcX8q8MUgALEDZjTblc+u9CdNFNNpVLLQLz7fe" +
                "i+iHPfK8KZAYFRzV6udnLIoWDCPURl8xFGs+tsGLupQGsQWrZeWdjBbN8Cn/BlLTgTvrAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
