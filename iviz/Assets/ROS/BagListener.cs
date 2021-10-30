﻿using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Rosbag.Writer;
using Iviz.Tools;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;
using Logger = Iviz.Tools.Logger;

namespace Iviz.Ros
{
    public sealed class BagListener
    {
        readonly AsyncCollection<(IMessage, IRosConnection, time)> messageQueue =
            new AsyncCollection<(IMessage, IRosConnection, time)>();

        readonly Task task;
        readonly string path;
        bool disposed;

        public long Length { get; private set; }

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
            task = Task.Run(WriteMessagesAsync);
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                Logger.LogError("Tried to close rosbag file twice");
                return;
            }

            disposed = true; 
            messageQueue.CompleteAdding();
            await task;
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
                messageQueue.Add((msg, connection, time.Now()));
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
            RosbagFileWriter writer = null;
            
            try
            {
                writer = await RosbagFileWriter.CreateAsync(path);
                while (true)
                {
                    var (message, connection, time) = await messageQueue.TakeAsync();
                    await writer.WriteAsync(message, connection, time);
                    Length = writer.Length;
                }
            }
            catch (InvalidOperationException)
            {
                // bag is closing! ignore 
            }
            catch (Exception e)
            {
                Core.Logger.Debug($"{this}: Exception during WriteMessagesAsync: ", e);
            }
            finally
            {
                if (writer != null)
                {
                    Length = writer.Length;
                    await writer.DisposeAsync().AwaitNoThrow(this);
                }
            }
        }

        [NotNull]
        public override string ToString()
        {
            return "[BagListener]";
        }
    }
}