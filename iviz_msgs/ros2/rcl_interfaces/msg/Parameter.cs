/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class Parameter : IDeserializableRos2<Parameter>, IMessageRos2
    {
        // This is the message to communicate a parameter. It is an open struct with an enum in
        // the descriptor to select which value is active.
        // The full name of the parameter.
        [DataMember (Name = "name")] public string Name;
        // The parameter's value which can be one of several types, see
        // `ParameterValue.msg` and `ParameterType.msg`.
        [DataMember (Name = "value")] public ParameterValue Value;
    
        /// Constructor for empty message.
        public Parameter()
        {
            Name = "";
            Value = new ParameterValue();
        }
        
        /// Explicit constructor.
        public Parameter(string Name, ParameterValue Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public Parameter(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            Value = new ParameterValue(ref b);
        }
        
        public Parameter RosDeserialize(ref ReadBuffer2 b) => new Parameter(ref b);
    
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
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            Value.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/Parameter";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
