using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using Color = UnityEngine.Color;
using Object = UnityEngine.Object;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Time = UnityEngine.Time;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class TfListener : ListenerController
    {
        public const string DefaultTopic = "/tf";
        public const string OriginFrameId = "_origin_";
        public const string MapFrameId = "map";
        const int MaxQueueSize = 10000;

        const string DefaultTopicStatic = "/tf_static";

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

        const float HighlightDuration = 2.0f;
        float? highlightFrameStart;
        readonly FrameNode highlightFrameNode;
        readonly AxisFrameResource highlightFrame;

        public static float RootScale
        {
            get => RootFrame.transform.localScale.x;
            set => RootFrame.transform.localScale = value * Vector3.one;
        }

        public TfListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            highlightFrame = ResourcePool.RentDisplay<AxisFrameResource>();
            highlightFrame.Visible = false;
            highlightFrame.CastsShadows = false;
            highlightFrame.Emissive = 1;
            highlightFrame.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);

            highlightFrameNode = FrameNode.Instantiate("Highlight FrameNode");
            highlightFrame.Transform.parent = highlightFrameNode.Transform;

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

            Instance = this;
            Config = new TfConfiguration();

            Publisher = new Sender<TFMessage>(DefaultTopic);


            GameThread.LateEveryFrame += LateUpdate;
        }

        [NotNull]
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
                TfFrame frame = GetOrCreateFrame(value, Instance.fixedFrameListener);
                Instance.FixedFrame = frame;
                OriginFrame.Transform.SetLocalPose(frame.OriginWorldPose.Inverse());
            }
        }

        public static TfListener Instance { get; private set; }
        [NotNull] public Sender<TFMessage> Publisher { get; }
        public IListener ListenerStatic { get; private set; }

        //[NotNull] public static TfFrame MapFrame => Instance.mapFrame;
        [NotNull] public static TfFrame RootFrame => Instance.rootFrame;
        [NotNull] public static TfFrame OriginFrame => Instance.originFrame;
        [NotNull] public static TfFrame UnityFrame => Instance.unityFrame;
        [NotNull] public static TfFrame ListenersFrame => OriginFrame;
        [NotNull] public override TfFrame Frame => FixedFrame;
        [NotNull] public static TfFrame DefaultFrame => OriginFrame;

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
                FixedFrameId = value.FixedFrameId;
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
            TfFrame lastChild = null;
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

                    TfFrame child;
                    if (!(lastChild is null) && lastChild.Id == childId)
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

                    if (!(child.Parent is null) && parentId == child.Parent.Id)
                    {
                        child.SetPose(rosTransform.Ros2Unity());
                    }
                    else
                    {
                        TfFrame parent = GetOrCreateFrame(parentId);
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
        public static string ResolveFrameId([NotNull] string frameId)
        {
            if (frameId[0] != '~')
            {
                return frameId;
            }

            string frameIdSuffix = frameId.Substring(1);
            if (ConnectionManager.MyId == null)
            {
                return frameIdSuffix;
            }
                
            return ConnectionManager.MyId[0] == '/' 
                ? $"{ConnectionManager.MyId.Substring(1)}/{frameIdSuffix}"
                : $"{ConnectionManager.MyId}/{frameIdSuffix}";
        }

        [NotNull]
        public static TfFrame ResolveFrame([NotNull] string frameId, [CanBeNull] FrameNode listener = null) =>
            GetOrCreateFrame(ResolveFrameId(frameId), listener);

        [NotNull]
        public static TfFrame GetOrCreateFrame([NotNull] string frameId, [CanBeNull] FrameNode listener = null)
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

        [NotNull]
        TfFrame GetOrCreateFrameImpl([NotNull] string id)
        {
            return TryGetFrameImpl(id, out TfFrame frame)
                ? frame
                : Add(CreateFrameObject(id, DefaultFrame.Transform, DefaultFrame));
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
            if (messageList.Count > MaxQueueSize)
            {
                return false;
            }

            //DateTime now = DateTime.Now;
            //Debug.Log((now - msg.Transforms[0].Header.Stamp.ToDateTime()).TotalMilliseconds);
            //Debug.Log(messageList.Count);

            messageList.Enqueue((msg.Transforms, false));
            return true;
        }

        bool HandlerStatic([NotNull] TFMessage msg)
        {
            if (messageList.Count > MaxQueueSize)
            {
                return false;
            }

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
            staticListener.DestroySelf();
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

        public static void Publish([NotNull] string childFrame, in Pose absoluteUnityPose)
        {
            Pose relativePose = RelativePoseToFixedFrame(absoluteUnityPose);
            Publish(FixedFrameId, childFrame, relativePose.Unity2RosTransform());
        }

        public static void Publish([CanBeNull] string parentFrame, [NotNull] string childFrame,
            in Msgs.GeometryMsgs.Transform rosTransform)
        {
            if (Instance == null)
            {
                return;
            }

            TFMessage msg = new TFMessage
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