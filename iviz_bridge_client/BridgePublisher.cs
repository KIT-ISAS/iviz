using System;
using System.IO;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.Bridge.Client
{
    public class BridgePublisher<T> where T : IMessage
    {
        readonly TcpClient client;
        readonly BinaryWriter writer;
        byte[] buffer = Array.Empty<byte>();

        public bool IsAlive => client.Connected;

        public BridgePublisher(string hostname, int port)
        {
            client = new TcpClient();
            client.Connect(hostname, port);
            writer = new BinaryWriter(client.GetStream());

            Console.WriteLine("+++ " + hostname + ":" + port);
        }

        public void Publish(T msg)
        {
            int size = msg.RosMessageLength;

            Console.WriteLine(">>> " + size);

            if (buffer.Length < size)
            {
                buffer = new byte[size + size / 10];
            }

            BuiltIns.Serialize(msg, buffer);
            writer.Write(size);
            writer.Write(buffer, 0, size);
        }

        public void Stop()
        {
            client.Close();
        }
    }
}
