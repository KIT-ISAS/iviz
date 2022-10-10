/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciActionResult : IHasSerializer<FibonacciActionResult>, IMessage, IActionResult<FibonacciResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public FibonacciResult Result { get; set; }
    
        public FibonacciActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new FibonacciResult();
        }
        
        public FibonacciActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public FibonacciActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        public FibonacciActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        public FibonacciActionResult RosDeserialize(ref ReadBuffer b) => new FibonacciActionResult(ref b);
        
        public FibonacciActionResult RosDeserialize(ref ReadBuffer2 b) => new FibonacciActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference(nameof(Status));
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = Result.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "actionlib_tutorials/FibonacciActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bee73a9fe29ae25e966e105f5553dd03";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXMaRxC976+YKg6WUhGO7cR2VMWBAJZxybYKkVxcLtXsbLM7ye4smQ8Q/z6vZ5cF" +
                "ZBFzsE0hrQQzr9+8ft3Tb0lmZEURH4lUXtem1Old5XL39KqW5a2XPjjh4iN5o9PaSKX0jFwovbDxkQy+" +
                "8St5f3t1iZhZw+Ntw64nQMZk0maiIi8z6aVY1CCv84LsRUkrKplotaRMxG/9Zkmuj43zQjuBd06GrCzL" +
                "jQgOi3wtVF1VwWglPQmvKzrYj53aCCmW0nqtQikt1tc204aXL6ysiNHxdvRvIKNITMeXWGMcqeA1CG2A" +
                "oCxJp02OL0UStPEvnvMG0ROfZrV79jnpzdf1BT6nHLnoWAhfSM+s6X4JoZmwdJcI9lNzyj6CQCVCuMyJ" +
                "s/jZHf515wLRwIWWtSrEGY5ws/FFbQBIYiWtlmlJDKwgBVCf8KYn53vIJkIbaeotfIO4i3EKrOlw+UwX" +
                "BZJXsgwu5FASC5e2XukMS9NNBFGlJuMFDGil3SS8qwmZ9N6w2FiEXTE1eErnaqWRiUystS8S5y2jx7Tc" +
                "6Sz5TrY8WiQJ/4kU53hwfM70623lNP/cTD6Mpx+uxPY1EL/gN/uT4jZRSCc25NmZKbE+qkl8K1ATGzm3" +
                "KxREgzkczad/TcQe5rNDTM5IsBbKwo0psUYnAd/MJpP3N/PJuAN+fghsSRE8Dlsi5bAHf4IycF7IhYeT" +
                "tefTW04Q3ceCMHki/ufVww9MElVoDIfyXJbECNq7LQqIns3JVijDknuCp/OW8u2fo9FkMt6j/OKQ8hrI" +
                "UhWamLYLilVYBG4IjwlxLMzwj4+znS4c5tdHwqR1PHoWoi133B+NlAX6qjTsClejDBZSl8HSMXqzybvJ" +
                "aI/fQPz2JT1Lf5PyRxwQC6oO/qFdfv46x5SURHONmF2wgIbpJZhyh0DL1mYlS50dO0DrvK5SBuLlD3Be" +
                "Zz1T+1iEO/N1yesUHg2vr3eVPBCvTiWYEu4sepThKeoiJ19m65C0WWhb8e3G14ff7wKRCWUHh9i3yetv" +
                "cIjTZGZTHJRfE4CvjSOeuP54O9+HGojfI+DQbMVobw8giQxZYxBqRJCdBIzSb8YBB4OXWdQtPaH2HGPX" +
                "rDZLutY4PipHmgetM+kNy7Jex8GEF6IULNdtd1mBTHtRcY2JvRmLt2SUhjxnGdtFnu598gOvsuk4aRzQ" +
                "jCCtSM5zuvk88U6GpOtCY7aI9/FeS4nuoIyHomkcXUJ7xzzUCfvJsH9wSnIsEEYcqpbIVVliN2O6Jnlr" +
                "QugOems9WJIst5TIaH9UaPmju7TjBVox6G0Os7AgylKp/mE3YkczyGKudE7m1KTGLUnphVbbYogMXL9F" +
                "56GvWQBSVYhFgT6nsaq/TR4PId89dT4gORpyPX0wnSdJHDU/fe6m0+Q/UFQvTO8LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciActionResult> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciActionResult>
        {
            public override void RosSerialize(FibonacciActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciActionResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FibonacciActionResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FibonacciActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciActionResult msg) => msg = new FibonacciActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciActionResult msg) => msg = new FibonacciActionResult(ref b);
        }
    }
}
