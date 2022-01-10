#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers.TF
{
    public sealed class TfListener : IController, IHasFrame
    {
        public const string OriginFrameId = "[origin]";
        const string DefaultTopicStatic = "/tf_static";

        const int MaxQueueSize = 10000;

        public const string DefaultTopic = "/tf";
        const string MapFrameId = "map";

        static TfListener? instance;
        static uint tfSeq;

        uint tapSeq;
        Pose cachedOriginPose = Pose.identity;

        readonly TfConfiguration config = new();

        readonly ConcurrentQueue<(TransformStamped[] frame, string? callerId, bool isStatic)> incomingMessages = new();
        readonly ConcurrentBag<TransformStamped> outgoingMessages = new();

        readonly Dictionary<string, TfFrame> frames = new();
        readonly FrameNode keepAllListenerNode;
        readonly FrameNode staticListenerNode;
        readonly FrameNode fixedFrameListenerNode;

        readonly TfFrame mapFrame;
        readonly TfFrame rootFrame;
        readonly TfFrame originFrame;
        readonly TfFrame unityFrame;

        public static TfListener Instance =>
            instance ?? throw new NullReferenceException("No TFListener has been set!");

        public static bool HasInstance => instance != null;
        public static TfFrame RootFrame => Instance.rootFrame;
        public static TfFrame OriginFrame => Instance.originFrame;
        public static TfFrame UnityFrame => Instance.unityFrame;
        public static TfFrame ListenersFrame => OriginFrame;
        public static TfFrame DefaultFrame => OriginFrame;

        public static IEnumerable<string> FramesUsableAsHints =>
            Instance.frames.Values.Where(IsFrameUsableAsHint).Select(frame => frame.Id);

        public static event Action? AfterProcessMessages;

        public Listener<TFMessage> Listener { get; }
        public Listener<TFMessage> ListenerStatic { get; }

        public Sender<TFMessage> Publisher { get; }

        //public Sender<PoseStamped> TapPublisher { get; }
        public TfFrame FixedFrame { get; private set; }
        public TfFrame Frame => FixedFrame;
        public event Action? ResetFrames;

        public static float RootScale
        {
            set => RootFrame.transform.localScale = value * Vector3.one;
        }

        public TfConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
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
                foreach (TfFrame frame in frames.Values)
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
                foreach (TfFrame frame in frames.Values)
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
                if (FixedFrame != null)
                {
                    FixedFrame.RemoveListener(fixedFrameListenerNode);
                }

                if (string.IsNullOrEmpty(value))
                {
                    FixedFrame = mapFrame;
                    originFrame.Transform.SetLocalPose(Pose.identity);
                    Config.FixedFrameId = "";
                    return;
                }

                Config.FixedFrameId = value;
                var frame = GetOrCreateFrame(value, fixedFrameListenerNode);
                FixedFrame = frame;
                originFrame.Transform.SetLocalPose(frame.OriginWorldPose.Inverse());
            }
        }

        public TfListener(TfConfiguration? config, string topic)
        {
            instance = this;

            try
            {
                unityFrame = Add(CreateFrameObject("TF", null, null));
                unityFrame.ForceInvisible = true;
                unityFrame.Visible = false;

                var defaultListener = FrameNode.Instantiate("[.]");
                unityFrame.AddListener(defaultListener);

                keepAllListenerNode = FrameNode.Instantiate("[TFNode]");
                staticListenerNode = FrameNode.Instantiate("[TFStatic]");
                fixedFrameListenerNode = FrameNode.Instantiate("[TFFixedFrame]");
                defaultListener.transform.parent = unityFrame.Transform;

                rootFrame = Add(CreateFrameObject("/", unityFrame.Transform, unityFrame));
                rootFrame.ForceInvisible = true;
                rootFrame.Visible = false;
                rootFrame.AddListener(defaultListener);

                originFrame = Add(CreateFrameObject(OriginFrameId, rootFrame.Transform, rootFrame));
                originFrame.Parent = rootFrame;
                originFrame.ForceInvisible = true;
                originFrame.Visible = false;
                originFrame.AddListener(defaultListener);
                originFrame.ParentCanChange = false;

                mapFrame = Add(CreateFrameObject(MapFrameId, originFrame.Transform, originFrame));
                mapFrame.Parent = originFrame;
                mapFrame.AddListener(defaultListener);
                FixedFrame = mapFrame;

                Publisher = new Sender<TFMessage>(DefaultTopic);
                //TapPublisher = new Sender<PoseStamped>(DefaultTapTopic);

                Listener = new Listener<TFMessage>(DefaultTopic, HandlerNonStatic,
                    PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp);
                ListenerStatic = new Listener<TFMessage>(DefaultTopicStatic, HandlerStatic);

                Config = config ?? new TfConfiguration
                {
                    Topic = topic
                };

                GameThread.LateEveryFrame += LateUpdate;
            }
            catch (Exception)
            {
                instance = null;
                throw;
            }
        }

        public int NumFrames => frames.Count;

        static readonly Func<TfFrame, bool> IsFrameUsableAsHint =
            frame => frame != RootFrame && frame != UnityFrame && frame != OriginFrame;

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
                var (transforms, callerId, isStatic) = value;
                foreach (var (parentIdUnchecked, childIdUnchecked, rosTransform, _) in transforms)
                {
                    if (rosTransform.IsInvalid()
                        || childIdUnchecked.Length == 0
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
                    if (parentIdUnchecked.Length == 0)
                    {
                        child.TrySetParent(DefaultFrame);
                        child.SetPose(rosTransform.Ros2Unity(), callerId);
                        continue;
                    }

                    string parentId = parentIdUnchecked[0] != '/'
                        ? parentIdUnchecked
                        : parentIdUnchecked[1..];

                    if (child.Parent is not null && parentId == child.Parent.Id)
                    {
                        child.SetPose(rosTransform.Ros2Unity(), callerId);
                    }
                    else
                    {
                        var parent = GetOrCreateFrame(parentId);
                        if (child.TrySetParent(parent))
                        {
                            child.SetPose(rosTransform.Ros2Unity(), callerId);
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
                    frame.SetPose(Pose.identity);
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
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Instance.TryGetFrameImpl(id, out frame);
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

            if (frameId is "")
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
                : Add(CreateFrameObject(id, DefaultFrame.Transform, DefaultFrame));
        }

        TfFrame CreateFrameObject(string id, Transform? parent, TfFrame? parentFrame)
        {
            var frame = Resource.Displays.TfFrame.Instantiate<TfFrame>(parent);
            frame.Setup(id);
            frame.Visible = config.Visible;
            frame.FrameSize = config.FrameSize;
            frame.LabelVisible = config.FrameLabelsVisible;
            frame.ConnectorVisible = config.ParentConnectorVisible;
            if (parentFrame != null)
            {
                frame.Parent = parentFrame;
            }

            return frame;
        }

        bool TryGetFrameImpl(string id, [NotNullWhen(true)] out TfFrame? t)
        {
            return frames.TryGetValue(id, out t);
        }

        bool HandlerNonStatic(TFMessage msg, IRosReceiver? receiver)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, receiver?.RemoteId, false));
            return true;
        }

        bool HandlerStatic(TFMessage msg, IRosReceiver? receiver)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, receiver?.RemoteId, true));
            return true;
        }

        public void MarkAsDead(TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            frames.Remove(frame.Id);
            frame.DestroySelf();
        }

        void ProcessWorldOffset()
        {
            var originPose = FixedFrame.OriginWorldPose;

            if (FlipZ)
            {
                var rotateAroundForward = new Quaternion(0, 0, 1, 0); // 180 deg around forward axis
                originPose.rotation = rotateAroundForward * originPose.rotation;
                originPose.position = rotateAroundForward * originPose.position;
            }

            if (originPose.EqualsApprox(cachedOriginPose))
            {
                return;
            }

            cachedOriginPose = originPose;
            OriginFrame.Transform.SetLocalPose(originPose.Inverse());
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
            staticListenerNode.DestroySelf();
            ResetFrames = null;
            AfterProcessMessages = null;
            instance = null;
        }

        public static Vector3 RelativeToOrigin(in Vector3 unityPosition) =>
            OriginFrame.Transform.InverseTransformPoint(unityPosition);

        public static Pose RelativeToOrigin(in Pose unityPose)
        {
            var originFrame = OriginFrame.Transform;
            var (position, rotation) = unityPose;

            Pose p;
            p.position = originFrame.InverseTransformPoint(position);
            p.rotation = originFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose RelativeToFixedFrame(in Pose unityPose)
        {
            // equals unityPose unless in AR mode or flipped z 
            var fixedFrame = Instance.FixedFrame.Transform;
            var (position, rotation) = unityPose;
            
            Pose p;
            p.position = fixedFrame.InverseTransformPoint(position);
            p.rotation = fixedFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose FixedFramePose => Instance.FixedFrame.Transform.AsPose();

        public static void Publish(string childFrame, in Pose absoluteUnityPose)
        {
            var relativePose = RelativeToFixedFrame(absoluteUnityPose);
            Publish(FixedFrameId, childFrame, relativePose.Unity2RosTransform());
        }

        public static void Publish(string childFrame, in Msgs.GeometryMsgs.Transform rosTransform) =>
            Publish(FixedFrameId, childFrame, rosTransform);

        public static void Publish(string parentFrame, string childFrame, in Msgs.GeometryMsgs.Transform rosTransform)
        {
            TransformStamped transformStamped;
            transformStamped.Header = CreateHeader(tfSeq++, parentFrame);
            transformStamped.ChildFrameId = ResolveFrameId(childFrame);
            transformStamped.Transform = rosTransform;
            instance?.outgoingMessages.Add(transformStamped);
        }

        public static void Publish(TfFrame frame)
        {
            string parentFrameId = frame.Parent == OriginFrame ? "" : frame.Id;
            Publish(parentFrameId, frame.Id, frame.Transform.AsLocalPose().Unity2RosTransform());
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
                incomingMessages.Enqueue((messages, null, false));
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