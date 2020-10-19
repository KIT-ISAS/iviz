/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeMsgs
{
    [DataContract (Name = "rosbridge_msgs/ConnectedClients")]
    public sealed class ConnectedClients : IMessage
    {
        [DataMember (Name = "clients")] public ConnectedClient[] Clients { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ConnectedClients()
        {
            Clients = System.Array.Empty<ConnectedClient>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ConnectedClients(ConnectedClient[] Clients)
        {
            this.Clients = Clients;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ConnectedClients(ref Buffer b)
        {
            Clients = b.DeserializeArray<ConnectedClient>();
            for (int i = 0; i < Clients.Length; i++)
            {
                Clients[i] = new ConnectedClient(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ConnectedClients(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Clients, 0);
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Clients)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
