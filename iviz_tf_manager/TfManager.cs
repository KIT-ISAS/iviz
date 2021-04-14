using System;
using System.Collections.Generic;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.Tf2Msgs;

namespace Iviz.TfManager
{
    public class TfManager
    {
        readonly Dictionary<string, TransformStamped> frames = new();

        public void Update(in TransformStamped ts)
        {
            if (ts.ChildFrameId == null)
            {
                return;
            }

            frames[ts.ChildFrameId] = ts;
        }

        public void Update(TFMessage t)
        {
            foreach (var ts in t.Transforms)
            {
                Update(ts);
            }
        }

        bool TryGetTransformToRoot(string frame, out (Transform transform, string root) t)
        {
            t.root = frame ?? throw new ArgumentNullException(nameof(frame));
            t.transform = Transform.Identity;
            
            while (frames.TryGetValue(t.root, out TransformStamped ts) && !string.IsNullOrEmpty(ts.Header.FrameId))
            {
                t.root = ts.Header.FrameId;
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
                transform = Transform.Identity;
                return false;
            }

            transform = to.transform.Inverse * from.transform;
            return true;
        }
    }
}