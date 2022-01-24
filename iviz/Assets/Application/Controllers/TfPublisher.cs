#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class TfPublisher
    {
        const int PublishTimeInSec = 5;

        static TfPublisher? instance;

        public static TfPublisher Instance =>
            instance ?? throw new NullReferenceException("TfPublisher has not been set!");

        readonly Dictionary<string, TfPublishedFrame> frames = new();
        PublishedFramePanelData? panelData;
        int secondCounter;

        public int NumPublishedFrames => frames.Count;

        public TfPublisher()
        {
            instance = this;
            GameThread.EverySecond += OnEverySecond;
            RosConnection.ConnectionStateChanged += OnConnectionStateChanged;
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
            if (secondCounter++ != PublishTimeInSec)
            {
                return;
            }

            secondCounter = 0;
            PublishFramesWithoutUpdate();
        }

        void PublishFramesWithoutUpdate()
        {
            var framesToPublish = frames.Values
                .Where(frame => (GameThread.Now - frame.lastUpdate).TotalSeconds >= PublishTimeInSec);
            foreach (var frame in framesToPublish)
            {
                if (!frame.isInternal)
                {
                    TfListener.Publish(frame.TfFrame);
                }
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
                panelData = new PublishedFramePanelData(newFrame.TfFrame);
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

            panelData = new PublishedFramePanelData(frame.TfFrame);
            panelData.ShowPanel();
        }

        public bool IsPublishing(string frameId) => frames.ContainsKey(frameId);

        public void Dispose()
        {
            GameThread.EverySecond -= OnEverySecond;
            RosConnection.ConnectionStateChanged -= OnConnectionStateChanged;
            instance = null;
        }

        public override string ToString()
        {
            return $"[{nameof(TfPublisher)}]";
        }

        sealed class TfPublishedFrame : IPublishedFrame
        {
            readonly string id;
            readonly FrameNode frameNode;
            public DateTime lastUpdate;
            public readonly bool isInternal;
            public TfFrame TfFrame { get; private set; }

            internal TfPublishedFrame(string id, bool isInternal)
            {
                this.id = id;
                this.isInternal = isInternal;
                frameNode = new FrameNode("TfPublisher");
                frameNode.AttachTo(id);
                TfFrame = frameNode.Parent ??
                          throw new NullReferenceException("Newly created frame has null parent"); // shouldn't happen

                if (isInternal)
                {
                    TfFrame.ForceInvisible();
                }
            }

            public void Dispose()
            {
                frameNode.Dispose();
            }

            public Pose LocalPose
            {
                set
                {
                    if (TfFrame.SetLocalPose(value))
                    {
                        TfListener.Publish(TfFrame);
                    }

                    lastUpdate = GameThread.Now;
                }
            }

            public void AttachToFixed()
            {
                TfFrame.Parent = TfListener.FixedFrame;
            }

            public void UpdateFrame()
            {
                frameNode.AttachTo(id);
                TfFrame = frameNode.Parent ??
                          throw new NullReferenceException("Failed to set origin parent"); // shouldn't happen
            }
        }
    }

    public interface IPublishedFrame
    {
        public Pose LocalPose { set; }
        public TfFrame TfFrame { get; }
        public void AttachToFixed();
    }


    /*
    public class TfPublisherConfiguration : IConfiguration
    {
        public string Id { get; set; } = "";
        public ModuleType ModuleType => ModuleType.TFPublisher;
        public bool Visible => true;
    }
    */
}