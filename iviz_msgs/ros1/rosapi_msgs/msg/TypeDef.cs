/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class TypeDef : IHasSerializer<TypeDef>, IMessage
    {
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "fieldnames")] public string[] Fieldnames;
        [DataMember (Name = "fieldtypes")] public string[] Fieldtypes;
        [DataMember (Name = "fieldarraylen")] public int[] Fieldarraylen;
        [DataMember (Name = "examples")] public string[] Examples;
        [DataMember (Name = "constnames")] public string[] Constnames;
        [DataMember (Name = "constvalues")] public string[] Constvalues;
    
        public TypeDef()
        {
            Type = "";
            Fieldnames = EmptyArray<string>.Value;
            Fieldtypes = EmptyArray<string>.Value;
            Fieldarraylen = EmptyArray<int>.Value;
            Examples = EmptyArray<string>.Value;
            Constnames = EmptyArray<string>.Value;
            Constvalues = EmptyArray<string>.Value;
        }
        
        public TypeDef(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
            Fieldnames = b.DeserializeStringArray();
            Fieldtypes = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Fieldarraylen = array;
            }
            Examples = b.DeserializeStringArray();
            Constnames = b.DeserializeStringArray();
            Constvalues = b.DeserializeStringArray();
        }
        
        public TypeDef(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
            b.Align4();
            Fieldnames = b.DeserializeStringArray();
            b.Align4();
            Fieldtypes = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Fieldarraylen = array;
            }
            Examples = b.DeserializeStringArray();
            b.Align4();
            Constnames = b.DeserializeStringArray();
            b.Align4();
            Constvalues = b.DeserializeStringArray();
        }
        
        public TypeDef RosDeserialize(ref ReadBuffer b) => new TypeDef(ref b);
        
        public TypeDef RosDeserialize(ref ReadBuffer2 b) => new TypeDef(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
            b.Serialize(Fieldnames.Length);
            b.SerializeArray(Fieldnames);
            b.Serialize(Fieldtypes.Length);
            b.SerializeArray(Fieldtypes);
            b.Serialize(Fieldarraylen.Length);
            b.SerializeStructArray(Fieldarraylen);
            b.Serialize(Examples.Length);
            b.SerializeArray(Examples);
            b.Serialize(Constnames.Length);
            b.SerializeArray(Constnames);
            b.Serialize(Constvalues.Length);
            b.SerializeArray(Constvalues);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Type);
            b.Align4();
            b.Serialize(Fieldnames.Length);
            b.SerializeArray(Fieldnames);
            b.Align4();
            b.Serialize(Fieldtypes.Length);
            b.SerializeArray(Fieldtypes);
            b.Align4();
            b.Serialize(Fieldarraylen.Length);
            b.SerializeStructArray(Fieldarraylen);
            b.Serialize(Examples.Length);
            b.SerializeArray(Examples);
            b.Align4();
            b.Serialize(Constnames.Length);
            b.SerializeArray(Constnames);
            b.Align4();
            b.Serialize(Constvalues.Length);
            b.SerializeArray(Constvalues);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Fieldnames, nameof(Fieldnames));
            BuiltIns.ThrowIfNull(Fieldtypes, nameof(Fieldtypes));
            BuiltIns.ThrowIfNull(Fieldarraylen, nameof(Fieldarraylen));
            BuiltIns.ThrowIfNull(Examples, nameof(Examples));
            BuiltIns.ThrowIfNull(Constnames, nameof(Constnames));
            BuiltIns.ThrowIfNull(Constvalues, nameof(Constvalues));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 28;
                size += WriteBuffer.GetStringSize(Type);
                size += WriteBuffer.GetArraySize(Fieldnames);
                size += WriteBuffer.GetArraySize(Fieldtypes);
                size += 4 * Fieldarraylen.Length;
                size += WriteBuffer.GetArraySize(Examples);
                size += WriteBuffer.GetArraySize(Constnames);
                size += WriteBuffer.GetArraySize(Constvalues);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Type);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Fieldnames);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Fieldtypes);
            size = WriteBuffer2.Align4(size);
            size += 4; // Fieldarraylen.Length
            size += 4 * Fieldarraylen.Length;
            size = WriteBuffer2.AddLength(size, Examples);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Constnames);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Constvalues);
            return size;
        }
    
        public const string MessageType = "rosapi_msgs/TypeDef";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "80597571d79bbeef6c9c4d98f30116a0";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1coqSxI5SoGs6NjFdIyU3NS8hJzU4vRxEDKirky80qMjWBCiUVFiZU5qXkI" +
                "lakVibkFOch6k/PzikvQzAOLlSXmlAIFuQB/w6D2hgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TypeDef> CreateSerializer() => new Serializer();
        public Deserializer<TypeDef> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TypeDef>
        {
            public override void RosSerialize(TypeDef msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TypeDef msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TypeDef msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TypeDef msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TypeDef msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TypeDef>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TypeDef msg) => msg = new TypeDef(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TypeDef msg) => msg = new TypeDef(ref b);
        }
    }
}
