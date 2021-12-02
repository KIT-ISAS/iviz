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
        internal ExecuteTrajectoryResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryResult(ref b);
        
        ExecuteTrajectoryResult IDeserializable<ExecuteTrajectoryResult>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryResult(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACq1Sy47bIBTd8xWWInVntUnm0VZiQRziocGQARypqyt3as1Ykzit7Ubq3/cyA5HHqroq" +
                "C4w5h/s49xBSnM61GHjXnbrs9L3uk9of4QHPhND/vEhh88/JEVM2Axz7x/79ND1p2mG5SM7VgZBZgmBX" +
                "HQ7Jt/qpOjenLqC2zDJuLZ2H/w0TsjScfvKLhMudZEoJlYNH+ZqmkS3UnkmxhkI7oRV4Hk0XARxdQiAy" +
                "x9ew+gpc7YXRquDKQXbHVM5pugzPMq2c0fKS6yrcl4qtJAengd2XwnCwXFltAIMyml4HlhMFptClo+lN" +
                "rN5wXuwwM01vvRI/DlXbNu1j8i55btr6WA3NQ5909c9fdT+8zixqZx0zDnB3HFuATEspLDaFCnz4C2Uv" +
                "tMSvhR1zd8hW1hkmlLPIn0cxc83kNNhijP0rynJMHEHxkZ/NFZlMJze63IFiBao8v56Ck0hIuZlQjF7p" +
                "0CJN57cTVAq1jcE/TjC9+sIzF1H00yzpf/dDfXwr88YgAbAAZTfaFC+j9yZMF9FoF7HQLjzbei+iH/bI" +
                "86ZAYlRwVKvfX7AoWjCMUBt9wVCs2dgGb+pSGsQWrJaldzJaFIf4BzZHx+vnAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
