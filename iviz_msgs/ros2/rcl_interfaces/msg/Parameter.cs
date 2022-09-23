/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class Parameter : IHasSerializer<Parameter>, IMessage
    {
        // This is the message to communicate a parameter. It is an open struct with an enum in
        // the descriptor to select which value is active.
        // The full name of the parameter.
        [DataMember (Name = "name")] public string Name;
        // The parameter's value which can be one of several types, see
        // `ParameterValue.msg` and `ParameterType.msg`.
        [DataMember (Name = "value")] public ParameterValue Value;
    
        public Parameter()
        {
            Name = "";
            Value = new ParameterValue();
        }
        
        public Parameter(string Name, ParameterValue Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        public Parameter(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Value = new ParameterValue(ref b);
        }
        
        public Parameter(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            Value = new ParameterValue(ref b);
        }
        
        public Parameter RosDeserialize(ref ReadBuffer b) => new Parameter(ref b);
        
        public Parameter RosDeserialize(ref ReadBuffer2 b) => new Parameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            Value.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            Value.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Value is null) BuiltIns.ThrowNullReference();
            Value.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Name);
                size += Value.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = Value.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "rcl_interfaces/Parameter";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3965b6672807ab03da22801e25720a70";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61Uy27bMBC86ysWyaEPyGqQOHFaoAcXMIoc0gSJm0tROLS0kglQpEBSdt2v75CSHLtI" +
                "0ksNw4bI3Znd2Vkd03wlHeHrV0w1OycqJm8oN3XdapkLzySoEVbU7NlmdOVDtNBkGtbkvG1zTxvpV+GM" +
                "dVuT1MlxhCvY5VY23tiA6FhxCF3JfEVroVqOQLmXa84SpMyRUrZKkQYXmTJiPDEn4JK6irdD+O72jesh" +
                "O/gctSyBoSOO4zVbochvG3YpHhnpj7dD7kNIzGpXPaKFYu9ijvh4niWHwR1Xknz+z5/k+v7rJ7K5WkgN" +
                "rlLk7D4cUqPy746LIGgRDmuph6Z7xTT/8vR+0clRSlYFVLaMtn2G7MPubqd30+vZfHa3+HYzX9zP5phe" +
                "EaceHCH84QxoIxxp4wMWoN7KkirjPet3hBljmnCMll4KJX9zEdhizR1/8AbG4FG81M+r3E81zKnrBpA7" +
                "8nSYrbGWXWM0+oIKoUDRNNY0Vgazxo4xfGU2WdJCx8uIF7CPHgRitD+CbbcKisCZwe5/W60bb0Y3Wm3j" +
                "TaflEzFs2Fs8ltpRbiSsuxLrGC5Dk6WxtfDS6NjZF2MUw5cRLB0sytgcMGKNOGhYCuWwDUvEUvhZ9E47" +
                "pis4ohpqIyt0FZahtKam0cf09PQsPZucpidnF+nl+TidTM7TyxO0bpD60vUkS6DPxZhkh/1ENqXCtEtI" +
                "1FjOpUMLVCqDXkDZGIT3ZZRGQedwejWbzWhyPs6SGAjQDmEf08OZLfaw39TwztAGFOEdkONcsa5wpmQt" +
                "/W7du789GA03WbENU1tufVjoNiwExAaaHg0kB/KHwB8/Y/wiZr+Atz8i140hpIVBvJJ2MR4tpR9U3GVH" +
                "bZE+qPtvhOc0djtFAdVr+gpSr9qQ2j0is5fxIPMPeJQPi/0FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Parameter> CreateSerializer() => new Serializer();
        public Deserializer<Parameter> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Parameter>
        {
            public override void RosSerialize(Parameter msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Parameter msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Parameter msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Parameter msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Parameter msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Parameter>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Parameter msg) => msg = new Parameter(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Parameter msg) => msg = new Parameter(ref b);
        }
    }
}
