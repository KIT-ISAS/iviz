using System;
using System.Threading;
using Iviz.Msgs;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Iviz.Bridge.Client
{
    public sealed class BridgeClient : IDisposable
    {
        readonly WebSocket socket;

        public Uri Uri { get; }

        public BridgeClient(string uri) : this(new Uri(uri)) { }

        public BridgeClient(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            socket = new WebSocket(uri.ToString());
            socket.Connect();
        }

        GenericResponse Query(GenericMessage msgOut)
        {
            GenericResponse msgIn = null;

            void callback(object s, MessageEventArgs e)
            {
                string data;
                if (e.IsBinary)
                {
                    data = BuiltIns.UTF8.GetString(e.RawData);
                }
                else
                {
                    data = e.Data;
                }
                msgIn = JsonConvert.DeserializeObject<GenericResponse>(data);
            }

            socket.OnMessage += callback;

            Console.WriteLine(">>> " + JsonConvert.SerializeObject(msgOut));
            socket.Send(JsonConvert.SerializeObject(msgOut));

            for (int i = 0; i < 50 && msgIn == null; i++)
            {
                Thread.Sleep(100);
            }

            socket.OnMessage -= callback;

            Console.WriteLine("<<< " + JsonConvert.SerializeObject(msgIn));

            return msgIn;
        }

        public BridgeSubscriber<T> Subscribe<T>(string topic) where T : IMessage, new()
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentException("Empty topic", nameof(topic));
            }

            GenericMessage msgOut = new GenericMessage()
            {
                Op = "iviz:subscribe",
                Id = "",
                Topic = topic,
                Type = BuiltIns.GetMessageType(typeof(T))
            };
            GenericResponse msgIn = Query(msgOut);
            if (msgIn == null)
            {
                throw new TimeoutException();
            }
            return new BridgeSubscriber<T>(Uri.Host, int.Parse(msgIn.Value, BuiltIns.Culture));
        }

        public BridgePublisher<T> Advertise<T>(string topic) where T : IMessage
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentException("Empty topic", nameof(topic));
            }

            GenericMessage msgOut = new GenericMessage()
            {
                Op = "iviz:advertise",
                Id = "",
                Topic = topic,
                Type = BuiltIns.GetMessageType(typeof(T))
            };
            GenericResponse msgIn = Query(msgOut);
            if (msgIn == null)
            {
                throw new TimeoutException();
            }
            return new BridgePublisher<T>(Uri.Host, int.Parse(msgIn.Value, BuiltIns.Culture));
        }

        public void Stop()
        {
            socket.Close();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
