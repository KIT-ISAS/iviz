using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.TfHelpers
{
    public class TfManager : IDisposable
    {
        readonly ConcurrentDictionary<string, Frame> frames = new();
        readonly RosChannelReader<TFMessage>? reader;
        readonly RosChannelWriter<TFMessage>? writer;
        uint seqId;

        public TfManager()
        {
            frames["map"] = new Frame(Transform.Identity);
        }

        public TfManager(IRosClient client) : this()
        {
            reader = client.CreateReader<TFMessage>("/tf");
            writer = client.CreateWriter<TFMessage>("/tf");
        }

        public TfManager(RosChannelWriter<TFMessage> writer) : this()
        {
            this.writer = writer;
        }

        void Update(in TransformStamped ts)
        {
            if (string.IsNullOrEmpty(ts.ChildFrameId))
            {
                return;
            }

            frames[ts.ChildFrameId] = new Frame(ts);

            string parentId = ts.Header.FrameId;
            if (!string.IsNullOrEmpty(parentId) && !frames.ContainsKey(parentId))
            {
                frames[parentId] = new Frame(Transform.Identity);
            }
        }

        public void UpdateAll(CancellationToken token = default)
        {
            if (reader == null)
            {
                throw new NullReferenceException("Reader not set!");
            }

            foreach (var tfMessage in reader.ReadAll(token))
            {
                foreach (var transforms in tfMessage.Transforms)
                {
                    Update(transforms);
                }
            }
        }

        public void UpdateInBackground(CancellationToken token = default)
        {
            if (reader == null)
            {
                throw new NullReferenceException("Reader not set!");
            }

            TaskUtils.Run(() => UpdateAllAsync(token).AsTask(), token);
        }

        public async ValueTask UpdateAllAsync(CancellationToken token = default)
        {
            if (reader == null)
            {
                throw new NullReferenceException("Reader not set!");
            }

            await foreach (var tfMessage in reader.ReadAllAsync(token))
            {
                foreach (var transforms in tfMessage.Transforms)
                {
                    Update(transforms);
                }
            }
        }

        void GetAbsoluteTransform(string frame, out Transform transform, out string root)
        {
            root = frame;
            transform = Transform.Identity;
            
            while (frames.TryGetValue(root, out Frame ts))
            {
                transform = ts.transform * transform;

                if (ts.parent.Length > 0)
                {
                    root = ts.parent;
                }
                else
                {
                    break;
                }
            }
        }

        public Transform GetTransformToRoot(string fromFrame)
        {
            GetAbsoluteTransform(fromFrame, out var transform, out _);
            return transform;
        }

        public Transform GetLocalTransform(string fromFrame)
        {
            return frames.TryGetValue(fromFrame, out Frame ts) ? ts.transform : Transform.Identity;
        }

        public bool TryGetTransformTo(string fromFrame, string toFrame, out Transform transform)
        {
            GetAbsoluteTransform(fromFrame, out var fromTransform, out string fromRoot);
            GetAbsoluteTransform(toFrame, out var toTransform, out string toRoot);
            if (toRoot != fromRoot)
            {
                transform = default;
                return false;
            }

            transform = toTransform.Inverse * fromTransform;
            return true;
        }


        // --------------

        public void WaitForAnySubscriber()
        {
            writer?.Publisher.WaitForAnySubscriber();
        }
        
        public void WaitForAnyPublisher()
        {
            reader?.Subscriber.WaitForAnyPublisher();
        }
        
        public void Publish(string frameId, string parentId) => Publish(frameId, parentId, Transform.Identity);

        public void Publish(string frameId, in Transform transform)
        {
            string parentId = frames.TryGetValue(frameId, out var frame) ? frame.parent : "";
            frames[frameId] = new Frame(parentId, transform, true);

            if (writer == null)
            {
                return;
            }

            PublishSingle(frameId, parentId, transform);
        }

        public void Publish(string frameId, in Quaternion rotation)
        {
            var (parentId, transform) =
                frames.TryGetValue(frameId, out var frame)
                    ? (frame.parent, frame.transform.WithRotation(rotation))
                    : ("", Transform.Identity.WithRotation(rotation));

            PublishSingle(frameId, parentId, transform);
        }

        public void Publish(string frameId, string parentId, in Transform transform)
        {
            frames[frameId] = new Frame(parentId, transform, true);

            if (writer == null)
            {
                return;
            }

            PublishSingle(frameId, parentId, transform);
        }

        public void Publish(TFMessage message)
        {
            foreach (var transform in message.Transforms)
            {
                frames[transform.ChildFrameId] = new Frame(transform, true);
            }

            writer?.Write(message);
        }

        void PublishSingle(string frameId, string parentId, in Transform transform)
        {
            var msg = new TFMessage(new[]
            {
                new TransformStamped(
                    new Header(seqId++, time.Now(), parentId),
                    frameId,
                    transform
                )
            });

            writer?.Write(msg);
        }

        public void PublishOwn()
        {
            if (writer == null)
            {
                return;
            }

            var transforms = frames
                .Where(pair => pair.Value.isOwn)
                .Select(pair => new TransformStamped(
                    new Header(seqId++, time.Now(), pair.Value.parent), pair.Key, pair.Value.transform))
                .ToArray();

            var msg = new TFMessage(transforms);
            writer.Write(msg);
            writer.Publisher.LatchedMessage = msg;
        }

        public void PublishInBackground(CancellationToken token = default)
        {
            TaskUtils.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(3000, token);
                    PublishOwn();
                }
            }, token);
        }

        public void Dispose()
        {
            reader?.Dispose();
            writer?.Dispose();
        }

        readonly struct Frame
        {
            public readonly string parent;
            public readonly Transform transform;
            public readonly bool isOwn;

            public Frame(string parent, in Transform transform, bool isOwn = false) =>
                (this.parent, this.transform, this.isOwn) = (parent, transform, isOwn);

            public Frame(in TransformStamped ts, bool isOwn = false) =>
                (parent, transform, this.isOwn) = (ts.Header.FrameId, ts.Transform, isOwn);

            public Frame(in Transform transform) => (parent, this.transform, isOwn) = ("", transform, false);

            public override string ToString() => (parent, isOwn, transform).ToString();
        }
    }
}