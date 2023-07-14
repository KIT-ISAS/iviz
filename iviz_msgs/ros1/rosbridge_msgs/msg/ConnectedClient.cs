/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeMsgs
{
    [DataContract]
    public sealed class ConnectedClient : IHasSerializer<ConnectedClient>, IMessage
    {
        [DataMember (Name = "ip_address")] public string IpAddress;
        [DataMember (Name = "connection_time")] public time ConnectionTime;
    
        public ConnectedClient()
        {
            IpAddress = "";
        }
        
        public ConnectedClient(string IpAddress, time ConnectionTime)
        {
            this.IpAddress = IpAddress;
            this.ConnectionTime = ConnectionTime;
        }
        
        public ConnectedClient(ref ReadBuffer b)
        {
            IpAddress = b.DeserializeString();
            b.Deserialize(out ConnectionTime);
        }
        
        public ConnectedClient(ref ReadBuffer2 b)
        {
            b.Align4();
            IpAddress = b.DeserializeString();
            b.Align4();
            b.Deserialize(out ConnectionTime);
        }
        
        public ConnectedClient RosDeserialize(ref ReadBuffer b) => new ConnectedClient(ref b);
        
        public ConnectedClient RosDeserialize(ref ReadBuffer2 b) => new ConnectedClient(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(IpAddress);
            b.Serialize(ConnectionTime);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(IpAddress);
            b.Align4();
            b.Serialize(ConnectionTime);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(IpAddress, nameof(IpAddress));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetStringSize(IpAddress);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, IpAddress);
            size = WriteBuffer2.Align4(size);
            size += 8; // ConnectionTime
            return size;
        }
    
        public const string MessageType = "rosbridge_msgs/ConnectedClient";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7f2187ce389b39b2b3bb2a3957e54c04";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fILIhPTEkpSi0u5koqzcwpycyLz8wrSS1KS0xOLdYPycxNVUjOz8tLTS7J" +
                "zM+LLwHyubgAtzZU6jsAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ConnectedClient> CreateSerializer() => new Serializer();
        public Deserializer<ConnectedClient> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ConnectedClient>
        {
            public override void RosSerialize(ConnectedClient msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ConnectedClient msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ConnectedClient msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ConnectedClient msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ConnectedClient msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ConnectedClient>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ConnectedClient msg) => msg = new ConnectedClient(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ConnectedClient msg) => msg = new ConnectedClient(ref b);
        }
    }
}
