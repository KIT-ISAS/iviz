/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class DiagnosticArray : IHasSerializer<DiagnosticArray>, IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        /// <summary> For timestamp </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> An array of components being reported on </summary>
        [DataMember (Name = "status")] public DiagnosticStatus[] Status;
    
        public DiagnosticArray()
        {
            Status = EmptyArray<DiagnosticStatus>.Value;
        }
        
        public DiagnosticArray(in StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        public DiagnosticArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<DiagnosticStatus>.Value
                    : new DiagnosticStatus[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new DiagnosticStatus(ref b);
                }
                Status = array;
            }
        }
        
        public DiagnosticArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<DiagnosticStatus>.Value
                    : new DiagnosticStatus[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new DiagnosticStatus(ref b);
                }
                Status = array;
            }
        }
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer b) => new DiagnosticArray(ref b);
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer2 b) => new DiagnosticArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Status.Length);
            foreach (var t in Status)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Status.Length);
            foreach (var t in Status)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) BuiltIns.ThrowNullReference(nameof(Status), i);
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var msg in Status) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Status.Length
            foreach (var msg in Status) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "diagnostic_msgs/DiagnosticArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "60810da900de1dd6ddd437c3503511da";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwU7cMBC9+ytG2gNQCSj0hrQHJGiLaAtaUHtACE2SIbFI7NR2dpu/77O92d0iVeqh" +
                "7Sor28nMm5k3bzyj+0Z76sR7roWwHbxUFCx5MRVVmmtjfdAlafNsXcdBW0Nc2CFQaIR84CBkn9PB2cIG" +
                "9VG4EkdNXmbwoqARIHDXq4sN4B08B//wmCAGTzNiADvHY4QrbddbIyZ4KkSbmpz01gWkZo2a/+Wf+nz3" +
                "4Qx5VE+dr/1xLkDNCCmail0FegJXHJhiMY2uG3GHrSylpVQVskpfw9iLP4Jj4hRPLUYct+24YRV1dYPR" +
                "ZWRtw8rkD08NDqhnB4KGlh3srau0iebPjjuJ6Hi8fB/ElEJXF2ewMV7KIWgkNAKhdMI+cnZ1QWrQJrw7" +
                "jQ5g+GFh/cmjmt2v7CHeSy07vUELOcSs5UfvoAdkxf4Mwd7kKo8QBCwJwlWe9tO7Jxz9ASEackGHyob2" +
                "UcLtGBrIJGpiyU5z0SZplaACqHvRae9gB9kkaMNQxho+I25j/Ams2eDGmg4bNK+NNPihBpMw7J1d6gqm" +
                "xZhAylZDYdTqwrEbVfTKIdXsfSQbRvBKrcHK3ttSc9TgSodG+eAiemrLk67UP5LldgSzOl9P0CS3aYQb" +
                "24K/aTYxWJgmTJY2lUbxA7fb2fplbpOy8L+13uvIbBJ4crc9VBwH36tihBJvrudv8+7b+eLL/CTvLxeL" +
                "m8X8NB/u7s8/Xc7fqXzKszJbr7uIJGbo4j6qrbBLoYlXExuAS4Eq8aXTfbJeJxyg2ONtGflugNPkO3Hx" +
                "G/dMzGTcYMJX7GIPk8N0JswppoyymbqW8Su3g+DGWsb19Y21fvlKJbvx/pdCpkynAl9kRK6rON24gVou" +
                "0IOk7JQyPoihpZbVDoH5S2Qj7+AXHJcvhA7lK0OpnyqsdYA7BgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<DiagnosticArray> CreateSerializer() => new Serializer();
        public Deserializer<DiagnosticArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DiagnosticArray>
        {
            public override void RosSerialize(DiagnosticArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DiagnosticArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DiagnosticArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DiagnosticArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DiagnosticArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DiagnosticArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DiagnosticArray msg) => msg = new DiagnosticArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DiagnosticArray msg) => msg = new DiagnosticArray(ref b);
        }
    }
}
