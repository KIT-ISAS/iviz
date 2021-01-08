/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionResult")]
    public sealed class ShapeActionResult : IDeserializable<ShapeActionResult>, IActionResult<ShapeResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ShapeResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ShapeResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionResult(ref b);
        }
        
        ShapeActionResult IDeserializable<ShapeActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXttEk/UgM6qLLqKHASw1Z7NbjL0S5bLrkluZb17/uGq11J" +
                "sYzqkESwvZZEvnl882Y470gq8qJKj0wWUTtrdH5fhzK8unLS3EUZ2yBCemR3lWzolkJrovDpMcrGX/g1" +
                "yj7cXV0goupYvEvcRtkLAS5WSa9ETVEqGaVYOnDXZUX+zNADGeZZN6RE+jauGwrnvHNR6SDwU5IlL41Z" +
                "izZgVXSicHXdWl3ISCLqmvYAeKu2QopG+qiL1kiPDc4rbXn90suaEj7/Bvq3JVuQmF9eYJUNVLRRg9Qa" +
                "GIUnGbQt8SUWt9rGN695BzYuVu4M76lEHgYGIlYyMmN6bCA0k5XhgsN8153xHPAQiRBIBXGSPrvH23Aq" +
                "EAcsqHFFJU5A/2YdK2eBSOJBei1zQ4xcQAfAvuRNL093oZn6hbDSuh6/g9wGOQbXboH5WGcVkmdYgtCW" +
                "0BErG+8etMLafJ1QCqPJRgH/eenXo4y3dUEB8geLjWXYl3KDpwzBFRqZUGKlYzXKQvQcIOXlXqtR9tXc" +
                "+WyljDL+H1ku8UgcONlvNwXUv7uZfbycf7wS/WssfsBf9imljaKSQawpskNzYqGKzgQbpbrwSL9/4NLo" +
                "QCfTxfyvmdgB/XEflJPTeg+N4cmcWKrjkG9uZ7MPN4vZ5YD8eh/ZU0GwOkyK9MMq/AmqIUQhlxG+1pEF" +
                "8Jwpekx1YctRtqX69PUCvzBMEqJzHyq1McQQOoYeBlRPFuRrFKTh/hDptCd99+d0Optd7pB+s096BWhZ" +
                "VBqNQ8GUBQuxbLk5HNLi2TiT3z/dbqXhOD8diJO7dHrVJodu2R8MpVr6f3XYG8GhJpZSm9bTswRvZ+9n" +
                "0x2GY/HzU4Ke/qaCGR4kxOXl2vi5ab4/gmVOhUSzTaBDtBb9M0pw5Z6BHq7tgzRaPXuEjQGHkhmLX76F" +
                "AQcHWhdTOW49OGRwq/J0cn29Leqx+PVYijnhHqODHI9SGIl5mrJ92napfc03Hl8rQypSt2YqpPaPsWuW" +
                "t1/gGEdKzdbYK8QuAl8nzznj+tPdYhdrLH5LiBPb67G5VQAlFFLHKNTpIAcVGOW8mxICjG5Uki4/pgoD" +
                "gztWnGVdaSiAEkKwzzoprrCJMW6VZhZeiqLAP257i4HP5gLjchM70xdvUZS3ZZm03KyK9Ijx69tecvPL" +
                "bpza3Mu9WiFy5vlU6c6GtqtKY/xI1/VOj0lGIZVmpnmab9IcdkAwAJBlL+GsFFgnzEFUN8iaMbydUUOX" +
                "xxUh+ADe+xD+JM9NJnHanyb6Q6DlbIYQtGhwXO8nZEmkcln8w+bkLd3Ii/EzBFlyspGm0FChl7roqyOx" +
                "CGwmhk+DYbcCzOo2lQnan8YyqLDJZDeqfLU8xtZHQ/dDOl/tjPGjLFsaJ3kc5SHUa+fvpS0NDR/LxiGx" +
                "9Sj7D/+UnDwsDAAA";
                
    }
}
