namespace Iviz.Msgs.rosbridge_msgs
{
    public sealed class ConnectedClients : IMessage
    {
        public ConnectedClient[] clients;
    
        /// <summary> Constructor for empty message. </summary>
        public ConnectedClients()
        {
            clients = System.Array.Empty<ConnectedClient>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out clients, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(clients, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 4;
            for (int i = 0; i < clients.Length; i++)
            {
                size += clients[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new ConnectedClients();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_msgs/ConnectedClients";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d0d53b0c0aa23aa7e4cf52f49bca4b69";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE3POz8tLTS5JTXHOyUzNK4mOVUgGM4q5uGypDLh8g92tFIryi5OKMlPSU+Nzi9OL9Z1R" +
                "7ecqLinKzEtXyCyIT0xJKUotLuYqycxNVUiGKMvMz4sH8bm4AIb8WuK4AAAA";
                
    }
}
