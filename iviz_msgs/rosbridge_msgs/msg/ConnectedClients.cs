using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_msgs
{
    public sealed class ConnectedClients : IMessage
    {
        public ConnectedClient[] clients { get; set; }
    
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
            this.clients = b.DeserializeArray<ConnectedClient>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new ConnectedClients(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.clients, 0);
        }
        
        public void Validate()
        {
            if (clients is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
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
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_msgs/ConnectedClients";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d0d53b0c0aa23aa7e4cf52f49bca4b69";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE3POz8tLTS5JTXHOyUzNK4mOVUgGM4q5uGypDLh8g92tFIryi5OKMlPSU+Nzi9OL9Z1R" +
                "7ecqLinKzEtXyCyIT0xJKUotLuYqycxNVUiGKMvMz4sH8bm4AIb8WuK4AAAA";
                
    }
}
