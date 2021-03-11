/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapResult")]
    public sealed class GetMapResult : IDeserializable<GetMapResult>, IResult<GetMapActionResult>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapResult()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapResult(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapResult(ref Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapResult(ref b);
        }
        
        GetMapResult IDeserializable<GetMapResult>.RosDeserialize(ref Buffer b)
        {
            return new GetMapResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/bOBC9E8h/GCCHJgvbcdtFUQToYYGg2R6CzaLpyQiMMTWWuJVIlaTian99HylL" +
                "jrvBooemhmCJo/l6bz5k+WHdhDJc/KV117LV/bU3BTXcnqh3P/l3om4+Xl+SfTLkiTqlu8oE8tJ6CWJj" +
                "IKZX8ysq9wnNyFjaVUZXJIw/LXX9WDtWQq13G96Y2sSe3BYu3RhjodSfwoV4qoabUqc3EvmKI9PW+WyO" +
                "KOqG20lu7NapnFh+RwWEOQ3vdvOG/4GZ83A2oxDZR2NL2plY0dlytjxfEE0I4eOQmhFA85L8pKCebSm0" +
                "Ws5eLpf3MPpkP1u3swQu5i8Xytj4dnWfQ6tnq0mIxVCTgaNUjI+RbcEe1IONYmSpMmUlfl7Lg9QJdNNK" +
                "kXOj2LcSFmMVcZVixXNd99QFKEVH2jVNZ43mKBRNI0f2sAQhTG0iUnc1e+iDXWOT+tZzI8k7riBfOrFa" +
                "6MPVJXRsEN1Fg4R6eNBeOKRCfLgi1YG816+SgTq927k5jlKi9lNwFIBjSla+pj5KeXK4RIzfBnAL+AY7" +
                "gihFoLMsW+MYzglBkIK0Dq14hsxv+1i5oaIP7A1valQ4kAYD8PoiGb04f+Q5pZ2GwbrR/eDxEONH3NrJ" +
                "b8I0r1CzOqEPXQkCoYi2ezAFVDd9dqJrg2mh2mw8+14lqyGkOn2fOIYSrHJFcOcQnDYoQJEbW4Xok/dc" +
                "jbUp1PMviUfjOK2IytXAgzLrPKG+4WjAEW9cFweQFXvWUbwJWAxum4VH62ac6owfLTDslf0OoB0Hqh0m" +
                "oRgIgmydzut0erQP0DKu7nLwVXORFtK92kIxNd3hHQyAAgQWWA2rpBXux9bMwr1CJRiu+L3GIN0Hdd6U" +
                "aIo9opTCqpkRLs9F2h3j7OW9IlzPd86DqxY9tjeCo7w484oaVxAcLVQpDqPu+4H322ySwz1bkf8bMZX4" +
                "j8NSH+qKxDMAJLv1gnZtWcssbZMkLvbvzdADFmdvRtsFqVsHHicF9XeHbvY2+z3oqV+GEclMfYyxj2zs" +
                "/tM1QgAcbMGc9RFilTvrze/0dXrqp6d/fxWCA39PfrGPWD3OP52+HNhPc4uP8v+DGp92gPcNppr+O6sI" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
