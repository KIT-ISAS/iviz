using UnityEngine;
using System.Collections.Generic;
using tfMessage_v2 = Iviz.Msgs.Tf2Msgs.TFMessage;
using System.Linq;
using Iviz.Roslib;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.Tf;
using System.Runtime.Serialization;
using System;
using Iviz.App;
using Iviz.Displays;
using Iviz.Resources;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
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
        [DataMember] public bool ParentConnectorVisible { get; set; } = false;
        [DataMember] public bool ShowAllFrames { get; set; } = true;
    }

    public sealed class TFListener : ListenerController
    {
        public const string DefaultTopic = "/tf";
        public const string BaseFrameId = "map";
        
        const string DefaultTopicStatic = "/tf_static";

        public static TFListener Instance { get; private set; }
        public RosSender<tfMessage_v2> Publisher { get; }
        public RosListener ListenerStatic { get; private set; }

        public static Camera MainCamera { get; set; }
        public static GuiCamera GuiCamera => GuiCamera.Instance;
        public static Light MainLight { get; set; }

        public static TFFrame MapFrame { get; private set; }
        public static TFFrame RootFrame { get; private set; }
        public static TFFrame UnityFrame { get; private set; }

        readonly InteractiveControl rootMarker;
        public static InteractiveControl RootMarker => Instance.rootMarker;

        public static TFFrame ListenersFrame => RootFrame;

        public override TFFrame Frame => MapFrame;

        readonly DisplayNode showAllListener;
        readonly DisplayNode staticListener;

        readonly Dictionary<string, TFFrame> frames = new Dictionary<string, TFFrame>();

        public static IEnumerable<string> FramesUsableAsHints =>
            Instance.frames.Values.Where(IsFrameUsableAsHint).Select(frame => frame.Id);

        static bool IsFrameUsableAsHint(TFFrame frame) => frame != RootFrame && frame != UnityFrame;

        public override IModuleData ModuleData { get; }

        readonly TFConfiguration config = new TFConfiguration();

        public TFConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                AxisVisible = value.AxisVisible;
                FrameSize = value.AxisSize;
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

                if (rootMarker != null)
                {
                    rootMarker.Visible = value;
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

        public float FrameSize
        {
            get => config.AxisSize;
            set
            {
                config.AxisSize = value;
                foreach (var frame in frames.Values)
                {
                    frame.FrameSize = value;
                }

                if (rootMarker != null)
                {
                    rootMarker.BaseScale = 2.5f * FrameSize;
                }
            }
        }

        public float FrameLabelSize => 0.5f * FrameSize;

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
                        frame.AddListener(showAllListener);
                    }
                }
                else
                {
                    // we create a copy because this generally modifies the collection
                    var framesCopy = frames.Values.ToList();
                    foreach (var frame in framesCopy)
                    {
                        frame.RemoveListener(showAllListener);
                    }
                }
            }
        }

        public TFListener(IModuleData moduleData)
        {
            ModuleData = moduleData;
            Instance = this;

            UnityFrame = Add(CreateFrameObject("TF", null, null));
            UnityFrame.ForceInvisible = true;
            UnityFrame.Visible = false;
            UnityFrame.AddListener(null);

            showAllListener = SimpleDisplayNode.Instantiate("[TFNode]", UnityFrame.transform);
            staticListener = SimpleDisplayNode.Instantiate("[TFStatic]", UnityFrame.transform);

            GameObject mainCameraObj = GameObject.Find("MainCamera");
            MainCamera = mainCameraObj.GetComponent<Camera>();

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

            rootMarker = ResourcePool.GetOrCreate<InteractiveControl>(
                Resource.Displays.InteractiveControl,
                RootFrame.transform);
            rootMarker.name = "[InteractiveController for /]";
            rootMarker.TargetTransform = RootFrame.transform;
            rootMarker.InteractionMode = InteractiveControl.InteractionModeType.Disabled;
            rootMarker.BaseScale = 2.5f * FrameSize;

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

                TimeSpan timestamp = t.Header.Stamp == default ? TimeSpan.MaxValue : t.Header.Stamp.ToTimeSpan();

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
                        child.AddListener(showAllListener);
                    }
                }
                else if (config.ShowAllFrames)
                {
                    child = GetOrCreateFrame(childId, showAllListener);
                }
                else if (!TryGetFrameImpl(childId, out child))
                {
                    continue;
                }

                string parentId = t.Header.FrameId;
                if (parentId.Length != 0 && parentId[0] == '/') // remove starting '/' from tf v1
                {
                    parentId = parentId.Substring(1);
                }

                TFFrame parent = string.IsNullOrEmpty(parentId) ? RootFrame : GetOrCreateFrame(parentId);

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
            Publisher?.Reset();

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

            if (listener != null)
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
            frame.name = "{" + id + "}";
            frame.Id = id;
            frame.Visible = config.AxisVisible;
            frame.FrameSize = config.AxisSize;
            frame.LabelVisible = config.AxisLabelVisible;
            frame.ConnectorVisible = config.ParentConnectorVisible;
            if (parentFrame != null)
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
            GuiCamera.Unselect(frame);
            frame.Stop();
            ResourcePool.Dispose(Resource.Displays.TFFrame, frame.gameObject);
        }

        public static void Publish(tfMessage_v2 msg)
        {
            Instance.Publisher.Publish(msg);
        }

        public static UnityEngine.Pose RelativePoseToRoot(in UnityEngine.Pose unityPose)
        {
            if (!Settings.IsMobile)
            {
                return unityPose;
            }

            Transform rootFrame = RootFrame.transform;
            return new UnityEngine.Pose(
                rootFrame.InverseTransformPoint(unityPose.position),
                UnityEngine.Quaternion.Inverse(rootFrame.rotation) * unityPose.rotation
            );
        }

        static uint tfSeq;

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
                        Transform: RelativePoseToRoot(unityPose).Unity2RosTransform()
                    )
                }
            );
            Publish(msg);
        }

        public void OnARModeChanged(bool _)
        {
            UpdateRootMarkerVisibility();
        }

        public static void UpdateRootMarkerVisibility()
        {
            bool arEnabled = ARController.Instance?.Visible ?? false;
            bool viewEnabled = ARController.Instance?.ShowRootMarker ?? false;
            if (arEnabled && viewEnabled)
            {
                RootMarker.InteractionMode = InteractiveControl.InteractionModeType.Frame;
                //MapFrame.ColliderEnabled = false;
                MapFrame.Selected = false;
            }
            else
            {
                RootMarker.InteractionMode = InteractiveControl.InteractionModeType.Disabled;
                //MapFrame.ColliderEnabled = !Settings.IsHololens;
            }
        }
    }
}