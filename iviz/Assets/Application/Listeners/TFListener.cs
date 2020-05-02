using UnityEngine;
using System.Collections.Generic;
using tfMessage_v2 = Iviz.Msgs.tf2_msgs.TFMessage;
using System;
using System.Linq;
using Iviz.Msgs.tf;
using Iviz.Msgs.geometry_msgs;
using Iviz.RoslibSharp;

namespace Iviz.App
{
    public class TFListener : DisplayableListener
    {
        public static TFListener Instance { get; private set; }
        public static Camera MainCamera => Instance.mainCamera;
        public static FlyCamera GuiManager => Instance.guiManager;

        public static TFFrame BaseFrame { get; private set; }

        public static TFFrame DisplaysFrame { get; private set; }

        Camera mainCamera;
        FlyCamera guiManager;

        readonly Dictionary<string, TFFrame> frames = new Dictionary<string, TFFrame>();

        [Serializable]
        public class Configuration : JsonToString
        {
            public Resource.Module module => Resource.Module.TF;
            public string topic = "";
            public bool axisVisible = true;
            public float axisSize = 0.25f;
            public bool axisLabelVisible = false;
            public float axisLabelSize = 0.1f;
            public bool parentConnectorVisible = false;
            public bool showAllFrames = false;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                AxisVisible = value.axisVisible;
                AxisSize = value.axisSize;
                AxisLabelSize = value.axisLabelSize;
                AxisLabelVisible = value.axisLabelVisible;
                ParentConnectorVisible = value.parentConnectorVisible;
                ShowAllFrames = value.showAllFrames;
            }
        }

        public bool AxisVisible
        {
            get => config.axisVisible;
            set
            {
                config.axisVisible = value;
                frames.Values.ForEach(x => x.AxisVisible = value);
            }
        }

        public bool AxisLabelVisible
        {
            get => config.axisLabelVisible;
            set
            {
                config.axisLabelVisible = value;
                frames.Values.ForEach(x => x.LabelVisible = value);
            }
        }

        public float AxisLabelSize
        {
            get => config.axisLabelSize;
            set
            {
                config.axisLabelSize = value;
                frames.Values.ForEach(x =>
                {
                    x.LabelSize = value;
                });
            }
        }

        public float AxisSize
        {
            get => config.axisSize;
            set
            {
                config.axisSize = value;
                frames.Values.ForEach(x => x.AxisLength = value);
            }
        }

        public bool ParentConnectorVisible
        {
            get => config.parentConnectorVisible;
            set
            {
                config.parentConnectorVisible = value;
                frames.Values.ForEach(x => x.ConnectorVisible = value);
            }
        }

        public bool ShowAllFrames
        {
            get => config.showAllFrames;
            set
            {
                config.showAllFrames = value;
                if (value)
                {
                    frames.Values.ForEach(x => x.AddListener(this));
                }
                else
                {
                    // we create a copy because this generally modifies the collection
                    frames.Values.ToArray().ForEach(x => x.RemoveListener(this));
                }
            }
        }

        void Awake()
        {
            Instance = this;

            mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            guiManager = mainCamera.GetComponent<FlyCamera>();

            Resource.Materials.Initialize();
            Resource.DisplaysType.Initialize();

            Config = new Configuration();

            BaseFrame = Add(CreateFrameObject("world", gameObject));
            BaseFrame.AddListener(null);

            DisplaysFrame = Add(CreateFrameObject("_displays_", gameObject));
            DisplaysFrame.AddListener(null);
            DisplaysFrame.ForceInvisible = true;

            TFFrame f;
            f = GetOrCreateFrame("navigation", this);
            f.AddListener(null);
            f.IgnoreUpdates = true;
        }

        public override void StartListening()
        {
            Topic = config.topic;
            GameThread.EverySecond += UpdateStats;
            Listener = new RosListener<tfMessage_v2>(Topic, SubscriptionHandler_v2);
        }

        public override void Unsubscribe()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;
        }

        void TopicsUpdated()
        {
            /*
            if (ConnectionManager.Topics.TryGetValue("/tf", out string messageType))
            {
                if (messageType == tfMessage.MessageType)
                {
                    Listener = new RosListener<tfMessage>(Topic, SubscriptionHandler_v1);
                    ConnectionManager.Instance.TopicsUpdated -= TopicsUpdated;
                }
                else if (messageType == tfMessage_v2.MessageType)
                {
                    Listener = new RosListener<tfMessage_v2>(Topic, SubscriptionHandler_v2);
                    ConnectionManager.Instance.TopicsUpdated -= TopicsUpdated;
                }
            }
            */
        }

        void ProcessMessages(TransformStamped[] transforms)
        {
            foreach (TransformStamped t in transforms)
            {
                string childId = t.child_frame_id;
                if (childId.Length != 0 && childId[0] == '/')
                {
                    childId = childId.Substring(1);
                }
                TFFrame child;
                if (config.showAllFrames)
                {
                    child = GetOrCreateFrame(childId, this); ;
                }
                else if (!TryGetFrameImpl(childId, out child))
                {
                    continue;
                }
                string parentId = t.header.frame_id;
                if (parentId.Length != 0 && parentId[0] == '/')
                {
                    parentId = parentId.Substring(1);
                }
                TFFrame parent = string.IsNullOrEmpty(parentId) ?
                    null :
                    GetOrCreateFrame(parentId, null);
                if (!child.SetParent(parent))
                {
                    continue;
                }
                child.Pose = t.transform.Ros2Unity();
            }
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


        public static TFFrame GetOrCreateFrame(string id, Display listener = null)
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
            if (TryGetFrameImpl(id, out TFFrame t))
            {
                return t;
            }
            return Add(CreateFrameObject(id, BaseFrame.gameObject));
        }

        TFFrame CreateFrameObject(string id, GameObject parent)
        {
            GameObject o = ResourcePool.GetOrCreate(Resource.DisplaysType.TFFrame, parent.transform);
            o.name = id;

            TFFrame frame = o.GetComponent<TFFrame>();
            frame.Id = id;
            frame.IgnoreUpdates = false;
            frame.AxisVisible = config.axisVisible;
            frame.AxisLength = config.axisSize;
            frame.LabelSize = config.axisLabelSize;
            frame.LabelVisible = config.axisLabelVisible;
            frame.ConnectorVisible = config.parentConnectorVisible;
            return frame;
        }

        bool TryGetFrameImpl(string id, out TFFrame t)
        {
            return frames.TryGetValue(id, out t);
        }

        void SubscriptionHandler_v1(tfMessage msg)
        {
            ProcessMessages(msg.transforms);
        }

        void SubscriptionHandler_v2(tfMessage_v2 msg)
        {
            ProcessMessages(msg.transforms);
        }

        public void MarkAsDead(TFFrame frame)
        {
            frames.Remove(frame.Id);
            GuiManager.Unselect(frame);
            frame.Parent = null;
            ResourcePool.Dispose(Resource.DisplaysType.TFFrame, frame.gameObject);
        }

        public override void Recycle()
        {
        }
    }
}
