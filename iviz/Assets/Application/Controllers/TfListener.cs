#nullable enable

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Channels;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Ros;
using Iviz.Roslib;
using UnityEngine;
using Pose = UnityEngine.Pose;

namespace Iviz.Controllers
{
    public sealed class TfListener : Controller, IHasFrame
    {
        const int MaxQueueSize = 10000;

        readonly TfConfiguration config = new();

        readonly ChannelReader<(TransformStamped[] frame, bool isStatic)> incomingReader;
        readonly ChannelWriter<(TransformStamped[] frame, bool isStatic)> incomingWriter;

        readonly ChannelReader<TransformStamped> outgoingReader;
        readonly ChannelWriter<TransformStamped> outgoingWriter;

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

        public override bool Visible
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
            ListenerStatic = new Listener<TFMessage>(TfModule.DefaultTopicStatic, HandleStatic,
                RosSubscriptionProfile.ImportantKeepAll);
            Listener = new Listener<TFMessage>(TfModule.DefaultTopic, HandleNonStatic,
                RosSubscriptionProfile.ImportantKeepAll);

            Config = config ?? new TfConfiguration
            {
                Topic = TfModule.DefaultTopic,
                Id = TfModule.DefaultTopic
            };

            GameThread.LateEveryFrame += LateUpdate;

            var incomingOptions = new BoundedChannelOptions(MaxQueueSize)
                { SingleReader = true, FullMode = BoundedChannelFullMode.DropOldest };

            var incomingMessages = Channel.CreateBounded<(TransformStamped[], bool)>(incomingOptions);
            incomingReader = incomingMessages.Reader;
            incomingWriter = incomingMessages.Writer;

            var outgoingMessages = Channel.CreateBounded<TransformStamped>(incomingOptions);
            outgoingReader = outgoingMessages.Reader;
            outgoingWriter = outgoingMessages.Writer;
        }

        void ProcessMessages()
        {
            var tf = Tf;
            while (incomingReader.TryRead(out var value))
            {
                var (transforms, isStatic) = value;
                foreach (var transform in transforms)
                {
                    tf.Process(transform, isStatic);
                }
            }
        }

        bool HandleNonStatic(TFMessage msg, IRosConnection? _)
        {
            return incomingWriter.TryWrite((msg.Transforms, false));
        }

        bool HandleStatic(TFMessage msg, IRosConnection? _)
        {
            return incomingWriter.TryWrite((msg.Transforms, true));
        }

        public override void ResetController()
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
            var parent = frame.Parent;
            string parentFrameId;
            string childFrameId = frame.Id;
            Pose localPose;

            if (parent == null || parent == TfModule.OriginFrame)
            {
                // Debug.Log("Publishing orphan frame " + frame.Id);
                parentFrameId = "";
                localPose = TfModule.RelativeToFixedFrame(frame.Transform);
            }
            else
            {
                parentFrameId = parent.Id;
                localPose = frame.Transform.AsLocalPose();
            }

            if (localPose.position.IsInvalid() || localPose.rotation.IsInvalid())
            {
                return;
            }

            var t = new TransformStamped();
            t.Header.Seq = tfSeq++;
            t.Header.Stamp = GameThread.TimeNow;
            t.Header.FrameId = parentFrameId;
            t.ChildFrameId = TfModule.ResolveFrameId(childFrameId);
            localPose.Unity2Ros(out t.Transform);
            outgoingWriter.TryWrite(t);
        }

        void DoPublish()
        {
            int count = outgoingReader.Count;
            if (count == 0)
            {
                return;
            }

            var transforms = new TransformStamped[count];
            for (int i = 0; i < count; i++)
            {
                transforms[i] = outgoingReader.TryRead(out var transform)
                    ? transform
                    : new TransformStamped(); // shouldn't happen, single reader!
            }

            if (RosManager.IsConnected)
            {
                Publisher.Publish(new TFMessage(transforms));
            }
            else
            {
                incomingWriter.TryWrite((transforms, false));
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