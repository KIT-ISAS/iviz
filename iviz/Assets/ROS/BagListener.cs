#nullable enable

using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    public sealed class BagListener
    {
        readonly ChannelWriter<(IMessage, IRosConnection, time)> messageWriter;

        readonly Task task;
        readonly string path;
        bool disposed;

        public long Length { get; private set; }

        public BagListener(string path)
        {
            this.path = path;
            if (Settings.IsMobile)
            {
                string filename = Path.GetFileName(path);
                RosLogger.Info($"{this}: Writing rosbag to file '{filename}' in the app's /bags folder");
            }
            else
            {
                RosLogger.Info($"{this}: Writing rosbag to path {path}");
            }

            var options = new UnboundedChannelOptions { SingleReader = true };
            var messageChannel = Channel.CreateUnbounded<(IMessage, IRosConnection, time)>(options);

            var messageReader = messageChannel.Reader;
            messageWriter = messageChannel.Writer;
            task = TaskUtils.Run(() => WriteMessagesAsync(messageReader).AwaitNoThrow(this));
        }

        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                RosLogger.Error($"{this}: Tried to close rosbag file twice");
                return;
            }

            disposed = true;
            messageWriter.Complete();
            await task.AwaitNoThrow(this); // shouldn't throw

            if (Settings.IsMobile)
            {
                string filename = Path.GetFileName(path);
                RosLogger.Info($"{this}: Closing rosbag file '{filename}' in the app's /bags folder");
            }
            else
            {
                RosLogger.Info($"{this}: Closing rosbag on path {path}");
            }
        }

        internal void EnqueueMessage(IMessage msg, MessageInfo messageInfo)
        {
            try
            {
                //messageQueue.Add((msg, connection, time.Now()));
                messageWriter.TryWrite((msg, messageInfo.Connection, time.Now()));
            }
            catch (ChannelClosedException)
            {
                // bag is closing! ignore 
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Exception during {nameof(EnqueueMessage)}", e);
            }
        }

        async ValueTask WriteMessagesAsync(ChannelReader<(IMessage, IRosConnection, time)> reader)
        {
            // keep the writer within this function!
            await using var writer = await RosbagFileWriter.CreateAsync(path);
            
            try
            {
                while (true)
                {
                    //var (message, connection, time) = await messageQueue.TakeAsync();
                    var (message, connection, time) = await reader.ReadAsync();
                    await writer.WriteAsync(message, connection, time);
                    Length = writer.Length;
                }
            }
            catch (ChannelClosedException)
            {
                // bag is closing! ignore 
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Exception during {nameof(WriteMessagesAsync)}", e);
            }

            Length = writer.Length;
        }

        public override string ToString() => $"[{nameof(BagListener)}]";
    }
}