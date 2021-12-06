/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ConnectedClients : IDeserializable<ConnectedClients>, IMessage
    {
        [DataMember (Name = "clients")] public ConnectedClient[] Clients;
    
        /// Constructor for empty message.
        public ConnectedClients()
        {
            Clients = System.Array.Empty<ConnectedClient>();
        }
        
        /// Explicit constructor.
        public ConnectedClients(ConnectedClient[] Clients)
        {
            this.Clients = Clients;
        }
        
        /// Constructor with buffer.
        internal ConnectedClients(ref ReadBuffer b)
        {
            Clients = b.DeserializeArray<ConnectedClient>();
            for (int i = 0; i < Clients.Length; i++)
            {
                Clients[i] = new ConnectedClient(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ConnectedClients(ref b);
        
        public ConnectedClients RosDeserialize(ref ReadBuffer b) => new ConnectedClients(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Clients);
        }
        
        public void RosValidate()
        {
            if (Clients is null) throw new System.NullReferenceException(nameof(Clients));
            for (int i = 0; i < Clients.Length; i++)
            {
                if (Clients[i] is null) throw new System.NullReferenceException($"{nameof(Clients)}[{i}]");
                Clients[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Clients);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_msgs/ConnectedClients";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d0d53b0c0aa23aa7e4cf52f49bca4b69";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE3POz8tLTS5JTXHOyUzNK4mOVUgGM4q5uGypDLh8g92tFIryi5OKMlPSU+Nzi9OL9Z1R" +
                "7ecqLinKzEtXyCyIT0xJKUotLuYqycxNVUiGKMvMz4sH8bm4AIb8WuK4AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
