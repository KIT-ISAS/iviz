/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterDescriptor : IHasSerializer<ParameterDescriptor>, IMessage
    {
        // This is the message to communicate a parameter's descriptor.
        // The name of the parameter.
        [DataMember (Name = "name")] public string Name;
        // Enum values are defined in the `ParameterType.msg` message.
        [DataMember (Name = "type")] public byte Type;
        // Description of the parameter, visible from introspection tools.
        [DataMember (Name = "description")] public string Description;
        // Parameter constraints
        // Plain English description of additional constraints which cannot be expressed
        // with the available constraints, e.g. "only prime numbers".
        //
        // By convention, this should only be used to clarify constraints which cannot
        // be completely expressed with the parameter constraints below.
        [DataMember (Name = "additional_constraints")] public string AdditionalConstraints;
        // If 'true' then the value cannot change after it has been initialized.
        [DataMember (Name = "read_only")] public bool ReadOnly;
        // If any of the following sequences are not empty, then the constraint inside of
        // them apply to this parameter.
        //
        // FloatingPointRange and IntegerRange are mutually exclusive.
        // FloatingPointRange consists of a from_value, a to_value, and a step.
        [DataMember (Name = "floating_point_range")] public FloatingPointRange[] FloatingPointRange;
        // IntegerRange consists of a from_value, a to_value, and a step.
        [DataMember (Name = "integer_range")] public IntegerRange[] IntegerRange;
    
        public ParameterDescriptor()
        {
            Name = "";
            Description = "";
            AdditionalConstraints = "";
            FloatingPointRange = EmptyArray<FloatingPointRange>.Value;
            IntegerRange = EmptyArray<IntegerRange>.Value;
        }
        
        public ParameterDescriptor(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Type);
            Description = b.DeserializeString();
            AdditionalConstraints = b.DeserializeString();
            b.Deserialize(out ReadOnly);
            {
                int n = b.DeserializeArrayLength();
                FloatingPointRange[] array;
                if (n == 0) array = EmptyArray<FloatingPointRange>.Value;
                else
                {
                    array = new FloatingPointRange[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new FloatingPointRange(ref b);
                    }
                }
                FloatingPointRange = array;
            }
            {
                int n = b.DeserializeArrayLength();
                IntegerRange[] array;
                if (n == 0) array = EmptyArray<IntegerRange>.Value;
                else
                {
                    array = new IntegerRange[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new IntegerRange(ref b);
                    }
                }
                IntegerRange = array;
            }
        }
        
        public ParameterDescriptor(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            b.Deserialize(out Type);
            b.Align4();
            Description = b.DeserializeString();
            b.Align4();
            AdditionalConstraints = b.DeserializeString();
            b.Deserialize(out ReadOnly);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                FloatingPointRange[] array;
                if (n == 0) array = EmptyArray<FloatingPointRange>.Value;
                else
                {
                    array = new FloatingPointRange[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new FloatingPointRange(ref b);
                    }
                }
                FloatingPointRange = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                IntegerRange[] array;
                if (n == 0) array = EmptyArray<IntegerRange>.Value;
                else
                {
                    array = new IntegerRange[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new IntegerRange(ref b);
                    }
                }
                IntegerRange = array;
            }
        }
        
        public ParameterDescriptor RosDeserialize(ref ReadBuffer b) => new ParameterDescriptor(ref b);
        
        public ParameterDescriptor RosDeserialize(ref ReadBuffer2 b) => new ParameterDescriptor(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(Description);
            b.Serialize(AdditionalConstraints);
            b.Serialize(ReadOnly);
            b.Serialize(FloatingPointRange.Length);
            foreach (var t in FloatingPointRange)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(IntegerRange.Length);
            foreach (var t in IntegerRange)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Name);
            b.Serialize(Type);
            b.Align4();
            b.Serialize(Description);
            b.Align4();
            b.Serialize(AdditionalConstraints);
            b.Serialize(ReadOnly);
            b.Align4();
            b.Serialize(FloatingPointRange.Length);
            foreach (var t in FloatingPointRange)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(IntegerRange.Length);
            foreach (var t in IntegerRange)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(FloatingPointRange, nameof(FloatingPointRange));
            foreach (var msg in FloatingPointRange) msg.RosValidate();
            BuiltIns.ThrowIfNull(IntegerRange, nameof(IntegerRange));
            foreach (var msg in IntegerRange) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 22;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Description);
                size += WriteBuffer.GetStringSize(AdditionalConstraints);
                size += 24 * FloatingPointRange.Length;
                size += 24 * IntegerRange.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size += 1; // Type
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Description);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, AdditionalConstraints);
            size += 1; // ReadOnly
            size = WriteBuffer2.Align4(size);
            size += 4; // FloatingPointRange.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * FloatingPointRange.Length;
            size += 4; // IntegerRange.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * IntegerRange.Length;
            return size;
        }
    
        public const string MessageType = "rcl_interfaces/ParameterDescriptor";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "78d2e8de30e0d7349be132fa485ca911";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE91XTW/bRhC961cM7IMvBJsGcVEY6CFBP+BDgSDJrSiUFTmUFljusrtLqUrh/943S3JJ" +
                "RWphO0UPMQybInfevHkz82hf04edDoTvuGNqOQS1ZYqOKte2vdWVikyKOuVVy5H9TaCaQ+V1F50vV6tr" +
                "xDNZPCTXJIx8tFyF6LXdpqdy8ifbt7RXpudAyjOAGm25Jm1T4Me3U+SHY8dlG7YfJ0Llqtc2fk8RDwTp" +
                "x5GCdvYsbUF7HfTGMDXetQCP3oWOq3Q4OmdCJlbPMIKa86N4iyMKsSE9MLgE/a3RYbeMkuSqrrVcK7MM" +
                "o8NOVzuqlLUu0oaJ/+w8iuEaeAcdd4m02ittlHBdhBbE5bakK2fNkTqvIS2E27APV+XqGuFvjnJ8z1bS" +
                "FgBC98LO9aamFINsPRKlLhrldXP8R2ZA20jytjMoHLGZ5kyyuyQLwow7ZCVnEdafaXff0E30Pd8I1tDo" +
                "NAKTNNVOWUycaiSBjrRTgo2T2gJRGf2J63K1Qd/Is6rXqcJGmYBBGOCVPU5D0DgDVkIo8B8922qcNEnE" +
                "bRePxcxi5olUQdcywADEo5ZU1yEL9EvaLiZa5P/ZOBWR461D7LuBva3p3kbesh9vIGnbx16ZpGll+qD3" +
                "nPblQrhQ0QGiyjilsV0njQp8ii5fI4miELkrV+cgv/1OzXhz3cndtZfbSaMls6fnWoYjix4+TvA//Mdf" +
                "q1/f/3JHvjJrSeQbhR5+c14uynrHMqqchtH1tg4L1uOMNc5LkWM0JV2SidTLpgLrfVQ+LoJwpevRqwqU" +
                "nBuYsL57tRBucLb6KcGTzikzBlx6MQQJd5n/eOBxTJOJSWGYxlQmhpASY1SJ4Uz9rNkP+74Rs27VFrvT" +
                "15ym3TM4YQV4CxH2vDTgiI2KyZEkUxATx/Z1LujFyeJcVtD9xN6Rhm1owRIWULh3fQBYmoyxJgAMW3Nf" +
                "syxDMaQSrENyLDA2MBzcVrBTT9hbOGlaPbwhMKkKW7xURCwrtbsQsgc2Rn4jmOGIWDoTNcxscoRLCDl+" +
                "04sBWtYiU1LEI7324jjXg7kkl94EZ/rIc/W5BjQADot1+Hf+yWbn/PHgMofsR+MMHzQKgihyK3ndcprK" +
                "4dWgR6MaldYhv0qhxF/zaN7Rt+WLIo/bHb2Uj0L8jm7LFw85OeCWaTIJhKfuI+5EkVx77txjtD49pGVU" +
                "xJjVWdMEfqHMVXRXA85ATJmDOoZh1he0C+CN8jxOnJen4tzO4uDJwyBxfmOdi5PCX8kPkehWJJoWXFD+" +
                "J2tcuvMTTNFOPv5ldgiQ55rhEPqlVoiw14+1pjNjOjcl+ePsebZ02ZRkJJ9jS1+3KZ1YUjakh8/2bTCl" +
                "EzsazWh4A35dXnTiRNmHBk1GYS+7EDxocqD0X1K2n78BJieAHtsNAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ParameterDescriptor> CreateSerializer() => new Serializer();
        public Deserializer<ParameterDescriptor> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ParameterDescriptor>
        {
            public override void RosSerialize(ParameterDescriptor msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ParameterDescriptor msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ParameterDescriptor msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ParameterDescriptor msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ParameterDescriptor msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ParameterDescriptor>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ParameterDescriptor msg) => msg = new ParameterDescriptor(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ParameterDescriptor msg) => msg = new ParameterDescriptor(ref b);
        }
    }
}
