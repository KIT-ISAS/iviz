/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class DiagnosticArray : IDeserializableCommon<DiagnosticArray>, IMessageCommon
    {
        // This message is used to send diagnostic information about the state of the robot
        /// <summary> For timestamp </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> An array of components being reported on </summary>
        [DataMember (Name = "status")] public DiagnosticStatus[] Status;
    
        public DiagnosticArray()
        {
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        public DiagnosticArray(in StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        public DiagnosticArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public DiagnosticArray(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new DiagnosticArray(ref b);
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer b) => new DiagnosticArray(ref b);
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer2 b) => new DiagnosticArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status);
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
    
        public int RosMessageLength => 4 + Header.RosMessageLength + WriteBuffer.GetArraySize(Status);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Status);
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
    }
}
