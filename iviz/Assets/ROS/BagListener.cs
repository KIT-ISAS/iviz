#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using Nito.AsyncEx;

namespace Iviz.Ros
{
    public sealed class BagListener
    {
        readonly AsyncCollection<(IMessage, IRosConnection, time)> messageQueue = new();

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

            task = Task.Run(WriteMessagesAsync);
        }

        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                RosLogger.Error("Tried to close rosbag file twice");
                return;
            }

            disposed = true;
            messageQueue.CompleteAdding();
            await task;
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

        internal void EnqueueMessage(IMessage msg, IRosConnection connection)
        {
            try
            {
                messageQueue.Add((msg, connection, time.Now()));
            }
            catch (InvalidOperationException)
            {
                // bag is closing! ignore 
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Exception during EnqueueMessage: ", e);
            }
        }

        async ValueTask WriteMessagesAsync()
        {
            RosbagFileWriter? writer = null;
            
            try
            {
                await using (writer = await RosbagFileWriter.CreateAsync(path))
                {
                    while (true)
                    {
                        var (message, connection, time) = await messageQueue.TakeAsync();
                        await writer.WriteAsync(message, connection, time);
                        Length = writer.Length;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // bag is closing! ignore 
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Exception during WriteMessagesAsync: ", e);
            }

            if (writer != null)
            {
                Length = writer.Length;
            }
        }

        public override string ToString() => "[BagListener]";
    }
}