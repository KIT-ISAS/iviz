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
                "H4sIAAAAAAAACr1X227bOBB911cQCLBtF+tsk/QaQA+KoyTa2lJWUgLsk0BLtM2tLLkkldR/v2eoS2w3" +
                "2eahrZFatjk8MzxzZji9ErwQii3tw+G5kXVVylm20gv952XNy8Rw02im7cPxv4q8MSJV/F+Rm1ptYqGb" +
                "0jBlH477g1/ONLk8he+ijeeqjfKAIaiq4KpgK2F4wQ1n8xqHkIulUKNS3ImSAl6tRcHsqtmshT7ExnQp" +
                "NcPfQlRC8bLcsEbDyNQsr1erppI5N4IZuRI7+7FTVoyzNVdG5k3JFexrVciKzOeKrwSh40+LL42ocsGC" +
                "81PYVJr4kghoA4RcCa5ltcAicxpZmZNj2uAcpPf1CF/FAqkYnDOz5IaCFV/X4Jfi5PoUPn5vD3cIbJAj" +
                "4KXQ7KX9LcNX/YrBCUIQ6zpfspeI/HpjlnUFQMHuuJJ8VgoCzsEAUF/QphevtpAp7FNW8aru4VvEBx/P" +
                "ga0GXDrTaImclXR63SxAIAzXqr6TBUxnGwuSl1JUhkF/iquNQ7tal87BBXEMI+yyGcGTa13nEgko2L00" +
                "S0cbReg2G5ksnJ+kxidrxKGPyOwCD/JPCf7QF0775doPz4PwkvUvl73GO8lS2G1syTXbCEOCnAniJ28T" +
                "3xHU+kbO1R3qoMX0xmlw67MtzKNdTMpIoxSYhQhngjh6FvB17PvT69Q/H4CPd4GVyAWkDVki5ZAH/QL1" +
                "a8P43EDJ0tDpFSVI2L4B185DoN++DvAPIrEstIJDVa5LQQjS6B4Fgb5MhVqh+kpqBUa86kJObsZj3z/f" +
                "CvlkN+R7IPN8KdEiCugwJxbmDfWBx4h4yo13FsUPvJCbN4+4mdX26EVjZfkQ+6OeikZ8lxpSha5RBnMu" +
                "y0aJp8KL/b/88VZ8Lnv7bXhKUAd/QgG2oOrG7Mvlj+/HOBM5R0+1mIOzBn3ScERKHQKdWlZ3vJTFUwfo" +
                "lDdUisve/QLlDdKramOL8EF8Q/IGhsfeZPJQyS57/9wAZwJXlXg0wuewi5x8m63doKu5VCu61Oj6GNJg" +
                "+zJFIoqdQ2zL5MMPOMTzaCZR7JRf64CujSc0MYmSdBvKZR8toFf1ZHS3B5BYgawRiGhJ4AMFhHLYTgEa" +
                "Ai8Ly9vsGbWnCbsmtonSe4njo3Lga7d1OgdeWdb3dh4hQ5QCPtQPlxWC6S4qqjG2NWLRlkLMmsWCaOyM" +
                "jPhqnF94lQXndkrq7t2eJI1Rry1peyeD0vulxGxh7+OtlmLVIQqahQI7utjp6hGesF9UpB+cUmgiCCOO" +
                "WK2Rq7LEbsLUbfLuBVwP0L30IEmhqKXYiLZHhS5+dJduvEArRnib3SzMhShmPP9MasSOdn7FOKk1X4g2" +
                "NXotcjmXeV8MNgJ92KHTrNcaIKhVY4sCfU7C6rBPHg0hPyl1K0hRmjZvTwzl8D2FVWB8pWo1rokJQR+z" +
                "HJ9/RWD77p125EXvpyxRMSHb4G3J72StulV7hyeJe9R9v/CCyU3sux/p5XQ/Xk+8MET/zWjVP3dHvXUQ" +
                "3nqT4DybRmkQhRnZuaPjbnHrx6wz9HBPZmf/ZH54G8RROPXDNBtfeeGl745Oum3jKEzjaDL4etP9fhN6" +
                "ZxM/S6PM+/smiP0s8cMkijOAeu7obWeVBlO4iG5Sd/Suj76frNzRe2JiXfKqIsH8xj6jaa04/pMxlFOb" +
                "s567JPXiNMN76uMI2TjCNZTgUGDg9SMmt0E0wTPJrr30CtZhksZeEKYJ7I96Mi8jb7IPdry99n8oJ9uG" +
                "W0v9JsrNG2cvO5dxdHOdhd4ULB+93V/cQ4LJuz2TODqLuiO6o6P3e6u4mD/14B/21qIzmo36VegJZbxB" +
                "Y1nt0nwRwyBDAGFyEcVTm3oS4ei4F9pAFuTijz+RFqGHW9iRKGDYM7gVK73btZ60TjBBeBENayDrYFsG" +
                "O3GFURZ8ypJockNKhkSRxP8AVCYdj8MPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
