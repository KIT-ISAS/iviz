using System;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;

namespace Iviz.Ros
{
    public sealed class BagListener
    {
        [NotNull] static RoslibConnection Connection => ConnectionManager.Connection;
        
        readonly AsyncCollection<(IMessage, IRosbagConnection)> messageQueue = new AsyncCollection<(IMessage, IRosbagConnection)>();
        readonly RosbagFileWriter writer;
        readonly Task task;

        public BagListener([NotNull] string path)
        {
            writer = new RosbagFileWriter(path);
            task = Task.Run(WriteMessagesAsync);
        }
        
        public async Task StopAsync()
        {
            messageQueue.CompleteAdding();
            await task;
            writer.Dispose();
        }

        internal void EnqueueMessage([NotNull] IMessage msg, [NotNull] IRosbagConnection connection)
        {
            messageQueue.Add((msg, connection));
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