/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciActionResult : IDeserializable<FibonacciActionResult>, IActionResult<FibonacciResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public FibonacciResult Result { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new FibonacciResult();
        }
        
        /// Explicit constructor.
        public FibonacciActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal FibonacciActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FibonacciActionResult(ref b);
        
        FibonacciActionResult IDeserializable<FibonacciActionResult>.RosDeserialize(ref Buffer b) => new FibonacciActionResult(ref b);
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bee73a9fe29ae25e966e105f5553dd03";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W0XIaNxR936/QDA+xO7XTJm2SeoYHCsQh4yQem/al0/FopcuuWq2WSlowf99ztbBA" +
                "DBMekjDYC7Z07tG5517ddyQ1eVGmRyZVNLWzJn+oQhGeX9fS3kcZmyBCemRvTV47qZS5o9DYKHx6ZP2v" +
                "/Mo+3F9fIaZuebxr2fUEyDgtvRYVRalllGJWg7wpSvIXlhZkmWg1Jy3Sf+NqTuESG6elCQLvghx5ae1K" +
                "NAGLYi1UXVWNM0pGEtFUtLcfO40TUsylj0Y1Vnqsr702jpfPvKyI0fEO9F9DTpGYjK6wxgVSTTQgtAKC" +
                "8iSDcQX+KbLGuPjyBW/IetNlfYGvVCAFXXARSxmZLD3OoS/zlOEKMX5oD3cJbIhDiKKDOEt/e8DXcC4Q" +
                "BBRoXqtSnIH57SqWtQMgiYX0RuaWGFhBAaA+403PzneQmfaVcNLVG/gWcRvjFFjX4fKZLkrkzPLpQ1NA" +
                "QCyc+3phNJbmqwSirCEXBXznpV9lvKsNmfXessZYhF0pI3jKEGplkAAtliaWWYie0VM2HozOvpEbj9ZG" +
                "xh+R2QIPjs8JfrMpmPbL7fjjaPLxWmxeffETfrMtKW0TpQxiRZENmRPro9rErwVqYyPnfoE6aDEHw+nk" +
                "z7HYwfx5H5Mz0ngPZWHCnFijk4Bv78bjD7fT8agDfrEP7EkRrA1bIuWwB/8F7g9RyFmEk03k03tOED2m" +
                "OnBFtiX69NXDD0ySVGgNh6qcW2IEE8MGBUTPpuQrVJ/lVhDpfE35/o/hcDwe7VB+uU95CWSpSoMWoeFD" +
                "xSrMGu4Dh4Q4Fmbw+6e7rS4c5pcDYfI6HV03yZZb7gcj6Ya+KA27ItQog5k0tvF0jN7d+P14uMOvL359" +
                "Ss/TP6SY30E6XFB1Ez+3y49f5piTkuipCbML1qBPRgmm3CHQqY1bSGv0sQOsnddVSl+8+g7O66zn6piK" +
                "cGu+LnmdwsPBzc22kvvi9akEc8JVRQcZnqIucvI0W/uk3cz4ii81vj66NKS+zExI7x1i1yZvvsIhTpOZ" +
                "TbFXfm0AvjaOeOLm0/10F6ovfkuAA7cRY317AEloZI1BqBVBdhIwymU7BQQY3OqkW35C7QXGrlltlnRp" +
                "cHxUDmLtt86sN7C2XqZ5hBeiFPCh3l5WILO+qLjGxM5oxVs05U1RsIzrRZEeY/Ydr7LJKE1J63t3I1KI" +
                "nG4+T7qTIemyNJgt0n2801KSO0jzLDRJo0uarg7ohP3k2D84JQUWCCMOVXPkylrsZszQJm9JCN1Bb6wH" +
                "S5LnlpIY7Y4Ka/7oLuvxAq0Y9Fb7WZgR6Vyqf9mN2NHOrxgnQ5AFpxepCXNSZmbUphgSg8DuYXSe9doF" +
                "IFU1qSjQ5wxWXW6Sx0PIN09dbJAcA7mefzaUZ1maMP/6uxtKs/8Bgct8OOYLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
