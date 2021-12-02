/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingActionResult : IDeserializable<AveragingActionResult>, IActionResult<AveragingResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public AveragingResult Result { get; set; }
    
        /// Constructor for empty message.
        public AveragingActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        /// Explicit constructor.
        public AveragingActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal AveragingActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingActionResult(ref b);
        
        AveragingActionResult IDeserializable<AveragingActionResult>.RosDeserialize(ref Buffer b) => new AveragingActionResult(ref b);
    
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
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwXLbNhC98ysw40PsTuy0SZumntFBlVRHHSfx2GqvHpBYkWhBgAVAyfr7vgUlSoql" +
                "iQ5JNLZpScDbh7dvF/uepCIvqvTIZBG1s0bnj3Uow6sbJ81DlLENIqRHNlyQl6W25T2F1kTh0yMbfOVX" +
                "9uHh5hoxVcfjfcfuTICMVdIrUVOUSkYp5g7kdVmRvzS0IMNE64aUSN/GVUPhChtnlQ4CPyVZHMCYlWgD" +
                "FkUnClfXrdWFjCSirmlvP3ZqK6RopI+6aI30WO+80paXz72sidHxE+i/lmxBYjq+xhobqGijBqEVEApP" +
                "MkA0fCmyVtv45jVvyM5mS3eJt1QiBX1wESsZmSw9NdCXecpwjRg/dIe7AjbEIURRQZynzx7xNlwIBAEF" +
                "alxRiXMwv1vFylkAklhIr2VuiIELKADUF7zpxcUOMtO+FlZat4HvELcxToG1PS6f6bJCzgyfPrQlBMTC" +
                "xruFVliarxJIYTTZKOA7L/0q411dyOzsD9YYi7ArZQRPGYIrNBKgxFLHKgvRM3rKxqNW2Tdy49HayPhf" +
                "ZLbEg+Nzgt9tCqZ7czf5OJ5+vBGb10D8iL9sS0rbRCWDWFFkQ+bE+hRd4tcCdbGRc4/6W2MOR7Pp3xOx" +
                "g/nTPiZnpPUeysKEObFGJwHf3U8mH+5mk3EP/Hof2FNBsDZsiZTDHvwJ3B+ikPMIJ+vIp/ecIHpKdWDL" +
                "bEv0+esMvzBJUqEzHKqyMcQIOoYNCoiez8jXqD7DrSDSxZryw1+j0WQy3qH8Zp/yEsiyqDRahIIPC1Zh" +
                "3nIfOCTEsTDD3z/db3XhMD8fCJO7dHTVJltuuR+MpFr6ojTsiuBQBnOpTevpGL37yZ+T0Q6/gfjlOT1P" +
                "/1DB/A7S4YJybfzcLi+/zDGnQqKnJsw+WIs+GSWYcodAp9Z2IY1Wxw6wdl5fKQPx9js4r7eedTEV4dZ8" +
                "ffJ6hUfD29ttJQ/Er6cSzAlXFR1keIq6yMnzbO2TtnPta77U+Pro05D6MjMhtXeIXZu8+wqHOE1mNsVe" +
                "+XUB+No44onbTw+zXaiB+C0BDu1GjPXtASShkDUGoU4E2UvAKFfdFBBgcKOSbvkJtRcY27HaLOlS4/io" +
                "HMTab53Z2dAYt0zzCC9EKeAft72sQGZ9UXGNiZ3RircoytuS56rNbRbpKWbf8SqbjtOUtL53NyKFyOnm" +
                "86Q7GZIuK43ZIt3HOy0luYMUz0LTNLqk6eqATthPlv2DU1JggTDiUN0gV8ZgN2OGLnlLQugeemM9WJI8" +
                "t5TEaHdUWPNHd1mPF2jFoLfaz8KcSOWy+JfdiB3d/IpxMgRZcnqRmtBQoee62BRDYhDYPYzOs163AKTq" +
                "NhUF+pzGqqtN8ngI+eapiy2SoyHXq8+G8iybGyd5xuTR0mvnH6UtDfUfy8YhpXX2P3ysJuD8CwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
