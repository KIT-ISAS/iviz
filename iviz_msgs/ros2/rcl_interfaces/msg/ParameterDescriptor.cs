/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterDescriptor : IDeserializable<ParameterDescriptor>, IMessage
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
            b.DeserializeString(out Name);
            b.Deserialize(out Type);
            b.DeserializeString(out Description);
            b.DeserializeString(out AdditionalConstraints);
            b.Deserialize(out ReadOnly);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<FloatingPointRange>.Value
                    : new FloatingPointRange[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new FloatingPointRange(ref b);
                }
                FloatingPointRange = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<IntegerRange>.Value
                    : new IntegerRange[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new IntegerRange(ref b);
                }
                IntegerRange = array;
            }
        }
        
        public ParameterDescriptor(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Deserialize(out Type);
            b.Align4();
            b.DeserializeString(out Description);
            b.Align4();
            b.DeserializeString(out AdditionalConstraints);
            b.Deserialize(out ReadOnly);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<FloatingPointRange>.Value
                    : new FloatingPointRange[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new FloatingPointRange(ref b);
                }
                FloatingPointRange = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<IntegerRange>.Value
                    : new IntegerRange[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new IntegerRange(ref b);
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
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Description is null) BuiltIns.ThrowNullReference();
            if (AdditionalConstraints is null) BuiltIns.ThrowNullReference();
            if (FloatingPointRange is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < FloatingPointRange.Length; i++)
            {
                if (FloatingPointRange[i] is null) BuiltIns.ThrowNullReference(nameof(FloatingPointRange), i);
                FloatingPointRange[i].RosValidate();
            }
            if (IntegerRange is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < IntegerRange.Length; i++)
            {
                if (IntegerRange[i] is null) BuiltIns.ThrowNullReference(nameof(IntegerRange), i);
                IntegerRange[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 22;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Description);
                size += WriteBuffer.GetStringSize(AdditionalConstraints);
                size += 24 * FloatingPointRange.Length;
                size += 24 * IntegerRange.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Name);
            c += 1; // Type
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Description);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, AdditionalConstraints);
            c += 1; // ReadOnly
            c = WriteBuffer2.Align4(c);
            c += 4; // FloatingPointRange length
            c = WriteBuffer2.Align8(c);
            c += 24 * FloatingPointRange.Length;
            c += 4; // IntegerRange length
            c = WriteBuffer2.Align8(c);
            c += 24 * IntegerRange.Length;
            return c;
        }
    
        public const string MessageType = "rcl_interfaces/ParameterDescriptor";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "78d2e8de30e0d7349be132fa485ca911";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
