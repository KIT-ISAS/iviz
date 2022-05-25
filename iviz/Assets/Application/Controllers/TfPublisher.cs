#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.App;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.Controllers.TF
{
    public sealed class TfPublisher
    {
        const int PublishTimeInSec = 5;

        static TfPublisher? instance;

        public static TfPublisher Instance =>
            instance ?? throw new NullReferenceException("TfPublisher has not been set!");

        readonly Dictionary<string, TfPublishedFrame> frames = new();
        
        PublishedFramePanelData? panelData;
        int secondsCounter;

        public int NumPublishedFrames => frames.Count;

        public TfPublisherConfiguration Configuration =>
            new()
            {
                Frames = frames.Values
                    .Where(frame => !frame.isInternal)
                    .Select(frame => frame.Configuration)
                    .ToArray()
            };

        public TfPublisher()
        {
            instance = this;
            GameThread.EverySecond += OnEverySecond;
            RosConnection.ConnectionStateChanged += OnConnectionStateChanged;
        }

        public void UpdateConfiguration(TfPublisherConfiguration config)
        {
            ResetNonInternal();
                
            foreach (var frameConfig in config.Frames)
            {
                var frame = GetOrCreate(frameConfig.Id);
                frame.TfFrame.AttachTo(frameConfig.Parent);
                frame.LocalPose = frameConfig.LocalPose.Ros2Unity();
                frame.Scale = frameConfig.Scale;
                frame.Visible = frameConfig.Visible;
            }
        }

        public void UpdateConfiguration(string configAsJson, string[] fields)
        {
            
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            foreach (var frame in frames.Values)
            {
                frame.UpdateFrame();
            }
        }

        void OnEverySecond()
        {
            if (secondsCounter++ != PublishTimeInSec)
            {
                return;
            }

            secondsCounter = 0;
            PublishFramesKeepAlive();
        }

        // send keep-alive message to keep other nodes from forgetting our frames
        void PublishFramesKeepAlive()
        {
            static bool HasNotBeenUpdatedRecently(TfPublishedFrame frame) =>
                (GameThread.Now - frame.lastUpdate).TotalSeconds >= PublishTimeInSec;

            var framesToPublish = frames.Values.Where(HasNotBeenUpdatedRecently);
            foreach (var frame in framesToPublish)
            {
                TfListener.Publish(frame.tfFrame);
            }
        }

        public IPublishedFrame GetOrCreate(string frameId, bool showPublisherPanel = false, bool isInternal = false)
        {
            if (frames.TryGetValue(frameId, out var oldFrame))
            {
                return oldFrame;
            }

            var newFrame = new TfPublishedFrame(frameId, isInternal);
            frames[frameId] = newFrame;

            if (showPublisherPanel)
            {
                panelData = new PublishedFramePanelData(newFrame.tfFrame);
                panelData.ShowPanel();
            }

            if (isInternal)
            {
                newFrame.AttachToFixed();
            }

            return newFrame;
        }

        public bool Remove(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                return false;
            }

            if (frame.isInternal)
            {
                RosLogger.Info($"{this}: Cannot remove '{frameId}'. Reason: It is being managed by iviz.");
                return false;
            }

            frame.Dispose();
            frames.Remove(frameId);

            if (panelData != null && panelData.Frame.Id == frameId)
            {
                panelData.HidePanel();
                panelData = null;
            }

            return true;
        }

        public void ShowPanelIfPublishing(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                panelData?.HidePanel();
                return;
            }

            if (panelData != null && panelData.Frame.Id == frameId)
            {
                panelData.ToggleShowPanel();
                return;
            }

            panelData = new PublishedFramePanelData(frame.tfFrame);
            panelData.ShowPanel();
        }

        public bool IsPublishing(string frameId) => frames.ContainsKey(frameId);

        public void Dispose()
        {
            GameThread.EverySecond -= OnEverySecond;
            RosConnection.ConnectionStateChanged -= OnConnectionStateChanged;
            panelData = null;
            instance = null;
        }

        public override string ToString()
        {
            return $"[{nameof(TfPublisher)}]";
        }
        
        void ResetNonInternal()
        {
            var toDelete = frames.Values
                .Where(frame => !frame.isInternal)
                .ToList();
                
            foreach (var frame in toDelete)
            {
                frame.Dispose();
                frames.Remove(frame.id);
            }                
        }        

        sealed class TfPublishedFrame : IPublishedFrame
        {
            readonly FrameNode frameNode;
            bool disposed;

            public readonly string id;
            public readonly bool isInternal;
            public DateTime lastUpdate;
            public TfFrame tfFrame;

            public TfFrame TfFrame => tfFrame;

            public Pose LocalPose
            {
                get => tfFrame.Transform.AsLocalPose();
                set
                {
                    AssertIsAlive();
                    if (TfFrame.TrySetLocalPose(value))
                    {
                        TfListener.Publish(TfFrame);
                    }

                    lastUpdate = GameThread.Now;
                }
            }

            public float Scale
            {
                get => tfFrame.Transform.localScale.x;
                set => tfFrame.Transform.localScale = value * Vector3.one;
            }

            public bool Visible
            {
                get => !tfFrame.ForceInvisible;
                set => tfFrame.ForceInvisible = !value;
            }

            public TfPublishedFrameConfiguration Configuration => new()
            {
                Id = id,
                Parent = tfFrame.Parent == TfModule.OriginFrame || tfFrame.Parent == null ? "" : tfFrame.Parent.Id,
                LocalPose = LocalPose.Unity2RosPose(),
                Scale = Scale,
                Visible = Visible
            };

            void AssertIsAlive()
            {
                if (disposed) throw new ObjectDisposedException(id);
            }

            internal TfPublishedFrame(string id, bool isInternal)
            {
                this.id = id;
                this.isInternal = isInternal;
                frameNode = new FrameNode("[TfPublisher Node]");
                frameNode.AttachTo(id);
                tfFrame = frameNode.Parent ??
                          throw new NullReferenceException("Newly created frame has null parent"); // shouldn't happen
                tfFrame.IsPublishedLocally = true;

                if (isInternal)
                {
                    tfFrame.ForceInvisible = true;
                }
            }

            public void Dispose()
            {
                disposed = true;
                tfFrame.IsPublishedLocally = false;
                frameNode.Dispose();
            }

            public void AttachToFixed()
            {
                AssertIsAlive();
                tfFrame.Parent = TfModule.FixedFrame;
            }
            
            public void UpdateFrame()
            {
                AssertIsAlive();
                frameNode.AttachTo(id);
                tfFrame = frameNode.Parent ??
                          throw new NullReferenceException("Failed to set origin parent"); // shouldn't happen
            }
        }
    }

    public interface IPublishedFrame
    {
        public Pose LocalPose { set; }
        public float Scale { set; }
        public bool Visible { set; }
        public TfFrame TfFrame { get; }
    }
}