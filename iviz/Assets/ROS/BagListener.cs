using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Core;
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
            if (Settings.IsMobile)
            {
                string filename = Path.GetFileName(path);
                Logger.LogFormat("{0}: Writing rosbag to file '{1}' in the app's /bags folder", this, filename);
            }
            else
            {
                Logger.LogFormat("{0}: Writing rosbag to path {1}", this, path);
            }
            writer = new RosbagFileWriter(path);
            task = Task.Run(WriteMessagesAsync);
        }

        public async Task StopAsync()
        {
            messageQueue.CompleteAdding();
            await task;
            await writer.DisposeAsync();
            if (Settings.IsMobile)
            {
                string filename = Path.GetFileName(path);
                Logger.LogFormat("{0}: Closing rosbag file '{1}' in the app's /bags folder", this, filename);
            }
            else
            {
                Logger.LogFormat("{0}: Closing rosbag on path {1}", this, path);
            }
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
            catch (Exception e)
            {
                Core.Logger.Debug($"{this}: Exception during EnqueueMessage", e);
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
                Core.Logger.Debug($"{this}: Exception during WriteMessagesAsync", e);
            }
        }

        [NotNull]
        public override string ToString()
        {
            return "[BagListener]";
        }
    }
}