using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Roslib;

namespace Iviz.TfManager
{
    public class TfManager
    {
        readonly ConcurrentDictionary<string, TransformStamped> frames = new();

        public bool TryGetTransform(string frame, out TransformStamped t) => frames.TryGetValue(frame, out t);

        void Update(in TransformStamped ts)
        {
            if (string.IsNullOrEmpty(ts.ChildFrameId))
            {
                return;
            }

            frames[ts.ChildFrameId!] = ts;

            string? parentId = ts.Header.FrameId;
            if (!string.IsNullOrEmpty(parentId) && !frames.ContainsKey(parentId!))
            {
                frames.TryAdd(parentId!, new TransformStamped
                {
                    Header = (0, ts.Header.Stamp, ""),
                    ChildFrameId = parentId,
                    Transform = Transform.Identity
                });
            }
        }

        public void Update(TFMessage tfMessage)
        {
            foreach (var transformStamped in tfMessage.Transforms)
            {
                Update(transformStamped);
            }
        }

        public void UpdateAll(RosChannelReader<TFMessage> reader, CancellationToken token = default)
        {
            foreach (var tfMessage in reader.ReadAll(token))
            {
                Update(tfMessage);
            }
        }


#if !NETSTANDARD2_0
        public async Task UpdateAllAsync(RosChannelReader<TFMessage> reader, CancellationToken token = default)
        {
            await foreach (var tfMessage in reader.ReadAllAsync(token))
            {
                Update(tfMessage);
            }
        }
#endif

        bool TryGetTransformToRoot(string frame, out (Transform transform, string root) t)
        {
            t.root = frame ?? throw new ArgumentNullException(nameof(frame));
            t.transform = Transform.Identity;

            while (frames.TryGetValue(t.root!, out TransformStamped ts) && !string.IsNullOrEmpty(ts.Header.FrameId))
            {
                t.root = ts.Header.FrameId!;
                t.transform = ts.Transform * t.transform;
            }

            return t.root != frame;
        }

        public bool TryGetTransformTo(string fromFrame, string toFrame, out Transform transform)
        {
            if (!TryGetTransformToRoot(fromFrame, out var from) ||
                !TryGetTransformToRoot(toFrame, out var to) ||
                to.root != from.root)
            {
                transform = default;
                return false;
            }

            transform = to.transform.Inverse * from.transform;
            return true;
        }
    }
}