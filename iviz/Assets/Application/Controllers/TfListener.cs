using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.App;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Nito.AsyncEx;
using Object = UnityEngine.Object;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class TfConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool FrameLabelsVisible { get; set; }
        [DataMember] public bool ParentConnectorVisible { get; set; }
        [DataMember] public bool KeepAllFrames { get; set; } = true;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.TF;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class TfListener : ListenerController
    {
        public const string DefaultTopic = "/tf";
        const string DefaultTopicStatic = "/tf_static";
        const string BaseFrameId = "map";

        static uint tfSeq;

        readonly TfConfiguration config = new TfConfiguration();

        //readonly AsyncLock mutex = new AsyncLock();
        readonly ConcurrentQueue<(UniqueRef<TransformStamped> frame, bool isStatic)> messageList =
            new ConcurrentQueue<(UniqueRef<TransformStamped>, bool)>();

        readonly Dictionary<string, TfFrame> frames = new Dictionary<string, TfFrame>();
        [NotNull] readonly FrameNode keepAllListener;
        [NotNull] readonly FrameNode staticListener;
        [NotNull] readonly FrameNode fixedFrameListener;

        [NotNull] readonly TfFrame mapFrame;
        [NotNull] readonly TfFrame rootFrame;
        [NotNull] readonly TfFrame originFrame;
        [NotNull] readonly TfFrame unityFrame;

        [NotNull] public TfFrame FixedFrame { get; private set; }

        public TfListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            Instance = this;

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

            originFrame = Add(CreateFrameObject("/_origin_", RootFrame.Transform, RootFrame));
            originFrame.Parent = RootFrame;
            originFrame.ForceInvisible = true;
            originFrame.Visible = false;
            originFrame.AddListener(defaultListener);
            originFrame.ParentCanChange = false;

            mapFrame = Add(CreateFrameObject(BaseFrameId, OriginFrame.Transform, OriginFrame));
            mapFrame.Parent = OriginFrame;
            mapFrame.AddListener(defaultListener);
            mapFrame.ParentCanChange = false;

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

        [NotNull]
        public static GuiInputModule GuiInputModule => GuiInputModule.Instance.SafeNull() ??
                                                       throw new InvalidOperationException(
                                                           "GuiInputModule has not been started!");

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
            Listener = new Listener<TFMessage>(DefaultTopic, SubscriptionHandlerNonStatic);
            ListenerStatic = new Listener<TFMessage>(DefaultTopicStatic, SubscriptionHandlerStatic);
        }

        void ProcessMessages()
        {
            while (messageList.TryDequeue(out var value))
            {
                bool isStatic = value.isStatic;
                using (var frame = value.frame)
                {
                    foreach (TransformStamped t in frame)
                    {
                        var ((_, _, frameId), childFrameId, transform) = t;

                        if (transform.HasNaN() || childFrameId.Length == 0)
                        {
                            continue;
                        }

                        const int maxPoseMagnitude = 10000;
                        if (transform.Translation.SquaredNorm > 3 * maxPoseMagnitude * maxPoseMagnitude)
                        {
                            continue; // TODO: Find better way to handle this
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

                        string parentId = frameId.Length == 0 || frameId[0] != '/'
                            ? frameId
                            : frameId.Substring(1);

                        if (parentId.Length == 0)
                        {
                            child.SetParent(OriginFrame);
                            child.SetPose(transform.Ros2Unity());
                        }
                        else if (!(child.Parent is null) && parentId == child.Parent.Id)
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
        public static TfFrame GetOrCreateFrame([NotNull] string reqId, [CanBeNull] FrameNode listener = null)
        {
            if (reqId == null)
            {
                throw new ArgumentNullException(nameof(reqId));
            }

            string frameId = reqId.Length != 0 && reqId[0] == '/' ? reqId.Substring(1) : reqId;

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
            frame.Name = "{" + id + "}";
            frame.Id = id;
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

        bool SubscriptionHandlerNonStatic([NotNull] TFMessage msg)
        {
            messageList.Enqueue((msg.Transforms.Release(), false));
            return true;
        }

        bool SubscriptionHandlerStatic([NotNull] TFMessage msg)
        {
            messageList.Enqueue((msg.Transforms.Release(), true));
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
            OriginFrame.Transform.SetLocalPose(FixedFrame.WorldPose.Inverse());
        }

        public override void StopController()
        {
            base.StopController();
            staticListener.Stop();
            Publisher.Stop();
            GameThread.LateEveryFrame -= LateUpdate;
            Instance = null;
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
            return new Pose(
                originFrame.InverseTransformPoint(unityPose.position),
                Quaternion.Inverse(originFrame.rotation) * unityPose.rotation
            );
        }

        public static void Publish([CanBeNull] string parentFrame, [CanBeNull] string childFrame,
            in Pose unityPose)
        {
            TFMessage msg = new TFMessage
            (
                new[]
                {
                    new TransformStamped
                    (
                        (tfSeq++, parentFrame),
                        childFrame ?? "",
                        RelativePoseToOrigin(unityPose).Unity2RosTransform()
                    )
                }
            );
            Publish(msg);
        }
    }
}