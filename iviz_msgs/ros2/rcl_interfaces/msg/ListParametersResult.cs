/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ListParametersResult : IDeserializable<ListParametersResult>, IHasSerializer<ListParametersResult>, IMessage
    {
        // The resulting parameters under the given prefixes.
        [DataMember (Name = "names")] public string[] Names;
        // The resulting prefixes under the given prefixes.
        // TODO(wjwwood): link to prefix definition and rules.
        [DataMember (Name = "prefixes")] public string[] Prefixes;
    
        public ListParametersResult()
        {
            Names = EmptyArray<string>.Value;
            Prefixes = EmptyArray<string>.Value;
        }
        
        public ListParametersResult(string[] Names, string[] Prefixes)
        {
            this.Names = Names;
            this.Prefixes = Prefixes;
        }
        
        public ListParametersResult(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Names);
            b.DeserializeStringArray(out Prefixes);
        }
        
        public ListParametersResult(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeStringArray(out Names);
            b.Align4();
            b.DeserializeStringArray(out Prefixes);
        }
        
        public ListParametersResult RosDeserialize(ref ReadBuffer b) => new ListParametersResult(ref b);
        
        public ListParametersResult RosDeserialize(ref ReadBuffer2 b) => new ListParametersResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Names);
            b.SerializeArray(Prefixes);
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetArraySize(Names);
                size += WriteBuffer.GetArraySize(Prefixes);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Names);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Prefixes);
            return size;
        }
    
        public const string MessageType = "rcl_interfaces/ListParametersResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2d4734ae1691b070356412e2d1174073";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE32NoRLCMBBEfb5iZ2rA8AFofA2OQWQmRzkIl87dhfTziSiiCMyafft2wPlOULKanWXC" +
                "HDW+yEkNVRIpvNcTv0kwK914ITsEc+3s5QrprIUw/EpW8o+iT8bTuGuP1kpJ+yMyyxNeVgSpp7BzEURJ" +
                "0Jo3x19RCB/nsVrwwQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ListParametersResult> CreateSerializer() => new Serializer();
        public Deserializer<ListParametersResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ListParametersResult>
        {
            public override void RosSerialize(ListParametersResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ListParametersResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ListParametersResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ListParametersResult msg) => msg.Ros2MessageLength;
            public override void RosValidate(ListParametersResult msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<ListParametersResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ListParametersResult msg) => msg = new ListParametersResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ListParametersResult msg) => msg = new ListParametersResult(ref b);
        }
    }
}
