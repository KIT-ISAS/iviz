/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class FloatingPointRange : IHasSerializer<FloatingPointRange>, IMessage
    {
        // Represents bounds and a step value for a floating point typed parameter.
        // Start value for valid values, inclusive.
        [DataMember (Name = "from_value")] public double FromValue;
        // End value for valid values, inclusive.
        [DataMember (Name = "to_value")] public double ToValue;
        // Size of valid steps between the from and to bound.
        // 
        // Step is considered to be a magnitude, therefore negative values are treated
        // the same as positive values, and a step value of zero implies a continuous
        // range of values.
        //
        // Ideally, the step would be less than or equal to the distance between the
        // bounds, as well as an even multiple of the distance between the bounds, but
        // neither are required.
        //
        // If the absolute value of the step is larger than or equal to the distance
        // between the two bounds, then the bounds will be the only valid values. e.g. if
        // the range is defined as {from_value: 1.0, to_value: 2.0, step: 5.0} then the
        // valid values will be 1.0 and 2.0.
        //
        // If the step is less than the distance between the bounds, but the distance is
        // not a multiple of the step, then the "to" bound will always be a valid value,
        // e.g. if the range is defined as {from_value: 2.0, to_value: 5.0, step: 2.0}
        // then the valid values will be 2.0, 4.0, and 5.0.
        [DataMember (Name = "step")] public double Step;
    
        public FloatingPointRange()
        {
        }
        
        public FloatingPointRange(double FromValue, double ToValue, double Step)
        {
            this.FromValue = FromValue;
            this.ToValue = ToValue;
            this.Step = Step;
        }
        
        public FloatingPointRange(ref ReadBuffer b)
        {
            b.Deserialize(out FromValue);
            b.Deserialize(out ToValue);
            b.Deserialize(out Step);
        }
        
        public FloatingPointRange(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out FromValue);
            b.Deserialize(out ToValue);
            b.Deserialize(out Step);
        }
        
        public FloatingPointRange RosDeserialize(ref ReadBuffer b) => new FloatingPointRange(ref b);
        
        public FloatingPointRange RosDeserialize(ref ReadBuffer2 b) => new FloatingPointRange(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FromValue);
            b.Serialize(ToValue);
            b.Serialize(Step);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(FromValue);
            b.Serialize(ToValue);
            b.Serialize(Step);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 24;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 24;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "rcl_interfaces/FloatingPointRange";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9dbe716e5d84c837e6684202a37a25de";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE5WTz27bMAzG734Kor0aRlc0O+S+w67rAwxMRHsEFMmTqBjp0HcfabmOs3/oLkks6/v4" +
                "8UfmHr7QmChTkAyHWILLgMEBQhYa4Yy+EPQx6UHvIwqHAcbIQUAuIzkYMeGJhFLXNPfwLJhkI9Jf7Opz" +
                "boHD0ZfMZ+qa2evjE/Qpnr7O703+Kbj/EUu8Sp/5hSD2i8iyazskE1EA+UZzobkxibXNTkVzYu2SMxxj" +
                "yOwoUb1B2u8Jh8BSHLXmkEgzEQQaFMKZllyAeiaJUMipm1XKygMwK6XMm5vt71g17gulCHwaPZuXpVDC" +
                "JZasZgnD8NaTGmhgPfzsCL2/tLWUeU2xeGeJPeWsxxhA4dH3gt5asXuOs2A40paIetVxtxZ2Iu/tW8V0" +
                "1gun4oVHP5f/m8OqPxRRt0BsmGYiScuzslwyVw885OiL0LX7tQcdgMc0qPqf+S3zpr5Mcc2gz9tQMLE2" +
                "pFDsKAZ/udmmDqgbOuB+GVklrSEc9Rx0BZTEj+tq7uFD99Cu67aHR3u04HvYdQ+va3G125ZZQ6h8nr7q" +
                "boisva+Tew/r20tsqxKi2ML+MjSz35C5k3hXfWow9BNect31TexW/RY874PzeAtnd4Wjb14r4prgj3Bm" +
                "+ZN9GKKdIXr7g5tL0/wESe+ahqMEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FloatingPointRange> CreateSerializer() => new Serializer();
        public Deserializer<FloatingPointRange> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FloatingPointRange>
        {
            public override void RosSerialize(FloatingPointRange msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FloatingPointRange msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FloatingPointRange _) => RosFixedMessageLength;
            public override int Ros2MessageLength(FloatingPointRange _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<FloatingPointRange>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FloatingPointRange msg) => msg = new FloatingPointRange(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FloatingPointRange msg) => msg = new FloatingPointRange(ref b);
        }
    }
}
