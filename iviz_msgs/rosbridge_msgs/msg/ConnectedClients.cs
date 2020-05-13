using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_msgs
{
    [DataContract]
    public sealed class ConnectedClients : IMessage
    {
        [DataMember] public ConnectedClient[] clients { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ConnectedClients()
        {
            clients = System.Array.Empty<ConnectedClient>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ConnectedClients(ConnectedClient[] clients)
        {
            this.clients = clients ?? throw new System.ArgumentNullException(nameof(clients));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ConnectedClients(Buffer b)
        {
            this.clients = b.DeserializeArray<ConnectedClient>();
            for (int i = 0; i < this.clients.Length; i++)
            {
                this.clients[i] = new ConnectedClient(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ConnectedClients(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.clients, 0);
        }
        
        public void Validate()
        {
            if (clients is null) throw new System.NullReferenceException();
            for (int i = 0; i < clients.Length; i++)
            {
                if (clients[i] is null) throw new System.NullReferenceException();
                clients[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                for (int i = 0; i < clients.Length; i++)
                {
                    size += clients[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_msgs/ConnectedClients";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d0d53b0c0aa23aa7e4cf52f49bca4b69";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE3POz8tLTS5JTXHOyUzNK4mOVUgGM4q5uGypDLh8g92tFIryi5OKMlPSU+Nzi9OL9Z1R" +
                "7ecqLinKzEtXyCyIT0xJKUotLuYqycxNVUiGKMvMz4sH8bm4AIb8WuK4AAAA";
                
    }
}
