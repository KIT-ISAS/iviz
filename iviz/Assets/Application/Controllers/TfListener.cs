#nullable enable

using System;
using System.Collections.Concurrent;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;
using Pose = UnityEngine.Pose;

namespace Iviz.Controllers
{
    public sealed class TfListener : IController, IHasFrame
    {
        const int MaxQueueSize = 10000;

        readonly TfConfiguration config = new();
        readonly ConcurrentQueue<(TransformStamped[] frame, bool isStatic)> incomingMessages = new();
        readonly ConcurrentBag<TransformStamped> outgoingMessages = new();
        uint tfSeq;

        static TfListener? instance;

        static TfModule Tf => TfModule.Instance;

        public Listener<TFMessage> Listener { get; }
        public Listener<TFMessage> ListenerStatic { get; }
        public Sender<TFMessage> Publisher { get; }
        public TfFrame Frame => TfModule.FixedFrame;

        public bool FlipZ
        {
            get => config.FlipZ;
            set
            {
                config.FlipZ = value;
                Tf.FlipZ = value;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                Tf.Visible = value;
            }
        }

        public bool LabelsVisible
        {
            get => config.FrameLabelsVisible;
            set
            {
                config.FrameLabelsVisible = value;
                Tf.LabelsVisible = value;
            }
        }

        public float FrameSize
        {
            get => config.FrameSize;
            set
            {
                config.FrameSize = value;
                Tf.FrameSize = value;
            }
        }

        public bool PreferUdp
        {
            get => config.PreferUdp;
            set
            {
                config.PreferUdp = value;
                Listener.TransportHint = value ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
            }
        }

        public bool ParentConnectorVisible
        {
            get => config.ParentConnectorVisible;
            set
            {
                config.ParentConnectorVisible = value;
                Tf.ParentConnectorVisible = value;
            }
        }

        public bool KeepAllFrames
        {
            get => config.KeepAllFrames;
            set
            {
                config.KeepAllFrames = value;
                Tf.KeepAllFrames = value;
            }
        }

        public bool Interactable
        {
            get => config.Interactable;
            set
            {
                config.Interactable = value;
                Tf.Interactable = value;
            }
        }

        string FixedFrameId
        {
            get => TfModule.FixedFrameId;
            set
            {
                config.FixedFrameId = value;
                TfModule.FixedFrameId = value;
            }
        }

        public TfConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Id = value.Id;
                Visible = value.Visible;
                FrameSize = value.FrameSize;
                LabelsVisible = value.FrameLabelsVisible;
                ParentConnectorVisible = value.ParentConnectorVisible;
                KeepAllFrames = value.KeepAllFrames;
                FixedFrameId = value.FixedFrameId;
                Interactable = value.Interactable;
                FlipZ = value.FlipZ;
                // todo: blacklisted frames
            }
        }

        public TfListener(TfConfiguration? config)
        {
            instance = this;

            Publisher = new Sender<TFMessage>(TfModule.DefaultTopic);
            ListenerStatic = new Listener<TFMessage>(TfModule.DefaultTopicStatic, HandleStatic);
            Listener = new Listener<TFMessage>(TfModule.DefaultTopic, HandleNonStatic);

            Config = config ?? new TfConfiguration
            {
                Topic = TfModule.DefaultTopic,
                Id = TfModule.DefaultTopic
            };

            GameThread.LateEveryFrame += LateUpdate;
        }

        void ProcessMessages()
        {
            while (incomingMessages.TryDequeue(out var value))
            {
                var (transforms, isStatic) = value;
                foreach (ref readonly var transform in transforms.AsSpan())
                {
                    Tf.Process(in transform, isStatic);
                }
            }
        }

        bool HandleNonStatic(TFMessage msg, IRosReceiver? _)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, false));
            return true;
        }

        bool HandleStatic(TFMessage msg, IRosReceiver? _)
        {
            if (incomingMessages.Count > MaxQueueSize)
            {
                return false;
            }

            incomingMessages.Enqueue((msg.Transforms, true));
            return true;
        }


        public void ResetController()
        {
            Tf.Reset();
            ListenerStatic.Reset();
        }

        void LateUpdate()
        {
            ProcessMessages();
            Tf.ProcessWorldOffset();
            DoPublish();
        }

        public static void Publish(TfFrame frame)
        {
            instance?.PublishFrame(frame);
        }

        void PublishFrame(TfFrame frame)
        {
            string childFrameId = frame.Id;
            string parentFrameId;
            Pose localPose;

            if (frame.Parent == null || frame.Parent == TfModule.OriginFrame)
            {
                parentFrameId = "";
                localPose = TfModule.RelativeToFixedFrame(frame.Transform);
            }
            else
            {
                parentFrameId = frame.Parent.Id;
                localPose = frame.Transform.AsLocalPose();
            }

            if (localPose.position.IsInvalid() || localPose.rotation.IsInvalid())
            {
                return;
            }

            TransformStamped t;
            t.Header.Seq = tfSeq++;
            t.Header.Stamp = GameThread.TimeNow;
            t.Header.FrameId = parentFrameId;
            t.ChildFrameId = TfModule.ResolveFrameId(childFrameId);
            localPose.Unity2Ros(out t.Transform);
            outgoingMessages.Add(t);
        }

        void DoPublish()
        {
            int count = outgoingMessages.Count;
            if (count == 0)
            {
                return;
            }

            var transforms = new TransformStamped[count];
            foreach (ref var transform in transforms.AsSpan())
            {
                outgoingMessages.TryTake(out transform);
            }

            if (RosManager.IsConnected)
            {
                Publisher.Publish(new TFMessage(transforms));
            }
            else
            {
                incomingMessages.Enqueue((transforms, false));
            }
        }

        public void Dispose()
        {
            Listener.Dispose();
            ListenerStatic.Dispose();
            Publisher.Dispose();
            GameThread.LateEveryFrame -= LateUpdate;
            instance = null;
        }

        /// <summary>
        /// Creates a header using the fixed frame as the frame id and the frame start as the timestamp.
        /// </summary>
        public static Header CreateHeader(uint seqId) => CreateHeader(seqId, TfModule.FixedFrameId);

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