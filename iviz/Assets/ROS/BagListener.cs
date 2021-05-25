using System;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;
using Logger = Iviz.Msgs.Logger;

namespace Iviz.Ros
{
    public sealed class BagListener
    {
        readonly AsyncCollection<(IMessage, IRosConnection)> messageQueue =
            new AsyncCollection<(IMessage, IRosConnection)>();

        readonly RosbagFileWriter writer;
        readonly Task task;
        readonly string path;

        public long Length => writer.Length;

        public BagListener([NotNull] string path)
        {
            this.path = path;
            Logger.LogFormat("{0}: Writing rosbag to path {1}", this, path);
            writer = new RosbagFileWriter(path);
            task = Task.Run(WriteMessagesAsync);
        }

        public async Task StopAsync()
        {
            messageQueue.CompleteAdding();
            await task.AwaitNoThrow(this);
            writer.Dispose();
            Logger.LogFormat("{0}: Closing rosbag on path {1}", this, path);
        }

        internal void EnqueueMessage([NotNull] IMessage msg, [NotNull] IRosConnection connection)
        {
            try
            {
                messageQueue.Add((msg, connection));
            }
            catch (InvalidOperationException)
            {
                // bag is closing! ignore 
            }
        }

        async Task WriteMessagesAsync()
        {
            try
            {
                while (await messageQueue.OutputAvailableAsync())
                {
                    var (message, connection) = await messageQueue.TakeAsync();
                    await writer.WriteAsync(message, connection, time.Now());
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        [NotNull]
        public override string ToString()
        {
            return "[BagListener]";
        }
    }
}