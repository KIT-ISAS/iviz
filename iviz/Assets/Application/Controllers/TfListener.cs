#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class TfListener : ListenerController
    {
        const string DefaultTapTopic = "~clicked_pose";
        const string OriginFrameId = "[origin]";
        const string DefaultTopicStatic = "/tf_static";

        const int MaxQueueSize = 10000;

        public const string DefaultTopic = "/tf";
        public const string MapFrameId = "map";

        uint tapSeq;
        static uint tfSeq;

        readonly TfConfiguration config = new();

        readonly ConcurrentQueue<(TransformStamped[] frame, bool isStatic)> messageList = new();

        readonly Dictionary<string, TfFrame> frames = new();
        readonly FrameNode keepAllListener;
        readonly FrameNode staticListener;
        readonly FrameNode fixedFrameListener;

        readonly TfFrame mapFrame;
        readonly TfFrame rootFrame;
        readonly TfFrame originFrame;
        readonly TfFrame unityFrame;

        public TfFrame FixedFrame { get; private set; }

        public static float RootScale
        {
            get => RootFrame.transform.localScale.x;
            set => RootFrame.transform.localScale = value * Vector3.one;
        }

        public TfListener(IModuleData moduleData)
        {
            instance = this;

            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            unityFrame = Add(CreateFrameObject("TF", null, null));
            unityFrame.ForceInvisible = true;
            unityFrame.Visible = false;

            var defaultListener = FrameNode.Instantiate("[.]");
            unityFrame.AddListener(defaultListener);

            keepAllListener = FrameNode.Instantiate("[TFNode]");
            staticListener = FrameNode.Instantiate("[TFStatic]");
            fixedFrameListener = FrameNode.Instantiate("[TFFixedFrame]");
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

            Config = new TfConfiguration();

            Publisher = new Sender<TFMessage>(DefaultTopic);
            TapPublisher = new Sender<PoseStamped>(DefaultTapTopic);

            GameThread.LateEveryFrame += LateUpdate;

            GuiInputModule.Instance.ShortClick += i => OnClick(i, true);
            GuiInputModule.Instance.LongClick += i => OnClick(i, false);
        }

        void OnClick(ClickInfo clickInfo, bool isShortClick)
        {
            if (clickInfo.TryGetRaycastResults(out var hitResults))
            {
                Vector3 hitPoint = hitResults[0].Position;
                bool anyHighlighted = false;
                foreach (var (gameObject, position, _) in hitResults)
                {
                    if (Vector3.Distance(position, hitPoint) > 1
                        || !TryGetHighlightable(gameObject, out var toHighlight))
                    {
                        continue;
                    }

                    toHighlight.Highlight();
                    anyHighlighted = true;
                }

                if (anyHighlighted)
                {
                    return;
                }
            }

            Pose poseToHighlight;
            if (clickInfo.TryGetARRaycastResults(out var arHitResults))
            {
                poseToHighlight = arHitResults[0].CreatePose();
            }
            else if (hitResults.Length != 0)
            {
                poseToHighlight = hitResults[0].CreatePose();
            }
            else
            {
                return;
            }

            ResourcePool.RentDisplay<ClickedPoseHighlighter>().HighlightPose(poseToHighlight);
            if (!isShortClick)
            {
                TapPublisher.Publish(new PoseStamped((tapSeq++, FixedFrameId),
                    RelativePoseToFixedFrame(poseToHighlight).Unity2RosPose()));
            }
        }

        static bool TryGetHighlightable(GameObject gameObject, out IHighlightable h)
        {
            Transform parent;
            return gameObject.TryGetComponent(out h) ||
                   (parent = gameObject.transform.parent) != null && parent.TryGetComponent(out h);
        }

        public static string FixedFrameId
        {
            get => Instance.FixedFrame.Id;
            set
            {
                if (Instance.FixedFrame != null)
                {
                    Instance.FixedFrame.RemoveListener(Instance.fixedFrameListener);
                }

                if (string.IsNullOrEmpty(value))
                {
                    Instance.FixedFrame = Instance.mapFrame;
                    OriginFrame.Transform.SetLocalPose(Pose.identity);
                    Instance.Config.FixedFrameId = "";
                    return;
                }

                Instance.Config.FixedFrameId = value;
                var frame = GetOrCreateFrame(value, Instance.fixedFrameListener);
                Instance.FixedFrame = frame;
                OriginFrame.Transform.SetLocalPose(frame.OriginWorldPose.Inverse());
            }
        }

        static TfListener? instance;
        public static bool HasInstance => instance != null;
        public static TfListener Instance => instance ?? throw new NullReferenceException("No TFListener was set!");
        public Sender<TFMessage> Publisher { get; }
        public Sender<PoseStamped> TapPublisher { get; }

        public IListener? ListenerStatic { get; private set; }

        public static TfFrame RootFrame => Instance.rootFrame;
        public static TfFrame OriginFrame => Instance.originFrame;
        public static TfFrame UnityFrame => Instance.unityFrame;
        public static TfFrame ListenersFrame => OriginFrame;
        public override TfFrame Frame => FixedFrame;
        public static TfFrame DefaultFrame => OriginFrame;

        public static IEnumerable<string> FramesUsableAsHints =>
            Instance.frames.Values.Where(IsFrameUsableAsHint).Select(frame => frame.Id);

        public override IModuleData ModuleData { get; }

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
            }
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

        public override bool Visible
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
                    foreach (TfFrame frame in frames.Values)
                    {
                        frame.AddListener(keepAllListener);
                    }
                }
                else
                {
                    // here we remove unused frames
                    // we create a copy because this generally modifies the collection
                    var framesCopy = frames.Values.ToList();
                    foreach (TfFrame frame in framesCopy)
                    {
                        frame.RemoveListener(keepAllListener);
                    }
                }
            }
        }

        public bool PreferUdp
        {
            get => config.PreferUdp;
            set
            {
                config.PreferUdp = value;
                if (Listener != null)
                {
                    Listener.TransportHint = value ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
                }
            }
        }

        static bool IsFrameUsableAsHint(TfFrame frame)
        {
            return frame != RootFrame && frame != UnityFrame && frame != OriginFrame;
        }

        public override void StartListening()
        {
            Listener = new Listener<TFMessage>(DefaultTopic, HandlerNonStatic,
                PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp);
            ListenerStatic = new Listener<TFMessage>(DefaultTopicStatic, HandlerStatic);
        }

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
            while (messageList.TryDequeue(out var value))
            {
                var (transforms, isStatic) = value;
                foreach (var (parentIdUnchecked, childIdUnchecked, rosTransform, _) in transforms)
                {
                    if (rosTransform.HasNaN()
                        || childIdUnchecked.Length == 0
                        || !IsValid(rosTransform.Translation))
                    {
                        continue;
                    }

                    // remove starting '/' from tf v1
                    string childId = childIdUnchecked[0] != '/'
                        ? childIdUnchecked
                        : childIdUnchecked.Substring(1);

                    TfFrame? child;
                    if (lastChild is not null && lastChild.Id == childId)
                    {
                        child = lastChild;
                    }
                    else if (isStatic)
                    {
                        child = GetOrCreateFrame(childId, staticListener);
                        if (config.KeepAllFrames)
                        {
                            child.AddListener(keepAllListener);
                        }
                    }
                    else if (config.KeepAllFrames)
                    {
                        child = GetOrCreateFrame(childId, keepAllListener);
                    }
                    else if (!TryGetFrameImpl(childId, out child))
                    {
                        continue;
                    }

                    lastChild = child;
                    if (parentIdUnchecked.Length == 0)
                    {
                        child.SetParent(DefaultFrame);
                        child.SetPose(rosTransform.Ros2Unity());
                        continue;
                    }

                    string parentId = parentIdUnchecked[0] != '/'
                        ? parentIdUnchecked
                        : parentIdUnchecked.Substring(1);

                    if (child.Parent is not null && parentId == child.Parent.Id)
                    {
                        child.SetPose(rosTransform.Ros2Unity());
                    }
                    else
                    {
                        var parent = GetOrCreateFrame(parentId);
                        if (child.SetParent(parent))
                        {
                            child.SetPose(rosTransform.Ros2Unity());
                        }
                    }
                }
            }
        }

        public override void ResetController()
        {
            base.ResetController();

            ListenerStatic?.Reset();
            Publisher.Reset();

            bool prevKeepAllFrames = KeepAllFrames;
            KeepAllFrames = false;

            var framesCopy = frames.Values.ToList();
            foreach (TfFrame frame in framesCopy)
            {
                frame.RemoveListener(staticListener);
            }

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

            if (frameId.Length == 1)
            {
                return ConnectionManager.MyId ?? FixedFrameId;
            }

            string frameIdSuffix = frameId[1] == '/' ? frameId.Substring(2) : frameId.Substring(1);
            if (ConnectionManager.MyId == null)
            {
                return frameIdSuffix;
            }

            return ConnectionManager.MyId[0] == '/'
                ? $"{ConnectionManager.MyId.Substring(1)}/{frameIdSuffix}"
                : $"{ConnectionManager.MyId}/{frameIdSuffix}";
        }

        public static TfFrame ResolveFrame(string frameId, FrameNode? listener = null) =>
            GetOrCreateFrame(ResolveFrameId(frameId), listener);

        public static TfFrame GetOrCreateFrame(string frameId, FrameNode? listener = null)
        {
            if (frameId == null)
            {
                throw new ArgumentNullException(nameof(frameId));
            }

            string validatedFrameId = frameId.Length != 0 && frameId[0] == '/' ? frameId.Substring(1) : frameId;
            TfFrame frame = Instance.GetOrCreateFrameImpl(validatedFrameId);

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
            TfFrame frame = Resource.Displays.TfFrame.Instantiate<TfFrame>(parent);
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

        bool HandlerNonStatic(TFMessage msg)
        {
            if (messageList.Count > MaxQueueSize)
            {
                return false;
            }

            messageList.Enqueue((msg.Transforms, false));
            return true;
        }

        bool HandlerStatic(TFMessage msg)
        {
            if (messageList.Count > MaxQueueSize)
            {
                return false;
            }

            messageList.Enqueue((msg.Transforms, true));
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

        void LateUpdate()
        {
            ProcessMessages();

            Pose worldPose = FixedFrame.OriginWorldPose;
            if (!worldPose.IsApproxIdentity())
            {
                OriginFrame.Transform.SetLocalPose(worldPose.Inverse());
            }
        }

        public override void StopController()
        {
            base.StopController();
            staticListener.DestroySelf();
            Publisher.Stop();
            GameThread.LateEveryFrame -= LateUpdate;
            instance = null;
        }

        static void Publish(TFMessage msg)
        {
            Instance.Publisher.Publish(msg);
        }

        public static Vector3 RelativePositionToOrigin(in Vector3 unityPosition)
        {
            Transform originFrame = OriginFrame.Transform;
            return originFrame.InverseTransformPoint(unityPosition);
        }

        public static Vector3 RelativePositionToFixedFrame(in Vector3 unityPosition)
        {
            Transform fixedFrame = Instance.FixedFrame.Transform;
            return fixedFrame.InverseTransformPoint(unityPosition);
        }


        public static Pose RelativePoseToOrigin(in Pose unityPose)
        {
            Transform originFrame = OriginFrame.Transform;
            var (position, rotation) = unityPose;
            return new Pose(
                originFrame.InverseTransformPoint(position),
                Quaternion.Inverse(originFrame.rotation) * rotation
            );
        }

        public static Pose RelativePoseToFixedFrame(in Pose unityPose)
        {
            Transform fixedFrame = Instance.FixedFrame.Transform;
            var (position, rotation) = unityPose;
            return new Pose(
                fixedFrame.InverseTransformPoint(position),
                Quaternion.Inverse(fixedFrame.rotation) * rotation
            );
        }

        public static Pose FixedFramePose => Instance.FixedFrame.Transform.AsPose();

        public static void Publish(string childFrame, in Pose absoluteUnityPose)
        {
            Pose relativePose = RelativePoseToFixedFrame(absoluteUnityPose);
            Publish(FixedFrameId, childFrame, relativePose.Unity2RosTransform());
        }

        public static void Publish(string childFrame, in Msgs.GeometryMsgs.Transform rosTransform) =>
            Publish(FixedFrameId, childFrame, rosTransform);

        public static void Publish(string? parentFrame, string childFrame, in Msgs.GeometryMsgs.Transform rosTransform)
        {
            if (instance == null)
            {
                return;
            }

            var msg = new TFMessage
            (
                new[]
                {
                    new TransformStamped(
                        (tfSeq++, parentFrame ?? FixedFrameId),
                        ResolveFrameId(childFrame),
                        rosTransform)
                }
            );

            if (ConnectionManager.IsConnected)
            {
                Publish(msg);
            }
            else
            {
                Instance.HandlerNonStatic(msg);
            }
        }
    }
}