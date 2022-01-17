#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers.TF
{
    public sealed class TfListener : IController, IHasFrame
    {
        public const string OriginFrameId = "[origin]";
        public const string DefaultTopic = "/tf";
        const string DefaultTopicStatic = "/tf_static";
        const string MapFrameId = "map";
        const int MaxQueueSize = 10000;

        static TfListener? instance;
        static uint tfSeq;

        readonly TfConfiguration config = new();
        readonly Func<string, TfFrame> frameFactory;

        readonly ConcurrentQueue<(TransformStamped[] frame, bool isStatic)> incomingMessages = new();
        readonly ConcurrentBag<TransformStamped> outgoingMessages = new();

        readonly Dictionary<string, TfFrame> frames = new();
        readonly FrameNode keepAllListenerNode;
        readonly FrameNode staticListenerNode;
        readonly FrameNode fixedFrameListenerNode;

        readonly TfFrame mapFrame;
        readonly TfFrame rootFrame;
        readonly TfFrame originFrame;
        readonly GameObject unityFrame;

        TfFrame fixedFrame;
        Pose cachedFixedPose = Pose.identity;

        public static TfListener Instance =>
            instance ?? throw new NullReferenceException("No TFListener has been set!");

        public static TfFrame RootFrame => Instance.rootFrame;
        public static TfFrame OriginFrame => Instance.originFrame;
        public static Transform UnityFrameTransform => Instance.unityFrame.transform;
        public static TfFrame ListenersFrame => OriginFrame;
        public static TfFrame DefaultFrame => OriginFrame;
        public static TfFrame FixedFrame => Instance.fixedFrame;
        public static IEnumerable<string> FrameNames => Instance.frames.Keys;
        public static event Action? AfterProcessMessages;

        public Listener<TFMessage> Listener { get; }
        public Listener<TFMessage> ListenerStatic { get; }
        public Sender<TFMessage> Publisher { get; }
        public TfFrame Frame => FixedFrame;
        public event Action? ResetFrames;

        public static float RootScale
        {
            set => RootFrame.Transform.localScale = value * Vector3.one;
        }

        public TfConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = DefaultTopic;
                FramesVisible = value.Visible;
                FrameSize = value.FrameSize;
                FrameLabelsVisible = value.FrameLabelsVisible;
                ParentConnectorVisible = value.ParentConnectorVisible;
                KeepAllFrames = value.KeepAllFrames;
                FixedFrameId = value.FixedFrameId;
                PreferUdp = value.PreferUdp;
                FlipZ = value.FlipZ;
            }
        }

        public bool FlipZ
        {
            get => config.FlipZ;
            set => config.FlipZ = value;
        }

        public bool FramesVisible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                foreach (var frame in frames.Values)
                {
                    frame.Visible = value;
                }
            }
        }

        public bool Visible
        {
            get => FramesVisible;
            set => FramesVisible = value;
        }

        public bool FrameLabelsVisible
        {
            get => config.FrameLabelsVisible;
            set
            {
                config.FrameLabelsVisible = value;
                foreach (var frame in frames.Values)
                {
                    frame.LabelVisible = value;
                }
            }
        }

        public float FrameSize
        {
            get => config.FrameSize;
            set
            {
                config.FrameSize = value;
                foreach (TfFrame frame in frames.Values)
                {
                    frame.FrameSize = value;
                }
            }
        }

        public bool ParentConnectorVisible
        {
            get => config.ParentConnectorVisible;
            set
            {
                config.ParentConnectorVisible = value;
                foreach (TfFrame frame in frames.Values)
                {
                    frame.ConnectorVisible = value;
                }
            }
        }

        public bool KeepAllFrames
        {
            get => config.KeepAllFrames;
            set
            {
                config.KeepAllFrames = value;
                if (value)
                {
                    foreach (var frame in frames.Values)
                    {
                        frame.AddListener(keepAllListenerNode);
                    }
                }
                else
                {
                    // here we remove unused frames
                    // we create a copy because this generally modifies the collection
                    var framesCopy = frames.Values.ToList();
                    foreach (var frame in framesCopy)
                    {
                        frame.RemoveListener(keepAllListenerNode);
                    }
                }
            }
        }

        bool PreferUdp
        {
            get => config.PreferUdp;
            set => config.PreferUdp = value;
        }

        public static string FixedFrameId
        {
            get => Instance.FixedFrameIdImpl;
            set => Instance.FixedFrameIdImpl = value;
        }

        string FixedFrameIdImpl
        {
            get => FixedFrame.Id;
            set
            {
                FixedFrame.RemoveListener(fixedFrameListenerNode);

                if (string.IsNullOrEmpty(value))
                {
                    fixedFrame = mapFrame;
                    originFrame.Transform.SetLocalPose(Pose.identity);
                    config.FixedFrameId = "";
                    return;
                }

                config.FixedFrameId = value;
                var frame = GetOrCreateFrame(value, fixedFrameListenerNode);
                fixedFrame = frame;
                originFrame.Transform.SetLocalPose(frame.OriginWorldPose.Inverse());
            }
        }

        public TfListener(TfConfiguration? config, Func<string, TfFrame> frameFactory)
        {
            instance = this;

            this.frameFactory = frameFactory ?? throw new ArgumentNullException(nameof(frameFactory));

            try
            {
                // hierarchy:  unityFrame ("TF") -> rootFrame -> originFrame -> fixedFrame
                // unityFrame: container for all TF stuff. has no parents. 
                // rootFrame:  managed by the AR system. any movements in the AR origin are instead reversed and applied to rootFrame.
                //             the root frame may have a uniform scale that is not 1. 
                // originFrame: managed by the fixed frame. originFrame is set to the inverse of the fixedFrame pose,
                //              to ensure that fixedFrame is always on the unity origin (excluding AR changes in root).
                //              the transformation also applies FlipZ if active.
                // fixedFrame: whatever TF frame the user has selected as fixed.
                // mapFrame:   the default fixed frame. cannot be deleted, to ensure that there is always at least
                //             one visible frame.

                unityFrame = GameObject.Find("TF").CheckedNull() ?? new GameObject("TF");

                rootFrame = CreateFrameObject("/", null);
                rootFrame.Transform.parent = unityFrame.transform;
                rootFrame.ForceInvisible();

                originFrame = CreateFrameObject(OriginFrameId, rootFrame);
                originFrame.Parent = rootFrame;
                originFrame.ForceInvisible();

                keepAllListenerNode = new FrameNode("[TFNode]") { Visible = false };
                staticListenerNode = new FrameNode("[TFStatic]") { Visible = false };
                fixedFrameListenerNode = new FrameNode("[TFFixedFrame]") { Visible = false };
                var defaultListener = new FrameNode("[.]")
                {
                    Visible = false,
                    Transform = { parent = unityFrame.transform }
                };

                rootFrame.AddListener(defaultListener);
                originFrame.AddListener(defaultListener);

                mapFrame = Add(CreateFrameObject(MapFrameId, originFrame));
                mapFrame.Parent = originFrame;
                mapFrame.AddListener(defaultListener);
                fixedFrame = mapFrame;

                Publisher = new Sender<TFMessage>(DefaultTopic);
                Listener = new Listener<TFMessage>(DefaultTopic, HandlerNonStatic,
                    PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp);
                ListenerStatic = new Listener<TFMessage>(DefaultTopicStatic, HandlerStatic);

                Config = config ?? new TfConfiguration { Topic = DefaultTopic };

                GameThread.LateEveryFrame += LateUpdate;
            }
            catch (Exception)
            {
                instance = null;
                throw;
            }
        }

        public int NumFrames => frames.Count;

        static bool IsValid(in Msgs.GeometryMsgs.Vector3 v)
        {
            const int maxPoseMagnitude = 10000;
            return Math.Abs(v.X) < maxPoseMagnitude
                   && Math.Abs(v.Y) < maxPoseMagnitude
                   && Math.Abs(v.Z) < maxPoseMagnitude;
        }

        void ProcessMessages()
        {
            TfFrame? lastChild = null;
            bool keepAllFrames = KeepAllFrames;

            while (incomingMessages.TryDequeue(out var value))
            {
                var (transforms, isStatic) = value;
                foreach (var (parentIdUnchecked, childIdUnchecked, rosTransform, _) in transforms)
                {
                    if (childIdUnchecked.Length == 0
                        || rosTransform.IsInvalid()
                        || !IsValid(rosTransform.Translation))
                    {
                        continue;
                    }

                    // remove starting '/' from tf v1
                    string childId = childIdUnchecked[0] != '/'
                        ? childIdUnchecked
                        : childIdUnchecked[1..];

                    TfFrame? child;
                    if (lastChild is not null && lastChild.Id == childId)
                    {
                        child = lastChild;
                    }
                    else if (isStatic)
                    {
                        child = GetOrCreateFrame(childId, staticListenerNode);
                        if (keepAllFrames)
                        {
                            child.AddListener(keepAllListenerNode);
                        }
                    }
                    else if (keepAllFrames)
                    {
                        child = GetOrCreateFrame(childId, keepAllListenerNode);
                    }
                    else if (!TryGetFrameImpl(childId, out child))
                    {
                        continue;
                    }

                    lastChild = child;

                    var unityPose = rosTransform.Ros2Unity();

                    if (parentIdUnchecked.Length == 0)
                    {
                        child.TrySetParent(DefaultFrame);
                        child.SetLocalPose(unityPose);
                        continue;
                    }

                    string parentId = parentIdUnchecked[0] != '/'
                        ? parentIdUnchecked
                        : parentIdUnchecked[1..];

                    if (child.Parent is not null && parentId == child.Parent.Id)
                    {
                        child.SetLocalPose(unityPose);
                    }
                    else
                    {
                        var parent = GetOrCreateFrame(parentId);
                        if (child.TrySetParent(parent))
                        {
                            child.SetLocalPose(unityPose);
                        }
                    }
                }
            }

            AfterProcessMessages?.Invoke();
        }

        public void ResetController()
        {
            Listener.Reset();
            ListenerStatic.Reset();
            Publisher.Reset();

            ResetFrames?.Invoke();

            bool prevKeepAllFrames = KeepAllFrames;

            var framesCopy = frames.Values.ToList();
            foreach (var frame in framesCopy)
            {
                if (frame.TrySetParent(OriginFrame))
                {
                    frame.SetLocalPose(Pose.identity);
                }
            }

            foreach (var frame in framesCopy)
            {
                frame.RemoveListener(staticListenerNode);
            }

            KeepAllFrames = false;
            KeepAllFrames = prevKeepAllFrames;
        }

        TfFrame Add(TfFrame t)
        {
            frames.Add(t.Id, t);
            return t;
        }

        public static bool TryGetFrame(string id, [NotNullWhen(true)] out TfFrame? frame)
        {
            return Instance.TryGetFrameImpl(id ?? throw new ArgumentNullException(nameof(id)), out frame);
        }

        public static string ResolveFrameId(string frameId)
        {
            if (string.IsNullOrEmpty(frameId))
            {
                return FixedFrameId;
            }

            if (frameId[0] != '~')
            {
                return frameId;
            }

            string? myId = ConnectionManager.MyId;
            if (frameId.Length == 1)
            {
                return myId ?? FixedFrameId;
            }

            string frameIdSuffix = frameId[1] == '/' ? frameId[2..] : frameId[1..];
            if (myId == null)
            {
                return frameIdSuffix;
            }

            return myId[0] == '/'
                ? $"{myId[1..]}/{frameIdSuffix}"
                : $"{myId}/{frameIdSuffix}";
        }

        public static TfFrame GetOrCreateFrame(string frameId, FrameNode? listener = null)
        {
            if (frameId is null)
            {
                throw new ArgumentNullException(nameof(frameId));
            }

            if (frameId.Length == 0)
            {
                throw new ArgumentException("Cannot create frame with empty name", nameof(frameId));
            }

            string validatedFrameId = frameId[0] switch
            {
                '~' => ResolveFrameId(frameId),
                '/' => frameId[1..],
                _ => frameId
            };

            var frame = Instance.GetOrCreateFrameImpl(validatedFrameId);

            if (listener != null)
            {
                frame.AddListener(listener);
            }

            return frame;
        }

        TfFrame GetOrCreateFrameImpl(string id)
        {
            return TryGetFrameImpl(id, out TfFrame? frame)
                ? frame
                : Add(CreateFrameObject(id, DefaultFrame));
        }

        TfFrame CreateFrameObject(string id, TfFrame? parentFrame)
        {
            var frame = frameFactory(id);
            frame.Visible = config.Visible;
            frame.FrameSize = config.FrameSize;
            frame.LabelVisible = config.FrameLabelsVisible;
            frame.ConnectorVisible = config.ParentConnectorVisible;
            frame.Parent = parentFrame;

            return frame;
        }

        bool TryGetFrameImpl(string id, [NotNullWhen(true)] out TfFrame? t)
        {
            return frames.TryGetValue(id, out t);
        }

        bool HandlerNonStatic(TFMessage msg, IRosReceiver? _)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, false));
            return true;
        }

        bool HandlerStatic(TFMessage msg, IRosReceiver? _)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, true));
            return true;
        }

        public void MarkAsDead(TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            frames.Remove(frame.Id);
            frame.Dispose();
        }

        void ProcessWorldOffset()
        {
            // task: move fixed frame so that it has identity transform to root frame
            // hierarchy: root -> origin -> fixed

            Pose fixedFramePose; // fixed relative to origin
            if (FlipZ)
            {
                var (position, rotation) = FixedFrame.OriginWorldPose;
                var rotateAroundForward = new Quaternion(0, 0, 1, 0); // 180 deg around forward axis
                fixedFramePose.rotation = rotateAroundForward * rotation;
                fixedFramePose.position = rotateAroundForward * position;
            }
            else
            {
                fixedFramePose = FixedFrame.OriginWorldPose;
            }

            if (fixedFramePose.EqualsApprox(cachedFixedPose))
            {
                return;
            }

            cachedFixedPose = fixedFramePose;
            OriginFrame.Transform.SetLocalPose(fixedFramePose.Inverse());
        }

        void LateUpdate()
        {
            ProcessMessages();
            ProcessWorldOffset();
            DoPublish();
        }

        public void Dispose()
        {
            Listener.Dispose();
            ListenerStatic.Dispose();
            Publisher.Dispose();

            GameThread.LateEveryFrame -= LateUpdate;
            staticListenerNode.Dispose();
            ResetFrames = null;
            AfterProcessMessages = null;
            instance = null;
            tfSeq = 0;
        }

        public static Vector3 RelativeToOrigin(in Vector3 absoluteUnityPosition) =>
            OriginFrame.Transform.InverseTransformPoint(absoluteUnityPosition);

        public static Pose RelativeToOrigin(in Pose absoluteUnityPose)
        {
            var originFrame = OriginFrame.Transform;
            var (position, rotation) = absoluteUnityPose;

            Pose p;
            p.position = originFrame.InverseTransformPoint(position); // may contain scaling
            p.rotation = originFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose RelativeToFixedFrame(in Pose absoluteUnityPose)
        {
            // equals unityPose unless in AR mode or flipped z 
            var fixedFrame = FixedFrame.Transform;
            var (position, rotation) = absoluteUnityPose;

            Pose p;
            p.position = fixedFrame.InverseTransformPoint(position); // may contain scaling
            p.rotation = fixedFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose FixedFrameToAbsolute(in Pose relativePose)
        {
            // equals unityPose unless in AR mode or flipped z 
            var fixedFrame = FixedFrame.Transform;
            var (position, rotation) = relativePose;

            Pose p;
            p.position = fixedFrame.TransformPoint(position); // may contain scaling
            p.rotation = fixedFrame.rotation * rotation;
            return p;
        }

        public static void Publish(TfFrame frame)
        {
            string parentFrame = frame.Parent == OriginFrame ? "" : frame.Id;
            string childFrame = frame.Id;
            var localPose = frame.Transform.AsLocalPose();
            if (localPose.position.IsInvalid() || localPose.rotation.IsInvalid())
            {
                RosLogger.Error(
                    $"TfListener: Cannot publish invalid transform: ChildFrameId='{childFrame}' " +
                    $"Parent='{parentFrame}' Transform={localPose.ToString()}");
                return;
            }

            TransformStamped t;
            t.Header.Seq = tfSeq++;
            t.Header.Stamp = GameThread.TimeNow;
            t.Header.FrameId = parentFrame;
            t.ChildFrameId = ResolveFrameId(childFrame);
            localPose.Unity2Ros(out t.Transform);
            instance?.outgoingMessages.Add(t);
        }

        void DoPublish()
        {
            int count = outgoingMessages.Count;
            if (count == 0)
            {
                return;
            }

            var messages = new TransformStamped[count];
            foreach (int i in ..count)
            {
                outgoingMessages.TryTake(out messages[i]);
            }

            if (ConnectionManager.IsConnected)
            {
                Publisher.Publish(new TFMessage(messages));
            }
            else
            {
                incomingMessages.Enqueue((messages, false));
            }
        }

        /// <summary>
        /// Creates a header using the fixed frame as the frame id and the frame start as the timestamp.
        /// </summary>
        public static Header CreateHeader(uint seqId)
        {
            Header h;
            h.Seq = seqId;
            h.Stamp = GameThread.TimeNow;
            h.FrameId = FixedFrameId;
            return h;
        }

        /// <summary>
        /// Creates a header with the given frame id using the frame start as the timestamp.
        /// </summary>
        public static Header CreateHeader(uint seqId, string frameId)
        {
            Header h;
            h.Seq = seqId;
            h.Stamp = GameThread.TimeNow;
            h.FrameId = frameId;
            return h;
        }
    }
}