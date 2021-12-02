/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeActionResult : IDeserializable<ShapeActionResult>, IActionResult<ShapeResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ShapeResult Result { get; set; }
    
        /// Constructor for empty message.
        public ShapeActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ShapeResult();
        }
        
        /// Explicit constructor.
        public ShapeActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal ShapeActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeActionResult(ref b);
        
        ShapeActionResult IDeserializable<ShapeActionResult>.RosDeserialize(ref Buffer b) => new ShapeActionResult(ref b);
    
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
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwXLbNhC98ysw40PsTu20SZukntFBlVTHGSfxWGqvGpBYkWhBgAVAy/r7vgUlSqql" +
                "iQ5JNLZpScDbh7dvF/uepCIvqvTIZBG1s0bn8zqU4eWNk2YaZWyDCOmRTSvZ0AOF1kTh0yMbfOVX9nF6" +
                "c414quPwvmN2JkDEKumVqClKJaMUCwfiuqzIXxp6JMMk64aUSN/GVUPhChtnlQ4CPyVZ8tKYlWgDFkUn" +
                "ClfXrdWFjCSirmlvP3ZqK6RopI+6aI30WO+80paXL7ysidHxE+jflmxB4nZ8jTU2UNFGDUIrIBSeZNC2" +
                "xJcia7WNr1/xhuxstnSXeEsl5O+Di1jJyGTpqYG+zFOGa8T4oTvcFbAhDiGKCuI8fTbH23AhEAQUqHFF" +
                "Jc7B/H4VK2cBSOJRei1zQwxcQAGgvuBNLy52kJn2tbDSug18h7iNcQqs7XH5TJcVcmb49KEtISAWNt49" +
                "aoWl+SqBFEaTjQKe89KvMt7VhczO/mCNsQi7UkbwlCG4QiMBSix1rLIQPaOnbMy1yr6RG4/WRcb/IrMl" +
                "HhyfE/xuUyzdm/vJp/HtpxuxeQ3ET/jLtqS0TVQyiBVFNmROrE/RJX4tUBcbOfePqIMOczia3f41ETuY" +
                "P+9jckZa76EsTJgTa3QS8P3DZPLxfjYZ98Cv9oE9FQRrw5ZIOezBn8D9IQq5iHCyjnx6zwmip1QHtsy2" +
                "RJ+/zvALkyQVOsOhKhtDjKBj2KCA6PmMfI3qM9wKIl2sKU//HI0mk/EO5df7lJdAlkWl0SIUfFiwCouW" +
                "+8AhIY6FGf7++WGrC4f55UCY3KWjqzbZcsv9YCTV0helYVcEhzJYSG1aT8foPUw+TEY7/Abi1+f0PP1N" +
                "BfM7SIcLyrXx/3b58csccyokemrC7IO16JNRgil3CHRqbR+l0erYAdbO6ytlIN58B+f11rMupiLcmq9P" +
                "Xq/waHh3t63kgXh7KsGccFXRQYanqIucPM/WPmm70L7mS42vjz4NqS8zE1J7h9i1ybuvcIjTZGZT7JVf" +
                "F4CvjSOeuPs8ne1CDcRvCXBoN2Ksbw8gCYWsMQh1IsheAka56qaAAIMblXTLT6i9wNiO1WZJlxrHR+Ug" +
                "1n7rzM6Gxrhlmkd4IUoB/7jtZQUy64uKa0zsjFW8RVHeliXLuF4U6Slm3/Equx2nKWl9725ECpHTzedJ" +
                "dzIkXVYas0W6j3daSnIHKZ6FbtPokqarAzphP1n2D05JgQXCiEN1g1wZg92MGbrkLQmhe+iN9WBJ8txS" +
                "EqPdUWHNH91lPV6gFYPeaj8LCyKVy+IfdiN2dPMrxskQZMnpRWpCQ4Ve6GJTDIlBYPcwOs963QKQqttU" +
                "FOhzGquuNsnjIeQbpS62Phqa9xl8uTONZ9nCOMnDJc+UXjs/l7Y01H8sG4dc1tl/Pgc2pvELAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
