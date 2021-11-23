/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryActionResult : IDeserializable<ExecuteTrajectoryActionResult>, IActionResult<ExecuteTrajectoryResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ExecuteTrajectoryResult Result { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ExecuteTrajectoryResult();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ExecuteTrajectoryResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionResult(ref b);
        
        ExecuteTrajectoryActionResult IDeserializable<ExecuteTrajectoryActionResult>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8aaeab5c1cdb613e6a2913ebcc104c0d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X227bOBB911cQCLBtF+tsc+kV8IPiKIm2tpSVlAD7JNASbXMrS16SSuq/3zPUJbKb" +
                "tHloa6SWbQ7PDM+cGU6vBM+FYiv7cHhmZFUWcp6u9VL/eVnxIjbc1Jpp+3C8LyKrjUgU/1dkplLbSOi6" +
                "MEzZhzP+wS9nFl9+hO+8ieeqifKAIagy5ypna2F4zg1niwqHkMuVUKNC3ImCAl5vRM7sqtluhD7ExmQl" +
                "NcPfUpRC8aLYslrDyFQsq9brupQZN4IZuRY7+7FTloyzDVdGZnXBFewrlcuSzBeKrwWh40+L/2pRZoL5" +
                "5x9hU2riSyKgLRAyJbiW5RKLzKllaU6OaYNzkNxXI3wVS6Sid87MihsKVnzZgF+Kk+uP8PF7c7hDYIMc" +
                "AS+5Zi/tbym+6lcMThCC2FTZir1E5Ndbs6pKAAp2x5Xk80IQcAYGgPqCNr14NUAuLXTJy6qDbxAffDwH" +
                "tuxx6UyjFXJW0Ol1vQSBMNyo6k7mMJ1vLUhWSFEaBv0prrYO7WpcOgcXxDGMsMtmBE+udZVJJCBn99Ks" +
                "HG0UodtspDJ3fpIan6wRhz4is0s8yD8l+H1XOM2Xay8494NL1r3G7DXeSZbCbmMrrtlWGBLkXBA/WZP4" +
                "lqDGN3Ku7lAHDaY7Sfxbjw0wj3YxKSO1UmAWIpwL4uhZwNeR582uE++8Bz7eBVYiE5A2ZImUQx70C9Sv" +
                "DeMLAyVLQ6dXlCBh+wZcO+wbrwP8g0gsC43gUJWbQhCCNLpDQaAvE6HWqL6CWoERr9qQ45vJxPPOByGf" +
                "7IZ8D2SeraSgsHWdEQuLmvrAY0Q85cY9C6MHXsjN6SNu5pU9el5bWT7E/qinvBbfpYZUoSuUwYLLolbi" +
                "qfAi7y9vMohvzN58HZ4S1MGfUIAtqKo2+3L54/sxzkXG0VMtZu+sRp80HJFSh0CnluUdL2T+1AFa5fWV" +
                "MmZvf4HyeumVlbFF+CC+Pnk9wxN3On2o5DF799wA5wJXlXg0wuewi5x8na3doMuFVGu61Oj6MMMuYCMR" +
                "+c4hhjJ5/wMO8TyaSRQ75dc4oGvjCU1MwzgZQo3ZBwvolh0Z7e0BJJYjawQiGhJ4TwGhHDZTgIbAi9zy" +
                "Nn9G7WnCrohtovRe4vioHF7utU7nwC2K6t7OI2SIUlBUt/1lhWDai4pqjA1GLNqSi3m9XBKNrZERX4zz" +
                "C68y/9xpFNCMIC1J2lC66Tz2Tgal9yuJ2cLex4OWYtUhcpqFfDu61O0ds88T9ouS9INTCk0EYcQR6w1y" +
                "VRTYTZi6Sd69gOseupMeJCkUtRQb0XBUaONHd2nHC7RihLfdzcJCiHzOs8+kRuxo5leMk1rzpWhSozci" +
                "kwuZdcVgI9CHLTrNeo0BglrXtijQ5ySsDrvk0RDyk1K3hhSlafL2xFAO3zNY+cZTqlKTipgQ9DHN8PlX" +
                "BLbv3mlGXvR+yhIVE7IN3lb8TlaqXbV3eByPj9rvF64/vYm88Qd6Oe2P11M3CNB/U1r1zsejztoPbt2p" +
                "f57OwsQPg5TsxqPjdnHwY9oaurgn07N/Ui+49aMwmHlBkk6u3ODSG49O2m2TMEiicNr7Om1/vwncs6mX" +
                "JmHq/n3jR14ae0EcRilA3fHoTWuV+DO4CG+S8ehtF303WY1H74iJTcHLkgTzG/uMprXm+E9GX05Nzjru" +
                "4sSNkhTviYcjpJMQ11CMQ4GB14+Y3PrhFM84vXaTK1gHcRK5fpDEsD/qyLwM3ek+2PFw7VsoJ0PDwVK3" +
                "iXJz6uxl5zIKb67TwJ2B5aM3+4t7SDB5u2cShWdhe0SsvttbxcX8qQN/v7cWntFs1K1+IPb1Fo1lvUvz" +
                "RQSDFAEE8UUYzdJOhKPjo14ULVmQizf5RFqEHm5hR6KAYcfgIFZ6t2sdaa1g/OAi7NdOKaaBDHbiCsLU" +
                "/5TG4fQmsXk6QRL/B1QmHY/DDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
