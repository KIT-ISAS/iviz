#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Ros;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers.TF
{
    public sealed class TfModule
    {
        public const string OriginFrameId = "[origin]";
        public const string DefaultTopic = "/tf";
        public const string DefaultTopicStatic = "/tf_static";
        const string MapFrameId = "map";
        const int WarningBlacklistTimeInSec = 5;

        static TfModule? instance;

        readonly Func<string, TfFrame> frameFactory;
        readonly Dictionary<string, TfFrame> frames = new();
        readonly FrameNode keepAllListenerNode;
        readonly FrameNode staticListenerNode;
        readonly FrameNode fixedFrameListenerNode;

        readonly TfFrame mapFrame;
        readonly TfFrame rootFrame;
        readonly TfFrame originFrame;
        readonly GameObject unityFrame;

        readonly Dictionary<string, float> warningTimestamps = new();

        TfFrame fixedFrame;
        Pose cachedFixedPose = Pose.identity;

        bool keepAllFrames;
        bool visible;
        float frameSize;
        bool labelsVisible;
        bool parentConnectorVisible;

        TfFrame? lastChild;

        public static bool HasInstance => instance != null;

        public static TfModule Instance =>
            instance ?? throw new NullReferenceException("No TFListener has been set!");

        public static TfFrame RootFrame => Instance.rootFrame;
        public static TfFrame OriginFrame => Instance.originFrame;

        public static Transform UnityFrameTransform =>
            instance != null
                ? instance.unityFrame.transform
                : GameObject.Find("TF").transform;

        public static TfFrame ListenersFrame => OriginFrame;
        public static TfFrame DefaultFrame => OriginFrame;
        public static TfFrame FixedFrame => Instance.fixedFrame;
        public static IEnumerable<string> FrameNames => Instance.frames.Keys;

        public event Action? ResetFrames;

        public static float RootScale
        {
            set => RootFrame.Transform.localScale = value * Vector3.one;
        }

        public bool FlipZ { get; set; }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                foreach (var frame in frames.Values)
                {
                    frame.Visible = value;
                }
            }
        }

        public bool LabelsVisible
        {
            set
            {
                foreach (var frame in frames.Values)
                {
                    frame.LabelVisible = value;
                }
            }
        }

        public float FrameSize
        {
            get => frameSize;
            set
            {
                frameSize = value;
                foreach (var frame in frames.Values)
                {
                    frame.FrameSize = value;
                }
            }
        }

        public bool ParentConnectorVisible
        {
            set
            {
                parentConnectorVisible = value;
                foreach (var frame in frames.Values)
                {
                    frame.ConnectorVisible = value;
                }
            }
        }

        public bool KeepAllFrames
        {
            get => keepAllFrames;
            set
            {
                keepAllFrames = value;
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

        public bool Interactable
        {
            set
            {
                foreach (var frame in frames.Values)
                {
                    frame.EnableCollider = value;
                }
            }
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
                if (FixedFrameId == value)
                {
                    return;
                }

                FixedFrame.RemoveListener(fixedFrameListenerNode);

                if (string.IsNullOrEmpty(value))
                {
                    fixedFrame = mapFrame;
                    originFrame.Transform.SetLocalPose(Pose.identity);
                    return;
                }

                var frame = GetOrCreateFrame(value, fixedFrameListenerNode);
                fixedFrame = frame;
                originFrame.Transform.SetLocalPose(frame.OriginWorldPose.Inverse());
            }
        }

        public int NumFrames => frames.Count;

        public TfModule(Func<string, TfFrame> frameFactory)
        {
            instance = this;
            this.frameFactory = frameFactory ?? throw new ArgumentNullException(nameof(frameFactory));

            try
            {
                // hierarchy:  unityFrame ("TF") -> rootFrame -> originFrame -> fixedFrame
                // unityFrame: container for all TF stuff. has no parents. 
                // rootFrame:  managed by the AR system. any movements in the AR origin are instead reversed and applied to rootFrame.
                //             the root frame may have a uniform scale that is not 1. 
                // originFrame: managed by the fixed frame. originFrame is set to the inverse of the fixedFrame pose,
                //              to ensure that fixedFrame is always on the unity origin (excluding AR changes in root).
                //              the transformation also applies FlipZ if active.
                // fixedFrame: whatever TF frame the user has selected as fixed.
                // mapFrame:   the default fixed frame. cannot be deleted, to ensure that there is always at least
                //             one visible frame.

                unityFrame = GameObject.Find("TF").CheckedNull() ?? new GameObject("TF");

                rootFrame = CreateFrameObject("/", null);
                rootFrame.Transform.parent = unityFrame.transform;
                rootFrame.ForceInvisible();

                originFrame = CreateFrameObject(OriginFrameId, rootFrame);
                originFrame.Parent = rootFrame;
                originFrame.ForceInvisible();

                keepAllListenerNode = new FrameNode("TFNode") { Visible = false };
                staticListenerNode = new FrameNode("TFStatic") { Visible = false };
                fixedFrameListenerNode = new FrameNode("TFFixedFrame") { Visible = false };
                var defaultListener = new FrameNode(".")
                {
                    Visible = false,
                    Transform = { parent = unityFrame.transform }
                };

                rootFrame.AddListener(defaultListener);
                originFrame.AddListener(defaultListener);

                mapFrame = Add(CreateFrameObject(MapFrameId, originFrame));
                mapFrame.Parent = originFrame;
                mapFrame.AddListener(defaultListener);
                fixedFrame = mapFrame;

                KeepAllFrames = true;

                GameThread.LateEveryFrame += LateUpdate;
            }
            catch (Exception)
            {
                instance = null;
                throw;
            }
        }

        public void Process(in TransformStamped transform, bool isStatic)
        {
            ref readonly var rosTransform = ref transform.Transform;

            if (!CheckIfWithinThreshold(rosTransform.Translation))
            {
                SetFailedThreshold(transform.ChildFrameId);
                return;
            }

            if (rosTransform.IsInvalid())
            {
                SetFailedValid(transform.ChildFrameId);
                return;
            }

            var unityPose = rosTransform.Ros2Unity();

            string childIdUnchecked = transform.ChildFrameId;
            if (childIdUnchecked.Length == 0)
            {
                return;
            }

            // remove starting '/' from tf v1
            string childId = childIdUnchecked[0] != '/'
                ? childIdUnchecked
                : childIdUnchecked[1..];

            TfFrame? child;
            if (lastChild != null && lastChild.Id == childId)
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
                return;
            }

            lastChild = child;

            string parentIdUnchecked = transform.Header.FrameId;
            if (parentIdUnchecked.Length == 0)
            {
                child.TrySetParent(DefaultFrame);
                child.SetLocalPose(unityPose);
                return;
            }

            string parentId = parentIdUnchecked[0] != '/'
                ? parentIdUnchecked
                : parentIdUnchecked[1..];

            if (child.Parent != null && parentId == child.Parent.Id)
            {
                child.SetLocalPose(unityPose);
            }
            else
            {
                var parent = GetOrCreateFrame(parentId);
                if (child.TrySetParent(parent))
                {
                    child.SetLocalPose(unityPose);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool CheckIfWithinThreshold(in Msgs.GeometryMsgs.Vector3 t)
        {
            // unity cannot deal with very large floats, so we have to limit translation sizes
            const int maxPoseMagnitude = 10000;
            return Math.Abs(t.X) < maxPoseMagnitude
                   && Math.Abs(t.Y) < maxPoseMagnitude
                   && Math.Abs(t.Z) < maxPoseMagnitude;
        }

        void SetFailedThreshold(string childId)
        {
            if (warningTimestamps.TryGetValue(childId, out float expirationTimestamp)
                && expirationTimestamp >= GameThread.GameTime)
            {
                return;
            }

            RosLogger.Info($"{nameof(TfModule)}: Ignoring transform '{childId}' with too large values.");
            warningTimestamps[childId] = GameThread.GameTime + WarningBlacklistTimeInSec;
        }

        void SetFailedValid(string childId)
        {
            if (warningTimestamps.TryGetValue(childId, out float timestamp)
                && timestamp >= GameThread.GameTime)
            {
                return;
            }

            RosLogger.Info($"{nameof(TfModule)}: Ignoring transform '{childId}' with invalid values.");
            warningTimestamps[childId] = GameThread.GameTime + WarningBlacklistTimeInSec;
        }

        public void Reset()
        {
            ResetFrames?.Invoke();

            bool prevKeepAllFrames = keepAllFrames;

            var framesCopy = frames.Values.ToList();
            foreach (var frame in framesCopy)
            {
                if (frame.TrySetParent(OriginFrame))
                {
                    frame.SetLocalPose(Pose.identity);
                }
            }

            foreach (var frame in framesCopy)
            {
                frame.RemoveListener(staticListenerNode);
            }

            KeepAllFrames = false;
            KeepAllFrames = prevKeepAllFrames;

            warningTimestamps.Clear();
        }

        TfFrame Add(TfFrame t)
        {
            frames.Add(t.Id, t);
            return t;
        }

        public static bool TryGetFrame(string id, [NotNullWhen(true)] out TfFrame? frame)
        {
            return Instance.TryGetFrameImpl(id ?? throw new ArgumentNullException(nameof(id)), out frame);
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

            string? myId = RosManager.MyId;
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
            ThrowHelper.ThrowIfNull(frameId, nameof(frameId));

            if (frameId.Length == 0)
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
                : Add(CreateFrameObject(id, DefaultFrame));
        }

        TfFrame CreateFrameObject(string id, TfFrame? parentFrame)
        {
            var frame = frameFactory(id);
            frame.Visible = visible;
            frame.FrameSize = frameSize;
            frame.LabelVisible = labelsVisible;
            frame.ConnectorVisible = parentConnectorVisible;
            frame.Parent = parentFrame;

            return frame;
        }

        bool TryGetFrameImpl(string id, [NotNullWhen(true)] out TfFrame? t)
        {
            return frames.TryGetValue(id, out t);
        }


        public void MarkAsDead(TfFrame frame)
        {
            ThrowHelper.ThrowIfNull(frame, nameof(frame));
            frames.Remove(frame.Id);
            frame.Dispose();
        }

        void ProcessWorldOffset()
        {
            // task: move fixed frame so that it has identity transform to root frame
            // hierarchy: root -> origin -> fixed

            Pose fixedFramePose; // fixed relative to origin
            if (FlipZ)
            {
                var (position, rotation) = FixedFrame.OriginWorldPose;
                var rotateAroundForward = Quaternions.Rotate180AroundZ;
                fixedFramePose.rotation = rotateAroundForward * rotation;
                fixedFramePose.position = rotateAroundForward * position;
            }
            else
            {
                fixedFramePose = FixedFrame.OriginWorldPose;
            }

            if (fixedFramePose.EqualsApprox(cachedFixedPose))
            {
                return;
            }

            cachedFixedPose = fixedFramePose;
            OriginFrame.Transform.SetLocalPose(fixedFramePose.Inverse());
        }

        void LateUpdate()
        {
            ProcessWorldOffset();
        }

        public void Dispose()
        {
            OriginFrame.Dispose(); // get rid of children so they won't get deleted when it does

            GameThread.LateEveryFrame -= LateUpdate;
            staticListenerNode.Dispose();
            ResetFrames = null;
            instance = null;
        }

        public static Vector3 RelativeToOrigin(in Vector3 absoluteUnityPosition) =>
            OriginFrame.Transform.InverseTransformPoint(absoluteUnityPosition);

        public static Pose RelativeToOrigin(in Pose absoluteUnityPose)
        {
            var originFrame = OriginFrame.Transform;
            var (position, rotation) = absoluteUnityPose;

            Pose p;
            p.position = originFrame.InverseTransformPoint(position); // may contain scaling
            p.rotation = originFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose RelativeToFixedFrame(in Pose absoluteUnityPose)
        {
            // equals unityPose unless in AR mode or flipped z 
            var fixedFrame = FixedFrame.Transform;
            var (position, rotation) = absoluteUnityPose;

            Pose p;
            p.position = fixedFrame.InverseTransformPoint(position); // may contain scaling
            p.rotation = fixedFrame.rotation.Inverse() * rotation;
            return p;
        }

        public static Pose FixedFrameToAbsolute(in Pose relativePose)
        {
            // equals unityPose unless in AR mode or flipped z 
            var fixedFrame = FixedFrame.Transform;
            var (position, rotation) = relativePose;

            Pose p;
            p.position = fixedFrame.TransformPoint(position); // may contain scaling
            p.rotation = fixedFrame.rotation * rotation;
            return p;
        }

        public override string ToString() => $"[{nameof(TfModule)} '{DefaultTopic}']";
    }
}