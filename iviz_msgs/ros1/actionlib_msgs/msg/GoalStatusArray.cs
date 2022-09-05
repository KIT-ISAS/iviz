/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [DataContract]
    public sealed class GoalStatusArray : IDeserializable<GoalStatusArray>, IHasSerializer<GoalStatusArray>, IMessage
    {
        // Stores the statuses for goals that are currently being tracked
        // by an action server
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "status_list")] public GoalStatus[] StatusList;
    
        public GoalStatusArray()
        {
            StatusList = EmptyArray<GoalStatus>.Value;
        }
        
        public GoalStatusArray(in StdMsgs.Header Header, GoalStatus[] StatusList)
        {
            this.Header = Header;
            this.StatusList = StatusList;
        }
        
        public GoalStatusArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GoalStatus>.Value
                    : new GoalStatus[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new GoalStatus(ref b);
                }
                StatusList = array;
            }
        }
        
        public GoalStatusArray(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GoalStatus>.Value
                    : new GoalStatus[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new GoalStatus(ref b);
                }
                StatusList = array;
            }
        }
        
        public GoalStatusArray RosDeserialize(ref ReadBuffer b) => new GoalStatusArray(ref b);
        
        public GoalStatusArray RosDeserialize(ref ReadBuffer2 b) => new GoalStatusArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(StatusList.Length);
            foreach (var t in StatusList)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(StatusList.Length);
            foreach (var t in StatusList)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (StatusList is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < StatusList.Length; i++)
            {
                if (StatusList[i] is null) BuiltIns.ThrowNullReference(nameof(StatusList), i);
                StatusList[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + WriteBuffer.GetArraySize(StatusList);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // StatusList.Length
            for (int i = 0; i < StatusList.Length; i++)
            {
                c = StatusList[i].AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "actionlib_msgs/GoalStatusArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8b2b82f13216d0a8ea88bd3af735e619";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W0XIaNxR936/QDA+xOzVtkjZJPcMDBeqScRKPoX3JZDxa6bKrVrtLJS2Yv++52mUB" +
                "BxoekjLYDIt07tG5517dnpiFypEXISfhgwy1x5dF5URWScuPZRDSkVC1c1QGuxEpmTITwUn1N+mkJ9KN" +
                "kKWQKpiqFJ7cilzyO0lNTuTxI7kB1Cxif/zUBnmwxockSQZf+ZW8m91cI4Z+KHzmf2h4gCTCl1o6LQoK" +
                "Ussg4xlzk+XkriytyDKxYklaxF/DZkm+j43z3HiBd0YlOWlxfiikRaiEqoqiLo2SgUQwBR3sx04DUcRS" +
                "umBUbaXD+sppU/LyhZMFMTrenv6pqVQkpuNrrCk9qToYENoAQTmSnuWejkVSmzK8fMEbRE98vK/8809J" +
                "b76urvCcMsjdsWjSBtb0uERymbD01wj2XXPKPoJAJUI47cVFfPaAr/5SIBq40LJSubjAEe42IUde2R4r" +
                "6YxMLTGwghRAfcabnl3uIZcRupRltYVvEHcxzoEtO1w+01WO5FmWwdcZlMTCpatWRmMp3Mcgyhq4U1iT" +
                "Ouk2Ce9qQia931hsLMKumBp8Su8rZZAJLdYm5IkPjtFjWh6M/la2bGoEHBtz7soiVghSzDXH8TnTb9pK" +
                "ab/cTd6Pp+9vxPY1ED/iP/uT4jaRSy82FNiZKbE+qkl8K9BhfTaYw9F8+udE7GE+P8TkjDyp+7OA7+4n" +
                "k3d388m4A35xCOxIETwOWyLlsAc/QRl49JpFgJNN4NM7ThA9xoIos0T8x6uHP5gkqtAYDuW5tMQIJvgt" +
                "CohezMkVKEPLPSHQZUt59sdoNJmM9yi/PKS8BrJUuSGm7WvFKixqbgjHhDgVZvjrh/udLhzmpyNh0ioe" +
                "XdfRljvuRyPpmr4oDbvCVyiDhTS2Rjs/Qe9+8nYy2uM3ED9/Ts/RX6TCCQfEgqrq8NQu33+ZY0pKorlG" +
                "zC5YjYYZJJhyh0DLNuVKWqNPHaB1XlcpA/Hqf3BeZ72yCrEId+brktcpPBre3u4qeSBen0swJdxZdJTh" +
                "OeoiJ59n65B0uTCu4NuNr4+w3wUiE9IHh9i3yZuvcIjzZGZTHJRfE4CvjROeuP0wm+9DDcQvEXDYTS3t" +
                "7QEkoZE1BqF29OkkYJR+Mw54GNzqqFt6Ru15xq5YbZZ0bXD8YzNT0htaW63jYMILUQqO67a7rECmvai4" +
                "xsTu+ohbNKV1lrGM7aJAj99uwjpylU3HSeOAZgRpRfI8XsbzxDsZkq5zg9ki3sd7LSW6gzQPRdM4utTt" +
                "HfNUJ+ynkv2DU/LcWqHHEBVL5Mpa7N6bW9eE0B301nqwJDluKZHR/qjQ8kd3accLtGLQ2xxmYUGkUwy/" +
                "7EbswHxV24C50nuZUZMavyRlFkZtiyEy8P0WnYe+ZgFIFXUsCvQ5g1X9bfJ4CEn+BbvCNmudCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GoalStatusArray> CreateSerializer() => new Serializer();
        public Deserializer<GoalStatusArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GoalStatusArray>
        {
            public override void RosSerialize(GoalStatusArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GoalStatusArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GoalStatusArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GoalStatusArray msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<GoalStatusArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GoalStatusArray msg) => msg = new GoalStatusArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GoalStatusArray msg) => msg = new GoalStatusArray(ref b);
        }
    }
}
