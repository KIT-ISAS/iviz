/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class String : IDeserializable<String>, IHasSerializer<String>, IMessage
    {
        [DataMember (Name = "data")] public string Data;
    
        public String()
        {
            Data = "";
        }
        
        public String(string Data)
        {
            this.Data = Data;
        }
        
        public String(ref ReadBuffer b)
        {
            b.DeserializeString(out Data);
        }
        
        public String(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Data);
        }
        
        public String RosDeserialize(ref ReadBuffer b) => new String(ref b);
        
        public String RosDeserialize(ref ReadBuffer2 b) => new String(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Data);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Data);
            return c;
        }
    
        public const string MessageType = "std_msgs/String";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<String> CreateSerializer() => new Serializer();
        public Deserializer<String> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<String>
        {
            public override void RosSerialize(String msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(String msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(String msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(String msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<String>
        {
            public override void RosDeserialize(ref ReadBuffer b, out String msg) => msg = new String(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out String msg) => msg = new String(ref b);
        }
    }
}
