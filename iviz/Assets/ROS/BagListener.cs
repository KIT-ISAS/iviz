﻿#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using Iviz.Tools;
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

            task = TaskUtils.Run(() => WriteMessagesAsync().AsTask());
        }

        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                RosLogger.Error($"{this}: Tried to close rosbag file twice");
                return;
            }

            disposed = true;
            messageQueue.CompleteAdding();
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
                RosLogger.Debug($"{this}: Exception during {nameof(EnqueueMessage)}", e);
            }
        }

        async ValueTask WriteMessagesAsync()
        {
            // keep the writer within this function!
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
                RosLogger.Debug($"{this}: Exception during {nameof(WriteMessagesAsync)}", e);
            }

            if (writer != null)
            {
                Length = writer.Length;
            }
        }

        public override string ToString() => $"[{nameof(BagListener)}]";
    }
}