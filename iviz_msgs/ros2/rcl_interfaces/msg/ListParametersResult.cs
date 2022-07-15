/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class ListParametersResult : IDeserializable<ListParametersResult>, IMessageRos2
    {
        // The resulting parameters under the given prefixes.
        [DataMember (Name = "names")] public string[] Names;
        // The resulting prefixes under the given prefixes.
        // TODO(wjwwood): link to prefix definition and rules.
        [DataMember (Name = "prefixes")] public string[] Prefixes;
    
        /// Constructor for empty message.
        public ListParametersResult()
        {
            Names = System.Array.Empty<string>();
            Prefixes = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public ListParametersResult(string[] Names, string[] Prefixes)
        {
            this.Names = Names;
            this.Prefixes = Prefixes;
        }
        
        /// Constructor with buffer.
        public ListParametersResult(ref ReadBuffer2 b)
        {
            b.DeserializeStringArray(out Names);
            b.DeserializeStringArray(out Prefixes);
        }
        
        public ListParametersResult RosDeserialize(ref ReadBuffer2 b) => new ListParametersResult(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Names);
            b.SerializeArray(Prefixes);
        }
        
        public void RosValidate()
        {
            if (Names is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Names.Length; i++)
            {
                if (Names[i] is null) BuiltIns.ThrowNullReference(nameof(Names), i);
            }
            if (Prefixes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Prefixes.Length; i++)
            {
                if (Prefixes[i] is null) BuiltIns.ThrowNullReference(nameof(Prefixes), i);
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Names);
            WriteBuffer2.Advance(ref c, Prefixes);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/ListParametersResult";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
