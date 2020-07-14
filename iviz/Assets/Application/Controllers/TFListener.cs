using UnityEngine;
using System.Collections.Generic;
using tfMessage_v2 = Iviz.Msgs.Tf2Msgs.TFMessage;
using System.Linq;
using Iviz.RoslibSharp;
using Iviz.App.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.Tf;
using System.Runtime.Serialization;
using System;
using System.Collections.ObjectModel;
using Iviz.Displays;
using Iviz.Resources;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class TFConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.TF;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool AxisVisible { get; set; } = true;
        [DataMember] public float AxisSize { get; set; } = 0.125f;
        [DataMember] public bool AxisLabelVisible { get; set; } = false;
        [DataMember] public float AxisLabelSize { get; set; } = 0.1f;
        [DataMember] public bool ParentConnectorVisible { get; set; } = false;
        [DataMember] public bool ShowAllFrames { get; set; } = false;
    }

    public sealed class TFListener : ListenerController
    {
        public const string DefaultTopic = "/tf";
        public const string DefaultTopicStatic = "/tf_static";
        public const string BaseFrameId = "map";

        public static TFListener Instance { get; private set; }
        public static Camera MainCamera { get; set; }
        public static FlyCamera GuiManager => Instance.guiManager;

        public static Light MainLight { get; set; }

        public static TFFrame MapFrame { get; private set; }
        public static TFFrame RootFrame { get; private set; }
        public static TFFrame UnityFrame { get; private set; }

        readonly InteractiveControl rootControl;
        public static InteractiveControl RootControl => Instance.rootControl;

        public static TFFrame ListenersFrame => RootFrame;

        public override TFFrame Frame => MapFrame;

        readonly FlyCamera guiManager;
        readonly DisplayNode dummyListener;
        readonly DisplayNode staticListener;

        readonly Dictionary<string, TFFrame> frames = new Dictionary<string, TFFrame>();

        public static ReadOnlyDictionary<string, TFFrame> Frames =>
            new ReadOnlyDictionary<string, TFFrame>(Instance.frames);

        static bool IsUsableByHints(TFFrame frame) => frame != RootFrame && frame != UnityFrame;

        public static IEnumerable<string> FramesForHints => Instance.frames.Values.Where(IsUsableByHints).Select(frame => frame.Id);

        public RosSender<tfMessage_v2> Publisher { get; }

        public override ModuleData ModuleData { get; }

        public RosListener ListenerStatic { get; private set; }


        readonly TFConfiguration config = new TFConfiguration();

        public TFConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                AxisVisible = value.AxisVisible;
                AxisSize = value.AxisSize;
                AxisLabelSize = value.AxisLabelSize;
                AxisLabelVisible = value.AxisLabelVisible;
                ParentConnectorVisible = value.ParentConnectorVisible;
                ShowAllFrames = value.ShowAllFrames;
            }
        }

        public bool AxisVisible
        {
            get => config.AxisVisible;
            set
            {
                config.AxisVisible = value;
                foreach (var frame in frames.Values)
                {
                    frame.Visible = value;
                }

                if (!(rootControl is null))
                {
                    rootControl.Visible = value;
                }
            }
        }

        public bool AxisLabelVisible
        {
            get => config.AxisLabelVisible;
            set
            {
                config.AxisLabelVisible = value;
                foreach (var frame in frames.Values)
                {
                    frame.LabelVisible = value;
                }
            }
        }

        public float AxisLabelSize
        {
            get => config.AxisLabelSize;
            set
            {
                config.AxisLabelSize = value;
                foreach (var frame in frames.Values)
                {
                    frame.LabelSize = value;
                }
            }
        }

        public float AxisSize
        {
            get => config.AxisSize;
            set
            {
                config.AxisSize = value;
                foreach (var frame in frames.Values)
                {
                    frame.AxisLength = value;
                }
            }
        }

        public bool ParentConnectorVisible
        {
            get => config.ParentConnectorVisible;
            set
            {
                config.ParentConnectorVisible = value;
                foreach (var frame in frames.Values)
                {
                    frame.ConnectorVisible = value;
                }
            }
        }

        public bool ShowAllFrames
        {
            get => config.ShowAllFrames;
            set
            {
                config.ShowAllFrames = value;
                if (value)
                {
                    foreach (var frame in frames.Values)
                    {
                        frame.AddListener(dummyListener);
                    }
                }
                else
                {
                    // we create a copy because this generally modifies the collection
                    var framesCopy = frames.Values.ToList();
                    foreach (var frame in framesCopy)
                    {
                        frame.RemoveListener(dummyListener);
                    }
                }
            }
        }

        public TFListener(ModuleData moduleData)
        {
            ModuleData = moduleData;
            Instance = this;

            UnityFrame = Add(CreateFrameObject("TF", null, null));
            UnityFrame.ForceInvisible = true;
            UnityFrame.Visible = false;
            UnityFrame.AddListener(null);

            dummyListener = SimpleDisplayNode.Instantiate("[TFNode]", UnityFrame.transform);
            staticListener = SimpleDisplayNode.Instantiate("[TFStatic]", UnityFrame.transform);

            GameObject mainCameraObj = GameObject.Find("MainCamera");
            MainCamera = mainCameraObj.GetComponent<Camera>();
            guiManager = mainCameraObj.GetComponent<FlyCamera>();

            GameObject mainLight = GameObject.Find("MainLight");
            MainLight = mainLight.GetComponent<Light>();

            Config = new TFConfiguration();

            RootFrame = Add(CreateFrameObject("/", UnityFrame.transform, UnityFrame));
            RootFrame.ForceInvisible = true;
            RootFrame.Visible = false;
            RootFrame.AddListener(null);

            MapFrame = Add(CreateFrameObject(BaseFrameId, UnityFrame.transform, RootFrame));
            MapFrame.Parent = RootFrame;
            MapFrame.AddListener(null);
            MapFrame.AcceptsParents = false;
            //BaseFrame.ForceInvisible = true;

            rootControl =
                ResourcePool.GetOrCreate<InteractiveControl>(Resource.Displays.InteractiveControl,
                    RootFrame.transform);
            rootControl.name = "[InteractiveController for /]";
            rootControl.TargetTransform = RootFrame.transform;
            rootControl.InteractionMode = InteractiveControl.InteractionModeType.Disabled;
            rootControl.transform.localScale = 0.4f * Vector3.one;

            Publisher = new RosSender<tfMessage_v2>(DefaultTopic);
        }

        public override void StartListening()
        {
            Listener = new RosListener<tfMessage_v2>(DefaultTopic, SubscriptionHandler_v2);
            Listener.MaxQueueSize = 200;
            ListenerStatic = new RosListener<tfMessage_v2>(DefaultTopicStatic, SubscriptionHandlerStatic);
            ListenerStatic.MaxQueueSize = 200;
        }

        void ProcessMessages(IEnumerable<TransformStamped> transforms, bool isStatic)
        {
            foreach (TransformStamped t in transforms)
            {
                if (t.Transform.HasNaN())
                {
                    continue;
                }

                TimeSpan timestamp = t.Header.Stamp.ToTimeSpan();
                if (t.Header.Stamp.Nsecs == 0 && t.Header.Stamp.Secs == 0)
                {
                    timestamp = TimeSpan.MaxValue;
                }

                string childId = t.ChildFrameId;
                if (childId.Length != 0 && childId[0] == '/')
                {
                    childId = childId.Substring(1);
                }

                TFFrame child;
                if (isStatic)
                {
                    child = GetOrCreateFrame(childId, staticListener);
                    if (config.ShowAllFrames)
                    {
                        child.AddListener(dummyListener);
                    }
                }
                else if (config.ShowAllFrames)
                {
                    child = GetOrCreateFrame(childId, dummyListener);
                }
                else if (!TryGetFrameImpl(childId, out child))
                {
                    continue;
                }

                string parentId = t.Header.FrameId;
                if (parentId.Length != 0 && parentId[0] == '/')
                {
                    parentId = parentId.Substring(1);
                }

                //Debug.Log("Id " + childId + " requests parent " + parentId);
                TFFrame parent = string.IsNullOrEmpty(parentId) ? RootFrame : GetOrCreateFrame(parentId, null);
                //Debug.Log("Parent has parent " + parent.Parent.Id);

                if (child.SetParent(parent))
                {
                    child.SetPose(timestamp, t.Transform.Ros2Unity());
                }
            }
        }

        public override void Reset()
        {
            base.Reset();
            ListenerStatic?.Reset();
            
            bool prevShowAllFrames = ShowAllFrames;
            ShowAllFrames = false;

            var framesCopy = frames.Values.ToList();
            foreach (var frame in framesCopy)
            {
                frame.RemoveListener(staticListener);
            }

            ShowAllFrames = prevShowAllFrames;
        }

        TFFrame Add(TFFrame t)
        {
            frames.Add(t.Id, t);
            return t;
        }

        public static bool TryGetFrame(string id, out TFFrame frame)
        {
            return Instance.TryGetFrameImpl(id, out frame);
        }


        public static TFFrame GetOrCreateFrame(string id, DisplayNode listener = null)
        {
            if (id.Length != 0 && id[0] == '/')
            {
                id = id.Substring(1);
            }

            TFFrame frame = Instance.GetOrCreateFrameImpl(id);
            if (frame.Id != id)
            {
                Debug.LogWarning("Error: Broken resource pool! Requested " + id + ", received " + frame.Id);
            }

            if (!(listener is null))
            {
                frame.AddListener(listener);
            }

            return frame;
        }

        TFFrame GetOrCreateFrameImpl(string id)
        {
            return TryGetFrameImpl(id, out TFFrame t) ? t : Add(CreateFrameObject(id, RootFrame.transform, RootFrame));
        }

        TFFrame CreateFrameObject(string id, Transform parent, TFFrame parentFrame)
        {
            TFFrame frame = ResourcePool.GetOrCreate<TFFrame>(Resource.Displays.TFFrame, parent);
            //Debug.Log(parent + " -> " + frame.transform.parent);
            frame.name = "{" + id + "}";
            frame.Id = id;
            frame.Visible = config.AxisVisible;
            frame.AxisLength = config.AxisSize;
            frame.LabelSize = config.AxisLabelSize;
            frame.LabelVisible = config.AxisLabelVisible;
            frame.ConnectorVisible = config.ParentConnectorVisible;
            if (!(parentFrame is null))
            {
                frame.Parent = parentFrame;
            }

            return frame;
        }

        bool TryGetFrameImpl(string id, out TFFrame t)
        {
            return frames.TryGetValue(id, out t);
        }

        void SubscriptionHandler_v1(tfMessage msg)
        {
            ProcessMessages(msg.Transforms, false);
        }

        void SubscriptionHandler_v2(tfMessage_v2 msg)
        {
            ProcessMessages(msg.Transforms, false);
        }

        void SubscriptionHandlerStatic(tfMessage_v2 msg)
        {
            ProcessMessages(msg.Transforms, true);
        }

        public void MarkAsDead(TFFrame frame)
        {
            frames.Remove(frame.Id);
            GuiManager.Unselect(frame);
            frame.Stop();
            //Debug.Log("Frame " + frame.gameObject.GetInstanceID() + " with formed id '" + frame.Id + "' is dead!");
            ResourcePool.Dispose(Resource.Displays.TFFrame, frame.gameObject);
        }

        public static void Publish(tfMessage_v2 msg)
        {
            Instance.Publisher.Publish(msg);
        }

        public static UnityEngine.Pose RelativePose(in UnityEngine.Pose unityPose)
        {
            if (FlyCamera.IsMobile)
            {
                /*
                Transform rootFrame = RootFrame.transform;
                UnityEngine.Pose rootFrameInverse = RootFrame.transform.AsPose().Inverse();
                UnityEngine.Pose relative = rootFrameInverse.Multiply(unityPose);
                var localScale = RootFrame.transform.localScale;
                relative.position.x /= localScale.x;
                relative.position.y /= localScale.y;
                relative.position.z /= localScale.z;
                */
                Transform rootFrame = RootFrame.transform;
                return new UnityEngine.Pose(
                    rootFrame.InverseTransformPoint(unityPose.position),
                    UnityEngine.Quaternion.Inverse(rootFrame.rotation) * unityPose.rotation
                );
            }
            else
            {
                return unityPose;
            }
        }

        public static UnityEngine.Vector3 RelativePosition(in UnityEngine.Vector3 unityPosition)
        {
            return FlyCamera.IsMobile ? RootFrame.transform.InverseTransformPoint(unityPosition) : unityPosition;
        }

        static uint tfSeq = 0;

        public static void Publish(string parentFrame, string childFrame, in UnityEngine.Pose unityPose)
        {
            tfMessage_v2 msg = new tfMessage_v2
            (
                Transforms: new[]
                {
                    new TransformStamped
                    (
                        Header: RosUtils.CreateHeader(tfSeq++, parentFrame ?? BaseFrameId),
                        ChildFrameId: childFrame ?? "",
                        Transform: RelativePose(unityPose).Unity2RosTransform()
                    )
                }
            );
            Publish(msg);
        }
    }
}