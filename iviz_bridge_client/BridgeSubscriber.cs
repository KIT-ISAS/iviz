using System;
using System.IO;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.Bridge.Client
{
    public sealed class BridgeSubscriber<T> : IDisposable where T : IMessage, new()
    {
        readonly TcpClient client;
        readonly BinaryReader reader;
        byte[] buffer = Array.Empty<byte>();
        readonly T generator;

        public bool IsAlive => client.Connected;

        public BridgeSubscriber(string hostname, int port)
        {
            client = new TcpClient();
            client.Connect(hostname, port);
            reader = new BinaryReader(client.GetStream());
            generator = new T();

            Console.WriteLine("+++ " + hostname + ":" + port);
        }

        public T Read()
        {
            int size = reader.ReadInt32();
            if (buffer.Length < size)
            {
                buffer = new byte[size + size / 10];
            }
            reader.Read(buffer, 0, size);
            return (T)BuiltIns.Deserialize(generator, buffer, size);
        }

        public void Stop()
        {
            client.Close();
        }

        public void Dispose()
        {
            Stop();
            reader.Dispose();
            client.Dispose();
        }
    }
}
