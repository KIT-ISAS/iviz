/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRHandState : IDeserializable<XRHandState>, IMessage
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Palm);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Thumb = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Index = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Middle = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Ring = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Little = array;
            }
            b.Deserialize(out IsValid);
        }
        
        public XRHandState(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Palm);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Thumb = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Index = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Middle = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Ring = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
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
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
            b.Serialize(IsValid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Palm);
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
            b.Serialize(IsValid);
        }
        
        public void RosValidate()
        {
            if (Thumb is null) BuiltIns.ThrowNullReference();
            if (Index is null) BuiltIns.ThrowNullReference();
            if (Middle is null) BuiltIns.ThrowNullReference();
            if (Ring is null) BuiltIns.ThrowNullReference();
            if (Little is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
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
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 56; // Palm
            c += 4; // Thumb length
            c = WriteBuffer2.Align8(c);
            c += 56 * Thumb.Length;
            c += 4; // Index length
            c = WriteBuffer2.Align8(c);
            c += 56 * Index.Length;
            c += 4; // Middle length
            c = WriteBuffer2.Align8(c);
            c += 56 * Middle.Length;
            c += 4; // Ring length
            c = WriteBuffer2.Align8(c);
            c += 56 * Ring.Length;
            c += 4; // Little length
            c = WriteBuffer2.Align8(c);
            c += 56 * Little.Length;
            c += 1; // IsValid
            return c;
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
    }
}
