/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeMsgs
{
    [DataContract]
    public sealed class ConnectedClients : IHasSerializer<ConnectedClients>, IMessage
    {
        [DataMember (Name = "clients")] public ConnectedClient[] Clients;
    
        public ConnectedClients()
        {
            Clients = EmptyArray<ConnectedClient>.Value;
        }
        
        public ConnectedClients(ConnectedClient[] Clients)
        {
            this.Clients = Clients;
        }
        
        public ConnectedClients(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                ConnectedClient[] array;
                if (n == 0) array = EmptyArray<ConnectedClient>.Value;
                else
                {
                    array = new ConnectedClient[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ConnectedClient(ref b);
                    }
                }
                Clients = array;
            }
        }
        
        public ConnectedClients(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ConnectedClient[] array;
                if (n == 0) array = EmptyArray<ConnectedClient>.Value;
                else
                {
                    array = new ConnectedClient[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ConnectedClient(ref b);
                    }
                }
                Clients = array;
            }
        }
        
        public ConnectedClients RosDeserialize(ref ReadBuffer b) => new ConnectedClients(ref b);
        
        public ConnectedClients RosDeserialize(ref ReadBuffer2 b) => new ConnectedClients(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Clients.Length);
            foreach (var t in Clients)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Clients.Length);
            foreach (var t in Clients)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Clients, nameof(Clients));
            foreach (var msg in Clients) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Clients) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Clients.Length
            foreach (var msg in Clients) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "rosbridge_msgs/ConnectedClients";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d0d53b0c0aa23aa7e4cf52f49bca4b69";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE62NMQ6DMBAE+3uFf0CPlMoFVSrSRZEF9mGtBGd0d/yfiFTp2WqmGG1sIpydS1zB4u9P" +
                "yBcY0ePm0XMc+qDNZkWpnDar1sX/fzJXSA3Y01SKshnNB1aHJIizLlNm617YOORfiSbJv050AinJsp3L" +
                "AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ConnectedClients> CreateSerializer() => new Serializer();
        public Deserializer<ConnectedClients> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ConnectedClients>
        {
            public override void RosSerialize(ConnectedClients msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ConnectedClients msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ConnectedClients msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ConnectedClients msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ConnectedClients msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ConnectedClients>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ConnectedClients msg) => msg = new ConnectedClients(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ConnectedClients msg) => msg = new ConnectedClients(ref b);
        }
    }
}
