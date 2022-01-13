#nullable enable

using System;
using System.Collections.Generic;
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
            GameThread.EverySecond += OnEverySecond;
            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
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
            Publish();
        }

        void Publish()
        {
            foreach (var frame in frames.Values)
            {
                TfListener.Publish(frame.TfFrame);
            }
        }

        public TfPublishedFrame GetOrCreate(string frameId, bool showPublisherPanel = false, bool isInternal = false)
        {
            if (frames.TryGetValue(frameId, out var oldFrame))
            {
                return oldFrame;
            }

            var newFrame = new TfPublishedFrame(frameId, isInternal);
            frames[frameId] = newFrame;

            var newTfFrame = newFrame.TfFrame;
            if (showPublisherPanel)
            {
                panelData = new PublishedFramePanelData(newTfFrame);
                panelData.ShowPanel();
            }

            TfListener.Publish(newTfFrame);
            return newFrame;
        }

        public bool Remove(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                return false;
            }

            if (frame.IsInternal)
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
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
            instance = null;
        }

        public override string ToString()
        {
            return "[TfPublisher]";
        }
    }

    public sealed class TfPublishedFrame
    {
        readonly string id;
        readonly FrameNode frameNode;
        public TfFrame TfFrame { get; private set; }
        public bool IsInternal { get; }

        internal TfPublishedFrame(string id, bool isInternal)
        {
            this.id = id;
            frameNode = FrameNode.Instantiate("[Publisher]");
            frameNode.AttachTo(id);
            TfFrame = frameNode.Parent.CheckedNull()
                      ?? throw new NullReferenceException("Failed to set origin parent"); // shouldn't happen
            IsInternal = isInternal;
            
            if (isInternal)
            {
                TfFrame.ForceInvisible = true;
            }
        }

        public void Dispose()
        {
            frameNode.DestroySelf();
        }

        public Pose LocalPose
        {
            set
            {
                TfFrame.SetLocalPose(value);
                TfListener.Publish(TfFrame);
            }
        }

        public void AttachToFixed()
        {
            TfFrame.Parent = TfListener.Instance.FixedFrame;
        }

        internal void UpdateFrame()
        {
            frameNode.AttachTo(id);
            TfFrame = frameNode.Parent.CheckedNull()
                      ?? throw new NullReferenceException("Failed to set origin parent"); // shouldn't happen
        }
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