using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Sdf;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;
using Color = UnityEngine.Color;
using Object = UnityEngine.Object;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using String = System.String;
using Time = UnityEngine.Time;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class TfListener : ListenerController
    {
        public const string DefaultTopic = "/tf";
        public const string OriginFrameName = "/_origin_";

        const string DefaultTopicStatic = "/tf_static";
        const string BaseFrameId = "map";

        static uint tfSeq;

        readonly TfConfiguration config = new TfConfiguration();

        readonly ConcurrentQueue<(TransformStamped[] frame, bool isStatic)> messageList =
            new ConcurrentQueue<(TransformStamped[], bool)>();

        readonly Dictionary<string, TfFrame> frames = new Dictionary<string, TfFrame>();
        [NotNull] readonly FrameNode keepAllListener;
        [NotNull] readonly FrameNode staticListener;
        [NotNull] readonly FrameNode fixedFrameListener;

        [NotNull] readonly TfFrame mapFrame;
        [NotNull] readonly TfFrame rootFrame;
        [NotNull] readonly TfFrame originFrame;
        [NotNull] readonly TfFrame unityFrame;

        [NotNull] public TfFrame FixedFrame { get; private set; }

        const float HighlightDuration = 3.0f;
        float? highlightFrameStart;
        readonly FrameNode highlightFrameNode;
        readonly AxisFrameResource highlightFrame;

        public TfListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            Instance = this;
            
            highlightFrame = ResourcePool.RentDisplay<AxisFrameResource>();
            highlightFrame.Visible = false;
            highlightFrame.CastsShadows = false;
            highlightFrame.Emissive = 1;

            highlightFrameNode = FrameNode.Instantiate("Highlight FrameNode");
            highlightFrame.Transform.parent = highlightFrameNode.Transform;
            
            unityFrame = Add(CreateFrameObject("TF", null, null));
            unityFrame.ForceInvisible = true;
            unityFrame.Visible = false;

            FrameNode defaultListener = FrameNode.Instantiate("[.]");
            UnityFrame.AddListener(defaultListener);

            keepAllListener = FrameNode.Instantiate("[TFNode]");
            staticListener = FrameNode.Instantiate("[TFStatic]");
            fixedFrameListener = FrameNode.Instantiate("[TFFixedFrame]");
            defaultListener.transform.parent = UnityFrame.Transform;

            Config = new TfConfiguration();

            rootFrame = Add(CreateFrameObject("/", UnityFrame.Transform, UnityFrame));
            rootFrame.ForceInvisible = true;
            rootFrame.Visible = false;
            rootFrame.AddListener(defaultListener);
            
            originFrame = Add(CreateFrameObject(OriginFrameName, RootFrame.Transform, RootFrame));
            originFrame.Parent = RootFrame;
            originFrame.ForceInvisible = true;
            originFrame.Visible = false;
            originFrame.AddListener(defaultListener);
            originFrame.ParentCanChange = false;

            mapFrame = Add(CreateFrameObject(BaseFrameId, OriginFrame.Transform, OriginFrame));
            mapFrame.Parent = OriginFrame;
            mapFrame.AddListener(defaultListener);
            mapFrame.ParentCanChange = false;

            //rootFrame.Transform.localScale = 0.5f * Vector3.one;

            FixedFrame = MapFrame;

            Publisher = new Sender<TFMessage>(DefaultTopic);

            FixedFrameId = "";
            GameThread.LateEveryFrame += LateUpdate;
        }

        [CanBeNull]
        public static string FixedFrameId
        {
            get => Instance.FixedFrame.Id;
            set
            {
                if (Instance.FixedFrame != null)
                {
                    Instance.FixedFrame.RemoveListener(Instance.fixedFrameListener);
                }

                if (string.IsNullOrEmpty(value) || !TryGetFrame(value, out TfFrame frame))
                {
                    Instance.FixedFrame = MapFrame;
                    OriginFrame.Transform.SetLocalPose(Pose.identity);
                }
                else
                {
                    Instance.FixedFrame = frame;
                    Instance.FixedFrame.AddListener(Instance.fixedFrameListener);
                    OriginFrame.Transform.SetLocalPose(frame.WorldPose.Inverse());
                }
            }
        }

        public static TfListener Instance { get; private set; }
        [NotNull] public Sender<TFMessage> Publisher { get; }
        public IListener ListenerStatic { get; private set; }

        [NotNull] public static TfFrame MapFrame => Instance.mapFrame;
        [NotNull] public static TfFrame RootFrame => Instance.rootFrame;
        [NotNull] public static TfFrame OriginFrame => Instance.originFrame;
        [NotNull] public static TfFrame UnityFrame => Instance.unityFrame;
        [NotNull] public static TfFrame ListenersFrame => OriginFrame;
        [NotNull] public override TfFrame Frame => MapFrame;

        [NotNull]
        public static IEnumerable<string> FramesUsableAsHints =>
            Instance.frames.Values.Where(IsFrameUsableAsHint).Select(frame => frame.Id);

        public override IModuleData ModuleData { get; }

        [NotNull]
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

                highlightFrame.AxisLength = value * 3;
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

        static bool IsFrameUsableAsHint(TfFrame frame)
        {
            return frame != RootFrame && frame != UnityFrame && frame != OriginFrame;
        }

        public override void StartListening()
        {
            Listener = new Listener<TFMessage>(DefaultTopic, HandlerNonStatic);
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
            while (messageList.TryDequeue(out var value))
            {
                var (frame, isStatic) = value;
                foreach (var (parentFrameId, childFrameId, transform, _) in frame)
                {
                    if (transform.HasNaN()
                        || childFrameId.Length == 0
                        || !IsValid(transform.Translation))
                    {
                        continue;
                    }

                    // remove starting '/' from tf v1
                    string childId = childFrameId[0] != '/'
                        ? childFrameId
                        : childFrameId.Substring(1);

                    TfFrame child;
                    if (isStatic)
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

                    if (parentFrameId.Length == 0)
                    {
                        child.SetParent(OriginFrame);
                        child.SetPose(transform.Ros2Unity());
                        continue;
                    }

                    string parentId = parentFrameId[0] != '/'
                        ? parentFrameId
                        : parentFrameId.Substring(1);

                    if (!(child.Parent is null) && parentId == child.Parent.Id)
                    {
                        child.SetPose(transform.Ros2Unity());
                    }
                    else
                    {
                        TfFrame parent = GetOrCreateFrame(parentId);
                        if (child.SetParent(parent))
                        {
                            child.SetPose(transform.Ros2Unity());
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

        [NotNull]
        TfFrame Add([NotNull] TfFrame t)
        {
            frames.Add(t.Id, t);
            return t;
        }

        [ContractAnnotation("=> false, frame:null; => true, frame:notnull")]
        public static bool TryGetFrame([NotNull] string id, out TfFrame frame)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Instance.TryGetFrameImpl(id, out frame);
        }

        [NotNull]
        public static TfFrame GetOrCreateFrame([NotNull] string frameId, [CanBeNull] FrameNode listener = null)
        {
            if (frameId == null)
            {
                throw new ArgumentNullException(nameof(frameId));
            }

            //string frameId = reqId.Length != 0 && reqId[0] == '/' ? reqId.Substring(1) : reqId;
            TfFrame frame = Instance.GetOrCreateFrameImpl(frameId);

            if (!(listener is null))
            {
                frame.AddListener(listener);
            }

            return frame;
        }

        [NotNull]
        TfFrame GetOrCreateFrameImpl([NotNull] string id)
        {
            return TryGetFrameImpl(id, out TfFrame frame)
                ? frame
                : Add(CreateFrameObject(id, OriginFrame.Transform, OriginFrame));
        }

        [NotNull]
        TfFrame CreateFrameObject([NotNull] string id, [CanBeNull] Transform parent, [CanBeNull] TfFrame parentFrame)
        {
            TfFrame frame = Resource.Displays.TfFrame.Instantiate(parent).GetComponent<TfFrame>();
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

        [ContractAnnotation("=> false, t:null; => true, t:notnull")]
        bool TryGetFrameImpl([NotNull] string id, out TfFrame t)
        {
            return frames.TryGetValue(id, out t);
        }

        bool HandlerNonStatic([NotNull] TFMessage msg)
        {
            messageList.Enqueue((msg.Transforms, false));
            return true;
        }

        bool HandlerStatic([NotNull] TFMessage msg)
        {
            messageList.Enqueue((msg.Transforms, true));
            return true;
        }

        public void MarkAsDead([NotNull] TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            frames.Remove(frame.Id);

            frame.Stop();
            Object.Destroy(frame.gameObject);
        }

        void LateUpdate()
        {
            ProcessMessages();

            Pose worldPose = FixedFrame.WorldPose;
            if (!worldPose.IsApproxIdentity())
            {
                OriginFrame.Transform.SetLocalPose(worldPose.Inverse());
            }

            if (highlightFrameStart == null)
            {
                return;
            }
            
            float alpha = 1 - (Time.time - highlightFrameStart.Value) / HighlightDuration;
            if (alpha < 0)
            {
                highlightFrameStart = null;
                highlightFrame.Visible = false;
                return;
            }

            highlightFrame.Tint = Color.white.WithAlpha(alpha);
        }

        public override void StopController()
        {
            base.StopController();
            staticListener.Stop();
            Publisher.Stop();
            GameThread.LateEveryFrame -= LateUpdate;
            Instance = null;
        }

        public void HighlightFrame([NotNull] string frameId)
        {
            if (!frames.ContainsKey(frameId))
            {
                return;
            }

            highlightFrameNode.AttachTo(frameId);
            highlightFrameStart = Time.time;
            highlightFrame.Tint = Color.white;
            highlightFrame.Visible = true;
        }

        static void Publish([NotNull] TFMessage msg)
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

        public static void Publish([CanBeNull] string parentFrame, [CanBeNull] string childFrame,
            in Msgs.GeometryMsgs.Transform rosTransform)
        {
            TFMessage msg = new TFMessage
            (
                new[]
                {
                    new TransformStamped((tfSeq++, parentFrame), childFrame ?? "", rosTransform)
                }
            );

            Publish(msg);
        }
    }
}