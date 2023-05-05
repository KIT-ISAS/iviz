/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRHandState : IHasSerializer<XRHandState>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "palm")] public GeometryMsgs.Transform Palm;
        [DataMember (Name = "thumb")] public GeometryMsgs.Transform[] Thumb;
        [DataMember (Name = "index")] public GeometryMsgs.Transform[] Index;
        [DataMember (Name = "middle")] public GeometryMsgs.Transform[] Middle;
        [DataMember (Name = "ring")] public GeometryMsgs.Transform[] Ring;
        [DataMember (Name = "little")] public GeometryMsgs.Transform[] Little;
        [DataMember (Name = "is_valid")] public bool IsValid;
    
        public XRHandState()
        {
            Thumb = EmptyArray<GeometryMsgs.Transform>.Value;
            Index = EmptyArray<GeometryMsgs.Transform>.Value;
            Middle = EmptyArray<GeometryMsgs.Transform>.Value;
            Ring = EmptyArray<GeometryMsgs.Transform>.Value;
            Little = EmptyArray<GeometryMsgs.Transform>.Value;
        }
        
        public XRHandState(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Palm);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Thumb = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Index = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Middle = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Ring = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Little = array;
            }
            b.Deserialize(out IsValid);
        }
        
        public XRHandState(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
            b.Deserialize(out Palm);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Thumb = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Index = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Middle = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Ring = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Little = array;
            }
            b.Deserialize(out IsValid);
        }
        
        public XRHandState RosDeserialize(ref ReadBuffer b) => new XRHandState(ref b);
        
        public XRHandState RosDeserialize(ref ReadBuffer2 b) => new XRHandState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Palm);
            b.Serialize(Thumb.Length);
            b.SerializeStructArray(Thumb);
            b.Serialize(Index.Length);
            b.SerializeStructArray(Index);
            b.Serialize(Middle.Length);
            b.SerializeStructArray(Middle);
            b.Serialize(Ring.Length);
            b.SerializeStructArray(Ring);
            b.Serialize(Little.Length);
            b.SerializeStructArray(Little);
            b.Serialize(IsValid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align8();
            b.Serialize(in Palm);
            b.Serialize(Thumb.Length);
            b.Align8();
            b.SerializeStructArray(Thumb);
            b.Serialize(Index.Length);
            b.Align8();
            b.SerializeStructArray(Index);
            b.Serialize(Middle.Length);
            b.Align8();
            b.SerializeStructArray(Middle);
            b.Serialize(Ring.Length);
            b.Align8();
            b.SerializeStructArray(Ring);
            b.Serialize(Little.Length);
            b.Align8();
            b.SerializeStructArray(Little);
            b.Serialize(IsValid);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Thumb, nameof(Thumb));
            BuiltIns.ThrowIfNull(Index, nameof(Index));
            BuiltIns.ThrowIfNull(Middle, nameof(Middle));
            BuiltIns.ThrowIfNull(Ring, nameof(Ring));
            BuiltIns.ThrowIfNull(Little, nameof(Little));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 77;
                size += Header.RosMessageLength;
                size += 56 * Thumb.Length;
                size += 56 * Index.Length;
                size += 56 * Middle.Length;
                size += 56 * Ring.Length;
                size += 56 * Little.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 56; // Palm
            size += 4; // Thumb.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Thumb.Length;
            size += 4; // Index.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Index.Length;
            size += 4; // Middle.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Middle.Length;
            size += 4; // Ring.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Ring.Length;
            size += 4; // Little.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Little.Length;
            size += 1; // IsValid
            return size;
        }
    
        public const string MessageType = "iviz_msgs/XRHandState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6623f958b3a95eab309ad45ce791d67f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW4TMRC9+ytGyqEtSoNoEYdK3BC0B6RCKy5VFU3Wk10Lr721vUmXr+d5N9m00AYO" +
                "0CjSetfznmfevPG5sJZAVf9QpfhaUujmdSzj6+vALi59qKlhWz+3eXNLqWrrxZ5947Tc79mvjdZW9gQE" +
                "48o929akBPzCe0smzldsjVZKvf/HP/X56tMZxaSHDM4HzSZ0ldhpDpqQHmtOTMiLKlNWEo6trMQCxHUj" +
                "mvrd1DUSZwBeVyYiYSrFSWBrO2ojgpKnwtd160zBSSiZWh7hgTSOGF0JyRSt5YB4H7RxOXwZuJbMjn+U" +
                "u1ZcIXTx4QwxLkrRJoOEOjAUQThCWGySao1LpycZQBO6+erjm1s1uV77Y3yXEg4Zs0C3OeWs5b4JEnPC" +
                "HM9w2KuhyhkOgUqC43Skw/7bHK/xiHAacpHGFxUdooTLLlXegVBoxcHwwkomLiAFWA8y6ODoAbPrqR07" +
                "v6UfGHdn/A2tG3lzTccVmmezDLEtoSQCm+BXRiN00fUkhTXiEly2CBw6lVHDkWryMYuNIKD61uDJMfrC" +
                "oBOa1iZVKqbs3qEt8+zL/2PLZ6Zj67IguVkoI/YlpXG0F5LWIlBr7X9zUcw+WwZBuQ0XMJX6JkXy4XTA" +
                "W07GO/WlBSA4LCn4NHx7kSI3yTxRItOq3/sl/zwSF713vcMI1MJoK6ZtRAKoTQAUNczAKkEgkkzJJNIe" +
                "ejifwFHzd1AKjJTR3DQg44ea5M+AHMqsnE1pXUHfPioboZ/ffuJNgYutNHrXjRHMtCluSml5AiNZO+Q8" +
                "HIYWgmSr9tGMLpbU+ZbWuSAswuai8WjvmFc/B8n7ab5lNhSPBb30mHbIEiOXGBkXE644dH1pPad3b+l+" +
                "XHXj6seLtHrnsae67ciHPKKDfI96nt/udgbNIv+xoO1qrdRPVX5I2R8HAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<XRHandState> CreateSerializer() => new Serializer();
        public Deserializer<XRHandState> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<XRHandState>
        {
            public override void RosSerialize(XRHandState msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(XRHandState msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(XRHandState msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(XRHandState msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(XRHandState msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<XRHandState>
        {
            public override void RosDeserialize(ref ReadBuffer b, out XRHandState msg) => msg = new XRHandState(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out XRHandState msg) => msg = new XRHandState(ref b);
        }
    }
}
